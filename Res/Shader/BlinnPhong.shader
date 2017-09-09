Shader "Custom/BlinnPhong" {
	Properties {
		_Color ("Color", Color) = (1,1,1,1)
		_MainTex ("Albedo (RGB)", 2D) = "white" {}
		_SpecPower ("SpecPower",Range(1,60)) = 1
		_SpecularColor("SpecularColor",Color) = (1,1,1,1)


	}
	SubShader {
		Tags { "RenderType"="Opaque" }
		LOD 200

		CGPROGRAM

		#pragma surface surf CustomBlinnPhong fullforwardshadows

		#pragma target 3.0

		sampler2D _MainTex;

		struct Input {
			float2 uv_MainTex;
		};

		fixed4 _Color;
		half _SpecPower;
		fixed4 _SpecularColor;


		inline half4 LightingCustomBlinnPhong(SurfaceOutput s, fixed3 lightDir, half3 viewDir, fixed atten)
		{
			float3 halfVector = normalize(lightDir + viewDir);//半角向量
			float diff = max(0, dot(s.Normal, lightDir)); // 法线与光照夹角
			float nh = max(0, dot(s.Normal, halfVector)); // 法线与半角向量夹角
			float spec = pow(nh, _SpecPower); // 镜面高光强度和颜色
			float4 c;
			c.rgb = ((s.Albedo * _LightColor0.rgb * diff) +(_LightColor0.rgb * _SpecularColor * spec*2)) * (atten);
			// 漫反射                +   镜面高光  +										阴影衰减
			c.a = s.Alpha;
			return c;
		}

		void surf (Input IN, inout SurfaceOutput o) {
			fixed4 c = tex2D (_MainTex, IN.uv_MainTex) * _Color;
			o.Albedo = c.rgb;
			o.Alpha = c.a;
		}
		ENDCG
	}
	FallBack "Diffuse"
}
