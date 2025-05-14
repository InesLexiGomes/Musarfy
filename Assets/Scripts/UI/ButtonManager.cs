using NUnit.Framework;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour
{
    [SerializeField] private GameObject menu;
    private Animator[] animators;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animators = menu.GetComponentsInChildren<Animator>();
        /*if (pauseMenu == null)
        {
            return;
        }*/
        menu.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButton("Cancel"))
        {
            Time.timeScale = 0f;
            foreach (Animator animator in animators)
            {
                animator.ResetTrigger("Pressed");
            }
            menu.SetActive(true);
        }
    }

    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }

    public void ControlsMenu()
    {
        Time.timeScale = 0f;
        foreach (Animator animator in animators)
        {
            animator.ResetTrigger("Pressed");
        }
        menu.SetActive(true);
    }

    public void ExitControlsMenu()
    {
        Time.timeScale = 1f;
        menu.SetActive(false);
    }

    public void Quit()
    {
        Debug.Log("Exit game!");
        Application.Quit();
    }
    public void Resume()
    {
        Time.timeScale = 1f;
        menu.SetActive(false);
    }

    public void Exit()
    {
        SceneManager.LoadScene(0);
    }
}
