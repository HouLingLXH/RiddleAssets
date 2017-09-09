Shader "Unlit/Diffuse"
{
	Properties
	{
		_MainTex ("Texture", 2D) = "white" {}
	}
	SubShader
	{
		Tags { "RenderType"="Opaque" "LightMode" = "ForwardBase" }
		LOD 100

		Pass
		{

			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			// make fog work
			#pragma multi_compile_fog
			
			#include "UnityCG.cginc"
			#include "Lighting.cginc"


			struct appdata
			{
				float4 vertex : POSITION;
				float2 uv : TEXCOORD0;
				float3 normal:NORMAL;
			};

			struct v2f
			{
				float2 uv : TEXCOORD0;
				UNITY_FOG_COORDS(1)
				float4 pos:TEXCOORD2;
				float4 vertex : SV_POSITION;
				float3 normal:NORMAL;
			};

			sampler2D _MainTex;
			float4 _MainTex_ST;
			
			v2f vert (appdata v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.uv = TRANSFORM_TEX(v.uv, _MainTex);
				o.pos = v.vertex;
				o.normal = v.normal;
				UNITY_TRANSFER_FOG(o,o.vertex);
				return o;
			}
			
			fixed4 frag (v2f i) : SV_Target
			{
				// sample the texture
				fixed4 col = tex2D(_MainTex, i.uv);
				clip(col.a - 0.6)
				// apply fog
				UNITY_APPLY_FOG(i.fogCoord, col);

				float3 lightDir = WorldSpaceLightDir(i.pos);
				lightDir = normalize(lightDir);
				float3 worldNormal = UnityObjectToWorldNormal(i.normal); //已经归一化了

				float angle = dot(lightDir, worldNormal);
				//angle = saturate(angle);

				float3 diffuse = _LightColor0.xyz *  angle ;

				col.xyz *= diffuse;

				return col;
			}
			ENDCG
		}
	}
}
