Shader "Custom/Old/GrundeCubeShader"
{
    Properties
    {
        _Main1Tex ("Texture1", 2D) = "white" {}
        _Main2Tex ("Texture2", 2D) = "white" {}
        _Main3Tex ("Texture3", 2D) = "white" {}
        _Main4Tex ("Texture4", 2D) = "white" {}
        _NoiseTex ("NoiseTexture", 2D) = "white" {}
        _BumpMap("Normal Map", 2D) = "bump" {}
        _cells ("no. cells", Range(1,100)) = 5
        _Tint ("Tint", Color) = (1,1,1,1)
        _SpecularCol ("Specular Color", Color) = (1,1,1,1)
        _P ("Specular p", float) = 1
    }
    SubShader
    {
        Pass
        {
            // indicate that our pass is the "base" pass in forward
            // rendering pipeline. It gets ambient and main directional
            // light data set up; light direction in _WorldSpaceLightPos0
            // and color in _LightColor0
            Tags {"LightMode"="ForwardBase"}
        
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "Lighting.cginc"

            #include "UnityCG.cginc" // for UnityObjectToWorldNormal
            #include "UnityLightingCommon.cginc" // for _LightColor0

            #pragma multi_compile_fwdbase nolightmap nodirlightmap nodynlightmap novertexlight
            #include "AutoLight.cginc"


            struct v2f
            {
                float2 uv : TEXCOORD0;
                SHADOW_COORDS(1)
                float4 vertex : SV_POSITION;
                float3 normal: TEXCOORD2;
                float3 worldPos: TEXCOORD3;
                half3 tspace0 : TEXCOORD4;
                half3 tspace1 : TEXCOORD5;
                half3 tspace2 : TEXCOORD6;
                
            };

            float4 _Ambient;

            v2f vert (appdata_base v, float4 tangent : TANGENT)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.worldPos = mul(unity_ObjectToWorld, v.vertex);
                o.uv = v.texcoord;
                o.normal = UnityObjectToWorldNormal(v.normal); 
                half3 wNormal = UnityObjectToWorldNormal(v.normal);
                half3 wTangent = UnityObjectToWorldDir(tangent.xyz);
                half tangentSign = tangent.w * unity_WorldTransformParams.w;
                half3 wBitangent = cross(wNormal, wTangent) * tangentSign;
                o.tspace0 = half3(wTangent.x, wBitangent.x, wNormal.x);
                o.tspace1 = half3(wTangent.y, wBitangent.y, wNormal.y);
                o.tspace2 = half3(wTangent.z, wBitangent.z, wNormal.z);

                TRANSFER_SHADOW(o)
                return o;
            }

            uniform float _Season1_t;
            uniform float _Season2_t;

            
            sampler2D _Main1Tex;
            sampler2D _Main2Tex;
            sampler2D _Main3Tex;
            sampler2D _Main4Tex;
            sampler2D _BumpMap;

            sampler2D _NoiseTex;
            float4 _Tint;
            float4 _SpecularCol;
            float _P;
            int _cells;

            float invLerp(float from, float to, float value) {
                return (value - from) / (to - from);
            }

            float noiseTransition(fixed4 noise, float3 worldPos, float t) {
                t *= 3;
                float dist = length(worldPos.xy) / 3;
                t = clamp(t-noise-dist, 0, 1);
                t = clamp(invLerp(0.45, 0.55, t), 0, 1);
                return t;
            }


            fixed4 frag (v2f i) : SV_Target
            {
                // texturs
                fixed4 col1 = tex2D(_Main1Tex, i.uv);
                fixed4 col2 = tex2D(_Main2Tex, i.uv);
                fixed4 col3 = tex2D(_Main3Tex, i.uv);
                fixed4 col4 = tex2D(_Main4Tex, i.uv);

                half3 tnormal = UnpackNormal(tex2D(_BumpMap, i.uv));

                fixed4 noise = tex2D(_NoiseTex,  i.worldPos.xy/5) * tex2D(_NoiseTex,  i.worldPos.xy);
                noise += tex2D(_NoiseTex,  i.worldPos.xz/5) * tex2D(_NoiseTex,  i.worldPos.xz);
                noise += tex2D(_NoiseTex,  i.worldPos.yz/5) * tex2D(_NoiseTex,  i.worldPos.yz);
                noise /= 3;
                
                float tx = noiseTransition(noise, i.worldPos.xyz, _Season1_t);
                float ty = noiseTransition(noise, i.worldPos.xyz, _Season2_t);
                float4 colx1 = lerp(col1, col2, tx);
                float4 colx2 = lerp(col4, col3, tx);
                float4 col = lerp(colx1, colx2, ty);

                col = col * _Tint;

                // lighting
                float3 normal;
                normal.x = dot(i.tspace0, tnormal);
                normal.y = dot(i.tspace1, tnormal);
                normal.z = dot(i.tspace2, tnormal);
                normal = normalize(normal);

                //normal = dot(normal, tnormal);
                float3 viewDer = normalize(_WorldSpaceCameraPos - i.worldPos);  //WorldSpaceViewDir(float4(i.worldPos, 0));
                float3 lightDir = _WorldSpaceLightPos0.xyz;
                float ndotl = dot(normal, lightDir);

                // speculer
                float3 r = reflect(-lightDir, normal);
                float4 rdotv = pow(max(0, dot(r, viewDer)), _P);
                //rdotv = round(rdotv * 3) / 3;
                fixed4 speculer = _SpecularCol * _LightColor0 * rdotv;

                // diff
                half nl = max(0, ndotl);
                //nl = round(nl * 3) / 3;

                //Shado
                float shadow = SHADOW_ATTENUATION(i);
                //shadow = round(shadow * 5) / 5;


                float4 diff = nl * (_LightColor0);
                float3 lightTot = (diff + speculer) * shadow + ShadeSH9(float4(i.normal,1));
                float lightTotSum = (lightTot.r + lightTot.g + lightTot.b) / 3;
                lightTot = normalize(lightTot) * round(lightTotSum * _cells) / _cells  + ShadeSH9(float4(i.normal,1));
                col.rgb *= lightTot;   
                return col;
            }

            ENDCG
        } 
        
        // shadow casting support
        UsePass "Legacy Shaders/VertexLit/SHADOWCASTER"
        
    }
}