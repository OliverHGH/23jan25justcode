using System.Collections;
using System.Collections.Generic;
using GameManage;
using UnityEngine;

public class GunItem : InventoryObj
{


    int bullets;
    List<GameObject> enemyaims;
    MathsMethods mathsforangle = new MathsMethods();
    Transform cam;
    LayerMask walls;
    public GunItem(Transform play, GameObject obs,string id, List<GameObject> enemies, LayerMask blocked)
    {
        player = play;
        Name = "Gun";
        Object = obs;
        UseableOnce = false;
        stacknum = 1;
        objectparts.Add(obs);
        ID = id;
        enemyaims = enemies;
        walls = blocked;
    }
    public override void Use()
    {
        Debug.Log("pewpew");
        foreach(GameObject x in enemyaims)
        {
            Debug.Log("test");
   
            if (mathsforangle.islookingat(Object.transform, x.transform, 3, 3, walls,false) == true)
            {
                Debug.Log("enemy shot");
                AngelBT enemyscript = x.GetComponent<AngelBT>();;
                enemyscript.TakeDamage();

            }

            
        }
        bullets --;
    }

}
