using NUnit.Framework;
using Unity.VisualScripting;
using UnityEngine;

public class ButtonManager : MonoBehaviour
{
    [SerializeField] GameObject pauseMenu;
    private Animator animator;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animator = GetComponent<Animator>();
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
            pauseMenu.SetActive(true);
            Time.timeScale = 0f;
        }
    }

    public void Resume()
    {
        Time.timeScale = 1f;
        if (animator != null)
        {
            animator.ResetTrigger(2);
        }
        pauseMenu.SetActive(false);
    }

    public void Quit()
    {
        Debug.Log("Quit game!");
        Application.Quit();
    }
}
