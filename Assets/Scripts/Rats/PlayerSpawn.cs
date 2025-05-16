using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerSpawn : MonoBehaviour
{
    void Start()
    {
        Vector3 spawnPos = PlayerPositionManager.Instance.GetSavedPosition(SceneManager.GetActiveScene().name);
        if (spawnPos != Vector3.zero)
        {
            transform.position = spawnPos;
        }
    }
}
