Shader "Mobile/MobileWaterShader_EKV" {
    Properties{
        [Toggle(LIGHTING)]_SECTIONLIGHTING("---- LIGHTING ------------------------", float) = 1
        [PowerSlider(5.0)]_Shininess("Shininess", Range(0.01, 2)) = 0
        [PowerSlider(5.0)]_Brightness("Brightness", Range(0.01, 100)) = 0
        [PowerSlider(5.0)]_Attenuation("Attenuation", Range(0.001, 1)) = 0
        _Color("Color Tint",COLOR) = (0.5,0.5,0.5,1.0)

        [Toggle(TEXTURES)]_SECTIONTEXTURES("---- TEXTURES (if disabled, waves will disable too)---------------", float) = 1
        _MainTex("Texture A", 2D) = "black" {}
        _MainTexRot("Texture A Rotation", Range(0 , 360)) = 0

        _DiffTex("Texture B", 2D) = "black" {}
        _DiffTexRot("Texture B Rotation", Range(0 , 360)) = 0

        [Toggle(WAVES)]_SECTIONWAVES("---- WAVES AND FLOW ------------------------", float) = 1
        [NoScaleOffset]_DerivHeightMap("Wave Height Map", 2D) = "black" {}
        _Tiling("Tiling", Float) = 3
        [PowerSlider(1.0)]_Speed("Speed", Range(0.001, 1)) = 0.1
        [PowerSlider(1.0)]_FlowStrength("Flow Strength", Range(-1, 1)) = 0.1
        [PowerSlider(1.0)]_FlowOffset("Flow Offset", Range(-1, 1)) = 0.25
        [PowerSlider(1.0)]_HeightScale("Height Scale, Constant", Range(-1, 1)) = 0.5
        [PowerSlider(1.0)]_HeightScaleModulated("Height Scale, Modulated", Range(-5, 5)) = 4

        [Toggle(REFLECTION)]_REFLECTION("---- CUBEMAP REFLECTION ------------------------", float) = 1
        [Slider]_RefStrength("Reflection Strength", Range(0, 1)) = 1
        _Cube("Cubemap", CUBE) = "" {}

        [Toggle(BLENDING)]_BLENDING("---- BLENDING MODE (always on) ------------------------", float) = 1
        SrcMode("Src Mode - recommended: 4 (opaque), 3 or 5 (transparent)", int) = 4
        DstMode("Dst Mode - recommended: 3", int) = 3
        [Toggle(COMMTRANSP)]_COMMTRANSP("(Remember setting render queue to 3000 for transparency)", float) = 1

    }


        /////////////////////////////////// LOD 600

        SubShader{


            Tags { "RenderType" = "Opaque" "RenderQueue" = "Transparent"}
            LOD 600

            Blend [SrcMode][DstMode]
            //Blend Off
            

        CGPROGRAM

        #pragma shader_feature LIGHTING
        #pragma shader_feature TEXTURES
        #pragma shader_feature WAVES
        #pragma shader_feature REFLECTION
        #pragma shader_feature BLENDING
        #pragma surface surf MobileBlinnPhong fullforwardshadows exclude_path:prepass nolightmap noforwardadd halfasview interpolateview

        fixed4 _Color;
        float4 tint0;
        float _Brightness;
        float _RefStrength;
        float _Attenuation;
        float _MainTexRot;
        float _DiffTexRot;
        
        
        sampler2D _DerivHeightMap;
        float _Speed, _FlowStrength, _FlowOffset, _Tiling;
        float _HeightScale, _HeightScaleModulated;

        sampler2D _MainTex;
        sampler2D _DiffTex;
        samplerCUBE _Cube;
        half _Shininess;
        
        struct Input {
            float2 uv_MainTex;
            float2 uv_DiffTex;
            float3 worldRefl;
            float4 screenPos;
            INTERNAL_DATA
        };

        float3 FlowUVW(
            float2 uv, float2 flowVector, float2 jump,
            float flowOffset, float tiling, float time, bool flowB
        ) {
            float phaseOffset = flowB ? 0.5 : 0;
            float progress = frac(time + phaseOffset);
            float3 uvw;
            uvw.xy = uv - flowVector * (progress + flowOffset);
            uvw.xy *= tiling;
            uvw.xy += phaseOffset;
            uvw.xy += (time - progress) * jump;
            uvw.z = 1 - abs(1 - 2 * progress);
            return uvw;
        }

        void Unity_Rotate_Degrees_float(float2 UV, float2 Center, float Rotation, out float2 Out)
        {
            Rotation = Rotation * (3.1415926f / 180.0f);
            UV -= Center;
            float s = sin(Rotation);
            float c = cos(Rotation);
            float2x2 rMatrix = float2x2(c, -s, s, c);
            rMatrix *= 0.5;
            rMatrix += 0.5;
            rMatrix = rMatrix * 2 - 1;
            UV.xy = mul(UV.xy, rMatrix);
            UV += Center;
            Out = UV;
        }

        inline fixed4 LightingMobileBlinnPhong(SurfaceOutput s, fixed3 lightDir, fixed3 halfDir, fixed atten)
        {
            fixed diff = max(0, dot(s.Normal, halfDir));
            fixed nh = max(0, dot(s.Normal, halfDir));
            fixed spec = pow(nh, s.Specular * 128) * (s.Gloss);

            fixed4 c;
#ifdef LIGHTING
            c.rgb = ((s.Albedo * _LightColor0.rgb * diff + (_LightColor0.rgb + _Brightness) * spec) * _Attenuation);
            
#else
            c.rgb = (s.Albedo * _LightColor0.rgb * diff  * spec) * atten;
#endif

            UNITY_OPAQUE_ALPHA(c.a);
        
            return c;
        }

        float3 UnpackDerivativeHeight(float4 textureData) {
            float3 dh = textureData.agb;
            dh.xy = dh.xy * 2 -1 ;
            return dh;
        }

        void surf(Input IN, inout SurfaceOutput o) {

#ifdef TEXTURES
            //texture rotation
            Unity_Rotate_Degrees_float(IN.uv_MainTex, 0, _MainTexRot, IN.uv_MainTex);
            Unity_Rotate_Degrees_float(IN.uv_DiffTex, 0, _DiffTexRot, IN.uv_DiffTex);
#else
            IN.uv_MainTex = 0;
            IN.uv_DiffTex = 0;
#endif

            fixed4 tex = tex2D(_MainTex, IN.uv_MainTex) * _Color;
            fixed4 tex2 = tex2D(_DiffTex, IN.uv_DiffTex) * _Color;



#ifdef TEXTURES            
#ifdef WAVES
            //waves
            float3 flow = tex2D(_DiffTex, IN.uv_MainTex).rgb;

            flow.xy = flow.xy * 2 - 1;
            flow *= _FlowStrength;
            float noise = tex2D(_DiffTex, IN.uv_MainTex).a;
            float time = _Time.y * _Speed + noise;


            float3 uvwA = FlowUVW(
                IN.uv_MainTex, flow.xy, 0,
                _FlowOffset, _Tiling, time, false
            );
            float3 uvwB = FlowUVW(
                IN.uv_DiffTex, flow.xy, 0,
                _FlowOffset, _Tiling, time, true
            );


            float finalHeightScale =
                flow.z * _HeightScaleModulated + _HeightScale;

            float3 dhA =
                UnpackDerivativeHeight(tex2D(_DerivHeightMap, uvwA.xy)) *
                (uvwA.z * finalHeightScale);
            float3 dhB =
                UnpackDerivativeHeight(tex2D(_DerivHeightMap, uvwB.xy)) *
                (uvwB.z * finalHeightScale);
            //o.Normal = normalize(float3(-(dhA.xy + dhB.xy), 1));
            o.Normal = normalize(float3(-(dhA.xy + dhB.xy), 2));
            
            //end waves
#endif
#endif

            o.Albedo = tex.rgb + tex2.rgb;
            o.Gloss = tex.a + tex2.a;
            o.Alpha = tex.a + tex2.a;
#ifdef LIGHTING
            o.Specular = 1 / _Shininess;
#else
            o.Specular = 1;
#endif
            

           //reflection
            #ifdef REFLECTION
                o.Albedo = o.Albedo + ((texCUBE(_Cube, WorldReflectionVector(IN, o.Normal)).rgb * _Color) * _RefStrength);
            #endif


        }

        ENDCG
    }


    FallBack "Mobile/VertexLit"
}
