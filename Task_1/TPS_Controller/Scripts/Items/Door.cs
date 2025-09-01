using UnityEngine;

public class Door : MonoBehaviour, IInteractable
{
    [SerializeField] private float openAngle = 90f;
    [SerializeField] private float openSpeed = 2f;



    private Quaternion closedRotation;
    private Quaternion openRotation;
    private bool isOpen = false;


    private void Start()
    {
        closedRotation = transform.rotation;
        openRotation = Quaternion.Euler(transform.eulerAngles + new Vector3(0, openAngle, 0));
    }

    public void Interact(Inventory playerInventory)
    {
        isOpen = !isOpen;
    }

    private void Update()
    {
        if (isOpen)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, openRotation, Time.deltaTime * openSpeed);
        }
        else
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, closedRotation, Time.deltaTime * openSpeed);
        }

    }
}
