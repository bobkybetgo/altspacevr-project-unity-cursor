Shader "Custom/MyShader" {
	Properties {
		_Color ("Main Color", COLOR) = (1,1,1,1)
	}
	SubShader {
		Tags { "RenderType"="Opaque"  "Queue" = "Overlay"} //Queue overlay = render as last layer, always
		LOD 200
		
		ZTest Off
		Lighting Off
		
		Pass {
		
			CGPROGRAM
				#pragma vertex vert
				#pragma fragment frag

				#include "UnityCG.cginc"
				
				float4 vert(float4 vertex : POSITION) : SV_POSITION
				{
					return mul(UNITY_MATRIX_MVP, vertex);
				}
		
				fixed4 _Color; 
				fixed4 frag() : SV_Target
				{
					return _Color;			
				}
		
			ENDCG
		} 
	}
}