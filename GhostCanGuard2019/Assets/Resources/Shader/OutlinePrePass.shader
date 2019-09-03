

//アウトラインのshader、単色のテキスチャーをoutする

Shader "OutlineShader/OutlinePrePass"
{
	Properties{
		
		_OutlineColor("OutlineColor", Color) = (1,0,0,1)
	}
	
	SubShader
	{
		Pass
		{
			CGPROGRAM
			#include "UnityCG.cginc"
				
			    fixed4 _OutlineColor;

				struct v2f
				{
					float4 pos : SV_POSITION;
					/*fixed3 color : COLOR0;*/
				};
				

				/*fixed3 outlineColor(fixed4 color) {
					fixed3 result;
					result.x = color.x;
					result.y = color.y;
					result.z = color.z;
					return result;
				}*/
				v2f vert(appdata_full v)
				{
					v2f o;
					o.pos = UnityObjectToClipPos(v.vertex);
					/*o.color = outlineColor(_OutlineColor);*/
					return o;
				}

				

				fixed4 frag(v2f i) : SV_Target
				{
					
					//単色のテキスチャーをoutする
					//color = _OutlineColor;
					//return color;
					return fixed4(1,1,0,1);//アウトラインの色
				}

				
				#pragma vertex vert
				#pragma fragment frag
			ENDCG
		}
	}
}
