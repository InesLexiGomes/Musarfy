using UnityEngine;

public class NPC : MonoBehaviour
{
    Ray ray;

    [SerializeField] int maxDistance = 1000;
    [SerializeField] LayerMask layers;




    void Start()
    {
    }


    void Update()
    {

        Ray2D ray = new Ray2D(transform.position, transform.up);

        RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction, maxDistance, layers);

        if (hit.collider != null)
        {
            Debug.Log("Hit: " + hit.collider.name);

            if (hit.collider.CompareTag("Player"))
            {

                Debug.Log("Player detected!");

            }
        }
    }
}
