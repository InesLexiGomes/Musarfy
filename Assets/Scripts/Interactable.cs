using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;

public class Interactable : MonoBehaviour
{
    [SerializeField] private float distanceThreshold = 40f;
    [SerializeField] private uint itemID;
    
    private bool inDistance = false;

    private Inventory playerInventory;
    
    void Start()
    {
        playerInventory = FindFirstObjectByType<Inventory>();
    }

    void Update()
    {
        if(playerInventory == null)
        {
            playerInventory = FindFirstObjectByType<Inventory>();
        }
        CheckItemPickUp();
    }

    void CheckItemPickUp()
    {
        if (playerInventory == null)
        {
            return;
        }

        float distanceToPlayer = Vector2.Distance(playerInventory.transform.position, transform.position);

        if (distanceToPlayer < distanceThreshold && Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("Item picked up!");
            Debug.Log(itemID);

            playerInventory.AddItem(itemID);

            Destroy(gameObject);
        }
    }
}
