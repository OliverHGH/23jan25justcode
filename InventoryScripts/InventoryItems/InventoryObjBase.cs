using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class InventoryObj
{
    protected int stacknum;
    public int maxstack
    {
        get { return stacknum; }
    }
    protected string Name;
    public string objectname
    {
        get { return Name; }
    }

    protected bool UseableOnce = false;
    public bool UseOnce
    {
        get { return UseableOnce; }
    }
    public bool ininv=false;
    protected Transform player;
    public GameObject Object;
    public List<GameObject> objectparts = new List<GameObject>();
    public string ID;
    
    public virtual void Use()
    {
    }
    public void Drop()
    {
        ininv = false;
        Vector3 pos = player.position + player.forward*2f+player.up*.3f;
        Equip();
        Object.transform.position = pos;

    }


    public virtual void Equip()
    {
        foreach (GameObject x in objectparts)
        {
            x.SetActive(true);
        }
    }
    public virtual void UnEquip()
    {
        foreach(GameObject x in objectparts)
        {
            x.SetActive(false);
        }
    }
    public virtual void Pickup()
    {
        UnEquip();
        ininv = true;
    }
  
}
