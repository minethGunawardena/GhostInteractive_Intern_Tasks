using UnityEngine;

[CreateAssetMenu(fileName = "Equipment Item", menuName = "Inventory/Items/Equipment")]
public class EquipmentItemScriptableObject : InteractableItemObject
{
    public float attackBonous = 10f;
    public float defenceBonous = 10f;
    private void Awake()
    {
        itemType = ItemType.EquipmentItem;
    }
}
