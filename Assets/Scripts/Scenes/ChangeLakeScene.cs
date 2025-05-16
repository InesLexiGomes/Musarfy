using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeLakeScene : MonoBehaviour
{
    void OnTriggerEnter2D()
    {
        SceneManager.LoadScene(10);
    }
}
