Shader "Custom/GimmickHighlight"
{
	Properties{
		_OutlineColor("Outline Color", Color) = (0, 0, 0, 0)
		_OutlineWidth("Outline Width", float) = 0.1
		_MainColor("Main Color", Color) = (1, 1, 1, 1)
	}

		SubShader{
			Tags {
				"Queue" = "Geometry"
			}

		//1パス目.

		Cull Front

		CGPROGRAM

		#pragma surface surf Lambert vertex:vert

		float4 _MainColor;
		float4 _OutlineColor;
		float _OutlineWidth;

		struct Input {
			float4 vertexColor : COLOR;
		};

		void vert(inout appdata_full v, out Input o) {
			float distance = -UnityObjectToViewPos(v.vertex).z;
			v.vertex.xyz += v.normal * distance * _OutlineWidth;
			o.vertexColor = v.color;
		}

		void surf(Input IN, inout SurfaceOutput o) {
			o.Albedo = _OutlineColor.rgb;
			float t = abs( ((2 * _SinTime.w * _CosTime.w) + 1.0) * 0.5);
			if (t > 0.2) {
				o.Emission = _OutlineColor * t;
			}
			else {
				o.Emission = _OutlineColor * (0.5 - t);
			}
		}
		ENDCG


			//2パス目.

			Cull Back

			CGPROGRAM

			#pragma surface surf Lambert

			float4 _MainColor;

			struct Input {
				float4 vertexColor : COLOR;
			};

			void surf(Input IN, inout SurfaceOutput o) {
				o.Albedo = _MainColor;
			}
			ENDCG

	}
}













//	Properties{
//		_EmissionTex("Emission Texture", 2D) = "white" {}
//		_EmissionColor("Emission Color", Color) = (1.0, 1.0, 1.0, 1.0)
//		_MainTex("MainTex", 2D) = "white" {}
//		_Outline("_Outline", Range(0,0.1)) = 0
//		_OutlineColor("Color", Color) = (1, 1, 1, 1)
//	}
//		SubShader{
//			Pass {
//				Tags { "RenderType" = "Opaque" }
//				Cull Front
//			
//				CGPROGRAM
//
//				#pragma vertex vert
//				#pragma fragment frag
//				#include "UnityCG.cginc"
//
//				struct v2f {
//					float4 pos : SV_POSITION;
//				};
//
//				float _Outline;
//				float4 _OutlineColor;
//
//				float4 vert(appdata_base v) : SV_POSITION {
//					v2f o;
//					o.pos = UnityObjectToClipPos(v.vertex);
//					float3 normal = mul((float3x3) UNITY_MATRIX_MV, v.normal);
//					normal.x *= UNITY_MATRIX_P[0][0];
//					normal.y *= UNITY_MATRIX_P[1][1];
//					o.pos.xy += normal.xy * _Outline;
//					return o.pos;
//				}
//
//				half4 frag(v2f i) : COLOR {
//					return _OutlineColor;
//				}
//
//				ENDCG
//			}
//
//			LOD 200
//
//			CGPROGRAM
//			#pragma surface surf Lambert
//
//			float4 _EmissionColor;
//			sampler2D _MainTex;
//			sampler2D _EmissionTex;
//
//			struct Input {
//				float2 uv_MainTex;
//			};
//
//			void surf(Input IN, inout SurfaceOutput o) {
//				float4 c = tex2D(_MainTex, IN.uv_MainTex);
//				float t = ((2 * _SinTime.w * _CosTime.w) + 1.0) * 0.5;
//				float e = tex2D(_EmissionTex, IN.uv_MainTex).a * t;
//				o.Albedo = c.rgb;
//				o.Alpha = c.a;
//				o.Emission = _EmissionColor * e;
//			}
//
//			ENDCG
//		}
//		FallBack "Diffuse"
//}