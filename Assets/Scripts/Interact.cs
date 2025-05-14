using Unity.VisualScripting;
using UnityEngine;

public class Interact : MonoBehaviour
{
    private bool isHovered = false;

    private bool _objectWithMouse = false;

    private SpriteRenderer spriteRenderer;

    private CircleCollider2D collider2D;

    private bool lettuce = false;

    public bool Lettuce => lettuce;
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        collider2D = GetComponent<CircleCollider2D>();
    }

    void OnMouseEnter()
    {
        isHovered = true;
        Debug.Log("Mouse is over the platform!");

        if (isHovered)
        {
            _objectWithMouse = true;
            Debug.Log("Item picked up!");
            spriteRenderer.enabled = false;
            collider2D.enabled = false;
            lettuce = true;
        }   
    }
}
