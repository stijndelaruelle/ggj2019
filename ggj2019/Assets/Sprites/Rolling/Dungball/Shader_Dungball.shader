// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Disco Dungball/Dungball Shader" {
	
	Properties
	{
	    m_DiffuseTexture ("Albedo (RGB)", 2D) = "white" {}
		m_EdgeTexture("Albedo (RGB)", 2D) = "white" {}
	}

	Category
	{
		Tags { "RenderType"="Opaque" }
		Lighting Off

		SubShader
		{
			Pass
			{
				CGPROGRAM
				#pragma vertex vert
				#pragma fragment frag
				
				#include "UnityCG.cginc"
				
				sampler2D m_DiffuseTexture;
				sampler2D m_EdgeTexture;
				float4 m_DiffuseTexture_ST;

				struct Input
				{
					float4 vertex : POSITION;
					float2 uv : TEXCOORD0;
					float4 worldPos : TEXCOORD1;
				};

				struct v2f
				{
					float4 vertex : SV_POSITION;
					float2 uv : TEXCOORD0;
					float4 worldPos : TEXCOORD1;
				};

				v2f vert (Input v)
				{
					v2f o;
					o.vertex = UnityObjectToClipPos(v.vertex);
					o.uv = v.uv;

					fixed4 inverseVertexPos = v.vertex;
					inverseVertexPos.x = 1 - v.vertex.x;
					inverseVertexPos.y = 1 - v.vertex.y;
					inverseVertexPos.z = 1 - v.vertex.z;

					o.worldPos = mul(unity_ObjectToWorld, inverseVertexPos);
					return o;
				}
				
				fixed4 frag (v2f i) : SV_Target
				{
					//fixed4 diffuseSample = tex2D(m_DiffuseTexture, i.uv * m_DiffuseTexture_ST.xy); //m_DiffuseTexture_ST.yz
					//fixed4 diffuseSample = tex2D(m_DiffuseTexture, (i.uv + m_DiffuseTexture_ST.xy));
					fixed4 diffuseSample = tex2D(m_DiffuseTexture, (i.worldPos.xy * m_DiffuseTexture_ST.xy));
					return diffuseSample;
				}

				ENDCG 
			}
		}	
	}
}