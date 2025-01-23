using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestItem2 : InventoryObj
{
    public override void Use()
    {
        Debug.Log("use bob");
    }

    public TestItem2(Transform play, GameObject obs, string id)
    {
        player = play;
        Name = "bobtest";
        Object = obs;
        UseableOnce = true;
        stacknum = 2;
        objectparts.Add(obs);
        ID = id;
    }
}
