// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Custom/WaterShader"
{
    Properties {
      _MainTex ("Texture", 2D) = "white" {}
      _NoiseTex ("NoiseTexture", 2D) = "white" {}
      _BumpMap ("Bumpmap", 2D) = "bump" {}

      _Color ("Color", Color) = (1,1,1,1)
      _RimColor ("Rim Color", Color) = (0.26,0.19,0.16,0.0)
      _RimPower ("Rim Power", Range(0.5,8.0)) = 3.0
    }
    SubShader {
        Tags { "Queue"="Transparent" "RenderType"="Transparent" }
        LOD 200
        //Cull Off // this is optional, I think it looks good with ice
        //Blend SrcAlpha OneMinusSrcAlpha
        GrabPass { "_MyGrabTexture" }


    CGPROGRAM
    #pragma surface surf Lambert alpha fullforwardshadows vertex:vert
    
    struct Input {
        float2 uv_MainTex;
        float3 worldPos;
        float4 grabUV;
        float3 normal;
        float3 viewDir;
    };


    uniform float _lightIntensity;
    uniform float _Season1_t;
    uniform float3 _worldCenter;
    uniform float3 _worldRot;

    sampler2D _MainTex;
    sampler2D _MyGrabTexture;
    sampler2D _BumpMap;
    sampler2D _NoiseTex;

    

    float4 _Color;

    float4 _RimColor;
    float _RimPower;

    UNITY_INSTANCING_BUFFER_START(Props)
        // put more per-instance properties here
    UNITY_INSTANCING_BUFFER_END(Props)


    void vert (inout appdata_full v, out Input o) {
        float4 hpos = UnityObjectToClipPos (v.vertex);
        o.grabUV = ComputeGrabScreenPos(hpos);
        o.worldPos = mul(unity_ObjectToWorld, v.vertex);
        o.uv_MainTex = v.texcoord;
        o.normal = UnityObjectToWorldNormal(v.normal);
        o.viewDir = normalize(_WorldSpaceCameraPos - mul(unity_ObjectToWorld, v.vertex)); 
    }


    float invLerp(float from, float to, float value) {
            return (value - from) / (to - from);
    }

    float noiseTransition(fixed4 noise, float3 worldPos, float t) {
        t *= 3;
        float dist = length(worldPos.xy) / 10;
        t = clamp(t-noise-dist, 0, 1);
        t = clamp(invLerp(0.45, 0.55, t), 0, 1);
        return t;
    }

    void surf (Input IN, inout SurfaceOutput o) {
        

        // world and noise setup
        float zSin = sin(_worldRot * 0.0174532925);
        float zCos = cos(_worldRot * 0.0174532925);
        float3x3 rotMatrix = float3x3(zCos, -zSin, 0, zSin, zCos, 0, 0, 0, 1); 

        IN.worldPos =  IN.worldPos - _worldCenter;
        IN.worldPos = mul(IN.worldPos, rotMatrix);

        fixed4 noise = tex2D(_NoiseTex,  IN.worldPos.xy/5) * tex2D(_NoiseTex,  IN.worldPos.xy);
        noise += tex2D(_NoiseTex,  IN.worldPos.xz/5) * tex2D(_NoiseTex,  IN.worldPos.xz);
        noise += tex2D(_NoiseTex,  IN.worldPos.yz/5) * tex2D(_NoiseTex,  IN.worldPos.yz);
        noise /= 3;

        float t = noiseTransition(noise, IN.worldPos.xyz, _Season1_t);
        float offset = (sin(_Time * 10)) * 0.2 * (1 - t); 
        
        //Normal

        float3 blend = float3(1,1,1);

        float2 uvX = IN.worldPos.zy + float2(offset,0);
        float2 uvY = IN.worldPos.xz + float2(offset,0); 
        float2 uvZ = IN.worldPos.xy + float2(offset,0); 

        half3 tnormalX = UnpackNormal(tex2D(_BumpMap, uvX));
        half3 tnormalY = UnpackNormal(tex2D(_BumpMap, uvY));
        half3 tnormalZ = UnpackNormal(tex2D(_BumpMap, uvZ));

        tnormalX = half3(tnormalX.xy + IN.normal.zy, IN.normal.x);
        tnormalY = half3(tnormalY.xy + IN.normal.xz, IN.normal.y);
        tnormalZ = half3(tnormalZ.xy + IN.normal.xy, IN.normal.z);

        half3 normal = normalize(
            tnormalX.zyx * blend.x +
            tnormalY.xzy * blend.y +
            tnormalZ.xyz * blend.z
        );


        //distortion
        o.Albedo = tex2Dproj( _MyGrabTexture, UNITY_PROJ_COORD(IN.grabUV) + float4(normal, 0) / 15 );
        //o.Albedo.g = tex2Dproj( _MyGrabTexture, UNITY_PROJ_COORD(IN.grabUV) + float4(-0.1, 0, 0, 0) + float4(o.Normal, 0) / 20).g;
        //o.Albedo.b = tex2Dproj( _MyGrabTexture, UNITY_PROJ_COORD(IN.grabUV) + float4(0.1, 0, 0, 0) + float4(o.Normal, 0) / 20).b;


        //Color
        float3 col = abs(dot(float3(0,0,1), normal) * tex2D (_MainTex, (IN.worldPos.xy + float2(offset,0)) / 1).rgb);
        col += abs(dot(float3(0,1,0), normal) * tex2D (_MainTex, (IN.worldPos.xz + float2(offset,0)) / 1).rgb);
        col += abs(dot(float3(1,0,0), normal) * tex2D (_MainTex, (IN.worldPos.yz  + float2(offset,0)) / 1).rgb);
        col /= 2.5;

        col = col * (1 - t) + _Color * t;
        
        //half rim = 1.0 - saturate(dot (normalize(IN.viewDir), o.Normal));
        //o.Emission = _RimColor * pow (rim, _RimPower) * _lightIntensity;
        o.Albedo += col;
        o.Alpha = 1;
    }
    ENDCG
    } 

    Fallback "Diffuse"

}
