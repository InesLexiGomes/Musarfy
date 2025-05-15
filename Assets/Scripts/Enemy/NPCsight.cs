using UnityEngine;

public class NPC : MonoBehaviour
{
    Ray ray;

    [SerializeField] int maxDistance = 1000;
    [SerializeField] LayerMask layers;
    private PlayerInput player;



    void Start()
    {
        player = FindFirstObjectByType<PlayerInput>();
    }


    void Update()
    {

        Ray2D ray = new Ray2D(transform.position, transform.up);

        RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction, maxDistance, layers);

        if (hit.collider != null)
        {
            Debug.Log("Hit: " + hit.collider.name);

            PlayerInput playerInput = hit.collider.GetComponent<PlayerInput>();
            if (playerInput != null)
            {
                Debug.Log("Player detected!");      
            }
        }
    }
}
