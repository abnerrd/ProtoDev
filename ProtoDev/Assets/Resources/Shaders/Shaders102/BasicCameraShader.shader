Shader "Hidden/BasicCameraShader"
{
	Properties
	{
		_MainTex ("Texture", 2D) = "white" {}
		_DisplaceTex("Displacement Texture", 2D) = "white" {}
		_Magnitude("Magnitude", Range(0, 1)) = 1
	}
	SubShader
	{
		//	No culling or depth
		Cull Off ZWrite Off ZTest Always

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
			sampler2D _DisplaceTex;
			float _Magnitude;

			float4 _MainTex_TexelSize;	//	easy way to define PIXEL SIZE

			fixed4 frag(v2f i) : SV_Target
			{	
				//	--Use displacement texture & TIME to add "animated" effect to camera screen
				//		Note changes to uv used when getting the displacement texture
				//float2 distuv = float2(i.uv.x + _Time.x * 2, i.uv.y + _Time.x * 2);
				//float2 disp = tex2D(_DisplaceTex,  ).xy;
				//disp = ((disp * 2) - 1)  * _Magnitude;	//	this takes values from [0, 1] --> [-1, 1]
				//float4 return_color = tex2D(_MainTex, i.uv + disp);

				//	--Use displacement texture to add effect to camera screen
				float2 disp = tex2D(_DisplaceTex, i.uv).xy;
				disp = ((disp * 2) - 1) * _Magnitude;
				float4 return_color = tex2D(_MainTex, i.uv + disp);

				//	--Multiply camera screen w/ uv coordinates
				//float4 return_color = tex2D(_MainTex, i.uv);
				//return_color *= float4(i.uv.x, i.uv.y, 0, 1);

				return return_color;
			}
			ENDCG
		}
	}
}
