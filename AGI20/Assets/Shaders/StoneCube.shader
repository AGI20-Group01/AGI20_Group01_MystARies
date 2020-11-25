﻿Shader "Custom/StoneCube"
{
    Properties
    {
        _MainTex ("Texture1", 2D) = "white" {}
        _Main2Tex ("Texture2", 2D) = "white" {}
        _Main3Tex ("Texture3", 2D) = "white" {}
        _Main4Tex ("Texture4", 2D) = "white" {}
        _NoiseTex ("NoiseTexture", 2D) = "white" {}
        
        _sideColor1 ("Side Color", Color) = (1,1,1,1)
        _sideColor2 ("Side Color line", Color) = (0.2,0.2,0.2,1)

        _Glossiness ("Smoothness", Range(0,1)) = 0.5
        _Metallic ("Metallic", Range(0,1)) = 0.0
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 200

        CGPROGRAM
        // Physically based Standard lighting model, and enable shadows on all light types
        #pragma surface surf Standard fullforwardshadows

        // Use shader model 3.0 target, to get nicer looking lighting
        #pragma target 3.0


        struct Input
        {
            float2 uv_MainTex;
            float3 worldPos;
        };

        uniform float _Season1_t;
        uniform float _Season2_t;
        uniform float _worldRot;

        sampler2D _MainTex;
        sampler2D _Main2Tex;
        sampler2D _Main3Tex;
        sampler2D _Main4Tex;
        sampler2D _NoiseTex;

        float4 _sideColor1;
        float4 _sideColor2;

        half _Glossiness;
        half _Metallic;

        // Add instancing support for this shader. You need to check 'Enable Instancing' on materials that use the shader.
        // See https://docs.unity3d.com/Manual/GPUInstancing.html for more information about instancing.
        // #pragma instancing_options assumeuniformscaling
        UNITY_INSTANCING_BUFFER_START(Props)
            // put more per-instance properties here
        UNITY_INSTANCING_BUFFER_END(Props)
    
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

        void surf (Input IN, inout SurfaceOutputStandard o)
        {
            fixed4 col1 = tex2D(_MainTex, IN.uv_MainTex);
            fixed4 col2 = tex2D(_Main2Tex, IN.uv_MainTex);
            fixed4 col3 = tex2D(_Main3Tex, IN.uv_MainTex);
            fixed4 col4 = tex2D(_Main4Tex, IN.uv_MainTex);

            fixed4 noise = tex2D(_NoiseTex,  IN.worldPos.xy/5) * tex2D(_NoiseTex,  IN.worldPos.xy);
            noise += tex2D(_NoiseTex,  IN.worldPos.xz/5) * tex2D(_NoiseTex,  IN.worldPos.xz);
            noise += tex2D(_NoiseTex,  IN.worldPos.yz/5) * tex2D(_NoiseTex,  IN.worldPos.yz);
            noise /= 3;
            
            float tx = noiseTransition(noise, IN.worldPos.xyz, _Season1_t);
            float ty = noiseTransition(noise, IN.worldPos.xyz, _Season2_t);
            float4 colx1 = lerp(col1, col2, tx);
            float4 colx2 = lerp(col4, col3, tx);
            float4 colMasks = lerp(colx1, colx2, ty);
            
            // top Col
            half4 col = colMasks.r * _sideColor1;
            // bottom col
            col += colMasks.b * _sideColor1;


            float zSin = sin(_worldRot * 0.0174532925);
            float zCos = cos(_worldRot * 0.0174532925);
            float3x3 rotMatrix = float3x3(zCos, -zSin, 0, zSin, zCos, 0, 0, 0, 1); 

            
            
            //return _worldRot;
            // side col
            float3 pos = mul(IN.worldPos, rotMatrix);
            //float dist = length(pos.xyz);

            float t = (1 + sign(sin((pos.x+pos.y+pos.z) * 15 ))) / 2.0;

            col.rgb += colMasks.g * (t * _sideColor1 + (1-t) * _sideColor2);
           
            o.Albedo = col.rgb;
            // Metallic and smoothness come from slider variables
            o.Metallic = _Metallic;
            o.Smoothness = _Glossiness;
            o.Alpha = 1;
        }
        ENDCG
    }
    FallBack "Diffuse"
}
