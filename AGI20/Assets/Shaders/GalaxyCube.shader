Shader "Custom/GalaxyCube"
{
    Properties
    {
        _MainTex ("Texture1", 2D) = "white" {}
        _NoiseTex ("Noice Texture", 2D) = "white" {}
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
            float4 screenPos;
        };

        sampler2D _MainTex;
        sampler2D _NoiseTex;

        half _Glossiness;
        half _Metallic;

        float _RotationSpeed;

        // Add instancing support for this shader. You need to check 'Enable Instancing' on materials that use the shader.
        // See https://docs.unity3d.com/Manual/GPUInstancing.html for more information about instancing.
        // #pragma instancing_options assumeuniformscaling
        UNITY_INSTANCING_BUFFER_START(Props)
            // put more per-instance properties here
        UNITY_INSTANCING_BUFFER_END(Props)

        void surf (Input IN, inout SurfaceOutputStandard o)
        {
            _RotationSpeed = 2;

            float4 objectCenter = mul(unity_ObjectToWorld, float4(0.0,0.0,0.0,1.0));
            float dictToCamera = distance(_WorldSpaceCameraPos, objectCenter);

            float2 textureCoordinate = (IN.screenPos.xy / IN.screenPos.w);

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
