using UnityEngine;

[CreateAssetMenu(fileName ="Default Item",menuName ="Inventory/Items/Default")]
public class DefaultItemScriptableObject : InteractableItemObject
{
    private void Awake()
    {
        itemType = ItemType.Default;
    }
}
