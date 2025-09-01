using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;

public class Item_1 : MonoBehaviour, IInteractable
{
    public string itemName ="Cube";
    public void Interact(Inventory playerInventory)
    {
        playerInventory.AddItem(itemName);
 
        Debug.Log("Item_picked up :" +itemName);
        this.gameObject.SetActive(false);
    }
}
