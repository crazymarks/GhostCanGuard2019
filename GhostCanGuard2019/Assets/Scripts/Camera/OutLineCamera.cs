using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutLineCamera : PostEffectBase
{
    public Camera outLineCamera;

    public Shader drawOccupied;

    private Material occupiedMaterial = null;
    private Material OccupiedMaterial { get { return CheckShaderAndCreatMaterial(drawOccupied, ref occupiedMaterial); } }

    public Color outlineColor = Color.yellow;

    [Range(0, 10)]
    public int outlineWidth = 4;

    [Range(0, 9)]
    public int iterations = 1;

    [Range(0, 1)]
    public float gradient = 1;


    public GameObject[] targets;

    private MeshFilter[] meshfilters;
    private RenderTexture tempRT;

    // Start is called before the first frame update
    void Awake()
    {

        SetupOutlineCamera();
    }


    private void SetupOutlineCamera()
    {
        outLineCamera.CopyFrom(MainCamera);
        outLineCamera.clearFlags = CameraClearFlags.Color;
        outLineCamera.backgroundColor = Color.black;
        outLineCamera.cullingMask = 1 << LayerMask.NameToLayer("PostEffect");
    }


    private void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        if (TargetMaterial != null && drawOccupied != null && outLineCamera != null && targets != null)
        {
            SetupOutlineCamera();
            tempRT = RenderTexture.GetTemporary(source.width, source.height, 0);
            outLineCamera.targetTexture = tempRT;

            for (int i = 0; i < targets.Length; i++)
            {
                if (targets[i] = null)
                    continue;
                meshfilters = targets[i].GetComponentsInChildren<MeshFilter>();
                for (int j = 0; i < meshfilters.Length; j++)
                    if ((MainCamera.cullingMask & (1 << meshfilters[j].gameObject.layer)) != 0)
                        for (int k = 0; k < meshfilters[j].sharedMesh.subMeshCount; k++)
                            Graphics.DrawMesh(meshfilters[j].sharedMesh, meshfilters[j].transform.localToWorldMatrix, OccupiedMaterial, LayerMask.NameToLayer("PostEffect"), outLineCamera, k);
            }
            outLineCamera.Render();
            TargetMaterial.SetTexture("_SceneTex", source);
            TargetMaterial.SetColor("_Color", outlineColor);
            TargetMaterial.SetInt("_Width", outlineWidth);
            TargetMaterial.SetInt("_Iterations", iterations);
            TargetMaterial.SetFloat("_Gragient", gradient);

            Graphics.Blit(tempRT, destination, TargetMaterial);
            tempRT.Release();
        }
        else
            Graphics.Blit(source, destination);
    }
}
