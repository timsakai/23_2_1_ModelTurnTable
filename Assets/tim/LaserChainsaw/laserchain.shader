Shader "Custom/laserchain"
{
    Properties
    {
        _Color("Color", Color) = (1,1,1,1)
        _SpqColor ("SperqColor", Color) = (1,1,1,1)
        _MainTex ("Albedo (RGB)", 2D) = "white" {}
        _Power("Power",Float) = 8.0
        _NoiseSize("NoiseSize",Float) = 8.0
        _SpqPower("SparqPower",Float) = 1.0
        _SpqSize("SparqSize",Float) = 8.0
        _Speed("Speed",Float) = 10.0
        _Low("Low",Range(0,1))=0.0
        _SpqLow("SparqLow",Range(0,1))=1.0
        _EdgeTex ("EdgeTexture", 2D) = "white" {}
        _EdgeNoiseSize("EdgeNoiseSize",Float) = 8.0
        _EdgeHeight("EdgeHeight",Float) = 1.0
        _EdgeFloor("EdgeFloor",Float) = -1.0
        _EdgeSpeed("EdgeSpeed",Float) = 10.0
    }
    SubShader
    {
    Tags {
			"Queue" = "Transparent"
			"RenderType" = "Opaque"
		}
			
		Blend SrcAlpha OneMinusSrcAlpha
		Cull Off
        ZWrite On

    Pass
    {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            // make fog work
            #pragma multi_compile_fog

            #include "UnityCG.cginc"
            
            #define PI 3.141592

            struct appdata
            {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float2 uv : TEXCOORD0;
                float4 color : COLOR; 
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                UNITY_FOG_COORDS(1)
                float4 color : COLOR;
                float4 vertex : SV_POSITION;
            };

        float _Power;
        fixed4 _Color;
        fixed4 _SpqColor;
        float _NoiseSize;
        float _SpqPower;
        float _SpqSize;
        float _Speed;
        half _Low;
        half _SpqLow;

            float _EdgeNoiseSize;
            float _EdgeHeight;
            float _EdgeFloor;
            float _EdgeSpeed;

        fixed2 random2(fixed2 st){
            st = fixed2( dot(st,fixed2(127.1,311.7)),
                           dot(st,fixed2(269.5,183.3)) );
            return -1.0 + 2.0*frac(sin(st)*43758.5453123);
            }
        
        float perlinNoise(fixed2 st) 
            {
            fixed2 p = floor(st);
            fixed2 f = frac(st);
            fixed2 u = f*f*(3.0-2.0*f);

            float v00 = random2(p+fixed2(0,0));
            float v10 = random2(p+fixed2(1,0));
            float v01 = random2(p+fixed2(0,1));
            float v11 = random2(p+fixed2(1,1));

            return lerp( lerp( dot( v00, f - fixed2(0,0) ), dot( v10, f - fixed2(1,0) ), u.x ),
                         lerp( dot( v01, f - fixed2(0,1) ), dot( v11, f - fixed2(1,1) ), u.x ), 
                         u.y)+0.5f;
            }        
        
        float fBm (fixed2 st) 
            {
            float f = 0;
            fixed2 q = fixed2(cos(PI * 0.25)*st.x,sin(PI * 0.25)*st.y);//45度回転

            f += 0.5000*perlinNoise( q ); q = q*2.01;
             f += 0.2500*perlinNoise( q ); q = q*(2.02* (_Time.w % 1));
            f += 0.1250*perlinNoise( q ); q = q*2.03;
            f += 0.0625*perlinNoise( q ); q = q*2.01;

            return f;
            }

            sampler2D _MainTex;
            float4 _MainTex_ST;

            v2f vert (appdata v)
            {
                v2f o;
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                //端モデルを法線方向に拡縮
                v.vertex.xyz += (v.normal * (fBm(o.uv*_EdgeNoiseSize + fixed2(0,_Time.w*_EdgeSpeed)) - _EdgeFloor * 0.01) * (_EdgeHeight * 0.001f)) * v.color.b;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.color = v.color;
                UNITY_TRANSFER_FOG(o, o.vertex);
                //UNITY_TRANSFER_FOG(o,o.vertex);
                return o;
            }

        fixed4 frag (v2f i) : SV_Target
            {
            // 全体へ乗算するテクスチャ
            fixed4 tex = tex2D (_MainTex, i.uv + _Time);
            //頂点カラーで端部の重み取得
            float edge = fBm(i.uv *_NoiseSize + fixed2(0,_Time.w*(_Speed*_NoiseSize))) * _Power;
            float edgedist = saturate(i.color.r - _Low);
            edge *= edgedist;
            //体部分のスパーク取得
            float spq = (fBm(i.uv*_SpqSize * fixed2(0.5f,1) + fixed2(0,_Time.w*(_Speed*_SpqSize)))-_SpqLow) * _SpqPower;
            spq = max(spq,0);
            fixed4 col = saturate((edge + spq) * tex * _Color) * (1 - i.color.b) 
                        /*端モデル（頂点カラー青）の部分を白で塗りつぶし*/
                        + fixed4(1.0f,1.0f,1.0f,1.0f)* _SpqColor * i.color.b;
            col.rgb = saturate(col);
            col.a = col.rgb;
            UNITY_APPLY_FOG(i.fogCoord, col);
            return col;
            }
            ENDCG
        }
    }
}
