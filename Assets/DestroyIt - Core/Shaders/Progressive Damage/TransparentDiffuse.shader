// Shader created with Shader Forge v1.06 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.06;sub:START;pass:START;ps:flbk:Transparent/Diffuse,lico:1,lgpr:1,nrmq:0,limd:1,uamb:True,mssp:True,lmpd:False,lprd:False,rprd:False,enco:False,frtr:True,vitr:True,dbil:False,rmgx:False,rpth:0,hqsc:True,hqlp:False,tesm:0,blpr:1,bsrc:3,bdst:7,culm:2,dpts:2,wrdp:True,dith:0,ufog:False,aust:False,igpj:False,qofs:0,qpre:3,rntp:2,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,ofsf:0,ofsu:0,f2p0:False;n:type:ShaderForge.SFN_Final,id:0,x:35218,y:32442,varname:node_0,prsc:2|diff-9507-OUT,amdfl-9518-OUT,alpha-9589-OUT;n:type:ShaderForge.SFN_Vector3,id:216,x:34279,y:32939,varname:node_216,prsc:2,v1:1,v2:1,v3:1;n:type:ShaderForge.SFN_Color,id:1278,x:34011,y:32274,ptovrint:False,ptlb:Color,ptin:_Color,varname:node_7602,prsc:2,glob:False,c1:1,c2:1,c3:1,c4:1;n:type:ShaderForge.SFN_If,id:4369,x:34506,y:32443,varname:node_4369,prsc:2|A-4378-OUT,B-4373-OUT,GT-4380-B,EQ-4380-B,LT-4377-OUT;n:type:ShaderForge.SFN_If,id:4370,x:34496,y:33006,varname:node_4370,prsc:2|A-4378-OUT,B-4376-OUT,GT-4382-OUT,EQ-4382-OUT,LT-4372-OUT;n:type:ShaderForge.SFN_If,id:4371,x:34506,y:32625,varname:node_4371,prsc:2|A-4378-OUT,B-4374-OUT,GT-4380-G,EQ-4380-G,LT-4369-OUT;n:type:ShaderForge.SFN_If,id:4372,x:34506,y:32807,varname:node_4372,prsc:2|A-4378-OUT,B-4375-OUT,GT-4380-R,EQ-4380-R,LT-4371-OUT;n:type:ShaderForge.SFN_Vector1,id:4373,x:34506,y:32379,varname:node_4373,prsc:2,v1:0.2;n:type:ShaderForge.SFN_Vector1,id:4374,x:34506,y:32571,varname:node_4374,prsc:2,v1:0.4;n:type:ShaderForge.SFN_Vector1,id:4375,x:34506,y:32748,varname:node_4375,prsc:2,v1:0.6;n:type:ShaderForge.SFN_Vector1,id:4376,x:34496,y:32939,varname:node_4376,prsc:2,v1:0.8;n:type:ShaderForge.SFN_Vector1,id:4377,x:34506,y:32317,varname:node_4377,prsc:2,v1:1;n:type:ShaderForge.SFN_Slider,id:4378,x:33423,y:32426,ptovrint:False,ptlb:Damage Level,ptin:_DamageLevel,varname:node_7263,prsc:2,min:0,cur:0,max:1;n:type:ShaderForge.SFN_Tex2dAsset,id:4379,x:34001,y:32717,ptovrint:False,ptlb:Damage Mask,ptin:_DamageMask,varname:node_2826,tex:c4e987ef2d922ca47a8ace9435e0a07f,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Tex2d,id:4380,x:34225,y:32683,varname:node_3183,prsc:2,tex:c4e987ef2d922ca47a8ace9435e0a07f,ntxv:0,isnm:False|TEX-4379-TEX;n:type:ShaderForge.SFN_Vector3,id:4382,x:34279,y:33035,varname:node_4382,prsc:2,v1:0,v2:0,v3:0;n:type:ShaderForge.SFN_Slider,id:4732,x:33511,y:32605,ptovrint:False,ptlb:Transparency,ptin:_Transparency,varname:node_70,prsc:2,min:0,cur:0.6566329,max:1;n:type:ShaderForge.SFN_Tex2d,id:9499,x:34011,y:32075,ptovrint:False,ptlb:MainTex,ptin:_MainTex,varname:node_1922,prsc:2,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Multiply,id:9500,x:34340,y:32141,varname:node_9500,prsc:2|A-9499-RGB,B-1278-RGB;n:type:ShaderForge.SFN_Multiply,id:9507,x:34901,y:32339,varname:node_9507,prsc:2|A-9500-OUT,B-9520-OUT;n:type:ShaderForge.SFN_LightAttenuation,id:9513,x:34059,y:31891,varname:node_9513,prsc:2;n:type:ShaderForge.SFN_LightColor,id:9514,x:34078,y:31743,varname:node_9514,prsc:2;n:type:ShaderForge.SFN_Multiply,id:9515,x:34496,y:31903,varname:node_9515,prsc:2|A-9514-RGB,B-9513-OUT,C-9516-OUT;n:type:ShaderForge.SFN_Subtract,id:9516,x:34325,y:31983,varname:node_9516,prsc:2|A-9520-OUT,B-9517-RGB;n:type:ShaderForge.SFN_AmbientLight,id:9517,x:33615,y:31874,varname:node_9517,prsc:2;n:type:ShaderForge.SFN_Lerp,id:9518,x:34787,y:31991,varname:node_9518,prsc:2|A-9568-OUT,B-4382-OUT,T-9515-OUT;n:type:ShaderForge.SFN_Lerp,id:9520,x:34852,y:32678,varname:node_9520,prsc:2|A-9522-RGB,B-216-OUT,T-4370-OUT;n:type:ShaderForge.SFN_Tex2d,id:9522,x:33527,y:32206,ptovrint:False,ptlb:Damage MainTex,ptin:_DamageMainTex,varname:node_8525,prsc:2,ntxv:0,isnm:False;n:type:ShaderForge.SFN_OneMinus,id:9568,x:34888,y:31837,varname:node_9568,prsc:2|IN-9520-OUT;n:type:ShaderForge.SFN_OneMinus,id:9589,x:33836,y:32666,varname:node_9589,prsc:2|IN-4732-OUT;proporder:1278-4732-9499-9522-4379-4378;pass:END;sub:END;*/

Shader "DestroyIt/TransparentDiffuse" {
    Properties {
        _Color ("Color", Color) = (1,1,1,1)
        _Transparency ("Transparency", Range(0, 1)) = 0.6566329
        _MainTex ("MainTex", 2D) = "white" {}
        _DamageMainTex ("Damage MainTex", 2D) = "white" {}
        _DamageMask ("Damage Mask", 2D) = "white" {}
        _DamageLevel ("Damage Level", Range(0, 1)) = 0
        [HideInInspector]_Cutoff ("Alpha cutoff", Range(0,1)) = 0.5
    }
    SubShader {
        Tags {
            "Queue"="Transparent"
            "RenderType"="Transparent"
        }
        LOD 200
        Pass {
            Name "ForwardBase"
            Tags {
                "LightMode"="ForwardBase"
            }
            Blend SrcAlpha OneMinusSrcAlpha
            Cull Off
            
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDBASE
            #include "UnityCG.cginc"
            #include "AutoLight.cginc"
            #include "Lighting.cginc"
            #pragma multi_compile_fwdbase_fullshadows
            #pragma exclude_renderers gles xbox360 ps3 flash 
            #pragma target 3.0
            uniform float4 _Color;
            uniform float _DamageLevel;
            uniform sampler2D _DamageMask; uniform float4 _DamageMask_ST;
            uniform float _Transparency;
            uniform sampler2D _MainTex; uniform float4 _MainTex_ST;
            uniform sampler2D _DamageMainTex; uniform float4 _DamageMainTex_ST;
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
/////// Vectors:
                float3 viewDirection = normalize(_WorldSpaceCameraPos.xyz - i.posWorld.xyz);
                float3 normalDirection = i.normalDir;
                
                float nSign = sign( dot( viewDirection, i.normalDir ) ); // Reverse normal if this is a backface
                i.normalDir *= nSign;
                normalDirection *= nSign;
                
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
                float node_4370_if_leA = step(_DamageLevel,0.8);
                float node_4370_if_leB = step(0.8,_DamageLevel);
                float node_4372_if_leA = step(_DamageLevel,0.6);
                float node_4372_if_leB = step(0.6,_DamageLevel);
                float node_4371_if_leA = step(_DamageLevel,0.4);
                float node_4371_if_leB = step(0.4,_DamageLevel);
                float node_4369_if_leA = step(_DamageLevel,0.2);
                float node_4369_if_leB = step(0.2,_DamageLevel);
                float4 node_3183 = tex2D(_DamageMask,TRANSFORM_TEX(i.uv0, _DamageMask));
                float3 node_4382 = float3(0,0,0);
                float3 node_9520 = lerp(_DamageMainTex_var.rgb,float3(1,1,1),lerp((node_4370_if_leA*lerp((node_4372_if_leA*lerp((node_4371_if_leA*lerp((node_4369_if_leA*1.0)+(node_4369_if_leB*node_3183.b),node_3183.b,node_4369_if_leA*node_4369_if_leB))+(node_4371_if_leB*node_3183.g),node_3183.g,node_4371_if_leA*node_4371_if_leB))+(node_4372_if_leB*node_3183.r),node_3183.r,node_4372_if_leA*node_4372_if_leB))+(node_4370_if_leB*node_4382),node_4382,node_4370_if_leA*node_4370_if_leB));
                indirectDiffuse += lerp((1.0 - node_9520),node_4382,(_LightColor0.rgb*attenuation*(node_9520-UNITY_LIGHTMODEL_AMBIENT.rgb))); // Diffuse Ambient Light
                float4 _MainTex_var = tex2D(_MainTex,TRANSFORM_TEX(i.uv0, _MainTex));
                float3 diffuse = (directDiffuse + indirectDiffuse) * ((_MainTex_var.rgb*_Color.rgb)*node_9520);
/// Final Color:
                float3 finalColor = diffuse;
                return fixed4(finalColor,(1.0 - _Transparency));
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
            uniform float4 _Color;
            uniform float _DamageLevel;
            uniform sampler2D _DamageMask; uniform float4 _DamageMask_ST;
            uniform float _Transparency;
            uniform sampler2D _MainTex; uniform float4 _MainTex_ST;
            uniform sampler2D _DamageMainTex; uniform float4 _DamageMainTex_ST;
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
/////// Vectors:
                float3 viewDirection = normalize(_WorldSpaceCameraPos.xyz - i.posWorld.xyz);
                float3 normalDirection = i.normalDir;
                
                float nSign = sign( dot( viewDirection, i.normalDir ) ); // Reverse normal if this is a backface
                i.normalDir *= nSign;
                normalDirection *= nSign;
                
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
                float node_4370_if_leA = step(_DamageLevel,0.8);
                float node_4370_if_leB = step(0.8,_DamageLevel);
                float node_4372_if_leA = step(_DamageLevel,0.6);
                float node_4372_if_leB = step(0.6,_DamageLevel);
                float node_4371_if_leA = step(_DamageLevel,0.4);
                float node_4371_if_leB = step(0.4,_DamageLevel);
                float node_4369_if_leA = step(_DamageLevel,0.2);
                float node_4369_if_leB = step(0.2,_DamageLevel);
                float4 node_3183 = tex2D(_DamageMask,TRANSFORM_TEX(i.uv0, _DamageMask));
                float3 node_4382 = float3(0,0,0);
                float3 node_9520 = lerp(_DamageMainTex_var.rgb,float3(1,1,1),lerp((node_4370_if_leA*lerp((node_4372_if_leA*lerp((node_4371_if_leA*lerp((node_4369_if_leA*1.0)+(node_4369_if_leB*node_3183.b),node_3183.b,node_4369_if_leA*node_4369_if_leB))+(node_4371_if_leB*node_3183.g),node_3183.g,node_4371_if_leA*node_4371_if_leB))+(node_4372_if_leB*node_3183.r),node_3183.r,node_4372_if_leA*node_4372_if_leB))+(node_4370_if_leB*node_4382),node_4382,node_4370_if_leA*node_4370_if_leB));
                float3 diffuse = directDiffuse * ((_MainTex_var.rgb*_Color.rgb)*node_9520);
/// Final Color:
                float3 finalColor = diffuse;
                return fixed4(finalColor * (1.0 - _Transparency),0);
            }
            ENDCG
        }
    }
    FallBack "Transparent/Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}
