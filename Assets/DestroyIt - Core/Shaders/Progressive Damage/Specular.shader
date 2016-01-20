// Shader created with Shader Forge v1.06 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.06;sub:START;pass:START;ps:flbk:Specular,lico:1,lgpr:1,nrmq:1,limd:1,uamb:True,mssp:False,lmpd:False,lprd:False,rprd:False,enco:False,frtr:True,vitr:True,dbil:False,rmgx:False,rpth:0,hqsc:True,hqlp:False,tesm:0,blpr:0,bsrc:0,bdst:1,culm:0,dpts:2,wrdp:True,dith:0,ufog:True,aust:True,igpj:False,qofs:0,qpre:1,rntp:1,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,ofsf:0,ofsu:0,f2p0:False;n:type:ShaderForge.SFN_Final,id:1,x:33819,y:32621,varname:node_1,prsc:2|diff-6029-OUT,spec-7042-OUT,gloss-6892-OUT,amdfl-6942-OUT;n:type:ShaderForge.SFN_Tex2d,id:2,x:32304,y:31991,ptovrint:False,ptlb:MainTex,ptin:_MainTex,varname:node_287,prsc:2,ntxv:0,isnm:False;n:type:ShaderForge.SFN_If,id:61,x:32859,y:32494,varname:node_61,prsc:2|A-881-OUT,B-72-OUT,GT-270-B,EQ-270-B,LT-137-OUT;n:type:ShaderForge.SFN_Vector1,id:72,x:32859,y:32442,varname:node_72,prsc:2,v1:0.2;n:type:ShaderForge.SFN_If,id:90,x:32863,y:32684,varname:node_90,prsc:2|A-881-OUT,B-93-OUT,GT-270-G,EQ-270-G,LT-61-OUT;n:type:ShaderForge.SFN_Vector1,id:93,x:32845,y:32628,varname:node_93,prsc:2,v1:0.4;n:type:ShaderForge.SFN_If,id:126,x:32863,y:32879,varname:node_126,prsc:2|A-881-OUT,B-128-OUT,GT-270-R,EQ-270-R,LT-90-OUT;n:type:ShaderForge.SFN_Vector1,id:128,x:32863,y:32814,varname:node_128,prsc:2,v1:0.6;n:type:ShaderForge.SFN_Vector1,id:137,x:32859,y:32379,varname:node_137,prsc:2,v1:1;n:type:ShaderForge.SFN_Tex2dAsset,id:206,x:32320,y:33129,ptovrint:False,ptlb:Damage Mask,ptin:_DamageMask,varname:node_2384,tex:c4e987ef2d922ca47a8ace9435e0a07f,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Tex2d,id:270,x:32536,y:33173,varname:node_923,prsc:2,tex:c4e987ef2d922ca47a8ace9435e0a07f,ntxv:0,isnm:False|TEX-206-TEX;n:type:ShaderForge.SFN_Color,id:449,x:32315,y:32213,ptovrint:False,ptlb:Color,ptin:_Color,varname:node_4906,prsc:2,glob:False,c1:1,c2:1,c3:1,c4:1;n:type:ShaderForge.SFN_Multiply,id:450,x:32702,y:32277,varname:node_450,prsc:2|A-2-RGB,B-449-RGB;n:type:ShaderForge.SFN_Tex2d,id:569,x:32326,y:32770,ptovrint:False,ptlb:Damage MainTex,ptin:_DamageMainTex,varname:node_3238,prsc:2,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Vector1,id:581,x:32863,y:33012,varname:node_581,prsc:2,v1:0.8;n:type:ShaderForge.SFN_If,id:583,x:32863,y:33072,varname:node_583,prsc:2|A-881-OUT,B-581-OUT,GT-815-OUT,EQ-815-OUT,LT-126-OUT;n:type:ShaderForge.SFN_Lerp,id:692,x:33121,y:32732,varname:node_692,prsc:2|A-569-RGB,B-769-OUT,T-583-OUT;n:type:ShaderForge.SFN_Vector3,id:769,x:31978,y:32719,varname:node_769,prsc:2,v1:1,v2:1,v3:1;n:type:ShaderForge.SFN_Vector3,id:815,x:32000,y:33008,varname:node_815,prsc:2,v1:0,v2:0,v3:0;n:type:ShaderForge.SFN_Slider,id:881,x:32321,y:33332,ptovrint:False,ptlb:Damage Level,ptin:_DamageLevel,varname:node_4456,prsc:2,min:0,cur:0.5563341,max:1;n:type:ShaderForge.SFN_Multiply,id:6029,x:33614,y:32430,varname:node_6029,prsc:2|A-450-OUT,B-692-OUT;n:type:ShaderForge.SFN_Slider,id:6270,x:32281,y:32644,ptovrint:False,ptlb:Shininess,ptin:_Shininess,varname:node_1212,prsc:2,min:0.031,cur:0.078125,max:1;n:type:ShaderForge.SFN_Color,id:6271,x:32340,y:32458,ptovrint:False,ptlb:SpecColor,ptin:_SpecColor,varname:node_1750,prsc:2,glob:False,c1:0.5,c2:0.5,c3:0.5,c4:1;n:type:ShaderForge.SFN_Multiply,id:6892,x:33480,y:32684,varname:node_6892,prsc:2|A-6270-OUT,B-6928-OUT;n:type:ShaderForge.SFN_Vector1,id:6928,x:32561,y:32579,varname:node_6928,prsc:2,v1:128;n:type:ShaderForge.SFN_AmbientLight,id:6937,x:32877,y:31770,varname:node_6937,prsc:2;n:type:ShaderForge.SFN_LightAttenuation,id:6938,x:32877,y:31648,varname:node_6938,prsc:2;n:type:ShaderForge.SFN_LightColor,id:6939,x:32877,y:31525,varname:node_6939,prsc:2;n:type:ShaderForge.SFN_Subtract,id:6940,x:33117,y:31833,varname:node_6940,prsc:2|A-692-OUT,B-6937-RGB;n:type:ShaderForge.SFN_Multiply,id:6941,x:33317,y:31713,varname:node_6941,prsc:2|A-6939-RGB,B-6938-OUT,C-6940-OUT;n:type:ShaderForge.SFN_Lerp,id:6942,x:33600,y:32828,varname:node_6942,prsc:2|A-6943-OUT,B-815-OUT,T-6975-OUT;n:type:ShaderForge.SFN_OneMinus,id:6943,x:33348,y:32779,varname:node_6943,prsc:2|IN-692-OUT;n:type:ShaderForge.SFN_Relay,id:6975,x:33492,y:32141,varname:node_6975,prsc:2|IN-6941-OUT;n:type:ShaderForge.SFN_Lerp,id:7041,x:33269,y:32524,varname:node_7041,prsc:2|A-450-OUT,B-769-OUT,T-692-OUT;n:type:ShaderForge.SFN_Multiply,id:7042,x:33492,y:32545,varname:node_7042,prsc:2|A-7041-OUT,B-6271-RGB,C-6892-OUT;proporder:449-6271-6270-2-569-206-881;pass:END;sub:END;*/

Shader "DestroyIt/Specular" {
    Properties {
        _Color ("Color", Color) = (1,1,1,1)
        _SpecColor ("SpecColor", Color) = (0.5,0.5,0.5,1)
        _Shininess ("Shininess", Range(0.031, 1)) = 0.078125
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
            uniform float _Shininess;
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
                float3 viewDirection = normalize(_WorldSpaceCameraPos.xyz - i.posWorld.xyz);
                float3 normalDirection = i.normalDir;
                float3 lightDirection = normalize(_WorldSpaceLightPos0.xyz);
                float3 lightColor = _LightColor0.rgb;
                float3 halfDirection = normalize(viewDirection+lightDirection);
////// Lighting:
                float attenuation = LIGHT_ATTENUATION(i);
                float3 attenColor = attenuation * _LightColor0.xyz;
///////// Gloss:
                float node_6892 = (_Shininess*128.0);
                float gloss = node_6892;
                float specPow = gloss;
////// Specular:
                float NdotL = max(0, dot( normalDirection, lightDirection ));
                float4 _MainTex_var = tex2D(_MainTex,TRANSFORM_TEX(i.uv0, _MainTex));
                float3 node_450 = (_MainTex_var.rgb*_Color.rgb);
                float3 node_769 = float3(1,1,1);
                float4 _DamageMainTex_var = tex2D(_DamageMainTex,TRANSFORM_TEX(i.uv0, _DamageMainTex));
                float node_583_if_leA = step(_DamageLevel,0.8);
                float node_583_if_leB = step(0.8,_DamageLevel);
                float node_126_if_leA = step(_DamageLevel,0.6);
                float node_126_if_leB = step(0.6,_DamageLevel);
                float node_90_if_leA = step(_DamageLevel,0.4);
                float node_90_if_leB = step(0.4,_DamageLevel);
                float node_61_if_leA = step(_DamageLevel,0.2);
                float node_61_if_leB = step(0.2,_DamageLevel);
                float4 node_923 = tex2D(_DamageMask,TRANSFORM_TEX(i.uv0, _DamageMask));
                float3 node_815 = float3(0,0,0);
                float3 node_692 = lerp(_DamageMainTex_var.rgb,node_769,lerp((node_583_if_leA*lerp((node_126_if_leA*lerp((node_90_if_leA*lerp((node_61_if_leA*1.0)+(node_61_if_leB*node_923.b),node_923.b,node_61_if_leA*node_61_if_leB))+(node_90_if_leB*node_923.g),node_923.g,node_90_if_leA*node_90_if_leB))+(node_126_if_leB*node_923.r),node_923.r,node_126_if_leA*node_126_if_leB))+(node_583_if_leB*node_815),node_815,node_583_if_leA*node_583_if_leB));
                float3 specularColor = (lerp(node_450,node_769,node_692)*_SpecColor.rgb*node_6892);
                float3 directSpecular = attenColor * pow(max(0,dot(halfDirection,normalDirection)),specPow);
                float3 specular = directSpecular * specularColor;
/////// Diffuse:
                NdotL = max(0.0,dot( normalDirection, lightDirection ));
                float3 indirectDiffuse = float3(0,0,0);
                float3 directDiffuse = max( 0.0, NdotL) * attenColor;
                indirectDiffuse += UNITY_LIGHTMODEL_AMBIENT.rgb; // Ambient Light
                indirectDiffuse += lerp((1.0 - node_692),node_815,(_LightColor0.rgb*attenuation*(node_692-UNITY_LIGHTMODEL_AMBIENT.rgb))); // Diffuse Ambient Light
                float3 diffuse = (directDiffuse + indirectDiffuse) * (node_450*node_692);
/// Final Color:
                float3 finalColor = diffuse + specular;
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
            uniform float _Shininess;
            uniform float4 _SpecColor;
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
                float3 viewDirection = normalize(_WorldSpaceCameraPos.xyz - i.posWorld.xyz);
                float3 normalDirection = i.normalDir;
                float3 lightDirection = normalize(lerp(_WorldSpaceLightPos0.xyz, _WorldSpaceLightPos0.xyz - i.posWorld.xyz,_WorldSpaceLightPos0.w));
                float3 lightColor = _LightColor0.rgb;
                float3 halfDirection = normalize(viewDirection+lightDirection);
////// Lighting:
                float attenuation = LIGHT_ATTENUATION(i);
                float3 attenColor = attenuation * _LightColor0.xyz;
///////// Gloss:
                float node_6892 = (_Shininess*128.0);
                float gloss = node_6892;
                float specPow = gloss;
////// Specular:
                float NdotL = max(0, dot( normalDirection, lightDirection ));
                float4 _MainTex_var = tex2D(_MainTex,TRANSFORM_TEX(i.uv0, _MainTex));
                float3 node_450 = (_MainTex_var.rgb*_Color.rgb);
                float3 node_769 = float3(1,1,1);
                float4 _DamageMainTex_var = tex2D(_DamageMainTex,TRANSFORM_TEX(i.uv0, _DamageMainTex));
                float node_583_if_leA = step(_DamageLevel,0.8);
                float node_583_if_leB = step(0.8,_DamageLevel);
                float node_126_if_leA = step(_DamageLevel,0.6);
                float node_126_if_leB = step(0.6,_DamageLevel);
                float node_90_if_leA = step(_DamageLevel,0.4);
                float node_90_if_leB = step(0.4,_DamageLevel);
                float node_61_if_leA = step(_DamageLevel,0.2);
                float node_61_if_leB = step(0.2,_DamageLevel);
                float4 node_923 = tex2D(_DamageMask,TRANSFORM_TEX(i.uv0, _DamageMask));
                float3 node_815 = float3(0,0,0);
                float3 node_692 = lerp(_DamageMainTex_var.rgb,node_769,lerp((node_583_if_leA*lerp((node_126_if_leA*lerp((node_90_if_leA*lerp((node_61_if_leA*1.0)+(node_61_if_leB*node_923.b),node_923.b,node_61_if_leA*node_61_if_leB))+(node_90_if_leB*node_923.g),node_923.g,node_90_if_leA*node_90_if_leB))+(node_126_if_leB*node_923.r),node_923.r,node_126_if_leA*node_126_if_leB))+(node_583_if_leB*node_815),node_815,node_583_if_leA*node_583_if_leB));
                float3 specularColor = (lerp(node_450,node_769,node_692)*_SpecColor.rgb*node_6892);
                float3 directSpecular = attenColor * pow(max(0,dot(halfDirection,normalDirection)),specPow);
                float3 specular = directSpecular * specularColor;
/////// Diffuse:
                NdotL = max(0.0,dot( normalDirection, lightDirection ));
                float3 directDiffuse = max( 0.0, NdotL) * attenColor;
                float3 diffuse = directDiffuse * (node_450*node_692);
/// Final Color:
                float3 finalColor = diffuse + specular;
                return fixed4(finalColor * 1,0);
            }
            ENDCG
        }
    }
    FallBack "Specular"
    CustomEditor "ShaderForgeMaterialInspector"
}
