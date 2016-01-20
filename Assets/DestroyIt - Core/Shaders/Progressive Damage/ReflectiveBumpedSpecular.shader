// Shader created with Shader Forge v1.06 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.06;sub:START;pass:START;ps:flbk:Reflective/Bumped Specular,lico:1,lgpr:1,nrmq:1,limd:1,uamb:True,mssp:True,lmpd:False,lprd:False,rprd:False,enco:False,frtr:True,vitr:True,dbil:False,rmgx:False,rpth:0,hqsc:True,hqlp:False,tesm:0,blpr:0,bsrc:0,bdst:1,culm:2,dpts:2,wrdp:True,dith:0,ufog:False,aust:True,igpj:False,qofs:0,qpre:1,rntp:1,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,ofsf:0,ofsu:0,f2p0:False;n:type:ShaderForge.SFN_Final,id:0,x:35416,y:32442,varname:node_0,prsc:2|diff-7838-OUT,spec-4358-RGB,gloss-7839-OUT,normal-4383-OUT,amspl-9773-OUT;n:type:ShaderForge.SFN_Tex2d,id:663,x:34254,y:32697,ptovrint:False,ptlb:BumpMap,ptin:_BumpMap,varname:node_2162,prsc:2,tex:c6dfb00dbee6bc044a8a3bb22e56e064,ntxv:3,isnm:True;n:type:ShaderForge.SFN_Cubemap,id:1189,x:34234,y:33107,ptovrint:False,ptlb:Cube,ptin:_Cube,varname:node_7862,prsc:2,cube:2f821dbbb5e173e468876ef2e4eaa490,pvfc:0|DIR-9485-OUT;n:type:ShaderForge.SFN_Color,id:1278,x:34259,y:32144,ptovrint:False,ptlb:Color,ptin:_Color,varname:node_9178,prsc:2,glob:False,c1:1,c2:1,c3:1,c4:0.5;n:type:ShaderForge.SFN_Color,id:4358,x:34254,y:32315,ptovrint:False,ptlb:SpecColor,ptin:_SpecColor,varname:node_5000,prsc:2,glob:False,c1:0.5,c2:0.5,c3:0.5,c4:1;n:type:ShaderForge.SFN_If,id:4369,x:34703,y:32443,varname:node_4369,prsc:2|A-4378-OUT,B-4373-OUT,GT-4380-B,EQ-4380-B,LT-4377-OUT;n:type:ShaderForge.SFN_If,id:4370,x:34693,y:33006,varname:node_4370,prsc:2|A-4378-OUT,B-4376-OUT,GT-4382-OUT,EQ-4382-OUT,LT-4372-OUT;n:type:ShaderForge.SFN_If,id:4371,x:34703,y:32625,varname:node_4371,prsc:2|A-4378-OUT,B-4374-OUT,GT-4380-G,EQ-4380-G,LT-4369-OUT;n:type:ShaderForge.SFN_If,id:4372,x:34703,y:32807,varname:node_4372,prsc:2|A-4378-OUT,B-4375-OUT,GT-4380-R,EQ-4380-R,LT-4371-OUT;n:type:ShaderForge.SFN_Vector1,id:4373,x:34703,y:32379,varname:node_4373,prsc:2,v1:0.2;n:type:ShaderForge.SFN_Vector1,id:4374,x:34703,y:32571,varname:node_4374,prsc:2,v1:0.4;n:type:ShaderForge.SFN_Vector1,id:4375,x:34703,y:32748,varname:node_4375,prsc:2,v1:0.6;n:type:ShaderForge.SFN_Vector1,id:4376,x:34693,y:32939,varname:node_4376,prsc:2,v1:0.8;n:type:ShaderForge.SFN_Vector1,id:4377,x:34703,y:32317,varname:node_4377,prsc:2,v1:1;n:type:ShaderForge.SFN_Slider,id:4378,x:34222,y:33549,ptovrint:False,ptlb:Damage Level,ptin:_DamageLevel,varname:node_249,prsc:2,min:0,cur:0.6086708,max:1;n:type:ShaderForge.SFN_Tex2dAsset,id:4379,x:34234,y:33360,ptovrint:False,ptlb:Damage Mask,ptin:_DamageMask,varname:node_7988,tex:c4e987ef2d922ca47a8ace9435e0a07f,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Tex2d,id:4380,x:34409,y:33271,varname:node_3034,prsc:2,tex:c4e987ef2d922ca47a8ace9435e0a07f,ntxv:0,isnm:False|TEX-4379-TEX;n:type:ShaderForge.SFN_Vector3,id:4382,x:34476,y:33035,varname:node_4382,prsc:2,v1:0,v2:0,v3:0;n:type:ShaderForge.SFN_Lerp,id:4383,x:35238,y:32541,varname:node_4383,prsc:2|A-7917-OUT,B-663-RGB,T-4370-OUT;n:type:ShaderForge.SFN_Tex2d,id:4385,x:34244,y:32904,ptovrint:False,ptlb:Damage BumpMap,ptin:_DamageBumpMap,varname:node_5150,prsc:2,ntxv:3,isnm:True;n:type:ShaderForge.SFN_Slider,id:4482,x:34130,y:32487,ptovrint:False,ptlb:Shininess,ptin:_Shininess,varname:node_6945,prsc:2,min:0.01,cur:0.4499782,max:1;n:type:ShaderForge.SFN_Multiply,id:7838,x:35118,y:32273,varname:node_7838,prsc:2|A-9504-RGB,B-1278-RGB,C-9526-OUT;n:type:ShaderForge.SFN_Multiply,id:7839,x:34492,y:32500,varname:node_7839,prsc:2|A-7840-OUT,B-4482-OUT;n:type:ShaderForge.SFN_Vector1,id:7840,x:34413,y:32434,varname:node_7840,prsc:2,v1:128;n:type:ShaderForge.SFN_NormalBlend,id:7917,x:35079,y:32754,varname:node_7917,prsc:2|BSE-663-RGB,DTL-4385-RGB;n:type:ShaderForge.SFN_ViewVector,id:8495,x:33371,y:32803,varname:node_8495,prsc:2;n:type:ShaderForge.SFN_Normalize,id:8592,x:33739,y:32820,varname:node_8592,prsc:2|IN-9548-OUT;n:type:ShaderForge.SFN_NormalVector,id:8639,x:33488,y:33001,prsc:2,pt:False;n:type:ShaderForge.SFN_Normalize,id:8640,x:33691,y:33001,varname:node_8640,prsc:2|IN-8639-OUT;n:type:ShaderForge.SFN_Reflect,id:9485,x:33937,y:33043,varname:node_9485,prsc:2|A-8592-OUT,B-8640-OUT;n:type:ShaderForge.SFN_Tex2d,id:9504,x:34270,y:31966,ptovrint:False,ptlb:MainTex,ptin:_MainTex,varname:node_8104,prsc:2,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Tex2d,id:9525,x:34287,y:31778,ptovrint:False,ptlb:Damage Main Tex,ptin:_DamageMainTex,varname:node_9199,prsc:2,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Lerp,id:9526,x:34961,y:32543,varname:node_9526,prsc:2|A-9525-RGB,B-4377-OUT,T-4370-OUT;n:type:ShaderForge.SFN_OneMinus,id:9548,x:33585,y:32681,varname:node_9548,prsc:2|IN-8495-OUT;n:type:ShaderForge.SFN_Color,id:9772,x:33570,y:32490,ptovrint:False,ptlb:Reflection Color,ptin:_ReflectionColor,varname:node_5050,prsc:2,glob:False,c1:0.2509804,c2:0.2509804,c3:0.2509804,c4:0.5019608;n:type:ShaderForge.SFN_Multiply,id:9773,x:34935,y:33095,varname:node_9773,prsc:2|A-9772-RGB,B-1189-RGB;proporder:1278-4358-4482-9772-9504-663-9525-4385-1189-4379-4378;pass:END;sub:END;*/

Shader "DestroyIt/ReflectiveBumpedSpecular" {
    Properties {
        _Color ("Color", Color) = (1,1,1,0.5)
        _SpecColor ("SpecColor", Color) = (0.5,0.5,0.5,1)
        _Shininess ("Shininess", Range(0.01, 1)) = 0.4499782
        _ReflectionColor ("Reflection Color", Color) = (0.2509804,0.2509804,0.2509804,0.5019608)
        _MainTex ("MainTex", 2D) = "white" {}
        _BumpMap ("BumpMap", 2D) = "bump" {}
        _DamageMainTex ("Damage Main Tex", 2D) = "white" {}
        _DamageBumpMap ("Damage BumpMap", 2D) = "bump" {}
        _Cube ("Cube", Cube) = "_Skybox" {}
        _DamageMask ("Damage Mask", 2D) = "white" {}
        _DamageLevel ("Damage Level", Range(0, 1)) = 0.6086708
    }
    SubShader {
        Tags {
            "RenderType"="Opaque"
        }
        LOD 200
        Pass {
            Name "ForwardBase"
            Tags {
                "LightMode"="ForwardBase"
            }
            Cull Off
            
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDBASE
            #include "UnityCG.cginc"
            #include "AutoLight.cginc"
            #pragma multi_compile_fwdbase_fullshadows
            #pragma exclude_renderers gles xbox360 ps3 flash 
            #pragma target 3.0
            uniform float4 _LightColor0;
            uniform sampler2D _BumpMap; uniform float4 _BumpMap_ST;
            uniform samplerCUBE _Cube;
            uniform float4 _Color;
            uniform float4 _SpecColor;
            uniform float _DamageLevel;
            uniform sampler2D _DamageMask; uniform float4 _DamageMask_ST;
            uniform sampler2D _DamageBumpMap; uniform float4 _DamageBumpMap_ST;
            uniform float _Shininess;
            uniform sampler2D _MainTex; uniform float4 _MainTex_ST;
            uniform sampler2D _DamageMainTex; uniform float4 _DamageMainTex_ST;
            uniform float4 _ReflectionColor;
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float4 tangent : TANGENT;
                float2 texcoord0 : TEXCOORD0;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                float4 posWorld : TEXCOORD1;
                float3 normalDir : TEXCOORD2;
                float3 tangentDir : TEXCOORD3;
                float3 binormalDir : TEXCOORD4;
                LIGHTING_COORDS(5,6)
                #ifndef LIGHTMAP_OFF
                    float4 uvLM : TEXCOORD7;
                #else
                    float3 shLight : TEXCOORD7;
                #endif
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.normalDir = mul(_Object2World, float4(v.normal,0)).xyz;
                o.tangentDir = normalize( mul( _Object2World, float4( v.tangent.xyz, 0.0 ) ).xyz );
                o.binormalDir = normalize(cross(o.normalDir, o.tangentDir) * v.tangent.w);
                o.posWorld = mul(_Object2World, v.vertex);
                float3 lightColor = _LightColor0.rgb;
                o.pos = mul(UNITY_MATRIX_MVP, v.vertex);
                TRANSFER_VERTEX_TO_FRAGMENT(o)
                return o;
            }
            fixed4 frag(VertexOutput i) : COLOR {
                i.normalDir = normalize(i.normalDir);
                float3x3 tangentTransform = float3x3( i.tangentDir, i.binormalDir, i.normalDir);
/////// Vectors:
                float3 viewDirection = normalize(_WorldSpaceCameraPos.xyz - i.posWorld.xyz);
                float3 _BumpMap_var = UnpackNormal(tex2D(_BumpMap,TRANSFORM_TEX(i.uv0, _BumpMap)));
                float3 _DamageBumpMap_var = UnpackNormal(tex2D(_DamageBumpMap,TRANSFORM_TEX(i.uv0, _DamageBumpMap)));
                float3 node_7917_nrm_base = _BumpMap_var.rgb + float3(0,0,1);
                float3 node_7917_nrm_detail = _DamageBumpMap_var.rgb * float3(-1,-1,1);
                float3 node_7917_nrm_combined = node_7917_nrm_base*dot(node_7917_nrm_base, node_7917_nrm_detail)/node_7917_nrm_base.z - node_7917_nrm_detail;
                float3 node_7917 = node_7917_nrm_combined;
                float node_4370_if_leA = step(_DamageLevel,0.8);
                float node_4370_if_leB = step(0.8,_DamageLevel);
                float node_4372_if_leA = step(_DamageLevel,0.6);
                float node_4372_if_leB = step(0.6,_DamageLevel);
                float node_4371_if_leA = step(_DamageLevel,0.4);
                float node_4371_if_leB = step(0.4,_DamageLevel);
                float node_4369_if_leA = step(_DamageLevel,0.2);
                float node_4369_if_leB = step(0.2,_DamageLevel);
                float node_4377 = 1.0;
                float4 node_3034 = tex2D(_DamageMask,TRANSFORM_TEX(i.uv0, _DamageMask));
                float3 node_4382 = float3(0,0,0);
                float3 node_4370 = lerp((node_4370_if_leA*lerp((node_4372_if_leA*lerp((node_4371_if_leA*lerp((node_4369_if_leA*node_4377)+(node_4369_if_leB*node_3034.b),node_3034.b,node_4369_if_leA*node_4369_if_leB))+(node_4371_if_leB*node_3034.g),node_3034.g,node_4371_if_leA*node_4371_if_leB))+(node_4372_if_leB*node_3034.r),node_3034.r,node_4372_if_leA*node_4372_if_leB))+(node_4370_if_leB*node_4382),node_4382,node_4370_if_leA*node_4370_if_leB);
                float3 normalLocal = lerp(node_7917,_BumpMap_var.rgb,node_4370);
                float3 normalDirection = normalize(mul( normalLocal, tangentTransform )); // Perturbed normals
                
                float nSign = sign( dot( viewDirection, i.normalDir ) ); // Reverse normal if this is a backface
                i.normalDir *= nSign;
                normalDirection *= nSign;
                
                float3 lightDirection = normalize(_WorldSpaceLightPos0.xyz);
                float3 lightColor = _LightColor0.rgb;
                float3 halfDirection = normalize(viewDirection+lightDirection);
////// Lighting:
                float attenuation = LIGHT_ATTENUATION(i);
                float3 attenColor = attenuation * _LightColor0.xyz;
///////// Gloss:
                float gloss = (128.0*_Shininess);
                float specPow = gloss;
////// Specular:
                float NdotL = max(0, dot( normalDirection, lightDirection ));
                float3 specularColor = _SpecColor.rgb;
                float3 directSpecular = (floor(attenuation) * _LightColor0.xyz) * pow(max(0,dot(halfDirection,normalDirection)),specPow);
                float3 indirectSpecular = (0 + (_ReflectionColor.rgb*texCUBE(_Cube,reflect(normalize((1.0 - viewDirection)),normalize(i.normalDir))).rgb));
                float3 specular = (directSpecular + indirectSpecular) * specularColor;
/////// Diffuse:
                NdotL = max(0.0,dot( normalDirection, lightDirection ));
                float3 indirectDiffuse = float3(0,0,0);
                float3 directDiffuse = max( 0.0, NdotL) * attenColor;
                indirectDiffuse += UNITY_LIGHTMODEL_AMBIENT.rgb; // Ambient Light
                float4 _MainTex_var = tex2D(_MainTex,TRANSFORM_TEX(i.uv0, _MainTex));
                float4 _DamageMainTex_var = tex2D(_DamageMainTex,TRANSFORM_TEX(i.uv0, _DamageMainTex));
                float3 diffuse = (directDiffuse + indirectDiffuse) * (_MainTex_var.rgb*_Color.rgb*lerp(_DamageMainTex_var.rgb,float3(node_4377,node_4377,node_4377),node_4370));
/// Final Color:
                float3 finalColor = diffuse + specular;
                return fixed4(finalColor,1);
            }
            ENDCG
        }
        Pass {
            Name "ForwardAdd"
            Tags {
                "LightMode"="ForwardAdd"
            }
            Blend One One
            Cull Off
            
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDADD
            #include "UnityCG.cginc"
            #include "AutoLight.cginc"
            #pragma multi_compile_fwdadd_fullshadows
            #pragma exclude_renderers gles xbox360 ps3 flash 
            #pragma target 3.0
            uniform float4 _LightColor0;
            uniform sampler2D _BumpMap; uniform float4 _BumpMap_ST;
            uniform float4 _Color;
            uniform float4 _SpecColor;
            uniform float _DamageLevel;
            uniform sampler2D _DamageMask; uniform float4 _DamageMask_ST;
            uniform sampler2D _DamageBumpMap; uniform float4 _DamageBumpMap_ST;
            uniform float _Shininess;
            uniform sampler2D _MainTex; uniform float4 _MainTex_ST;
            uniform sampler2D _DamageMainTex; uniform float4 _DamageMainTex_ST;
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float4 tangent : TANGENT;
                float2 texcoord0 : TEXCOORD0;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                float4 posWorld : TEXCOORD1;
                float3 normalDir : TEXCOORD2;
                float3 tangentDir : TEXCOORD3;
                float3 binormalDir : TEXCOORD4;
                LIGHTING_COORDS(5,6)
                #ifndef LIGHTMAP_OFF
                    float4 uvLM : TEXCOORD7;
                #else
                    float3 shLight : TEXCOORD7;
                #endif
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.normalDir = mul(_Object2World, float4(v.normal,0)).xyz;
                o.tangentDir = normalize( mul( _Object2World, float4( v.tangent.xyz, 0.0 ) ).xyz );
                o.binormalDir = normalize(cross(o.normalDir, o.tangentDir) * v.tangent.w);
                o.posWorld = mul(_Object2World, v.vertex);
                float3 lightColor = _LightColor0.rgb;
                o.pos = mul(UNITY_MATRIX_MVP, v.vertex);
                TRANSFER_VERTEX_TO_FRAGMENT(o)
                return o;
            }
            fixed4 frag(VertexOutput i) : COLOR {
                i.normalDir = normalize(i.normalDir);
                float3x3 tangentTransform = float3x3( i.tangentDir, i.binormalDir, i.normalDir);
/////// Vectors:
                float3 viewDirection = normalize(_WorldSpaceCameraPos.xyz - i.posWorld.xyz);
                float3 _BumpMap_var = UnpackNormal(tex2D(_BumpMap,TRANSFORM_TEX(i.uv0, _BumpMap)));
                float3 _DamageBumpMap_var = UnpackNormal(tex2D(_DamageBumpMap,TRANSFORM_TEX(i.uv0, _DamageBumpMap)));
                float3 node_7917_nrm_base = _BumpMap_var.rgb + float3(0,0,1);
                float3 node_7917_nrm_detail = _DamageBumpMap_var.rgb * float3(-1,-1,1);
                float3 node_7917_nrm_combined = node_7917_nrm_base*dot(node_7917_nrm_base, node_7917_nrm_detail)/node_7917_nrm_base.z - node_7917_nrm_detail;
                float3 node_7917 = node_7917_nrm_combined;
                float node_4370_if_leA = step(_DamageLevel,0.8);
                float node_4370_if_leB = step(0.8,_DamageLevel);
                float node_4372_if_leA = step(_DamageLevel,0.6);
                float node_4372_if_leB = step(0.6,_DamageLevel);
                float node_4371_if_leA = step(_DamageLevel,0.4);
                float node_4371_if_leB = step(0.4,_DamageLevel);
                float node_4369_if_leA = step(_DamageLevel,0.2);
                float node_4369_if_leB = step(0.2,_DamageLevel);
                float node_4377 = 1.0;
                float4 node_3034 = tex2D(_DamageMask,TRANSFORM_TEX(i.uv0, _DamageMask));
                float3 node_4382 = float3(0,0,0);
                float3 node_4370 = lerp((node_4370_if_leA*lerp((node_4372_if_leA*lerp((node_4371_if_leA*lerp((node_4369_if_leA*node_4377)+(node_4369_if_leB*node_3034.b),node_3034.b,node_4369_if_leA*node_4369_if_leB))+(node_4371_if_leB*node_3034.g),node_3034.g,node_4371_if_leA*node_4371_if_leB))+(node_4372_if_leB*node_3034.r),node_3034.r,node_4372_if_leA*node_4372_if_leB))+(node_4370_if_leB*node_4382),node_4382,node_4370_if_leA*node_4370_if_leB);
                float3 normalLocal = lerp(node_7917,_BumpMap_var.rgb,node_4370);
                float3 normalDirection = normalize(mul( normalLocal, tangentTransform )); // Perturbed normals
                
                float nSign = sign( dot( viewDirection, i.normalDir ) ); // Reverse normal if this is a backface
                i.normalDir *= nSign;
                normalDirection *= nSign;
                
                float3 lightDirection = normalize(lerp(_WorldSpaceLightPos0.xyz, _WorldSpaceLightPos0.xyz - i.posWorld.xyz,_WorldSpaceLightPos0.w));
                float3 lightColor = _LightColor0.rgb;
                float3 halfDirection = normalize(viewDirection+lightDirection);
////// Lighting:
                float attenuation = LIGHT_ATTENUATION(i);
                float3 attenColor = attenuation * _LightColor0.xyz;
///////// Gloss:
                float gloss = (128.0*_Shininess);
                float specPow = gloss;
////// Specular:
                float NdotL = max(0, dot( normalDirection, lightDirection ));
                float3 specularColor = _SpecColor.rgb;
                float3 directSpecular = attenColor * pow(max(0,dot(halfDirection,normalDirection)),specPow);
                float3 specular = directSpecular * specularColor;
/////// Diffuse:
                NdotL = max(0.0,dot( normalDirection, lightDirection ));
                float3 directDiffuse = max( 0.0, NdotL) * attenColor;
                float4 _MainTex_var = tex2D(_MainTex,TRANSFORM_TEX(i.uv0, _MainTex));
                float4 _DamageMainTex_var = tex2D(_DamageMainTex,TRANSFORM_TEX(i.uv0, _DamageMainTex));
                float3 diffuse = directDiffuse * (_MainTex_var.rgb*_Color.rgb*lerp(_DamageMainTex_var.rgb,float3(node_4377,node_4377,node_4377),node_4370));
/// Final Color:
                float3 finalColor = diffuse + specular;
                return fixed4(finalColor * 1,0);
            }
            ENDCG
        }
    }
    FallBack "Reflective/Bumped Specular"
    CustomEditor "ShaderForgeMaterialInspector"
}
