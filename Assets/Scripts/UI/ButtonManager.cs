using NUnit.Framework;
using Unity.VisualScripting;
using UnityEngine;

public class ButtonManager : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenu;
    private Animator[] animators;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animators = pauseMenu.GetComponentsInChildren<Animator>();
        //GetComponent<Animator>();
        /*if (pauseMenu == null)
        {
            return;
        }*/
        pauseMenu.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButton("Cancel"))
        {
            Time.timeScale = 0f;
            pauseMenu.SetActive(true);
            foreach (Animator animator in animators)
            {
                animator.ResetTrigger("Pressed");
            }
        }
    }

    public void Resume()
    {
        Time.timeScale = 1f;
        pauseMenu.SetActive(false);
    }

    public void Quit()
    {
        Debug.Log("Quit game!");
        Application.Quit();
    }
}
