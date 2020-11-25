// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Custom/WaterShader"
{
    Properties {
      _MainTex ("Texture", 2D) = "white" {}
      _BumpMap ("Bumpmap", 2D) = "bump" {}

      _Color ("Color", Color) = (1,1,1,1)
      _RimColor ("Rim Color", Color) = (0.26,0.19,0.16,0.0)
      _RimPower ("Rim Power", Range(0.5,8.0)) = 3.0
    }
    SubShader {
      Tags { "Queue"="Transparent" "RenderType"="Transparent" }
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

    uniform float _Season1_t;
    uniform float _Season2_t;

    sampler2D _MainTex;
    sampler2D _MyGrabTexture;
    sampler2D _BumpMap;

    float3 _worldRot;

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

    void surf (Input IN, inout SurfaceOutput o) {
        float t = (sin(_Time * 10)) * 0.2 * (1 - _Season1_t); 

        float4 objectCenter = mul(unity_ObjectToWorld, float4(0.0,0.0,0.0,1.0));
        float dictToCamera = distance(_WorldSpaceCameraPos, objectCenter);

        float zSin = sin(_worldRot * 0.0174532925);
        float zCos = cos(_worldRot * 0.0174532925);
        float3x3 rotMatrix = float3x3(zCos, -zSin, 0, zSin, zCos, 0, 0, 0, 1); 
        
        IN.worldPos = mul(IN.worldPos, rotMatrix);

        
        
        //t *= dictToCamera;
        float3 normal = IN.normal;
        normal = mul(normal, rotMatrix);
        //float3 normal = UnpackNormal (tex2D (_BumpMap, IN.uv_MainTex));
        o.Normal = abs(dot(float3(0,0,1), normal) * UnpackNormal (tex2D (_BumpMap, (IN.worldPos.xy + float2(t,0)) / 1.5) / 3));
        o.Normal += abs(dot(float3(1,0,0), normal) * UnpackNormal (tex2D (_BumpMap, (IN.worldPos.yz + float2(t,0)) / 1.5) / 3));
        o.Normal += abs(dot(float3(0,1,0), normal)  * UnpackNormal (tex2D (_BumpMap, (IN.worldPos.xz  + float2(t,0)) / 1.5) / 3));
        o.Normal = normalize(o.Normal);
        o.Normal /= 1.5;

        o.Albedo = tex2Dproj( _MyGrabTexture, UNITY_PROJ_COORD(IN.grabUV) + float4(o.Normal, 0) / 15 );
        //o.Albedo.g = tex2Dproj( _MyGrabTexture, UNITY_PROJ_COORD(IN.grabUV) + float4(-0.1, 0, 0, 0) + float4(o.Normal, 0) / 20).g;
        //o.Albedo.b = tex2Dproj( _MyGrabTexture, UNITY_PROJ_COORD(IN.grabUV) + float4(0.1, 0, 0, 0) + float4(o.Normal, 0) / 20).b;
        float3 col = abs(dot(float3(0,0,1), normal) * tex2D (_MainTex, (IN.worldPos.xy + + float2(t,0)) / 1).rgb);
        col += abs(dot(float3(0,1,0), normal) * tex2D (_MainTex, (IN.worldPos.xz + float2(t,0)) / 1).rgb);
        col += abs(dot(float3(1,0,0), normal) * tex2D (_MainTex, (IN.worldPos.yz  + float2(t,0)) / 1).rgb);
        col /= 2.5;
        col = col * (1 - _Season1_t) + _Color * _Season1_t;
        
        half rim = 1.0 - saturate(dot (normalize(IN.viewDir), o.Normal));
        o.Emission = _RimColor * pow (rim, _RimPower);
        o.Albedo += col;
        o.Alpha = 1;
    }
    ENDCG
    } 

    Fallback "Diffuse"

}
