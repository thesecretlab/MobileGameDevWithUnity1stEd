// BEGIN unlit_shader
Shader "Custom/SimpleUnlitShader"
{
    Properties
    {
        _Color ("Color", Color) = (1.0,1.0,1.0,1)

    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 100

        Pass
        {
            CGPROGRAM

            // Define which functions should be 
            // used in this shader.

            // The 'vert' function will be used as
            // the vertex shader.
            #pragma vertex vert

            // The 'frag' function will be used as
            // the fragment shader.
            #pragma fragment frag

            // Include a number of useful utilities from Unity.
            #include "UnityCG.cginc"

            float4 _Color;

            // This structure is given to the 
            // vertex shader for each vertex
            struct appdata
            {
                // The position of the vertex in world space.
                float4 vertex : POSITION;

            };

            // This structure is given to the 
            // fragment shader for each fragment
            struct v2f
            {
                // The position of the fragment in 
                // screen space 
                float4 vertex : SV_POSITION;
            };

            // Given a vertex, transform it
            v2f vert (appdata v)
            {
                v2f o;

                // Convert the vertex from world space to
                // view space by multiplying it with a matrix
                // provided by Unity. (This comes from UnityCG.cginc)
                o.vertex = mul(UNITY_MATRIX_MVP, v.vertex);

                // Return it, passing it to the fragment shader
                return o;
            }

            // Given interpolated information about
            // nearby vertices, return the final colour
            // BEGIN unlit_shader_frag
            fixed4 frag (v2f i) : SV_Target
            {
               fixed4 col;

               // Render the provided color
               col = _Color;

               // BEGIN unlit_shader_fade
               // Fade over time - start black, fade up to _Color
               col *= abs(_SinTime[3]);
               // END unlit_shader_fade

               return col;
            }
            // END unlit_shader_frag
            ENDCG
        }
    }
}
// END unlit_shader