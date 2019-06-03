
using UnityEngine;
/// <summary>
/// PostEffectを利用するクラス
/// </summary>
/// 
[ExecuteInEditMode]
[RequireComponent(typeof(Camera))]
public class PostEffectBase : MonoBehaviour
{
    private Camera mainCamera;
    public Camera MainCamera { get { return mainCamera = mainCamera == null ? GetComponent<Camera>() : mainCamera; } }

    public Shader targetShader;
    private Material targetMaterial = null;
    public Material TargetMaterial { get { return CheckShaderAndCreatMaterial(targetShader, ref targetMaterial); } }
    
        
    /// <summary>
    ///支持するかどうかを検査 
    /// </summary>
    void Start()
    {
        if (CheckSupport() == false)
            enabled = false;
    }
    
    /// <summary>
    /// システムが画像テキスチャーを支持するかどうかを検査
    /// </summary>
    /// <returns></returns>
    protected bool CheckSupport()
    {
        if(SystemInfo.supportsImageEffects == false)
        {
            Debug.Log("このプラットフォームがimage renderに支持されない");
            return false;
        }
        return true;
    }

    /// <summary>
    ///　指定のshaderが利用できますかを検査で、このshaderを使ったmaterialを戻す
    /// </summary>
    /// <param name="shader">指定shader</param>
    /// <param name="material">作れたmaterial</param>
    /// <returns>指定shaderのmaterialを戻す</returns>
    protected Material CheckShaderAndCreatMaterial(Shader shader,ref Material material)
    {
        if (shader == null || !shader.isSupported)
            return null;
        if (material && material.shader == shader)
            return material;
        material = new Material(shader);
        material.hideFlags = HideFlags.DontSave;
        return material;
    }

  

}
