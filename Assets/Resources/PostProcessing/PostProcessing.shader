// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'

Shader "Shader/PostProcessingShader"
{
	Properties
	{
		_MainTex("Texture", 2D) = "white" {}
		_PlayerPos("Player position", vector) = (0.0, 0.0, 0.0, 0.0)
		_BlurSize("Blur Size", Range(0,0.1)) = 0

	}
		SubShader
		{
			// No culling or depth
			Cull Off ZWrite Off ZTest Always

			Pass
			{
				CGPROGRAM
				// Upgrade NOTE: excluded shader from DX11; has structs without semantics (struct v2f members position_in_world_space)
				//#pragma exclude_renderers d3d11
							#pragma vertex vert
							#pragma fragment frag

							#include "UnityCG.cginc"

							#define SAMPLES 10

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


							v2f vert(appdata v)
							{
								v2f o;
								o.vertex = UnityObjectToClipPos(v.vertex);
								o.uv = v.uv;
								return o;
							}

							sampler2D _MainTex;
							float3 _PlayerPos;
							int _Blur;
							int _Dead;
							float _BlurSize;
							float _Radius;

							fixed4 frag(v2f i) : SV_Target
							{
								fixed4 col = tex2D(_MainTex, i.uv);

							//Threshold Distance
							float threshold_x = pow(_ScreenParams.x * _Radius, 2.0f);
							float threshold_y = pow(_ScreenParams.y * _Radius, 2.0f);
							float threshold = threshold_x + threshold_y;
							//Pixel Coords of Fragment 
							float x = (_ScreenParams.x * i.uv.x);
							float y = (_ScreenParams.y * i.uv.y);
							//Distance from Player
							float dist_x = pow((_ScreenParams.x * _PlayerPos.x - x), 2.0f);
							float dist_y = pow((_ScreenParams.y * _PlayerPos.y - y), 2.0f);
							float dist = dist_x + dist_y;

							//GREYSCALE
							if (!_Blur && dist > threshold)
							{
								float intensity = 0.2126f * col.r + 0.7152f * col.g + 0.0722 * col.b;
								col = fixed4(intensity, intensity, intensity, 1.0f);
								return col;
							}

							//X BLUR 
							if (_Blur && dist > threshold)
							{
								float sqrt_dist = sqrt(dist_x);
								
								if(!_Dead)
									_BlurSize *= sqrt_dist / 500.0f;

								float invAspect = _ScreenParams.y / _ScreenParams.x;
								for (float index = 0; index < SAMPLES; index++)
								{
									float offset = (index / (SAMPLES - 1) - 0.5f) * _BlurSize * invAspect;
									float2 uv = i.uv + float2(offset, 0);
									col += tex2D(_MainTex, uv);
								}
								return col / SAMPLES;
							}

							return col;
						}
						ENDCG
					}
		}
}