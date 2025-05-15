using TMPro;
using UnityEngine;

public class Dialogue : MonoBehaviour
{
    [SerializeField] private GameObject dialogueUI;
    [SerializeField] private TextMeshPro text;

    private PlayerInput player;
    private bool dialogueEnabled;

    private RatFSM currentRat;

    private void Update()
    {
        if (dialogueEnabled && Input.GetKeyDown(KeyCode.Mouse0))
        {
            DisableDialogue();
        }
    }

    public void EnableDialogueUI(RatFSM rat)
    {
        dialogueUI.SetActive(true);

        dialogueEnabled = true;
        player.enabled = false;
        currentRat = rat;

        if (rat.CheckCondition("Requirements Fulfilled?"))
        {
            text.text = rat.QuestEndDialogue;
        }
        else
        {
            text.text = rat.QuestDialogue;
        }
    }

    private void DisableDialogue()
    {
        dialogueUI.SetActive(false);

        dialogueEnabled = false;
        player.enabled = true;
        currentRat.StopInteracting();
        currentRat = null;
    }
}
