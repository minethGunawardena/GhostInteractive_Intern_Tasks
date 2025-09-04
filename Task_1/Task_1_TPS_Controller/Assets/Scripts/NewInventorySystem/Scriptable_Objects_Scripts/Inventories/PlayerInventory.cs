using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "New Inventory", menuName = "Inventory System/Inventory")]

public class PlayerInventory : ScriptableObject
{
    public List<InventorySlot> inventoryContainer = new List<InventorySlot>();
    
    public void AddItem(InteractableItemObject _item ,int _ammount)
    {
        
        for (int i = 0; i < inventoryContainer.Count; i++)
        {
            if (inventoryContainer[i].item == _item)
            {
                inventoryContainer[i].AddAmmount(_ammount);
                
               return;

            }
        }
        inventoryContainer.Add(new InventorySlot(_item, _ammount));


    }

}

[System.Serializable]
public class InventorySlot
{
    public InteractableItemObject item;
    public int ammount;
    public InventorySlot(InteractableItemObject _item, int _ammount)
    {
        item=_item;
        ammount = _ammount;
    }
    public void AddAmmount(int value)
    {
        ammount += value;
    }

}
