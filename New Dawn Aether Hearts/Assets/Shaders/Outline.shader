// Reference: https://www.youtube.com/watch?v=WnIGh8xrdG8

Shader "Unlit/Outline"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        [Space(10)]
        _MainColor ("Main Color", Color) = (1, 1, 1, 1)
        _OutlineColor ("Outline Color", Color) = (1, 1, 1, 1)
        _OutlineValue ("Outline Value", Range(0.0, 0.5)) = 0.2
    }
    SubShader
    {
        // OUTLINE
        Pass
        {
            Tags
            {
                "Queue"="Transparent"
            }

            Blend SrcAlpha OneMinusSrcAlpha
            ZWrite Off

            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

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

            sampler2D _MainTex;
            float4 _MainTex_ST;
            float4 _OutlineColor;
            float _OutlineValue;

            float4 outline(float4 vertexPosition, float outlineValue)
            {
                float4x4 scale = float4x4
                (
                    1 + outlineValue, 0, 0, 0,
                    0, 1 + outlineValue, 0, 0,
                    0, 0, 1 + outlineValue, 0,
                    0, 0, 0, 1 + outlineValue
                );

                return mul(scale, vertexPosition);
            }

            v2f vert (appdata v)
            {
                v2f o;
                float4 vertexPosition = outline(v.vertex, _OutlineValue);
                o.vertex = UnityObjectToClipPos(vertexPosition);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                fixed4 col = tex2D(_MainTex, i.uv);
                return float4(_OutlineColor.r, _OutlineColor.g, _OutlineColor.b, col.a);
            }
            ENDCG
        }

        // TEXTURE
        Pass
        {
            Tags
            {
                "Queue" = "Transparent+1"
            }

            Blend SrcAlpha OneMinusSrcAlpha

            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

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

            sampler2D _MainTex;
            float4 _MainTex_ST;
            float4 _MainColor;

            v2f vert(appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                return o;
            }

            fixed4 frag(v2f i) : SV_Target
            {
                fixed4 col = tex2D(_MainTex, i.uv);
                return _MainColor;
            }
            ENDCG
        }
    }
}
