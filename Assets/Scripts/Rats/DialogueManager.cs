using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    [SerializeField] private GameObject dialogueUI;
    [SerializeField] private Image image;
    [SerializeField] private TextMeshProUGUI text;

    private PlayerInput player;
    private bool dialogueEnabled;

    private RatFSM currentRat;
    private uint currentDialogueID = 0;

    private void Start()
    {
        dialogueUI.SetActive(false);
        player = FindAnyObjectByType<PlayerInput>();
    }

    private void Update()
    {
        if (dialogueEnabled && Input.GetKeyDown(KeyCode.Mouse0))
        {
            NextDialogue();
        }
    }

    public void EnableDialogueUI(RatFSM rat)
    {
        dialogueUI.SetActive(true);

        dialogueEnabled = true;
        player.enabled = false;
        currentRat = rat;

        text.text = currentRat.GetQuestDialogue(0);
        image.sprite = currentRat.GetNPCSprite();
    }

    public void NextDialogue()
    {
        currentDialogueID++;
        if (currentDialogueID < currentRat.GetQuestDialogueLenght())
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
