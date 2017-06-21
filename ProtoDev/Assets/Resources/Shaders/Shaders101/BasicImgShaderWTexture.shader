Shader "Hidden/BasicImgShaderWTexture"
{
	Properties
	{
		_MainTex ("Texture", 2D) = "white" {}
		_Color("Color", Color) = (0.5, 0.5, 0.5, 1)

		_Tween("Tween", Range(0, 1)) = 0
		_SecondTex("Second Texture", 2D) = "white" {}
	}
	SubShader
	{
		Tags
		{
			//	Has sprites render BEHIND opaque geometry
			"Queue" = "Transparent"
		}

		Pass
		{
			//	Add transparency to Shader w/ AlphaBlending
			Blend SrcAlpha OneMinusSrcAlpha

			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			
			#include "UnityCG.cginc"

			struct appdata
			{
				float4 vertex : POSITION;
				float2 uv : TEXCOORD0;
			};

			struct v2f
			{
				float2 uv : TEXCOORD0;
				float4 vertex : SV_POSITION;
			};

			v2f vert (appdata v)
			{
				v2f o;
				o.vertex = mul(UNITY_MATRIX_MVP, v.vertex);
				o.uv = v.uv;
				return o;
			}
			
			sampler2D _MainTex;
			sampler2D _SecondTex;
			uniform float4 _Color;
			float _Tween;

			fixed4 frag(v2f i) : SV_Target
			{
				//	--Tween b/t two images
				float4 color1 = tex2D(_MainTex, i.uv);
				float4 color2 = tex2D(_SecondTex, i.uv);
				float4 return_color = lerp(color1, color2, _Tween);

				//	--Multiply uv values to Texture colors
				//	fixed4 return_color = tex2D(_MainTex, i.uv) * float4(i.uv.r, i.uv.g, 0, 1);		
				
				//	--Adds color tint w/ Color Property
				//	fixed4 return_color =  tex2D(_MainTex, i.uv) * _Color;		


				return return_color;
			}
			ENDCG
		}
	}
}
