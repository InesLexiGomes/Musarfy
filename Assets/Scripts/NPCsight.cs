using UnityEngine;

public class NPC : MonoBehaviour
{
    Ray ray;

    int maxDistance = 100;




    void Start()
    {
    }


    void Update()
    {

        Ray2D ray = new Ray2D(transform.position, transform.forward);

        RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction, maxDistance);

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
