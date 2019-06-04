
//PostEffectOutlineShader


Shader "OutlineShader/OutLinePostEffect" {

	Properties{
		_MainTex("Base (RGB)", 2D) = "white" {}
		_BlurTex("Blur", 2D) = "white"{}
	}

	CGINCLUDE
	#include "UnityCG.cginc"

		//目標モデルをぼんやりさせて、輪郭を出す
		struct v2f_blur
		{
			float4 pos : SV_POSITION;
			float2 uv  : TEXCOORD0;
			float4 uv01 : TEXCOORD1;
			float4 uv23 : TEXCOORD2;
			float4 uv45 : TEXCOORD3;
		};
		//目標モデルの中心のテキスチャーを消して、輪郭線だけを残す
		struct v2f_cull
		{
			float4 pos : SV_POSITION;
			float2 uv : TEXCOORD0;
		};

		//輪郭線と元のテキスチャーを統合する
		struct v2f_add
		{
			float4 pos : SV_POSITION;
			float2 uv  : TEXCOORD0;
			float2 uv1 : TEXCOORD1;
		};

		sampler2D _MainTex;
		float4 _MainTex_TexelSize;
		sampler2D _BlurTex;
		float4 _BlurTex_TexelSize;
		float4 _offsets;

		
		
		//ガウシアンぼかしを使って画像をぼかします　　vertex shaderの部分
		v2f_blur vert_blur(appdata_img v)
		{
			v2f_blur o;
			_offsets *= _MainTex_TexelSize.xyxy;
			o.pos = UnityObjectToClipPos(v.vertex);
			o.uv = v.texcoord.xy;

			o.uv01 = v.texcoord.xyxy + _offsets.xyxy * float4(1, 1, -1, -1);
			o.uv23 = v.texcoord.xyxy + _offsets.xyxy * float4(1, 1, -1, -1) * 2.0;
			o.uv45 = v.texcoord.xyxy + _offsets.xyxy * float4(1, 1, -1, -1) * 3.0;

			return o;
		}

		
		//ガウシアンぼかしを使って画像をぼかします	fragment shaderの部分
		fixed4 frag_blur(v2f_blur i) : SV_Target
		{
			fixed4 color = fixed4(0,0,0,0);
			color += 0.40 * tex2D(_MainTex, i.uv);
			color += 0.15 * tex2D(_MainTex, i.uv01.xy);
			color += 0.15 * tex2D(_MainTex, i.uv01.zw);
			color += 0.10 * tex2D(_MainTex, i.uv23.xy);
			color += 0.10 * tex2D(_MainTex, i.uv23.zw);
			color += 0.05 * tex2D(_MainTex, i.uv45.xy);
			color += 0.05 * tex2D(_MainTex, i.uv45.zw);
			return color;
		}


		//輪郭線だけを残した画像を読み取る,まずは頂点のデータ vertex shader
		v2f_cull vert_cull(appdata_img v)
		{
			v2f_cull o;
			o.pos = UnityObjectToClipPos(v.vertex);
			o.uv = v.texcoord.xy;
			//DirectXではテキスチャーの原点は左上ですから、その場合で読み取ったテキスチャーを上下逆転する
			#if UNITY_UV_STARTS_AT_TOP
			if (_MainTex_TexelSize.y < 0)
				o.uv.y = 1 - o.uv.y;
			#endif	
			return o;
		}
		//ぼんやりした画像に元の画像を減らして、輪郭線部分だけを残す 
		fixed4 frag_cull(v2f_cull i) : SV_Target
		{
			fixed4 colorMain = tex2D(_MainTex, i.uv);
			fixed4 colorBlur = tex2D(_BlurTex, i.uv);
			//輪郭以外の部分の色は 0-0 で黒、輪郭線の部分の色は アウトラインの色-0 でアウトラインの色そのまま、中身の色は アウトラインの色-アウトラインの色で黒だ、最終的には輪郭線の部分だけの色がoutされた
			//return fixed4((colorBlur - colorMain).rgb, 1);
			return colorBlur - colorMain;
		}

		

		//統合する vertex shader
		v2f_add vert_add(appdata_img v)
		{
			v2f_add o;
			//mvp変換
			o.pos = UnityObjectToClipPos(v.vertex);
			//uv座標を渡す
			o.uv.xy = v.texcoord.xy;
			o.uv1.xy = o.uv.xy;
			#if UNITY_UV_STARTS_AT_TOP
			if (_MainTex_TexelSize.y < 0)
				o.uv.y = 1 - o.uv.y;
			#endif	
			return o;
		}
		
		//統合する fragment shader
		fixed4 frag_add(v2f_add i) : SV_Target
		{
			
			//オリジナルsceneのテキスチャーを読み取る
			fixed4 ori = tex2D(_MainTex, i.uv1);
			//輪郭線のテキスチャーを読み取る
			fixed4 blur = tex2D(_BlurTex, i.uv);
			//両者を加えていく
			fixed4 final = ori + blur;
			return final;
		}

	ENDCG

	SubShader
		{
			//pass 0: ガウシアンぼかし
			Pass
			{
				ZTest Off
				Cull Off
				ZWrite Off
				Fog{ Mode Off }

				CGPROGRAM
				#pragma vertex vert_blur
				#pragma fragment frag_blur
				ENDCG
			}

			//pass 1: 中心のテキスチャーを消して
			Pass
			{
				ZTest Off
				Cull Off
				ZWrite Off
				Fog{ Mode Off }

				CGPROGRAM
				#pragma vertex vert_cull
				#pragma fragment frag_cull
				ENDCG
			}


			//pass 2: 統合する
			Pass
			{

				ZTest Off
				Cull Off
				ZWrite Off
				Fog{ Mode Off }

				CGPROGRAM
				#pragma vertex vert_add
				#pragma fragment frag_add
				ENDCG
			}

		}
}
