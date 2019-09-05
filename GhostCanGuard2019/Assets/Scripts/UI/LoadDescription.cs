using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadDescription : SingletonMonoBehavior<LoadDescription>
{
    //List<GimmickDescription> gimmickDescriptions = new List<GimmickDescription>();
    //Dictionary<string, int> descmap = new Dictionary<string, int>();
    LoadImageFromFile loadDescription = new LoadImageFromFile();
    public  DesceriptionAnimation desceription;

    //[SerializeField]
    //Text DescriptionText = null;


    // Start is called before the first frame update
    //void Start()
    //{
    //    TextAsset description = Resources.Load<TextAsset>("Senario/gimmickdescription");

    //    string[] data = description.text.Split(new char[] { '\n' });
    //    //Debug.Log(data.Length);


    //    for (int i = 1; i < data.Length - 1; i++)
    //    {
    //        string[] row = data[i].Split(new char[] { ',' });
    //        if (row[1] != "")
    //        {
    //            GimmickDescription de = new GimmickDescription();
    //            int.TryParse(row[0], out de.id);
    //            de.name = row[1];
    //            de.desc = row[2];
    //            gimmickDescriptions.Add(de);
    //            descmap.Add(de.name, de.id);
    //        }


    //    }
    //    //foreach (GimmickDescription de in gimmickDescriptions)
    //    //{
    //    //    Debug.Log(de.name + "," + de.desc);
    //    //}
    //}

    


    void setDescription(string name)
    {
        //DescriptionText.text = "\u3000" + gimmickDescriptions[descmap[name]].desc.Replace("|", "\n\u3000");
        loadDescription.loadImageByName(name);
        desceription.Description.sprite = loadDescription.GetSprite();
    }

    public void ShowDesc(string gimmickName)
    {
        //DescriptionPanel.SetActive(true);
        desceription.gameObject.SetActive(true);
        setDescription(gimmickName);
        desceription.CursorPosition = StopSystem.Instance.cursor.GetComponent<Image>().rectTransform;
        desceription.reset();
        desceription.trigger();
    }
    public void HideDesc()
    {
        desceription.reset();
        desceription.gameObject.SetActive(false);
        //DescriptionText.text = "";
        //DescriptionPanel.SetActive(false);
    }
    public void ShowDesc()
    {
        //DescriptionPanel.SetActive(true);
        
    }
}
