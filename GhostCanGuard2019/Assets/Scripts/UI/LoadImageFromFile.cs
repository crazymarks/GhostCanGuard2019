using UnityEngine;
using UnityEngine.UI;
public class LoadImageFromFile
{
    Sprite sprite;
    
    public Sprite GetSprite()
    {
        return sprite;
    }

    public void loadImageByName(string FileName)
    {
        sprite = Resources.Load<Sprite>("Senario/" + FileName);
    }
}
