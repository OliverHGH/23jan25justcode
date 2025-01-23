using UnityEngine;
using UnityEngine.InputSystem.EnhancedTouch;

public class torchbatteryscript : MonoBehaviour
{
    public GameObject player,battery;
    InventoryManager inventoryforbattery;
    TorchItem torch;
    MathsMethods diffinder = new MathsMethods();
    void Start()
    {
        inventoryforbattery= player.GetComponent<InventoryManager>();
    }

    // Update is called once per frame
    void Update()
    {
        for(int i = 0; i < 5; i++)
        {
            if (inventoryforbattery.loadlist[i, 0] != null)
            {
                if (inventoryforbattery.loadlist[i, 0].objectname == "Torch")
                {
                    torch = (TorchItem)inventoryforbattery.loadlist[i, 0];
                    if (diffinder.findDif(transform.position, player.transform.position) < 1)
                    {
                        Debug.Log("found battery;");
                        torch.chargepercent += 20;
                        if (torch.chargepercent > 100)
                        {
                            torch.chargepercent = 100;
                        }
                        battery.SetActive(false);

                    }
                }
            }
        }
    }
}
