Shader "M2U/Beast-RT Lighting/Transparent/Alpha Blending/Flat/Reflective/DiffuseMap"
{
Properties
{
	_Color ("Main Color (RGB)", Color) = (1,1,1,1)
	_MainTex ("Base (RGB)", 2D) = "white" {}
	_ReflectColor ("Reflection Color", Color) = (1,1,1,0.5)
	_Cube ("Reflection Cubemap", Cube) = "_Skybox" { TexGen CubeReflect }
}
SubShader
{
	Tags{  "RenderType"="Transparent"  "IgnoreProjector"="True"  "Queue"="Transparent" }
	LOD 200
	
	CGPROGRAM
	#pragma surface surf Lambert alpha
	
	sampler2D _MainTex;
	samplerCUBE _Cube;
	float4 _ReflectColor;
	float4 _Color;
	
	struct Input 
	{
		float2 uv_MainTex;
		float3 worldRefl;
	};

	void surf (Input IN, inout SurfaceOutput o)
	{
		half4 tex = tex2D(_MainTex, IN.uv_MainTex);
		half4 c = tex * _Color;
		o.Albedo = c.rgb;
		half4 reflcol = texCUBE (_Cube, IN.worldRefl);
		reflcol *= _ReflectColor.a;
		o.Alpha = c.a * reflcol.a;
		o.Emission = reflcol.rgb * _ReflectColor.rgb;
	}
	ENDCG
	}
FallBack "M2U/Beast-RT Lighting/Transparent/Alpha Blending/Flat/DiffuseMap"
}
