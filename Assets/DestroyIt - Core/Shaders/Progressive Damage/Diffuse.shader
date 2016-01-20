// Shader created with Shader Forge v1.06 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.06;sub:START;pass:START;ps:flbk:VertexLit,lico:1,lgpr:1,nrmq:1,limd:1,uamb:True,mssp:True,lmpd:False,lprd:False,rprd:False,enco:False,frtr:True,vitr:True,dbil:False,rmgx:False,rpth:0,hqsc:True,hqlp:False,tesm:0,blpr:0,bsrc:0,bdst:1,culm:0,dpts:2,wrdp:True,dith:0,ufog:True,aust:True,igpj:False,qofs:0,qpre:1,rntp:1,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,ofsf:0,ofsu:0,f2p0:False;n:type:ShaderForge.SFN_Final,id:1,x:33540,y:32621,varname:node_1,prsc:2|diff-6029-OUT,amdfl-6042-OUT;n:type:ShaderForge.SFN_Tex2d,id:2,x:32214,y:32429,ptovrint:False,ptlb:MainTex,ptin:_MainTex,varname:node_1910,prsc:2,ntxv:0,isnm:False;n:type:ShaderForge.SFN_If,id:61,x:32822,y:32438,varname:node_61,prsc:2|A-881-OUT,B-72-OUT,GT-270-B,EQ-270-B,LT-137-OUT;n:type:ShaderForge.SFN_Vector1,id:72,x:32822,y:32386,varname:node_72,prsc:2,v1:0.2;n:type:ShaderForge.SFN_If,id:90,x:32822,y:32626,varname:node_90,prsc:2|A-881-OUT,B-93-OUT,GT-270-G,EQ-270-G,LT-61-OUT;n:type:ShaderForge.SFN_Vector1,id:93,x:32804,y:32570,varname:node_93,prsc:2,v1:0.4;n:type:ShaderForge.SFN_If,id:126,x:32822,y:32821,varname:node_126,prsc:2|A-881-OUT,B-128-OUT,GT-270-R,EQ-270-R,LT-90-OUT;n:type:ShaderForge.SFN_Vector1,id:128,x:32822,y:32756,varname:node_128,prsc:2,v1:0.6;n:type:ShaderForge.SFN_Vector1,id:137,x:32822,y:32323,varname:node_137,prsc:2,v1:1;n:type:ShaderForge.SFN_Tex2dAsset,id:206,x:32219,y:32870,ptovrint:False,ptlb:Damage Mask,ptin:_DamageMask,varname:node_953,tex:c4e987ef2d922ca47a8ace9435e0a07f,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Tex2d,id:270,x:32499,y:33117,varname:node_6686,prsc:2,tex:c4e987ef2d922ca47a8ace9435e0a07f,ntxv:0,isnm:False|TEX-206-TEX;n:type:ShaderForge.SFN_Color,id:449,x:32214,y:32253,ptovrint:False,ptlb:Color,ptin:_Color,varname:node_1809,prsc:2,glob:False,c1:1,c2:1,c3:1,c4:1;n:type:ShaderForge.SFN_Multiply,id:450,x:32592,y:32372,varname:node_450,prsc:2|A-2-RGB,B-449-RGB;n:type:ShaderForge.SFN_Tex2d,id:569,x:32220,y:32660,ptovrint:False,ptlb:Damage MainTex,ptin:_DamageMainTex,varname:node_3550,prsc:2,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Vector1,id:581,x:32822,y:32954,varname:node_581,prsc:2,v1:0.8;n:type:ShaderForge.SFN_If,id:583,x:32822,y:33014,varname:node_583,prsc:2|A-881-OUT,B-581-OUT,GT-815-OUT,EQ-815-OUT,LT-126-OUT;n:type:ShaderForge.SFN_Lerp,id:692,x:33051,y:32741,varname:node_692,prsc:2|A-569-RGB,B-769-OUT,T-583-OUT;n:type:ShaderForge.SFN_Vector3,id:769,x:32518,y:32914,varname:node_769,prsc:2,v1:1,v2:1,v3:1;n:type:ShaderForge.SFN_Vector3,id:815,x:32518,y:33015,varname:node_815,prsc:2,v1:0,v2:0,v3:0;n:type:ShaderForge.SFN_Slider,id:881,x:32066,y:33061,ptovrint:False,ptlb:Damage Level,ptin:_DamageLevel,varname:node_925,prsc:2,min:0,cur:0.5563341,max:1;n:type:ShaderForge.SFN_Multiply,id:6029,x:33309,y:32605,varname:node_6029,prsc:2|A-450-OUT,B-692-OUT;n:type:ShaderForge.SFN_AmbientLight,id:6037,x:32460,y:32147,varname:node_6037,prsc:2;n:type:ShaderForge.SFN_LightAttenuation,id:6038,x:32460,y:32007,varname:node_6038,prsc:2;n:type:ShaderForge.SFN_LightColor,id:6039,x:32460,y:31877,varname:node_6039,prsc:2;n:type:ShaderForge.SFN_Multiply,id:6040,x:32925,y:32022,varname:node_6040,prsc:2|A-6039-RGB,B-6038-OUT,C-6041-OUT;n:type:ShaderForge.SFN_Subtract,id:6041,x:32822,y:32196,varname:node_6041,prsc:2|A-692-OUT,B-6037-RGB;n:type:ShaderForge.SFN_Lerp,id:6042,x:33375,y:32298,varname:node_6042,prsc:2|A-6058-OUT,B-815-OUT,T-6040-OUT;n:type:ShaderForge.SFN_OneMinus,id:6058,x:33352,y:32064,varname:node_6058,prsc:2|IN-692-OUT;proporder:449-2-569-206-881;pass:END;sub:END;*/

Shader "DestroyIt/Diffuse" {
    Properties {
        _Color ("Color", Color) = (1,1,1,1)
        _MainTex ("MainTex", 2D) = "white" {}
        _DamageMainTex ("Damage MainTex", 2D) = "white" {}
        _DamageMask ("Damage Mask", 2D) = "white" {}
        _DamageLevel ("Damage Level", Range(0, 1)) = 0.5563341
    }
    SubShader {
        Tags {
            "RenderType"="Opaque"
        }
        LOD 400
        Pass {
            Name "ForwardBase"
            Tags {
                "LightMode"="ForwardBase"
            }
            
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDBASE
            #include "UnityCG.cginc"
            #include "AutoLight.cginc"
            #include "Lighting.cginc"
            #pragma multi_compile_fwdbase_fullshadows
            #pragma multi_compile_fog
            #pragma exclude_renderers xbox360 ps3 flash 
            #pragma target 3.0
            uniform sampler2D _MainTex; uniform float4 _MainTex_ST;
            uniform sampler2D _DamageMask; uniform float4 _DamageMask_ST;
            uniform float4 _Color;
            uniform sampler2D _DamageMainTex; uniform float4 _DamageMainTex_ST;
            uniform float _DamageLevel;
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float2 texcoord0 : TEXCOORD0;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                float4 posWorld : TEXCOORD1;
                float3 normalDir : TEXCOORD2;
                LIGHTING_COORDS(3,4)
                UNITY_FOG_COORDS(5)
                #ifndef LIGHTMAP_OFF
                    float4 uvLM : TEXCOORD6;
                #else
                    float3 shLight : TEXCOORD6;
                #endif
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.normalDir = mul(_Object2World, float4(v.normal,0)).xyz;
                o.posWorld = mul(_Object2World, v.vertex);
                float3 lightColor = _LightColor0.rgb;
                o.pos = mul(UNITY_MATRIX_MVP, v.vertex);
                UNITY_TRANSFER_FOG(o,o.pos);
                TRANSFER_VERTEX_TO_FRAGMENT(o)
                return o;
            }
            fixed4 frag(VertexOutput i) : COLOR {
                i.normalDir = normalize(i.normalDir);
/////// Vectors:
                float3 normalDirection = i.normalDir;
                float3 lightDirection = normalize(_WorldSpaceLightPos0.xyz);
                float3 lightColor = _LightColor0.rgb;
////// Lighting:
                float attenuation = LIGHT_ATTENUATION(i);
                float3 attenColor = attenuation * _LightColor0.xyz;
/////// Diffuse:
                float NdotL = max(0.0,dot( normalDirection, lightDirection ));
                float3 indirectDiffuse = float3(0,0,0);
                float3 directDiffuse = max( 0.0, NdotL) * attenColor;
                indirectDiffuse += UNITY_LIGHTMODEL_AMBIENT.rgb; // Ambient Light
                float4 _DamageMainTex_var = tex2D(_DamageMainTex,TRANSFORM_TEX(i.uv0, _DamageMainTex));
                float node_583_if_leA = step(_DamageLevel,0.8);
                float node_583_if_leB = step(0.8,_DamageLevel);
                float node_126_if_leA = step(_DamageLevel,0.6);
                float node_126_if_leB = step(0.6,_DamageLevel);
                float node_90_if_leA = step(_DamageLevel,0.4);
                float node_90_if_leB = step(0.4,_DamageLevel);
                float node_61_if_leA = step(_DamageLevel,0.2);
                float node_61_if_leB = step(0.2,_DamageLevel);
                float4 node_6686 = tex2D(_DamageMask,TRANSFORM_TEX(i.uv0, _DamageMask));
                float3 node_815 = float3(0,0,0);
                float3 node_692 = lerp(_DamageMainTex_var.rgb,float3(1,1,1),lerp((node_583_if_leA*lerp((node_126_if_leA*lerp((node_90_if_leA*lerp((node_61_if_leA*1.0)+(node_61_if_leB*node_6686.b),node_6686.b,node_61_if_leA*node_61_if_leB))+(node_90_if_leB*node_6686.g),node_6686.g,node_90_if_leA*node_90_if_leB))+(node_126_if_leB*node_6686.r),node_6686.r,node_126_if_leA*node_126_if_leB))+(node_583_if_leB*node_815),node_815,node_583_if_leA*node_583_if_leB));
                indirectDiffuse += lerp((1.0 - node_692),node_815,(_LightColor0.rgb*attenuation*(node_692-UNITY_LIGHTMODEL_AMBIENT.rgb))); // Diffuse Ambient Light
                float4 _MainTex_var = tex2D(_MainTex,TRANSFORM_TEX(i.uv0, _MainTex));
                float3 diffuse = (directDiffuse + indirectDiffuse) * ((_MainTex_var.rgb*_Color.rgb)*node_692);
/// Final Color:
                float3 finalColor = diffuse;
                fixed4 finalRGBA = fixed4(finalColor,1);
                UNITY_APPLY_FOG(i.fogCoord, finalRGBA);
                return finalRGBA;
            }
            ENDCG
        }
        Pass {
            Name "ForwardAdd"
            Tags {
                "LightMode"="ForwardAdd"
            }
            Blend One One
            
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDADD
            #include "UnityCG.cginc"
            #include "AutoLight.cginc"
            #pragma multi_compile_fwdadd_fullshadows
            #pragma multi_compile_fog
            #pragma exclude_renderers xbox360 ps3 flash 
            #pragma target 3.0
            uniform float4 _LightColor0;
            uniform sampler2D _MainTex; uniform float4 _MainTex_ST;
            uniform sampler2D _DamageMask; uniform float4 _DamageMask_ST;
            uniform float4 _Color;
            uniform sampler2D _DamageMainTex; uniform float4 _DamageMainTex_ST;
            uniform float _DamageLevel;
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float2 texcoord0 : TEXCOORD0;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                float4 posWorld : TEXCOORD1;
                float3 normalDir : TEXCOORD2;
                LIGHTING_COORDS(3,4)
                #ifndef LIGHTMAP_OFF
                    float4 uvLM : TEXCOORD5;
                #else
                    float3 shLight : TEXCOORD5;
                #endif
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.normalDir = mul(_Object2World, float4(v.normal,0)).xyz;
                o.posWorld = mul(_Object2World, v.vertex);
                float3 lightColor = _LightColor0.rgb;
                o.pos = mul(UNITY_MATRIX_MVP, v.vertex);
                TRANSFER_VERTEX_TO_FRAGMENT(o)
                return o;
            }
            fixed4 frag(VertexOutput i) : COLOR {
                i.normalDir = normalize(i.normalDir);
/////// Vectors:
                float3 normalDirection = i.normalDir;
                float3 lightDirection = normalize(lerp(_WorldSpaceLightPos0.xyz, _WorldSpaceLightPos0.xyz - i.posWorld.xyz,_WorldSpaceLightPos0.w));
                float3 lightColor = _LightColor0.rgb;
////// Lighting:
                float attenuation = LIGHT_ATTENUATION(i);
                float3 attenColor = attenuation * _LightColor0.xyz;
/////// Diffuse:
                float NdotL = max(0.0,dot( normalDirection, lightDirection ));
                float3 directDiffuse = max( 0.0, NdotL) * attenColor;
                float4 _MainTex_var = tex2D(_MainTex,TRANSFORM_TEX(i.uv0, _MainTex));
                float4 _DamageMainTex_var = tex2D(_DamageMainTex,TRANSFORM_TEX(i.uv0, _DamageMainTex));
                float node_583_if_leA = step(_DamageLevel,0.8);
                float node_583_if_leB = step(0.8,_DamageLevel);
                float node_126_if_leA = step(_DamageLevel,0.6);
                float node_126_if_leB = step(0.6,_DamageLevel);
                float node_90_if_leA = step(_DamageLevel,0.4);
                float node_90_if_leB = step(0.4,_DamageLevel);
                float node_61_if_leA = step(_DamageLevel,0.2);
                float node_61_if_leB = step(0.2,_DamageLevel);
                float4 node_6686 = tex2D(_DamageMask,TRANSFORM_TEX(i.uv0, _DamageMask));
                float3 node_815 = float3(0,0,0);
                float3 node_692 = lerp(_DamageMainTex_var.rgb,float3(1,1,1),lerp((node_583_if_leA*lerp((node_126_if_leA*lerp((node_90_if_leA*lerp((node_61_if_leA*1.0)+(node_61_if_leB*node_6686.b),node_6686.b,node_61_if_leA*node_61_if_leB))+(node_90_if_leB*node_6686.g),node_6686.g,node_90_if_leA*node_90_if_leB))+(node_126_if_leB*node_6686.r),node_6686.r,node_126_if_leA*node_126_if_leB))+(node_583_if_leB*node_815),node_815,node_583_if_leA*node_583_if_leB));
                float3 diffuse = directDiffuse * ((_MainTex_var.rgb*_Color.rgb)*node_692);
/// Final Color:
                float3 finalColor = diffuse;
                return fixed4(finalColor * 1,0);
            }
            ENDCG
        }
    }
    FallBack "VertexLit"
    CustomEditor "ShaderForgeMaterialInspector"
}
