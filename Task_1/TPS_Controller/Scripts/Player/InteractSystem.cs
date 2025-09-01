using UnityEngine;
using UnityEngine.InputSystem;

public class InteractSystem : MonoBehaviour
{
    public Transform rayPosition;
    public float rayDistance = 3f;


    private PlayerInputManager playerInputManager;

    private void Awake()
    {
        playerInputManager = GetComponent<PlayerInputManager>();
    }

    private void Update()
    {
        HandelInteract();
    }

    private void HandelInteract()
    {
        Ray ray = new Ray(rayPosition.position, rayPosition.forward);
        if (Physics.Raycast(ray, out RaycastHit hit, rayDistance))
        {
            if (playerInputManager.GetIntractButtonPressed())
            {
                IInteractable interactable = hit.collider.GetComponent<IInteractable>();
                if (interactable != null)
                {
                    interactable.Interact(GetComponent<Inventory>());
                }

            }
            
        }
    }

   
}

