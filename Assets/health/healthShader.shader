Shader "Custom/healthShdr" {
	Properties {
		_Color ("Color", Color) = (1,1,1,1)
		_MainTex ("REFERENCE (RGB)", 2D) = "white" {}
		_HealthTex("HEALTH (RGB)", 2D) = "white" {}
		_BackTex("BACK (RGB)", 2D) = "white" {}
		_Glossiness ("Smoothness", Range(0,1)) = 0.5
		_Metallic ("Metallic", Range(0,1)) = 0.0
		_Value ("Health Value", Range(0,1)) = 0.0
		_EmissionColor("Color", Color) = (0,0,0)
		_EmissionMap("Emission", 2D) = "white" {}
	}
	SubShader {
		Tags { "RenderType" = "Transparent" "Queue" = "Transparent" }
		LOD 200
		
		CGPROGRAM
		// Physically based Standard lighting model, and enable shadows on all light types
		#pragma surface surf Standard fullforwardshadows alpha:fade

		// Use shader model 3.0 target, to get nicer looking lighting
		#pragma target 3.0

		sampler2D _MainTex;
	sampler2D _EmissionMap;
		sampler2D _HealthTex;
		sampler2D _BackTex;
		struct Input {
			float2 uv_MainTex;
		};

		half _Glossiness;
		half _Metallic;
		half _Value;
		fixed4 _Color;
		fixed4 _EmissionColor;
		void surf (Input IN, inout SurfaceOutputStandard o) {
			// Albedo comes from a texture tinted by color
			fixed4 b = tex2D(_BackTex, IN.uv_MainTex) * _Color;
			o.Albedo = b.rgb;
			fixed4 c = tex2D(_MainTex, IN.uv_MainTex);
			if (c.r <= _Value ) {
				fixed4 d = tex2D(_HealthTex, IN.uv_MainTex);
				
				o.Albedo = d.rgb;
			}
			
			// Metallic and smoothness come from slider variables
			o.Emission= b  * _EmissionColor ;
			o.Metallic = _Metallic;
			o.Smoothness = _Glossiness;
			o.Alpha = b.a;
		}
		ENDCG
	}
	FallBack "Diffuse"
}
