Shader "Custom/GimmickTargetPoint"
{
	Properties	{
		_MousePosition("MousePosition", Vector) = (0, 0, 0, 0)
	}

	SubShader	{

		Tags { "RenderType" = "Opaque" }
		LOD 200

		CGPROGRAM
		#pragma surface surf Standard alpha:fade
		#pragma target 3.0

		struct Input {
			float3 worldPos;
		};

		uniform float4 _MousePosition;

		void surf(Input IN, inout SurfaceOutputStandard o) 
		{
			float dist = distance(_MousePosition, IN.worldPos);
			float val = abs(sin(dist*3.0 - _Time * 50));
			if (val > 0.98) {
				o.Albedo = fixed4(0, 1, 1, 1);
				o.Alpha = 1;
			}
			else {
				o.Albedo = fixed4(0, 0, 0, 1);
				o.Alpha = 0;
				
			}
		}
		ENDCG
	}
	FallBack "Diffuse"
}