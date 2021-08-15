Shader "Custom/ChangeColours"
{
    Properties
    {
        _PrimaryColor ("PrimaryColor", Color) = (1,0,0,1)
        _SecondaryColor ("SecondaryColor", Color) = (1,0,0,1)
        _TertiaryColor ("TertiaryColor", Color) = (1,0,0,1)
        _MainTex ("Albedo (RGB)", 2D) = "white" {}
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }

       Pass
        {
            CGPROGRAM
            #pragma vertex vert
		    #pragma fragment frag

            #include "UnityCG.cginc"
			
			struct appdata_t
			{
				float4 vertex   : POSITION;
				float4 color    : COLOR;
				float2 texcoord : TEXCOORD0;
			};

			struct v2f
			{
				float4 vertex   : SV_POSITION;
				fixed4 color    : COLOR;
				float2 texcoord  : TEXCOORD0;
			};

            fixed4 _PrimaryColor;
            fixed4 _SecondaryColor;
            fixed4 _TertiaryColor;

            sampler2D _MainTex;

            v2f vert(appdata_t IN)
			{
				v2f OUT;
				OUT.vertex = UnityObjectToClipPos(IN.vertex);
				OUT.texcoord = IN.texcoord;
				OUT.color = IN.color;
				return OUT;
			}
        }
    }
    
}
