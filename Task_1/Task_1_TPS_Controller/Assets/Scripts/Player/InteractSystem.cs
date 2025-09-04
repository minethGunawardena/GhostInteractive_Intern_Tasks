using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class InteractSystem : MonoBehaviour
{

    private PlayerInputManager playerInputManager;

    [SerializeField] private GameObject pickupIcon;
    [SerializeField]private PlayerInventory playerInventory;
    [SerializeField] private RectTransform inventoryPanel;
    [SerializeField] private Vector2 hidePosition;
    [SerializeField] private Vector2 showPosition;
    [SerializeField] private float aniamtionTime;
    private Vector2 targetPosition;
    private bool inventoryVisible = false;


    private Item currentItem;

    private void Awake()
    {
        playerInputManager = GetComponent<PlayerInputManager>();
    }
    private void Start()
    {
        targetPosition = hidePosition;
        inventoryPanel.anchoredPosition = hidePosition;
        pickupIcon.SetActive(false);
    }
    private void Update()
    {
        HandelInteraction();
        HandelInventoryView();
    }

    private void OnTriggerEnter(Collider other)
    {
        var item = other.GetComponent<Item>();
        if (item)
        {
            pickupIcon.SetActive(true);
            currentItem = item;
               
        }

    }

    private void OnTriggerExit(Collider other)
    {
        var item = other.GetComponent<Item>();
        if (item && (item == currentItem))
        {
            pickupIcon.SetActive(false);
            currentItem = null;

        }
    }

    private void HandelInteraction()
    {
        if (currentItem != null && playerInputManager.GetIntractButtonPressed())
        {
            playerInventory.AddItem(currentItem.itemObject, 1);
            Destroy(currentItem.gameObject);
            currentItem = null;
            pickupIcon.SetActive(false);
        }
    }
    private void HandelInventoryView()
    {
        if (playerInputManager.GetInventoryButtonPressed())
        {
            inventoryVisible = !inventoryVisible;
            targetPosition = inventoryVisible ? showPosition : hidePosition;
        }
        inventoryPanel.anchoredPosition = Vector2.Lerp(inventoryPanel.anchoredPosition,targetPosition,Time.deltaTime * aniamtionTime);

    }

    private void OnApplicationQuit()
    {
        playerInventory.inventoryContainer.Clear();
    }


}

