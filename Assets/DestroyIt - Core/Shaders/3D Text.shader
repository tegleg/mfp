Shader "DestroyIt/3D Text" {
	Properties {
		_MainTex ("Base (RGB)", 2D) = "white" {}
		_Color ("Text Color", Color) = (1,1,1,1)
	}
	SubShader {
		Tags { "RenderType"="Transparent" "Queue"="Transparent" "IgnoreProjector"="True" }
		Lighting Off
		Cull Off
		Fog {Mode Off}
		ZWrite Off
		Blend SrcAlpha OneMinusSrcAlpha
		Pass {
			Color [_Color]
			ColorMaterial AmbientAndDiffuse
			SetTexture [_MainTex] {
				combine primary, texture*primary
			}
		}
	} 
	FallBack "Diffuse"
}