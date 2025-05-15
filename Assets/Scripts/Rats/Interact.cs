using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;

public class Interact : MonoBehaviour
{
    [SerializeField] private float distanceThreshold = 2f;
    
    private bool inDistance = false;

    private PlayerInput player;

    private Collider2D collider2D;

    private SpriteRenderer spriteRenderer;

    private bool lettuce = false;

    public bool Lettuce => lettuce;
    
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        collider2D = GetComponent<CircleCollider2D>();
        player = FindFirstObjectByType<PlayerInput>();
    }

    void Update()
    {
        if(player == null)
        {
            player = FindFirstObjectByType<PlayerInput>();
        }
        CheckItemPickUp();
    }

    void CheckItemPickUp()
    {
        if (player == null)
        {
            return;
        }

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
