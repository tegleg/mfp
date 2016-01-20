// Shader created with Shader Forge v1.06 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.06;sub:START;pass:START;ps:flbk:Bumped Diffuse,lico:1,lgpr:1,nrmq:1,limd:1,uamb:True,mssp:True,lmpd:False,lprd:False,rprd:False,enco:False,frtr:True,vitr:True,dbil:False,rmgx:False,rpth:0,hqsc:True,hqlp:False,tesm:0,blpr:0,bsrc:0,bdst:1,culm:0,dpts:2,wrdp:True,dith:0,ufog:True,aust:True,igpj:False,qofs:0,qpre:1,rntp:1,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,ofsf:0,ofsu:0,f2p0:False;n:type:ShaderForge.SFN_Final,id:1,x:34327,y:32621,varname:node_1,prsc:2|diff-6029-OUT,normal-2429-OUT,amdfl-10009-OUT;n:type:ShaderForge.SFN_Tex2d,id:2,x:33578,y:32160,ptovrint:False,ptlb:MainTex,ptin:_MainTex,varname:node_2203,prsc:2,ntxv:0,isnm:False;n:type:ShaderForge.SFN_If,id:61,x:32859,y:32741,varname:node_61,prsc:2|A-881-OUT,B-72-OUT,GT-270-B,EQ-270-B,LT-137-OUT;n:type:ShaderForge.SFN_Vector1,id:72,x:32859,y:32689,varname:node_72,prsc:2,v1:0.2;n:type:ShaderForge.SFN_If,id:90,x:32859,y:32929,varname:node_90,prsc:2|A-881-OUT,B-93-OUT,GT-270-G,EQ-270-G,LT-61-OUT;n:type:ShaderForge.SFN_Vector1,id:93,x:32841,y:32873,varname:node_93,prsc:2,v1:0.4;n:type:ShaderForge.SFN_If,id:126,x:32859,y:33124,varname:node_126,prsc:2|A-881-OUT,B-128-OUT,GT-270-R,EQ-270-R,LT-90-OUT;n:type:ShaderForge.SFN_Vector1,id:128,x:32859,y:33059,varname:node_128,prsc:2,v1:0.6;n:type:ShaderForge.SFN_Vector1,id:137,x:32859,y:32626,varname:node_137,prsc:2,v1:1;n:type:ShaderForge.SFN_Tex2dAsset,id:206,x:32265,y:33095,ptovrint:False,ptlb:Damage Mask,ptin:_DamageMask,varname:node_7137,tex:c4e987ef2d922ca47a8ace9435e0a07f,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Tex2d,id:270,x:32469,y:33083,varname:node_4703,prsc:2,tex:c4e987ef2d922ca47a8ace9435e0a07f,ntxv:0,isnm:False|TEX-206-TEX;n:type:ShaderForge.SFN_Color,id:449,x:33591,y:31974,ptovrint:False,ptlb:Color,ptin:_Color,varname:node_1196,prsc:2,glob:False,c1:1,c2:1,c3:1,c4:1;n:type:ShaderForge.SFN_Multiply,id:450,x:33796,y:32347,varname:node_450,prsc:2|A-2-RGB,B-449-RGB;n:type:ShaderForge.SFN_Tex2d,id:569,x:32480,y:32787,ptovrint:False,ptlb:Damage MainTex,ptin:_DamageMainTex,varname:node_1157,prsc:2,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Vector1,id:581,x:32859,y:33257,varname:node_581,prsc:2,v1:0.8;n:type:ShaderForge.SFN_If,id:583,x:32859,y:33317,varname:node_583,prsc:2|A-881-OUT,B-581-OUT,GT-815-OUT,EQ-815-OUT,LT-126-OUT;n:type:ShaderForge.SFN_Lerp,id:692,x:33317,y:33027,varname:node_692,prsc:2|A-569-RGB,B-769-OUT,T-583-OUT;n:type:ShaderForge.SFN_Vector3,id:769,x:32689,y:33855,varname:node_769,prsc:2,v1:1,v2:1,v3:1;n:type:ShaderForge.SFN_Vector3,id:815,x:32689,y:33744,varname:node_815,prsc:2,v1:0,v2:0,v3:0;n:type:ShaderForge.SFN_Slider,id:881,x:32060,y:32966,ptovrint:False,ptlb:Damage Level,ptin:_DamageLevel,varname:node_8237,prsc:2,min:0,cur:0.6,max:1;n:type:ShaderForge.SFN_Tex2d,id:1065,x:32319,y:32709,ptovrint:False,ptlb:BumpMap,ptin:_BumpMap,varname:node_8193,prsc:2,ntxv:3,isnm:True;n:type:ShaderForge.SFN_Tex2d,id:1067,x:32238,y:33387,ptovrint:False,ptlb:Damage BumpMap,ptin:_DamageBumpMap,varname:node_9753,prsc:2,ntxv:3,isnm:True;n:type:ShaderForge.SFN_Normalize,id:2429,x:33864,y:33081,varname:node_2429,prsc:2|IN-6045-OUT;n:type:ShaderForge.SFN_Multiply,id:6029,x:34011,y:32495,varname:node_6029,prsc:2|A-450-OUT,B-692-OUT;n:type:ShaderForge.SFN_If,id:6045,x:33670,y:33186,varname:node_6045,prsc:2|A-583-OUT,B-769-OUT,GT-1065-RGB,EQ-1065-RGB,LT-6257-OUT;n:type:ShaderForge.SFN_Lerp,id:6155,x:33308,y:33412,varname:node_6155,prsc:2|A-1067-RGB,B-6203-OUT,T-583-OUT;n:type:ShaderForge.SFN_Vector3,id:6203,x:33114,y:33544,varname:node_6203,prsc:2,v1:0,v2:0,v3:1;n:type:ShaderForge.SFN_NormalBlend,id:6257,x:33511,y:33328,varname:node_6257,prsc:2|BSE-1065-RGB,DTL-6155-OUT;n:type:ShaderForge.SFN_OneMinus,id:8534,x:33753,y:32783,varname:node_8534,prsc:2|IN-692-OUT;n:type:ShaderForge.SFN_AmbientLight,id:9209,x:32859,y:32410,varname:node_9209,prsc:2;n:type:ShaderForge.SFN_LightColor,id:9210,x:33221,y:32358,varname:node_9210,prsc:2;n:type:ShaderForge.SFN_LightAttenuation,id:9211,x:33235,y:32208,varname:node_9211,prsc:2;n:type:ShaderForge.SFN_Multiply,id:9863,x:33624,y:32466,varname:node_9863,prsc:2|A-9211-OUT,B-9210-RGB,C-10152-OUT;n:type:ShaderForge.SFN_Lerp,id:10009,x:34023,y:32810,varname:node_10009,prsc:2|A-8534-OUT,B-815-OUT,T-9863-OUT;n:type:ShaderForge.SFN_Subtract,id:10152,x:33221,y:32563,varname:node_10152,prsc:2|A-692-OUT,B-9209-RGB;proporder:449-2-1065-569-1067-206-881;pass:END;sub:END;*/

Shader "DestroyIt/BumpedDiffuse" {
    Properties {
        _Color ("Color", Color) = (1,1,1,1)
        _MainTex ("MainTex", 2D) = "white" {}
        _BumpMap ("BumpMap", 2D) = "bump" {}
        _DamageMainTex ("Damage MainTex", 2D) = "white" {}
        _DamageBumpMap ("Damage BumpMap", 2D) = "bump" {}
        _DamageMask ("Damage Mask", 2D) = "white" {}
        _DamageLevel ("Damage Level", Range(0, 1)) = 0.6
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
            uniform sampler2D _BumpMap; uniform float4 _BumpMap_ST;
            uniform sampler2D _DamageBumpMap; uniform float4 _DamageBumpMap_ST;
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
                UNITY_FOG_COORDS(7)
                #ifndef LIGHTMAP_OFF
                    float4 uvLM : TEXCOORD8;
                #else
                    float3 shLight : TEXCOORD8;
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
                UNITY_TRANSFER_FOG(o,o.pos);
                TRANSFER_VERTEX_TO_FRAGMENT(o)
                return o;
            }
            fixed4 frag(VertexOutput i) : COLOR {
                i.normalDir = normalize(i.normalDir);
                float3x3 tangentTransform = float3x3( i.tangentDir, i.binormalDir, i.normalDir);
/////// Vectors:
                float3 viewDirection = normalize(_WorldSpaceCameraPos.xyz - i.posWorld.xyz);
                float node_583_if_leA = step(_DamageLevel,0.8);
                float node_583_if_leB = step(0.8,_DamageLevel);
                float node_126_if_leA = step(_DamageLevel,0.6);
                float node_126_if_leB = step(0.6,_DamageLevel);
                float node_90_if_leA = step(_DamageLevel,0.4);
                float node_90_if_leB = step(0.4,_DamageLevel);
                float node_61_if_leA = step(_DamageLevel,0.2);
                float node_61_if_leB = step(0.2,_DamageLevel);
                float4 node_4703 = tex2D(_DamageMask,TRANSFORM_TEX(i.uv0, _DamageMask));
                float3 node_815 = float3(0,0,0);
                float3 node_583 = lerp((node_583_if_leA*lerp((node_126_if_leA*lerp((node_90_if_leA*lerp((node_61_if_leA*1.0)+(node_61_if_leB*node_4703.b),node_4703.b,node_61_if_leA*node_61_if_leB))+(node_90_if_leB*node_4703.g),node_4703.g,node_90_if_leA*node_90_if_leB))+(node_126_if_leB*node_4703.r),node_4703.r,node_126_if_leA*node_126_if_leB))+(node_583_if_leB*node_815),node_815,node_583_if_leA*node_583_if_leB);
                float3 node_769 = float3(1,1,1);
                float node_6045_if_leA = step(node_583,node_769);
                float node_6045_if_leB = step(node_769,node_583);
                float3 _BumpMap_var = UnpackNormal(tex2D(_BumpMap,TRANSFORM_TEX(i.uv0, _BumpMap)));
                float3 _DamageBumpMap_var = UnpackNormal(tex2D(_DamageBumpMap,TRANSFORM_TEX(i.uv0, _DamageBumpMap)));
                float3 node_6257_nrm_base = _BumpMap_var.rgb + float3(0,0,1);
                float3 node_6257_nrm_detail = lerp(_DamageBumpMap_var.rgb,float3(0,0,1),node_583) * float3(-1,-1,1);
                float3 node_6257_nrm_combined = node_6257_nrm_base*dot(node_6257_nrm_base, node_6257_nrm_detail)/node_6257_nrm_base.z - node_6257_nrm_detail;
                float3 node_6257 = node_6257_nrm_combined;
                float3 normalLocal = normalize(lerp((node_6045_if_leA*node_6257)+(node_6045_if_leB*_BumpMap_var.rgb),_BumpMap_var.rgb,node_6045_if_leA*node_6045_if_leB));
                float3 normalDirection = normalize(mul( normalLocal, tangentTransform )); // Perturbed normals
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
                float3 node_692 = lerp(_DamageMainTex_var.rgb,node_769,node_583);
                indirectDiffuse += lerp((1.0 - node_692),node_815,(attenuation*_LightColor0.rgb*(node_692-UNITY_LIGHTMODEL_AMBIENT.rgb))); // Diffuse Ambient Light
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
            uniform sampler2D _BumpMap; uniform float4 _BumpMap_ST;
            uniform sampler2D _DamageBumpMap; uniform float4 _DamageBumpMap_ST;
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
                float node_583_if_leA = step(_DamageLevel,0.8);
                float node_583_if_leB = step(0.8,_DamageLevel);
                float node_126_if_leA = step(_DamageLevel,0.6);
                float node_126_if_leB = step(0.6,_DamageLevel);
                float node_90_if_leA = step(_DamageLevel,0.4);
                float node_90_if_leB = step(0.4,_DamageLevel);
                float node_61_if_leA = step(_DamageLevel,0.2);
                float node_61_if_leB = step(0.2,_DamageLevel);
                float4 node_4703 = tex2D(_DamageMask,TRANSFORM_TEX(i.uv0, _DamageMask));
                float3 node_815 = float3(0,0,0);
                float3 node_583 = lerp((node_583_if_leA*lerp((node_126_if_leA*lerp((node_90_if_leA*lerp((node_61_if_leA*1.0)+(node_61_if_leB*node_4703.b),node_4703.b,node_61_if_leA*node_61_if_leB))+(node_90_if_leB*node_4703.g),node_4703.g,node_90_if_leA*node_90_if_leB))+(node_126_if_leB*node_4703.r),node_4703.r,node_126_if_leA*node_126_if_leB))+(node_583_if_leB*node_815),node_815,node_583_if_leA*node_583_if_leB);
                float3 node_769 = float3(1,1,1);
                float node_6045_if_leA = step(node_583,node_769);
                float node_6045_if_leB = step(node_769,node_583);
                float3 _BumpMap_var = UnpackNormal(tex2D(_BumpMap,TRANSFORM_TEX(i.uv0, _BumpMap)));
                float3 _DamageBumpMap_var = UnpackNormal(tex2D(_DamageBumpMap,TRANSFORM_TEX(i.uv0, _DamageBumpMap)));
                float3 node_6257_nrm_base = _BumpMap_var.rgb + float3(0,0,1);
                float3 node_6257_nrm_detail = lerp(_DamageBumpMap_var.rgb,float3(0,0,1),node_583) * float3(-1,-1,1);
                float3 node_6257_nrm_combined = node_6257_nrm_base*dot(node_6257_nrm_base, node_6257_nrm_detail)/node_6257_nrm_base.z - node_6257_nrm_detail;
                float3 node_6257 = node_6257_nrm_combined;
                float3 normalLocal = normalize(lerp((node_6045_if_leA*node_6257)+(node_6045_if_leB*_BumpMap_var.rgb),_BumpMap_var.rgb,node_6045_if_leA*node_6045_if_leB));
                float3 normalDirection = normalize(mul( normalLocal, tangentTransform )); // Perturbed normals
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
                float3 node_692 = lerp(_DamageMainTex_var.rgb,node_769,node_583);
                float3 diffuse = directDiffuse * ((_MainTex_var.rgb*_Color.rgb)*node_692);
/// Final Color:
                float3 finalColor = diffuse;
                return fixed4(finalColor * 1,0);
            }
            ENDCG
        }
    }
    FallBack "Bumped Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}
