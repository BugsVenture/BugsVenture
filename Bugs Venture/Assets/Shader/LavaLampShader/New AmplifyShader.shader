// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "LavaLamp"
{
	Properties
	{
		_Cutoff( "Mask Clip Value", Float ) = 0.5
		_TextureSample0("Texture Sample 0", 2D) = "white" {}
		_Float5("Float 5", Float) = 2.15
		_Float4("Float 4", Float) = 2.15
		_Float1("Float 1", Float) = -1.3
		_Float0("Float 0", Float) = 4.7
		_TextureSample1("Texture Sample 1", 2D) = "white" {}
		_TextureSample3("Texture Sample 3", 2D) = "white" {}
		_TextureSample4("Texture Sample 4", 2D) = "white" {}
		_Color0("Color 0", Color) = (0,0.4530959,1,0)
		_TextureSample2("Texture Sample 2", 2D) = "white" {}
		_Color1("Color 1", Color) = (0.8400435,0.7056337,0.9528302,0)
		_Float2("Float 2", Float) = 2.77
		[HideInInspector] _texcoord( "", 2D ) = "white" {}
		[HideInInspector] __dirty( "", Int ) = 1
	}

	SubShader
	{
		Tags{ "RenderType" = "TransparentCutout"  "Queue" = "Transparent+0" "IsEmissive" = "true"  }
		Cull Back
		Blend SrcAlpha OneMinusSrcAlpha
		CGINCLUDE
		#include "UnityShaderVariables.cginc"
		#include "UnityPBSLighting.cginc"
		#include "Lighting.cginc"
		#pragma target 3.0
		struct Input
		{
			float2 uv_texcoord;
			float3 worldPos;
			float3 worldNormal;
		};

		uniform float _Float0;
		uniform sampler2D _TextureSample0;
		uniform float4 _TextureSample0_ST;
		uniform sampler2D _TextureSample1;
		uniform float _Float1;
		uniform sampler2D _TextureSample2;
		uniform float _Float5;
		uniform sampler2D _TextureSample4;
		uniform float _Float4;
		uniform sampler2D _TextureSample3;
		uniform float4 _Color0;
		uniform float _Float2;
		uniform float4 _Color1;
		uniform float _Cutoff = 0.5;


		float4 CalculateContrast( float contrastValue, float4 colorTarget )
		{
			float t = 0.5 * ( 1.0 - contrastValue );
			return mul( float4x4( contrastValue,0,0,t, 0,contrastValue,0,t, 0,0,contrastValue,t, 0,0,0,1 ), colorTarget );
		}

		float3 mod2D289( float3 x ) { return x - floor( x * ( 1.0 / 289.0 ) ) * 289.0; }

		float2 mod2D289( float2 x ) { return x - floor( x * ( 1.0 / 289.0 ) ) * 289.0; }

		float3 permute( float3 x ) { return mod2D289( ( ( x * 34.0 ) + 1.0 ) * x ); }

		float snoise( float2 v )
		{
			const float4 C = float4( 0.211324865405187, 0.366025403784439, -0.577350269189626, 0.024390243902439 );
			float2 i = floor( v + dot( v, C.yy ) );
			float2 x0 = v - i + dot( i, C.xx );
			float2 i1;
			i1 = ( x0.x > x0.y ) ? float2( 1.0, 0.0 ) : float2( 0.0, 1.0 );
			float4 x12 = x0.xyxy + C.xxzz;
			x12.xy -= i1;
			i = mod2D289( i );
			float3 p = permute( permute( i.y + float3( 0.0, i1.y, 1.0 ) ) + i.x + float3( 0.0, i1.x, 1.0 ) );
			float3 m = max( 0.5 - float3( dot( x0, x0 ), dot( x12.xy, x12.xy ), dot( x12.zw, x12.zw ) ), 0.0 );
			m = m * m;
			m = m * m;
			float3 x = 2.0 * frac( p * C.www ) - 1.0;
			float3 h = abs( x ) - 0.5;
			float3 ox = floor( x + 0.5 );
			float3 a0 = x - ox;
			m *= 1.79284291400159 - 0.85373472095314 * ( a0 * a0 + h * h );
			float3 g;
			g.x = a0.x * x0.x + h.x * x0.y;
			g.yz = a0.yz * x12.xz + h.yz * x12.yw;
			return 130.0 * dot( m, g );
		}


		void surf( Input i , inout SurfaceOutputStandard o )
		{
			float2 uv_TextureSample0 = i.uv_texcoord * _TextureSample0_ST.xy + _TextureSample0_ST.zw;
			float4 temp_output_15_0 = CalculateContrast(_Float0,tex2D( _TextureSample0, uv_TextureSample0 ));
			float3 ase_vertex3Pos = mul( unity_WorldToObject, float4( i.worldPos , 1 ) );
			float4 appendResult7 = (float4(ase_vertex3Pos.x , ( ( _Time.y * -0.5 ) + ase_vertex3Pos.y ) , 0.0 , 0.0));
			float4 temp_output_12_0 = CalculateContrast(_Float0,tex2D( _TextureSample1, appendResult7.xy ));
			float4 temp_output_10_0 = ( temp_output_15_0 + temp_output_12_0 + CalculateContrast(_Float1,tex2D( _TextureSample2, appendResult7.xy )) );
			float4 appendResult68 = (float4(ase_vertex3Pos.x , ( ( _Time.y * 0.1 ) + ase_vertex3Pos.y ) , 0.0 , 0.0));
			float4 appendResult52 = (float4(ase_vertex3Pos.x , ( ( _Time.y * -0.5 ) + ase_vertex3Pos.y ) , 0.0 , 0.0));
			float4 temp_output_61_0 = CalculateContrast(_Float4,tex2D( _TextureSample3, appendResult52.xy ));
			float4 temp_output_76_0 = (( temp_output_12_0 + temp_output_61_0 )*1.0 + 2.56);
			float2 temp_cast_4 = (( 0.5 * _Time.y )).xx;
			float2 uv_TexCoord83 = i.uv_texcoord * float2( 6,6 ) + temp_cast_4;
			float simplePerlin2D79 = snoise( uv_TexCoord83 );
			float4 temp_cast_5 = (simplePerlin2D79).xxxx;
			float4 temp_output_78_0 = ( ( temp_output_10_0 + CalculateContrast(_Float5,tex2D( _TextureSample4, appendResult68.xy )) + ( temp_output_15_0 + temp_output_61_0 + temp_output_10_0 ) + temp_output_76_0 + CalculateContrast(_Float5,temp_cast_5) ) + temp_output_76_0 );
			float3 ase_worldPos = i.worldPos;
			float3 ase_worldViewDir = normalize( UnityWorldSpaceViewDir( ase_worldPos ) );
			float3 ase_worldNormal = i.worldNormal;
			float fresnelNDotV42 = dot( normalize( ase_worldNormal ), ase_worldViewDir );
			float fresnelNode42 = ( 0.0 + 1.0 * pow( 1.0 - fresnelNDotV42, _Float2 ) );
			float4 temp_output_45_0 = ( ( temp_output_78_0 * _Color0 ) + ( fresnelNode42 * _Color1 ) );
			o.Albedo = temp_output_45_0.rgb;
			o.Emission = temp_output_45_0.rgb;
			o.Alpha = 1;
			clip( temp_output_78_0.r - _Cutoff );
		}

		ENDCG
		CGPROGRAM
		#pragma surface surf Standard keepalpha fullforwardshadows exclude_path:deferred 

		ENDCG
		Pass
		{
			Name "ShadowCaster"
			Tags{ "LightMode" = "ShadowCaster" }
			ZWrite On
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#pragma target 3.0
			#pragma multi_compile_shadowcaster
			#pragma multi_compile UNITY_PASS_SHADOWCASTER
			#pragma skip_variants FOG_LINEAR FOG_EXP FOG_EXP2
			#include "HLSLSupport.cginc"
			#if ( SHADER_API_D3D11 || SHADER_API_GLCORE || SHADER_API_GLES3 || SHADER_API_METAL || SHADER_API_VULKAN )
				#define CAN_SKIP_VPOS
			#endif
			#include "UnityCG.cginc"
			#include "Lighting.cginc"
			#include "UnityPBSLighting.cginc"
			struct v2f
			{
				V2F_SHADOW_CASTER;
				float2 customPack1 : TEXCOORD1;
				float3 worldPos : TEXCOORD2;
				float3 worldNormal : TEXCOORD3;
				UNITY_VERTEX_INPUT_INSTANCE_ID
			};
			v2f vert( appdata_full v )
			{
				v2f o;
				UNITY_SETUP_INSTANCE_ID( v );
				UNITY_INITIALIZE_OUTPUT( v2f, o );
				UNITY_TRANSFER_INSTANCE_ID( v, o );
				Input customInputData;
				float3 worldPos = mul( unity_ObjectToWorld, v.vertex ).xyz;
				fixed3 worldNormal = UnityObjectToWorldNormal( v.normal );
				o.worldNormal = worldNormal;
				o.customPack1.xy = customInputData.uv_texcoord;
				o.customPack1.xy = v.texcoord;
				o.worldPos = worldPos;
				TRANSFER_SHADOW_CASTER_NORMALOFFSET( o )
				return o;
			}
			fixed4 frag( v2f IN
			#if !defined( CAN_SKIP_VPOS )
			, UNITY_VPOS_TYPE vpos : VPOS
			#endif
			) : SV_Target
			{
				UNITY_SETUP_INSTANCE_ID( IN );
				Input surfIN;
				UNITY_INITIALIZE_OUTPUT( Input, surfIN );
				surfIN.uv_texcoord = IN.customPack1.xy;
				float3 worldPos = IN.worldPos;
				fixed3 worldViewDir = normalize( UnityWorldSpaceViewDir( worldPos ) );
				surfIN.worldPos = worldPos;
				surfIN.worldNormal = IN.worldNormal;
				SurfaceOutputStandard o;
				UNITY_INITIALIZE_OUTPUT( SurfaceOutputStandard, o )
				surf( surfIN, o );
				#if defined( CAN_SKIP_VPOS )
				float2 vpos = IN.pos;
				#endif
				SHADOW_CASTER_FRAGMENT( IN )
			}
			ENDCG
		}
	}
	Fallback "Diffuse"
	CustomEditor "ASEMaterialInspector"
}
/*ASEBEGIN
Version=15301
7;29;1906;1004;-3199.339;523.5936;1;True;False
Node;AmplifyShaderEditor.SimpleTimeNode;2;-723.5849,263.0723;Float;False;1;0;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;5;-710.6418,408.8101;Float;False;Constant;_Speed;Speed;1;0;Create;True;0;0;False;0;-0.5;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;50;-934.807,1293.3;Float;False;Constant;_Float3;Float 3;1;0;Create;True;0;0;False;0;-0.5;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleTimeNode;54;-846.833,1035.433;Float;False;1;0;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.PosVertexDataNode;41;-543.693,673.9509;Float;False;0;0;5;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;3;-378.8096,307.4302;Float;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;49;-485.2385,1076.053;Float;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;53;-302.1713,1087.717;Float;True;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;4;-195.7422,319.0938;Float;True;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleTimeNode;73;-966.7485,1791.932;Float;False;1;0;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;72;-1054.723,2049.799;Float;False;Constant;_Float6;Float 6;1;0;Create;True;0;0;False;0;0.1;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;90;-466.5059,2852.921;Float;False;Constant;_Float8;Float 8;13;0;Create;True;0;0;False;0;0.5;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleTimeNode;89;-482.5059,3065.921;Float;False;1;0;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.DynamicAppendNode;7;102.6227,455.3935;Float;True;FLOAT4;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT4;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;66;-605.1539,1832.552;Float;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.DynamicAppendNode;52;-3.80616,1224.016;Float;True;FLOAT4;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT4;0
Node;AmplifyShaderEditor.RangedFloatNode;37;950.7662,702.7719;Float;False;Property;_Float1;Float 1;4;0;Create;True;0;0;False;0;-1.3;-0.61;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;33;605.4407,508.8546;Float;True;Property;_TextureSample2;Texture Sample 2;10;0;Create;True;0;0;False;0;53606611be8871f4f8099801efffddb2;53606611be8871f4f8099801efffddb2;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleAddOpNode;69;-422.0866,1844.216;Float;True;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;55;484.4396,1218.608;Float;True;Property;_TextureSample3;Texture Sample 3;7;0;Create;True;0;0;False;0;5a66d921244e9fb42b60c4fac13bf495;3072fcb20810dcd45af7f00352bcc0b0;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.Vector2Node;84;-307.0504,2451.387;Float;False;Constant;_Vector0;Vector 0;13;0;Create;True;0;0;False;0;6,6;0,0;0;3;FLOAT2;0;FLOAT;1;FLOAT;2
Node;AmplifyShaderEditor.SamplerNode;8;607.5593,70.12659;Float;True;Property;_TextureSample1;Texture Sample 1;6;0;Create;True;0;0;False;0;be2714524fdb772499714408610ce798;be2714524fdb772499714408610ce798;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;13;668.864,349.4138;Float;False;Property;_Float0;Float 0;5;0;Create;True;0;0;False;0;4.7;5.79;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;92;-260.5059,2760.921;Float;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;1;626,-292.5;Float;True;Property;_TextureSample0;Texture Sample 0;1;0;Create;True;0;0;False;0;928f7f001f525eb49a78a49363abe638;928f7f001f525eb49a78a49363abe638;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;62;775.942,1437.288;Float;False;Property;_Float4;Float 4;3;0;Create;True;0;0;False;0;2.15;2.15;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleContrastOpNode;34;1267.439,346.074;Float;True;2;1;COLOR;0,0,0,0;False;0;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleContrastOpNode;61;1314.504,1217.413;Float;True;2;1;COLOR;0,0,0,0;False;0;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;83;17.9496,2493.387;Float;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.DynamicAppendNode;68;-82.97903,1895.15;Float;True;FLOAT4;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT4;0
Node;AmplifyShaderEditor.SimpleContrastOpNode;12;1169.864,68.41379;Float;True;2;1;COLOR;0,0,0,0;False;0;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleContrastOpNode;15;1108.479,-189.6467;Float;True;2;1;COLOR;0,0,0,0;False;0;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleAddOpNode;75;2308.904,639.2529;Float;True;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleAddOpNode;10;1786.95,-55.93222;Float;True;3;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.RangedFloatNode;70;656.0265,2193.786;Float;False;Property;_Float5;Float 5;2;0;Create;True;0;0;False;0;2.15;2.15;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.NoiseGeneratorNode;79;451.9242,2519.26;Float;True;Simplex2D;1;0;FLOAT2;0,0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;67;364.5241,1975.107;Float;True;Property;_TextureSample4;Texture Sample 4;8;0;Create;True;0;0;False;0;5a66d921244e9fb42b60c4fac13bf495;5a66d921244e9fb42b60c4fac13bf495;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;77;2585.076,851.5427;Float;False;Constant;_Float7;Float 7;13;0;Create;True;0;0;False;0;2.56;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.ScaleAndOffsetNode;76;2628.866,483.0782;Float;True;3;0;COLOR;0,0,0,0;False;1;FLOAT;1;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleContrastOpNode;85;1494.66,2290.036;Float;True;2;1;COLOR;0,0,0,0;False;0;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleContrastOpNode;71;1194.588,1973.912;Float;True;2;1;COLOR;0,0,0,0;False;0;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleAddOpNode;64;2009.58,230.6419;Float;True;3;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleAddOpNode;65;2378.411,0.578433;Float;True;5;5;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;COLOR;0,0,0,0;False;3;COLOR;0,0,0,0;False;4;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.RangedFloatNode;46;3545.125,477.4709;Float;False;Property;_Float2;Float 2;12;0;Create;True;0;0;False;0;2.77;3.08;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.FresnelNode;42;3701.918,373.8311;Float;True;Tangent;4;0;FLOAT3;0,0,1;False;1;FLOAT;0;False;2;FLOAT;1;False;3;FLOAT;5;False;1;FLOAT;0
Node;AmplifyShaderEditor.ColorNode;43;3745.799,689.6774;Float;False;Property;_Color1;Color 1;11;0;Create;True;0;0;False;0;0.8400435,0.7056337,0.9528302,0;0.6389078,0.3151032,0.9150943,0;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ColorNode;38;3722.228,99.67284;Float;False;Property;_Color0;Color 0;9;0;Create;True;0;0;False;0;0,0.4530959,1,0;0,0.528654,1,0;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleAddOpNode;78;2878.076,186.5427;Float;True;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;39;4050.202,-175.697;Float;True;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;44;4038.802,258.2995;Float;False;2;2;0;FLOAT;0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleAddOpNode;45;4292.802,185.2993;Float;True;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.ScreenPosInputsNode;57;-1260.015,704.3394;Float;False;0;False;0;5;FLOAT4;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;0;4533.766,-172.8122;Float;False;True;2;Float;ASEMaterialInspector;0;0;Standard;LavaLamp;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;Back;0;False;-1;0;False;-1;False;0;0;False;0;Custom;0.5;True;True;0;True;TransparentCutout;;Transparent;ForwardOnly;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;0;False;-1;False;0;False;-1;255;False;-1;255;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;False;2;15;10;25;False;0.5;True;2;5;False;-1;10;False;-1;0;0;False;-1;0;False;-1;-1;False;-1;-1;False;-1;0;False;0;0,0,0,0;VertexOffset;True;False;Cylindrical;False;Relative;0;;0;-1;-1;-1;0;0;0;False;0;0;0;False;-1;-1;0;False;-1;16;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0;False;4;FLOAT;0;False;5;FLOAT;0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0;False;9;FLOAT;0;False;10;FLOAT;0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;3;0;2;0
WireConnection;3;1;5;0
WireConnection;49;0;54;0
WireConnection;49;1;50;0
WireConnection;53;0;49;0
WireConnection;53;1;41;2
WireConnection;4;0;3;0
WireConnection;4;1;41;2
WireConnection;7;0;41;1
WireConnection;7;1;4;0
WireConnection;66;0;73;0
WireConnection;66;1;72;0
WireConnection;52;0;41;1
WireConnection;52;1;53;0
WireConnection;33;1;7;0
WireConnection;69;0;66;0
WireConnection;69;1;41;2
WireConnection;55;1;52;0
WireConnection;8;1;7;0
WireConnection;92;0;90;0
WireConnection;92;1;89;0
WireConnection;34;1;33;0
WireConnection;34;0;37;0
WireConnection;61;1;55;0
WireConnection;61;0;62;0
WireConnection;83;0;84;0
WireConnection;83;1;92;0
WireConnection;68;0;41;1
WireConnection;68;1;69;0
WireConnection;12;1;8;0
WireConnection;12;0;13;0
WireConnection;15;1;1;0
WireConnection;15;0;13;0
WireConnection;75;0;12;0
WireConnection;75;1;61;0
WireConnection;10;0;15;0
WireConnection;10;1;12;0
WireConnection;10;2;34;0
WireConnection;79;0;83;0
WireConnection;67;1;68;0
WireConnection;76;0;75;0
WireConnection;76;2;77;0
WireConnection;85;1;79;0
WireConnection;85;0;70;0
WireConnection;71;1;67;0
WireConnection;71;0;70;0
WireConnection;64;0;15;0
WireConnection;64;1;61;0
WireConnection;64;2;10;0
WireConnection;65;0;10;0
WireConnection;65;1;71;0
WireConnection;65;2;64;0
WireConnection;65;3;76;0
WireConnection;65;4;85;0
WireConnection;42;3;46;0
WireConnection;78;0;65;0
WireConnection;78;1;76;0
WireConnection;39;0;78;0
WireConnection;39;1;38;0
WireConnection;44;0;42;0
WireConnection;44;1;43;0
WireConnection;45;0;39;0
WireConnection;45;1;44;0
WireConnection;0;0;45;0
WireConnection;0;2;45;0
WireConnection;0;10;78;0
ASEEND*/
//CHKSM=105BD16366947B35368C4821779F0FA03A14931A