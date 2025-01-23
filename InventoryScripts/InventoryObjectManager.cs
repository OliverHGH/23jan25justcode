using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using GameManage;

public class InventoryObjectManager : MonoBehaviour
{
    public GameObject o1, o2, o4, o5, batteryinfo;
    public Text batterytext;
    public Transform p;
    public GameObject managerobject;
    public List<InventoryObj> ItemList = new List<InventoryObj>();
    public LayerMask blocked;
    protected List<GameObject> enemies = new List<GameObject>();
    TorchItem torch;
    public GameManagerScript manager;
    public GameObject E1;

    void Start()
    {
        manager = managerobject.GetComponent<GameManagerScript>();
        enemies=manager.enemybodylist;
    }
    public virtual void ListofAllItems()
    {
        CubeTestItem cubey = new CubeTestItem(p, o1,"cube1");
        ItemList.Add(cubey);
        CubeTestItem cubey2 = new CubeTestItem(p, o2, "cube2");
        ItemList.Add(cubey2);
        torch = new TorchItem(p, o4, "torch1",batteryinfo,batterytext);
        ItemList.Add(torch);
        GunItem gun = new GunItem(p, o5, "gun1", enemies, blocked);
        ItemList.Add(gun);
    }

    protected virtual void Update()
    {
        if (torch.light.activeSelf)
        {
            torch.chargepercent-= (Time.deltaTime/2);
            if (torch.chargepercent < 0 )
            {
                torch.chargepercent = 0;
            }
            int intpercentage = Mathf.RoundToInt(torch.chargepercent);
            string percent = (string)intpercentage.ToString();
            torch.messagetext.text = "Torch Battery: " + percent+"%";
            if (torch.chargepercent == 0)
            {
                Debug.Log("torch out of battery");
                torch.Use();
                torch.working = false;
            }
        } 
    }


}
