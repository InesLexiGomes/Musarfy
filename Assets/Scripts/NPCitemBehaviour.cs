using UnityEngine;

public class NPCitemBehaviour : MonoBehaviour
{

    Interact interact;
    SpriteRenderer spriteRenderer;
    private bool isHovered = false;
    void Start()
    {
        interact = FindAnyObjectByType<Interact>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }


    void OnMouseEnter()
    {
        isHovered = true;
        Debug.Log("Mouse is over the platform!");

        if (isHovered && interact.Lettuce)
        {
            Debug.Log("Lettuce picked up!");
            spriteRenderer.color = new Color(0, 0, 0, 1f); // Alpha should be 0-1
        }
    }
}
