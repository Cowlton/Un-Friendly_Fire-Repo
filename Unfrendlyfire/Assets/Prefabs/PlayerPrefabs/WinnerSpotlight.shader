Shader "UI/WinnerSpotlight"
{
    Properties
    {
        [PerRendererData] _MainTex ("Texture", 2D) = "white" {}
        _Color ("Tint", Color) = (0,0,0,1)

        _Center ("Circle Center", Vector) = (0.5,0.5,0,0)
        _Radius ("Circle Radius", Range(0,1)) = 0.15
        _Softness ("Edge Softness", Range(0.001,0.2)) = 0.02

        _SpotlightEnabled ("Spotlight Enabled", Float) = 1
    }

    SubShader
    {
        Tags
        {
            "Queue" = "Transparent"
            "RenderType" = "Transparent"
            "IgnoreProjector" = "True"
            "CanUseSpriteAtlas" = "True"
        }

        Cull Off
        Lighting Off
        ZWrite Off
        ZTest [unity_GUIZTestMode]
        Blend SrcAlpha OneMinusSrcAlpha

        Pass
        {
            CGPROGRAM

            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
                fixed4 color : COLOR;
            };

            struct v2f
            {
                float4 vertex : SV_POSITION;
                float2 uv : TEXCOORD0;
                fixed4 color : COLOR;
            };

            sampler2D _MainTex;

            fixed4 _Color;
            float4 _Center;

            float _Radius;
            float _Softness;
            float _SpotlightEnabled;

            v2f vert(appdata v)
            {
                v2f o;

                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                o.color = v.color;

                return o;
            }

            fixed4 frag(v2f i) : SV_Target
            {
                fixed4 tex = tex2D(_MainTex, i.uv);

                // Calculate distance from the circle center.
                float2 p = i.uv - _Center.xy;

                // Correct for screen aspect ratio.
                p.x *= _ScreenParams.x / _ScreenParams.y;

                float distanceFromCenter = length(p);

                // Create the circular transparent hole.
                float hole = smoothstep(
                    _Radius - _Softness,
                    _Radius + _Softness,
                    distanceFromCenter
                );

                // When disabled, remove the transparent hole.
                hole = lerp(1.0, hole, _SpotlightEnabled);

                fixed4 col = tex * _Color * i.color;

                // Apply the cutout to the background alpha.
                col.a *= hole;

                return col;
            }

            ENDCG
        }
    }
}
