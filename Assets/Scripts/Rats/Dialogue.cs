using TMPro;
using UnityEngine;

public class Dialogue : MonoBehaviour
{
    [SerializeField] private GameObject dialogueUI;
    [SerializeField] private TextMeshPro text;

    private PlayerInput player;
    private bool dialogueEnabled;

    private RatFSM currentRat;
    private uint currentDialogueID;

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

        text.text = currentRat.GetQuestDialogue(currentDialogueID);
    }

    public void NextDialogue()
    {
        if (++currentDialogueID < currentRat.GetQuestDialogueLenght())
        {
            text.text = currentRat.GetQuestDialogue(currentDialogueID);
        }
        else
        {
            DisableDialogue();
        }
    }

    private void DisableDialogue()
    {
        dialogueUI.SetActive(false);

        currentDialogueID = 0;
        dialogueEnabled = false;
        player.enabled = true;
        currentRat.StopInteracting();
        currentRat = null;
    }


}
