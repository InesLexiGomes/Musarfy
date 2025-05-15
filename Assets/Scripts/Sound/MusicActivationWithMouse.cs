using UnityEngine;

public class MusicActivationWithMouse : MonoBehaviour
{
    private SoundManager soundManager;

    void Start()
    {
        soundManager = FindAnyObjectByType<SoundManager>();


        if (soundManager == null)
        {
            Debug.LogError("SoundManager not found!");
        }


    }

    void Update()
    {

    }

    public void ClickSound()
    {
        soundManager.SFXPlay(1);
    }

    void OnMouseEnter()
    {

        Debug.Log("SFX Played");
        soundManager.SFXPlay(0);

        if (Input.GetKey(KeyCode.Mouse0))
        {
            soundManager.SFXPlay(1);
        }

    }
}

