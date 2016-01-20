// Shader created with Shader Forge v1.17 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.17;sub:START;pass:START;ps:flbk:Specular,iptp:0,cusa:False,bamd:0,lico:1,lgpr:1,limd:1,spmd:1,trmd:0,grmd:0,uamb:True,mssp:False,bkdf:False,hqlp:False,rprd:False,enco:False,rmgx:False,rpth:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,culm:0,bsrc:0,bdst:1,dpts:2,wrdp:True,dith:0,rfrpo:True,rfrpn:Refraction,coma:15,ufog:True,aust:True,igpj:False,qofs:0,qpre:1,rntp:1,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,ofsf:0,ofsu:0,f2p0:False;n:type:ShaderForge.SFN_Final,id:1,x:34319,y:32621,varname:node_1,prsc:2|diff-6029-OUT,spec-7022-OUT,gloss-6892-OUT,normal-2429-OUT;n:type:ShaderForge.SFN_Tex2d,id:2,x:32471,y:32338,ptovrint:False,ptlb:MainTex,ptin:_MainTex,varname:node_439,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:b7e106a25ba8c7c4198888b4ed65fdc7,ntxv:0,isnm:False;n:type:ShaderForge.SFN_If,id:61,x:33010,y:32500,varname:node_61,prsc:2|A-881-OUT,B-72-OUT,GT-270-B,EQ-270-B,LT-137-OUT;n:type:ShaderForge.SFN_Vector1,id:72,x:33010,y:32448,varname:node_72,prsc:2,v1:0.2;n:type:ShaderForge.SFN_If,id:90,x:33010,y:32688,varname:node_90,prsc:2|A-881-OUT,B-93-OUT,GT-270-G,EQ-270-G,LT-61-OUT;n:type:ShaderForge.SFN_Vector1,id:93,x:32992,y:32632,varname:node_93,prsc:2,v1:0.4;n:type:ShaderForge.SFN_If,id:126,x:33010,y:32883,varname:node_126,prsc:2|A-881-OUT,B-128-OUT,GT-270-R,EQ-270-R,LT-90-OUT;n:type:ShaderForge.SFN_Vector1,id:128,x:33010,y:32818,varname:node_128,prsc:2,v1:0.6;n:type:ShaderForge.SFN_Vector1,id:137,x:33010,y:32385,varname:node_137,prsc:2,v1:1;n:type:ShaderForge.SFN_Tex2dAsset,id:206,x:32471,y:33135,ptovrint:False,ptlb:Damage Mask,ptin:_DamageMask,varname:node_6327,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:c4e987ef2d922ca47a8ace9435e0a07f,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Tex2d,id:270,x:32687,y:33179,varname:node_1850,prsc:2,tex:c4e987ef2d922ca47a8ace9435e0a07f,ntxv:0,isnm:False|TEX-206-TEX;n:type:ShaderForge.SFN_Color,id:449,x:32461,y:31915,ptovrint:False,ptlb:Color,ptin:_Color,varname:node_4643,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:1,c2:1,c3:1,c4:1;n:type:ShaderForge.SFN_Multiply,id:450,x:32914,y:32182,varname:node_450,prsc:2|A-2-RGB,B-449-RGB;n:type:ShaderForge.SFN_Tex2d,id:569,x:32471,y:32731,ptovrint:False,ptlb:Damage MainTex,ptin:_DamageMainTex,varname:node_3977,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Vector1,id:581,x:33010,y:33016,varname:node_581,prsc:2,v1:0.8;n:type:ShaderForge.SFN_If,id:583,x:33010,y:33076,varname:node_583,prsc:2|A-881-OUT,B-581-OUT,GT-815-OUT,EQ-815-OUT,LT-126-OUT;n:type:ShaderForge.SFN_Lerp,id:692,x:33537,y:32841,varname:node_692,prsc:2|A-569-RGB,B-769-OUT,T-583-OUT;n:type:ShaderForge.SFN_Vector3,id:769,x:32706,y:32976,varname:node_769,prsc:2,v1:1,v2:1,v3:1;n:type:ShaderForge.SFN_Vector3,id:815,x:32706,y:33077,varname:node_815,prsc:2,v1:0,v2:0,v3:0;n:type:ShaderForge.SFN_Slider,id:881,x:32472,y:33338,ptovrint:False,ptlb:Damage Level,ptin:_DamageLevel,varname:node_1910,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:0.5563341,max:1;n:type:ShaderForge.SFN_Tex2d,id:1065,x:32471,y:32524,ptovrint:False,ptlb:BumpMap,ptin:_BumpMap,varname:node_9458,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:d01c515efeed30b4cb7b6a631164cbb2,ntxv:3,isnm:True;n:type:ShaderForge.SFN_Tex2d,id:1067,x:32471,y:32941,ptovrint:False,ptlb:Damage BumpMap,ptin:_DamageBumpMap,varname:node_9281,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,ntxv:3,isnm:True;n:type:ShaderForge.SFN_Normalize,id:2429,x:34074,y:32975,varname:node_2429,prsc:2|IN-6964-OUT;n:type:ShaderForge.SFN_Multiply,id:6029,x:33927,y:32421,varname:node_6029,prsc:2|A-450-OUT,B-692-OUT;n:type:ShaderForge.SFN_Slider,id:6270,x:32393,y:32241,ptovrint:False,ptlb:Shininess,ptin:_Shininess,varname:node_1866,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0.031,cur:0.5,max:1;n:type:ShaderForge.SFN_Color,id:6271,x:32461,y:32077,ptovrint:False,ptlb:SpecColor,ptin:_SpecColor,varname:node_4478,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:0.5,c2:0.5,c3:0.5,c4:1;n:type:ShaderForge.SFN_Multiply,id:6892,x:33392,y:32401,varname:node_6892,prsc:2|A-6270-OUT,B-6928-OUT;n:type:ShaderForge.SFN_Vector1,id:6928,x:33180,y:32354,varname:node_6928,prsc:2,v1:128;n:type:ShaderForge.SFN_Vector3,id:6961,x:33348,y:33453,varname:node_6961,prsc:2,v1:0,v2:0,v3:1;n:type:ShaderForge.SFN_Lerp,id:6962,x:33549,y:33341,varname:node_6962,prsc:2|A-1067-RGB,B-6961-OUT,T-583-OUT;n:type:ShaderForge.SFN_NormalBlend,id:6963,x:33731,y:33290,varname:node_6963,prsc:2|BSE-1065-RGB,DTL-6962-OUT;n:type:ShaderForge.SFN_If,id:6964,x:33911,y:33146,varname:node_6964,prsc:2|A-583-OUT,B-769-OUT,GT-1065-RGB,EQ-1065-RGB,LT-6963-OUT;n:type:ShaderForge.SFN_Lerp,id:6975,x:33883,y:32794,varname:node_6975,prsc:2|A-450-OUT,B-769-OUT,T-692-OUT;n:type:ShaderForge.SFN_OneMinus,id:6986,x:33715,y:32873,varname:node_6986,prsc:2|IN-692-OUT;n:type:ShaderForge.SFN_Multiply,id:7022,x:34053,y:32690,varname:node_7022,prsc:2|A-6271-RGB,B-6975-OUT,C-6892-OUT;proporder:449-6271-6270-2-1065-569-1067-206-881;pass:END;sub:END;*/

Shader "DestroyIt/BumpedSpecular" {
    Properties {
        _Color ("Color", Color) = (1,1,1,1)
        _SpecColor ("SpecColor", Color) = (0.5,0.5,0.5,1)
        _Shininess ("Shininess", Range(0.031, 1)) = 0.5
        _MainTex ("MainTex", 2D) = "white" {}
        _BumpMap ("BumpMap", 2D) = "bump" {}
        _DamageMainTex ("Damage MainTex", 2D) = "white" {}
        _DamageBumpMap ("Damage BumpMap", 2D) = "bump" {}
        _DamageMask ("Damage Mask", 2D) = "white" {}
        _DamageLevel ("Damage Level", Range(0, 1)) = 0.5563341
    }
    SubShader {
        Tags {
            "RenderType"="Opaque"
        }
        LOD 400
        Pass {
            Name "FORWARD"
            Tags {
                "LightMode"="ForwardBase"
            }
            
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDBASE
            #include "UnityCG.cginc"
            #include "AutoLight.cginc"
            #pragma multi_compile_fwdbase_fullshadows
            #pragma multi_compile_fog
            #pragma exclude_renderers xbox360 ps3 
            #pragma target 3.0
            uniform float4 _LightColor0;
            uniform sampler2D _MainTex; uniform float4 _MainTex_ST;
            uniform sampler2D _DamageMask; uniform float4 _DamageMask_ST;
            uniform float4 _Color;
            uniform sampler2D _DamageMainTex; uniform float4 _DamageMainTex_ST;
            uniform float _DamageLevel;
            uniform sampler2D _BumpMap; uniform float4 _BumpMap_ST;
            uniform sampler2D _DamageBumpMap; uniform float4 _DamageBumpMap_ST;
            uniform float _Shininess;
            uniform float4 _SpecColor;
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
                float3 bitangentDir : TEXCOORD4;
                LIGHTING_COORDS(5,6)
                UNITY_FOG_COORDS(7)
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.normalDir = UnityObjectToWorldNormal(v.normal);
                o.tangentDir = normalize( mul( _Object2World, float4( v.tangent.xyz, 0.0 ) ).xyz );
                o.bitangentDir = normalize(cross(o.normalDir, o.tangentDir) * v.tangent.w);
                o.posWorld = mul(_Object2World, v.vertex);
                float3 lightColor = _LightColor0.rgb;
                o.pos = mul(UNITY_MATRIX_MVP, v.vertex);
                UNITY_TRANSFER_FOG(o,o.pos);
                TRANSFER_VERTEX_TO_FRAGMENT(o)
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
                i.normalDir = normalize(i.normalDir);
                float3x3 tangentTransform = float3x3( i.tangentDir, i.bitangentDir, i.normalDir);
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
                float4 node_1850 = tex2D(_DamageMask,TRANSFORM_TEX(i.uv0, _DamageMask));
                float3 node_815 = float3(0,0,0);
                float3 node_583 = lerp((node_583_if_leA*lerp((node_126_if_leA*lerp((node_90_if_leA*lerp((node_61_if_leA*1.0)+(node_61_if_leB*node_1850.b),node_1850.b,node_61_if_leA*node_61_if_leB))+(node_90_if_leB*node_1850.g),node_1850.g,node_90_if_leA*node_90_if_leB))+(node_126_if_leB*node_1850.r),node_1850.r,node_126_if_leA*node_126_if_leB))+(node_583_if_leB*node_815),node_815,node_583_if_leA*node_583_if_leB);
                float3 node_769 = float3(1,1,1);
                float node_6964_if_leA = step(node_583,node_769);
                float node_6964_if_leB = step(node_769,node_583);
                float3 _BumpMap_var = UnpackNormal(tex2D(_BumpMap,TRANSFORM_TEX(i.uv0, _BumpMap)));
                float3 _DamageBumpMap_var = UnpackNormal(tex2D(_DamageBumpMap,TRANSFORM_TEX(i.uv0, _DamageBumpMap)));
                float3 node_6963_nrm_base = _BumpMap_var.rgb + float3(0,0,1);
                float3 node_6963_nrm_detail = lerp(_DamageBumpMap_var.rgb,float3(0,0,1),node_583) * float3(-1,-1,1);
                float3 node_6963_nrm_combined = node_6963_nrm_base*dot(node_6963_nrm_base, node_6963_nrm_detail)/node_6963_nrm_base.z - node_6963_nrm_detail;
                float3 node_6963 = node_6963_nrm_combined;
                float3 normalLocal = normalize(lerp((node_6964_if_leA*node_6963)+(node_6964_if_leB*_BumpMap_var.rgb),_BumpMap_var.rgb,node_6964_if_leA*node_6964_if_leB));
                float3 normalDirection = normalize(mul( normalLocal, tangentTransform )); // Perturbed normals
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
                float4 _DamageMainTex_var = tex2D(_DamageMainTex,TRANSFORM_TEX(i.uv0, _DamageMainTex));
                float3 node_692 = lerp(_DamageMainTex_var.rgb,node_769,node_583);
                float3 specularColor = (_SpecColor.rgb*lerp(node_450,node_769,node_692)*node_6892);
                float3 directSpecular = attenColor * pow(max(0,dot(halfDirection,normalDirection)),specPow)*specularColor;
                float3 specular = directSpecular;
/////// Diffuse:
                NdotL = max(0.0,dot( normalDirection, lightDirection ));
                float3 directDiffuse = max( 0.0, NdotL) * attenColor;
                float3 indirectDiffuse = float3(0,0,0);
                indirectDiffuse += UNITY_LIGHTMODEL_AMBIENT.rgb; // Ambient Light
                float3 diffuseColor = (node_450*node_692);
                float3 diffuse = (directDiffuse + indirectDiffuse) * diffuseColor;
/// Final Color:
                float3 finalColor = diffuse + specular;
                fixed4 finalRGBA = fixed4(finalColor,1);
                UNITY_APPLY_FOG(i.fogCoord, finalRGBA);
                return finalRGBA;
            }
            ENDCG
        }
        Pass {
            Name "FORWARD_DELTA"
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
            #pragma exclude_renderers xbox360 ps3 
            #pragma target 3.0
            uniform float4 _LightColor0;
            uniform sampler2D _MainTex; uniform float4 _MainTex_ST;
            uniform sampler2D _DamageMask; uniform float4 _DamageMask_ST;
            uniform float4 _Color;
            uniform sampler2D _DamageMainTex; uniform float4 _DamageMainTex_ST;
            uniform float _DamageLevel;
            uniform sampler2D _BumpMap; uniform float4 _BumpMap_ST;
            uniform sampler2D _DamageBumpMap; uniform float4 _DamageBumpMap_ST;
            uniform float _Shininess;
            uniform float4 _SpecColor;
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
                float3 bitangentDir : TEXCOORD4;
                LIGHTING_COORDS(5,6)
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.normalDir = UnityObjectToWorldNormal(v.normal);
                o.tangentDir = normalize( mul( _Object2World, float4( v.tangent.xyz, 0.0 ) ).xyz );
                o.bitangentDir = normalize(cross(o.normalDir, o.tangentDir) * v.tangent.w);
                o.posWorld = mul(_Object2World, v.vertex);
                float3 lightColor = _LightColor0.rgb;
                o.pos = mul(UNITY_MATRIX_MVP, v.vertex);
                TRANSFER_VERTEX_TO_FRAGMENT(o)
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
                i.normalDir = normalize(i.normalDir);
                float3x3 tangentTransform = float3x3( i.tangentDir, i.bitangentDir, i.normalDir);
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
                float4 node_1850 = tex2D(_DamageMask,TRANSFORM_TEX(i.uv0, _DamageMask));
                float3 node_815 = float3(0,0,0);
                float3 node_583 = lerp((node_583_if_leA*lerp((node_126_if_leA*lerp((node_90_if_leA*lerp((node_61_if_leA*1.0)+(node_61_if_leB*node_1850.b),node_1850.b,node_61_if_leA*node_61_if_leB))+(node_90_if_leB*node_1850.g),node_1850.g,node_90_if_leA*node_90_if_leB))+(node_126_if_leB*node_1850.r),node_1850.r,node_126_if_leA*node_126_if_leB))+(node_583_if_leB*node_815),node_815,node_583_if_leA*node_583_if_leB);
                float3 node_769 = float3(1,1,1);
                float node_6964_if_leA = step(node_583,node_769);
                float node_6964_if_leB = step(node_769,node_583);
                float3 _BumpMap_var = UnpackNormal(tex2D(_BumpMap,TRANSFORM_TEX(i.uv0, _BumpMap)));
                float3 _DamageBumpMap_var = UnpackNormal(tex2D(_DamageBumpMap,TRANSFORM_TEX(i.uv0, _DamageBumpMap)));
                float3 node_6963_nrm_base = _BumpMap_var.rgb + float3(0,0,1);
                float3 node_6963_nrm_detail = lerp(_DamageBumpMap_var.rgb,float3(0,0,1),node_583) * float3(-1,-1,1);
                float3 node_6963_nrm_combined = node_6963_nrm_base*dot(node_6963_nrm_base, node_6963_nrm_detail)/node_6963_nrm_base.z - node_6963_nrm_detail;
                float3 node_6963 = node_6963_nrm_combined;
                float3 normalLocal = normalize(lerp((node_6964_if_leA*node_6963)+(node_6964_if_leB*_BumpMap_var.rgb),_BumpMap_var.rgb,node_6964_if_leA*node_6964_if_leB));
                float3 normalDirection = normalize(mul( normalLocal, tangentTransform )); // Perturbed normals
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
                float4 _DamageMainTex_var = tex2D(_DamageMainTex,TRANSFORM_TEX(i.uv0, _DamageMainTex));
                float3 node_692 = lerp(_DamageMainTex_var.rgb,node_769,node_583);
                float3 specularColor = (_SpecColor.rgb*lerp(node_450,node_769,node_692)*node_6892);
                float3 directSpecular = attenColor * pow(max(0,dot(halfDirection,normalDirection)),specPow)*specularColor;
                float3 specular = directSpecular;
/////// Diffuse:
                NdotL = max(0.0,dot( normalDirection, lightDirection ));
                float3 directDiffuse = max( 0.0, NdotL) * attenColor;
                float3 diffuseColor = (node_450*node_692);
                float3 diffuse = directDiffuse * diffuseColor;
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
