using UnityEngine;

[CreateAssetMenu(fileName = "Consumable Item", menuName = "Inventory/Items/Consumabale")]
public class ConsumableItemScriptableObject : InteractableItemObject
{
    public float restoreHealth = 25f;
    private void Awake()
    {
        itemType =ItemType.ConsumableItem;
        
    }
}
