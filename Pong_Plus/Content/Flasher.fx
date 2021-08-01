#if OPENGL
	#define SV_POSITION POSITION
	#define VS_SHADERMODEL vs_3_0
	#define PS_SHADERMODEL ps_3_0
#else
	#define VS_SHADERMODEL vs_4_0_level_9_1
	#define PS_SHADERMODEL ps_4_0_level_9_1
#endif

Texture2D SpriteTexture;

sampler2D SpriteTextureSampler = sampler_state
{
	Texture = <SpriteTexture>;
};

float4 color;

struct PXData
{
	float4 Position : SV_POSITION;
	float4 Color : COLOR0;
	float2 TextureCoordinates : TEXCOORD0;
};

float4 MainPS(PXData input) : COLOR
{
	float4 n = tex2D(SpriteTextureSampler,input.TextureCoordinates);
	if (n.a == 0) { return n; }
	float4 d = color - n;
	n.rgb += d.rgb * color.a;
	return n;
}

technique SpriteDrawing
{
	pass Pass0
	{
		PixelShader = compile PS_SHADERMODEL MainPS();
	}
};