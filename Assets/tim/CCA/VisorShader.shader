Shader "Custom/VisorShader"
{
    Properties
    {
        _Color ("Color", Color) = (1,1,1,1)
        _MainTex ("Albedo (RGB)", 2D) = "white" {}
        _Glossiness ("Smoothness", Range(0,1)) = 0.5
        _Metallic ("Metallic", Range(0,1)) = 0.0
            _Size("Size",Int) = 100
        _Progress("Progress",Range(-1,3)) = 0.0
            _Position("Position",Range(-3,2)) = 0.0
            _ShiftX("ShiftX",Range(-1,1)) = 0.0
    }
    SubShader
    {
        Tags { "RenderType"="Transparent" }
        LOD 200

        CGPROGRAM
        // Physically based Standard lighting model, and enable shadows on all light types
        #pragma surface surf Standard alpha

        // Use shader model 3.0 target, to get nicer looking lighting
        #pragma target 3.0

        sampler2D _MainTex;

        struct Input
        {
            float2 uv_MainTex;
        };

        half _Glossiness;
        half _Metallic;
        int _Size;
        half _Progress;
        half _Position;
        half _ShiftX;
        fixed4 _Color;

        // Add instancing support for this shader. You need to check 'Enable Instancing' on materials that use the shader.
        // See https://docs.unity3d.com/Manual/GPUInstancing.html for more information about instancing.
        // #pragma instancing_options assumeuniformscaling
        UNITY_INSTANCING_BUFFER_START(Props)
            // put more per-instance properties here
        UNITY_INSTANCING_BUFFER_END(Props)

        void surf (Input IN, inout SurfaceOutputStandard o)
        {
            // Albedo comes from a texture tinted by color
            fixed4 c = tex2D (_MainTex, IN.uv_MainTex) * _Color;
            o.Albedo = c.rgb;
            // Metallic and smoothness come from slider variables
            float rot = 0.785398;
            float2x2 rotmat = float2x2 (cos(rot), -sin(rot),
                                        sin(rot), cos(rot));
            float2 roted = mul(IN.uv_MainTex * _Size, rotmat) + float2(_ShiftX,0);
            float2 muled = roted % 1.0f -float2(0.5, 0.5 * sign(roted.y));
            float position_weight = saturate(abs(IN.uv_MainTex.y + _Position - (1.0 * sign(IN.uv_MainTex.y + _Position))));
            float weight_hole = 1.0 < abs(IN.uv_MainTex.y + _Position);
            position_weight *= weight_hole;
            position_weight = 1 - position_weight;
            float weight = ((pow(muled.x, 2) + pow(muled.y, 2)) * position_weight) < ((1 - position_weight) / 10);
            //float weight = ((pow(muled.x, 2) + pow(muled.y, 2)) * IN.uv_MainTex.y);
            //float weight = IN.uv_MainTex.y < _Progress;
            o.Alpha = weight * _Color.a;

            o.Metallic = weight * _Metallic;
            o.Smoothness = weight * _Glossiness;
        }
        ENDCG
    }
    FallBack "Diffuse"
}
