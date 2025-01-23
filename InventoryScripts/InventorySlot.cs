using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlot
{

    private Button uiSlot;
    private Text slotT;
    private InventoryObj[,] Iarray;
    
    private int SlotNumber;
    private string message;
    public string keychoice;
    private int slottotal;
    public InventorySlot(Button uis, Text t, int num,InventoryObj[,]Invarray)
    {
        uiSlot = uis;
        slotT = t;
        SlotNumber = num;
        Iarray = Invarray;
        keychoice = (SlotNumber + 1).ToString();
    }
    public void display()
    {
        slottotal = 0;
        if (Iarray[SlotNumber,0] == null)
        {
            message = "empty";
        }
        else
        {
            for(int x=0;x< Iarray[SlotNumber, 0].maxstack; x++)
            {
                if(Iarray[SlotNumber, x]!= null)
                {
                    slottotal++;
                }
            }
            string total = slottotal.ToString();
            message = Iarray[SlotNumber, 0].objectname + " ("+total+")";
        }
        slotT.text = message;
    }

    public void highlight()
    {
        uiSlot.GetComponent<Image>().color = Color.yellow;
    }
    public void unhighlight()
    {
        uiSlot.GetComponent<Image>().color = Color.white;
    }
    
}
