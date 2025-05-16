using UnityEngine;

public class NPCItemBehaviour : MonoBehaviour
{

    Interact interact;

    SpriteRenderer spriteRenderer;

    [SerializeField] private float distanceThreshold = 2f;

    private bool inDistance = false;

    private bool itemchecked = false;

    private GameObject player;

    void Start()
    {
        interact = FindAnyObjectByType<Interact>();
        player = FindFirstObjectByType<PlayerInput>().gameObject;
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        ItemCheck();

        if (inDistance && Input.GetKeyDown(KeyCode.E))
        {
            ItemInteract();
        }
    }

    void ItemCheck()
    {
        float distanceToPlayer = Vector2.Distance(player.transform.position, transform.position);

        if (distanceToPlayer < distanceThreshold && interact.Lettuce)
        {
            inDistance = true;
            Debug.Log("Mouse in distance!");
            itemchecked = true;
        }

    }
    void ItemInteract()
    {
        if (itemchecked)
        {
            Debug.Log("Item checked!");
            spriteRenderer.color = new Color(0, 0, 0, 1f);
            enabled = false;
        }
    }
}
