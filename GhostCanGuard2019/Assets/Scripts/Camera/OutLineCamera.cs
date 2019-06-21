using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutLineCamera : PostEffectBase
{
    private static OutLineCamera _instance;
    private Camera MainCamera = null;
    private Camera outlineCamera = null;

    private RenderTexture renderTexture = null;


    public Shader preRenderShader = null;
    //public Color outlineColor = Color.yellow;

    [Range(0, 10)]
    public int downSample = 4;
    
  
    [Range(0, 9)]
    public int iterations = 1;

    [Range(0, 5)]
    public float samplerScale = 1;


  

    private MeshFilter[] meshfilters;
    private RenderTexture tempRT;

    
    void Awake()
    {
        InitOutlineCamera();
    }


    private void InitOutlineCamera()
    {
        MainCamera = GetComponent<Camera>();
        if (MainCamera == null)
            return;


        Transform outlinecamTransform = transform.Find("outlineCamera");
        if (outlinecamTransform != null)
            DestroyImmediate(outlinecamTransform.gameObject);

        GameObject outlinecamobj = new GameObject("outlineCamera");
        outlineCamera = outlinecamobj.AddComponent<Camera>();

        SetUpOutlineCamera();

       
    }

    private void SetUpOutlineCamera()
    {
        if (outlineCamera)
        {
            outlineCamera.CopyFrom(MainCamera);
            outlineCamera.transform.parent = MainCamera.transform;
            //outlineCamera.transform.localPosition = Vector3.zero;
            //outlineCamera.transform.localRotation = Quaternion.identity;
            //outlineCamera.transform.localScale = Vector3.one;
            //outlineCamera.farClipPlane = MainCamera.farClipPlane;
            //outlineCamera.nearClipPlane = MainCamera.nearClipPlane;
            //outlineCamera.fieldOfView = MainCamera.fieldOfView;
            outlineCamera.backgroundColor = Color.clear;
            outlineCamera.clearFlags = CameraClearFlags.Color;
            outlineCamera.cullingMask = 1 << LayerMask.NameToLayer("outline");
            outlineCamera.depth = -999;
            if (renderTexture == null)
                renderTexture = RenderTexture.GetTemporary(outlineCamera.pixelWidth >> downSample , outlineCamera.pixelHeight >> downSample, 0);
        }
    }


    private void OnEnable()
    {
        SetUpOutlineCamera();
        outlineCamera.enabled = true;
        
    }

    private void OnDisable()
    {
        outlineCamera.enabled = false; 
    }

    private void OnDestroy()
    {
        if (renderTexture)
        {
            RenderTexture.ReleaseTemporary(renderTexture);
        }
        DestroyImmediate(outlineCamera.gameObject);
    }

    private void OnPreRender()
    {
        if (outlineCamera.enabled)
        {
            if (renderTexture != null && (renderTexture.width != Screen.width >> downSample || renderTexture.height != Screen.height >> downSample))
            {
                RenderTexture.ReleaseTemporary(renderTexture);
                renderTexture = RenderTexture.GetTemporary(Screen.width >> downSample, Screen.height >> downSample,0);
            }
            outlineCamera.targetTexture = renderTexture;
            outlineCamera.RenderWithShader(preRenderShader, "");
        }
    }


    private void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        if (GetMaterial&&renderTexture)
        {
            RenderTexture temp1 = RenderTexture.GetTemporary(Screen.width >> downSample, Screen.height >> downSample, 0);
            RenderTexture temp2 = RenderTexture.GetTemporary(Screen.width >> downSample, Screen.height >> downSample, 0);

            GetMaterial.SetVector("_offsets", new Vector4(0, samplerScale, 0, 0));
            Graphics.Blit(renderTexture, temp1, GetMaterial, 0);
            GetMaterial.SetVector("_offsets", new Vector4(samplerScale, 0, 0, 0));
            Graphics.Blit(temp1, temp2, GetMaterial, 0);

            for (int i = 0; i < iterations; i++)
            {
                GetMaterial.SetVector("_offsets", new Vector4(0, samplerScale, 0, 0));
                Graphics.Blit(temp2, temp1, GetMaterial, 0);
                GetMaterial.SetVector("_offsets", new Vector4(samplerScale, 0, 0, 0));
                Graphics.Blit(temp1, temp2, GetMaterial, 0);
            }

            GetMaterial.SetTexture("_BlurTex", temp2);
            Graphics.Blit(renderTexture, temp1, GetMaterial, 1);

            
            GetMaterial.SetTexture("_BlurTex", temp1);
            Graphics.Blit(source, destination, GetMaterial, 2);

            RenderTexture.ReleaseTemporary(temp1);
            RenderTexture.ReleaseTemporary(temp2);



        }
        else
            Graphics.Blit(source, destination);
    }
}
