using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DisplayInventory : MonoBehaviour
{
    public PlayerInventory inventory;

    public int xstart;
    public int ystart;
    public int xSpaceBetweenIcons;
    public int ySpaceBetweenIcons;
    public int numOfColums;

    Dictionary<InventorySlot,GameObject>itemDisplayed = new Dictionary<InventorySlot,GameObject>();

    private void Start()
    {
        CreateDisplay();
    }

    private void Update()
    {
       UpdateDisplay();
    }

    public void CreateDisplay()
    {
        for (int i=0;i<inventory.inventoryContainer.Count;i++)
        {
            HandelInventoryIcoObject(i);


        }

    }

    public Vector3 GetPosition(int i)
    {
        return new Vector3(
        xstart+(xSpaceBetweenIcons * (i % numOfColums)),
        ystart+(-ySpaceBetweenIcons * (i / numOfColums)),
        0f
    ); ;

    }


    private void UpdateDisplay()
    {
        for (int i = 0; i < inventory.inventoryContainer.Count; i++)
        {
            if (itemDisplayed.ContainsKey(inventory.inventoryContainer[i]))
            {
                itemDisplayed[inventory.inventoryContainer[i]].GetComponentInChildren<TextMeshProUGUI>().text = inventory.inventoryContainer[i].ammount.ToString("n0");
            }
            else
            {
                HandelInventoryIcoObject(i);


            }

        }
        

    }

    private void HandelInventoryIcoObject(int i)
    {
        var obj = Instantiate(inventory.inventoryContainer[i].item.itemPrefab, Vector3.zero, Quaternion.identity, transform);
        obj.GetComponent<RectTransform>().localPosition = GetPosition(i);
        obj.GetComponentInChildren<TextMeshProUGUI>().text = inventory.inventoryContainer[i].ammount.ToString("n0");
        Image iconImage = obj.GetComponent<Image>();
        if (iconImage != null)
        {
            iconImage.sprite = inventory.inventoryContainer[i].item.itemIcon;
            iconImage.preserveAspect = true;
        }
        itemDisplayed.Add(inventory.inventoryContainer[i], obj);
    }
}
