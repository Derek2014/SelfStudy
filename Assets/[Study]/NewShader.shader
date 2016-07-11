Shader "Custom/NewShader" {
	Properties {
		_MainTex ("Base (RGBA)", 2D) = "white" {}
	}
	SubShader {
		Tags { "RenderType"="Opaque" }
		LOD 200
		
		CGPROGRAM
		#pragma surface surf Lambert

		sampler2D _MainTex;

		struct Input {
			float2 uv_MainTex;
		};

		void surf (Input IN, inout SurfaceOutput o) {
			half4 c = tex2D (_MainTex, IN.uv_MainTex);
			o.Albedo = float3(0.5, 0.5, 0); // c.rgb;
			o.Alpha = 0.2; //c.a * 0.1;
		}
		ENDCG
	} 

	FallBack "Diffuse"
}

//Shader "Custom/NewShader" {
//	Properties {
//		_MainTex ("Base (RGB)", 2D) = "white" {}
//	}
//	
//	SubShader {
//		Tags { "RenderType"="Opaque" }
//		LOD 200	
//		
//		CGPROGRAM
//		#pragma surface surf Lambert
//		struct C2E1v_Output {
//
//		  float4 position : POSITION;
//
//		  float4 color    : COLOR;
//
//		};
//
//		C2E1v_Output C2E1v_green(float2 position : POSITION)
//		{
//		  C2E1v_Output OUT;
//
//		  OUT.position = float4(position, 0, 1);
//
//		  OUT.color    = float4(0, 1, 0, 1);  // RGBA green
//
//		  return OUT;
//		}
//		ENDCG
//	}
//	
//	FallBack "Diffuse"
//}