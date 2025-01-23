using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeTestItem :InventoryObj
{
    public override void Use()
    {
        Debug.Log("use cube");
    }
   
    public CubeTestItem( Transform play,GameObject obs, string id)
    {
        player = play;
        Name = "blocktest";
        Object = obs;
        UseableOnce =false;
        stacknum =3;
        objectparts.Add(obs);
        ID = id;
    }
   

}
