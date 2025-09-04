using UnityEngine;

public abstract class InteractableItemObject : ScriptableObject
{
    public string itemId;
    public string itemName;
    public ItemType itemType;
    public int maxItemCapacity = 99;
    public bool canStack = true;
    public GameObject itemPrefab;
    public Sprite itemIcon = null;
    [TextArea(0,50)]
    public string itemDiscription;
    
}
public enum ItemType
{ 
    KeyItem,
    ConsumableItem,
    EquipmentItem,
    Default,

}
