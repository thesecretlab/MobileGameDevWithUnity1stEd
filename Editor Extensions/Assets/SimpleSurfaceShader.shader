// BEGIN surface_shader
Shader "Custom/SimpleSurfaceShader" {

    // BEGIN surface_shader_properties
    Properties {
        // The colour to tint the object with
        _Color ("Color", Color) = (0.5,0.5,0.5,1)

        // The texture to wrap the object in; 
        // defaults to a plain white texture
        _MainTex ("Albedo (RGB)", 2D) = "white" {}

        // How smooth the surface should be
        _Smoothness ("Smoothness", Range(0,1)) = 0.5

        // How metallic the surface should be
        _Metallic ("Metallic", Range(0,1)) = 0.0

        // BEGIN rim
        // BEGIN rim_properties
        // The colour the rim lighting should be
        _RimColor ("Rim Color", Color) = (1.0,1.0,1.0,0.0)

        // How thick the rim lighting should be
        _RimPower ("Rim Power", Range(0.5,8.0)) = 2.0
        // END rim_properties
        // END rim
      
    }
    // END surface_shader_properties

    SubShader {
        Tags { "RenderType"="Opaque" }
        LOD 200
        
        CGPROGRAM
            // Physically based Standard lighting model, and 
            // enable shadows on all light types
            #pragma surface surf Standard fullforwardshadows

            // Use shader model 3.0 target, to get nicer looking lighting
            #pragma target 3.0

            // The following variables are "uniform" - the same value
            // is used for every pixel

            // The texture to use for the albedo
            sampler2D _MainTex;

            // The colour to tint the albedo with
            fixed4 _Color;

            // BEGIN rimlight_context
            // The smoothness and metallicness properties
            half _Smoothness;
            half _Metallic;

            // BEGIN rimlight
            // The colour for the rim lighting
            float4 _RimColor;

            // How thick the rim lighting should be - closer to
            // zero means thicker rim
            float _RimPower;
            // END rimlight
            // END rimlight_context


            // 'Input' contains variables whose values are different
            // for every pixel
            // BEGIN surface_shader_input
            struct Input {
                // Texture coordinates for the vertex
                float2 uv_MainTex;

                // BEGIN rim
                // BEGIN rim_input_additions
                // The direction the camera is looking at this vertex
                float3 viewDir;
                // END rim_input_additions
                // END rim
            };
            // END surface_shader_input

            // BEGIN surface_shader_func
            // This single function computes the properties of this surface
            void surf (Input IN, inout SurfaceOutputStandard o) {

                // Using the data stored in IN and the variables above,
                // compute the values and store them in 'o'

                // Albedo comes from a texture tinted by color
                fixed4 c = tex2D (_MainTex, IN.uv_MainTex) * _Color;
                o.Albedo = c.rgb;

                   // Metallic and smoothness come from slider variables
                o.Metallic = _Metallic;
                o.Smoothness = _Smoothness;

                // Alpha value for this comes from the texture we're using
                // for albedo
                o.Alpha = c.a;

                // BEGIN rim
                // BEGIN rimlight_calculate
                // Calculate how bright the rim light should be at this pixel
                half rim = 1.0 - saturate(dot (normalize(IN.viewDir), o.Normal));

                // Use this brightness to calculate the rim colour, and
                // use it for the emission
                o.Emission = _RimColor.rgb * pow (rim, _RimPower);
                // END rimlight_calculate
                // END rim
                   
            }
            // END surface_shader_func
        ENDCG
    }

    // If the computer running this shader isn't capable of running at
    // shader model 3.0, fall back to the built-in "Diffuse" shader,
    // which doesn't look anywhere as good but is guaranteed to work
    FallBack "Diffuse"
}
// END surface_shader