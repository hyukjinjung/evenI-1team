Shader "Custom/SpotlightShader"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _PlayerPos ("Player Position", Vector) = (0,0,0,0)
        _Radius ("Spotlight Radius", Float) = 3
        _Softness ("Edge Softness", Float) = 0.5
    }
    SubShader
    {
        Tags { "Queue"="Transparent" "RenderType"="Transparent" }
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
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
                float4 worldPos : TEXCOORD1;
            };

            sampler2D _MainTex;
            float4 _PlayerPos;
            float _Radius;
            float _Softness;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.worldPos = mul(unity_ObjectToWorld, v.vertex);
                o.uv = v.uv;
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                float dist = distance(i.worldPos.xy, _PlayerPos.xy);
                float circle = 1 - smoothstep(_Radius - _Softness, _Radius + _Softness, dist);
                return fixed4(0, 0, 0, 1 - circle);
            }
            ENDCG
        }
    }
} 