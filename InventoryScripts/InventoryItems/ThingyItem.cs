using GameManage;
using UnityEngine;

public class ThingyItem : InventoryObj
{
    Transform placetoput;
    MathsMethods mathsdfordist= new MathsMethods();
    Scene1ObjectManager manage;
    public ThingyItem(Transform p,GameObject ob, Transform destination,Scene1ObjectManager m, string iD)
    {
        Name = "Shard";
        placetoput = destination;
        player = p;
        Object = ob;
        objectparts.Add(ob);
        stacknum = 5;
        UseableOnce = false;
        manage = m;
        ID = iD;
    }

    public override void Use()
    {
        if (mathsdfordist.findDif(player.position, placetoput.position) < 3)
        {
            Debug.Log("putting item in");
            UseableOnce = true;
            manage.usedshards++;
        }
        else
        {
            UseableOnce=false;
        }
    }
}
