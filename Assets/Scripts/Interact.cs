using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;

public class Interact : MonoBehaviour
{
    private bool inDistance = false;

    [SerializeField] private float distanceThreshold = 2f;

    private GameObject player;


    private SpriteRenderer spriteRenderer;

    private CircleCollider2D collider2D;


    private bool lettuce = false;

    public bool Lettuce => lettuce;
    
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        collider2D = GetComponent<CircleCollider2D>();

        
        player = FindFirstObjectByType<PlayerInput>().gameObject;
    }

    void Update()
    {
        ItemPickUp();
    }

    void ItemPickUp()
    {
        float distanceToPlayer = Vector2.Distance(player.transform.position, transform.position);

        if (distanceToPlayer < distanceThreshold)
        {
            inDistance = true;
            Debug.Log("Mouse in distance!");
        }

        if (inDistance && Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("Item picked up!");
            spriteRenderer.enabled = false;
            collider2D.enabled = false;
            enabled = false;

            lettuce = true;

        }
    }
}
