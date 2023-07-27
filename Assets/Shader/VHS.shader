Shader "Custom/VHS"
{
    Properties{
        _MainTex("Base (RGB)", 2D) = "white" {}
        _VHSTex("VHS Texture", 2D) = "white" {}
        _TimeX("TimeX", Range(0, 1)) = 0
        _Distortion("Distortion", Range(0, 0.1)) = 0.03
        _ScanLineSpeed("Scan Line Speed", Range(0, 10)) = 1
    }

        SubShader{
            Tags { "Queue" = "Transparent" "RenderType" = "Transparent" }
            LOD 200

            CGPROGRAM
            #pragma surface surf Lambert alpha
            #pragma target 3.0

            sampler2D _MainTex;
            sampler2D _VHSTex;
            float _TimeX;
            float _Distortion;
            float _ScanLineSpeed;

            struct Input {
                float2 uv_MainTex;
                float2 uv_VHSTex;
            };

            void surf(Input IN, inout SurfaceOutput o) {
                float2 screenUV = IN.uv_MainTex;

                // Add time-based distortion to the UV coordinates
                float distortionX = sin(screenUV.y * _Distortion + _TimeX * _ScanLineSpeed) * _Distortion;
                screenUV.x += distortionX;

                // Sample VHS texture to add the scan line effect
                float scanLine = tex2D(_VHSTex, IN.uv_VHSTex).r;
                o.Alpha *= scanLine;

                // Sample the base texture
                fixed4 c = tex2D(_MainTex, screenUV);

                o.Albedo = c.rgb;
            }
            ENDCG
        }
            FallBack "Diffuse"
}
