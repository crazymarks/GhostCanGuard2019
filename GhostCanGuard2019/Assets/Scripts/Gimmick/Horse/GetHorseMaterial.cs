using System.Collections.Generic;
using UnityEngine;

public class GetHorseMaterial : MonoBehaviour
{
    MeshRenderer[] mesh;
    public List<Material> materials = new List<Material>();
    [Range(0,1)]
    public float alpha = 1;
    // Start is called before the first frame update
    void Start()
    {
        mesh = GetComponentsInChildren<MeshRenderer>();
        materials.Clear();
        foreach (var a in mesh)
        {
            Material[] mat = a.materials;
            foreach(var m in mat)
            {
                materials.Add(m);
            }
        }
    }

    public void UpdateAlpha()
    {
        foreach (var item in materials)
        {
            item.color = new Color(item.color.r, item.color.g, item.color.b, alpha);
        }
    }
    //private void OnValidate()
    //{

    //    foreach (var item in materials)
    //    {
    //        item.color = new Color(item.color.r,item.color.g,item.color.b,alpha);
    //    }
    //}

    
}
