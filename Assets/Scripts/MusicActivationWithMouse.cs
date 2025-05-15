using UnityEngine;

public class MusicActivationWithMouse : MonoBehaviour
{
    private SoundManager soundManager;



    void Start()
    {
        soundManager  = GetComponent<SoundManager>();     
    }


    void Update()
    {   
    }


    void OEnter()
    {
        Debug.Log("MusicPlayed");
        soundManager.PlayMusic(0); 
    }
}
