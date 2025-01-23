using UnityEngine;
using UnityEngine.UI;

public class PaperItem : InventoryObj
{
    Text paperwriting;
    GameObject paper;
    public PaperItem(GameObject papersheet, GameObject paperobject, Text writing, string text, string name, Transform p)
    {
        Object = paperobject;
        Name = name;
        writing.text = text;
        paperwriting = writing;
        paper = papersheet;
        player = p;
        UseableOnce = false;
        stacknum = 1;
        objectparts.Add(paperobject);
    }
    public override void Use()
    {
        paper.SetActive(!paper.activeSelf);
    }
}
