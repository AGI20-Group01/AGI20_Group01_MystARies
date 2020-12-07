// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'

Shader "Custom/Old/StarSkyShader"
{
    Properties
    {
        _MainTex ("Texture1", 2D) = "white" {}
        _NoiseTex ("Noice Texture", 2D) = "white" {}
        _BumpMap("Normal Map", 2D) = "bump" {}
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
                float4 screenPosition : TEXCOORD4;
            };

            float4 _Ambient;

            v2f vert (appdata_base v, float4 tangent : TANGENT)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.screenPosition = ComputeScreenPos(o.vertex);
                o.worldPos = mul(unity_ObjectToWorld, v.vertex);
                o.uv = v.texcoord;
                o.normal = UnityObjectToWorldNormal(v.normal); 

                TRANSFER_SHADOW(o)
                return o;
            }

            uniform float _Season1_t;
            uniform float _Season2_t;
            
            sampler2D _MainTex;
            sampler2D _NoiseTex;

            float _RotationSpeed;

            float4 _SpecularCol;
            float _P;
            int _cells;

            fixed4 frag (v2f i) : SV_Target
            {


                //return distance(unity_LightPosition[0].xyz, i.vertex.xyz);
                _RotationSpeed = 2;
                _SpecularCol = float4(0,0,0,0);
                _P = 100;
                _cells = 3;

                float4 objectCenter = mul(unity_ObjectToWorld, float4(0.0,0.0,0.0,1.0));
                float dictToCamera = distance(_WorldSpaceCameraPos, objectCenter);

                float2 textureCoordinate = (i.screenPosition.xy / i.screenPosition.w);

                textureCoordinate -= ComputeScreenPos(objectCenter);
                textureCoordinate *= dictToCamera / 1.2;
                

                float tx = (1 + sin(_Time[0]/3)) / 2;
                float ty = (1 + cos(_Time[0]/3)) / 2;
                fixed4 offsetX = tex2D(_NoiseTex, textureCoordinate + float2(tx, ty));
                fixed4 offsetY = tex2D(_NoiseTex, textureCoordinate + float2(ty, tx));
                // texturs
                textureCoordinate += float2(offsetX.x, offsetY.x) / 25;

                textureCoordinate += ComputeScreenPos(objectCenter);

                float sinX = sin ( _RotationSpeed * _Time );
                float cosX = cos ( _RotationSpeed * _Time );
                float sinY = sin ( _RotationSpeed * _Time );
                float2x2 rotationMatrix = float2x2( cosX, -sinX, sinY, cosX);

                textureCoordinate = mul ( textureCoordinate, rotationMatrix );

                fixed4 col = tex2D(_MainTex, textureCoordinate);
                col = pow(col * 4, 2);




                // lighting
                float3 normal = i.normal;


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
                float3 lightTot = (diff * 2 + speculer) * shadow + ShadeSH9(float4(i.normal,1));
                float lightTotSum = (lightTot.r + lightTot.g + lightTot.b) / 3;
                lightTot = normalize(lightTot) * round(lightTotSum * _cells) / _cells + ShadeSH9(float4(i.normal,1));
                col.rgb *= lightTot;   
                return col;
            }

            ENDCG
        } 
        
        // shadow casting support
        UsePass "Legacy Shaders/VertexLit/SHADOWCASTER"
        
    }
}
