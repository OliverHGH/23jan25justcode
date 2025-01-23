using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using GameManage;
public class Scene1ObjectManager :InventoryObjectManager
{
    public GameObject b1, b2, b3;
    public Transform putdown;
    public int usedshards=0;
    public override void ListofAllItems()
    {
        ThingyItem thing1 = new ThingyItem(p, b1, putdown, this,"s1");
        ItemList.Add(thing1);
        ThingyItem thing2 = new ThingyItem(p, b2, putdown, this,"s2");
        ItemList.Add(thing2);
        ThingyItem thing3 = new ThingyItem(p, b3, putdown, this,"s3");
        ItemList.Add(thing3);
        base.ListofAllItems();
    }
    protected override void Update()
    {
       base.Update();
       manager.thingscollectedforlevel1 = usedshards;
    }

}
