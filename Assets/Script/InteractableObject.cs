using UnityEngine;

public class InteractableObject : MonoBehaviour, IInteractable
{
    [Header("Interaction")]
    public string interactionText = "Hello World!";
    public bool oneTimeUse = false;
    
    private bool hasBeenUsed = false;
    
    public void Interact(Player player)
    {
        if (oneTimeUse && hasBeenUsed)
            return;
            
        Debug.Log($"Interacted with {gameObject.name}: {interactionText}");
        
        // Add your interaction logic here
        // Examples: open doors, collect items, start dialogue, etc.
        
        if (oneTimeUse)
            hasBeenUsed = true;
    }
}
