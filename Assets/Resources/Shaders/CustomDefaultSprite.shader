Shader "Custom/CustomDefaultSprite"
{
    Properties
    {
        [PerRendererData] _MainTex("Sprite Texture", 2D) = "white" {}
		[PerRendererData] _color("Color", Color) = (1, 1, 1, 1)

		_tint("Tint", Color) = (1, 1, 1, 1)
		_strength("Strength", Range(0.0, 1.0)) = 0.0

		[MaterialToggle] PixelSnap ("Pixel snap", Float) = 0
    }
    SubShader
    {
        Tags 
		{ 
			"Queue" = "Transparent"
			"IgnoreProjector" = "True"
			"RenderType" = "Transparent"
			"PreviewType" = "Plane"
			"CanUseSpriteAtlas" = "True" 
		}
        
		Cull Off
		Lighting Off
		ZWrite Off
		Fog { Mode Off }
		Blend SrcAlpha OneMinusSrcAlpha

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
			#pragma multi_compile _ PIXELSNAP_ON

            #include "UnityCG.cginc"

			sampler2D _MainTex;

			fixed4 _color;
			fixed4 _tint;

			fixed _strength;

			struct appdata
            {
                float4 vertex : POSITION;
                float4 color  : COLOR;
				float2 texcoord : TEXCOORD0;
            };

			struct v2f
            {
				float4 vertex : SV_POSITION;
				fixed4 color  : COLOR;
				float2 texcoord : TEXCOORD0;
            };

			v2f vert (appdata i)
            {
                v2f o;

                o.vertex = UnityObjectToClipPos(i.vertex);
                o.texcoord = i.texcoord;
                o.color = i.color * _color;

				#ifdef PIXELSNAP_ON
				o.vertex = UnityPixelSnap(o.vertex);
				#endif

                return o;
            }

			fixed4 frag (v2f i) : SV_Target
            {
                fixed4 color = tex2D(_MainTex, i.texcoord) * i.color;

				color.rgb = lerp(color.rgb, _tint.rgb, _strength);

                return color;
            }

            ENDCG
        }
    }
}
