Shader "Custom/FerverGauge" {
    Properties {
        _MainTex ( "Base", 2D ) = "white" {}
    }
 
    SubShader {
        Tags {"Queue"="Transparent"}
        Cull Front
    
        CGPROGRAM
 
        #pragma surface surf Lambert alpha
 
        sampler2D _MainTex;
 
        struct Input {
            float2 uv_MainTex;
        };
 
        void surf( Input IN, inout SurfaceOutput o ) {
            half4 color = tex2D( _MainTex, IN.uv_MainTex );
            o.Albedo = color.rgb;
            o.Alpha = 0.6;
        }
 
        ENDCG
    }
 
    FallBack "Diffuse"
}