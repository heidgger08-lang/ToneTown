using TMPro;
using UnityEngine;

// Controla a interface de diálogo.
public class DialogueUI : MonoBehaviour
{
    [Header("Painel")]
    [SerializeField] private GameObject dialoguePanel;

    [Header("Textos")]
    [SerializeField] private TMP_Text npcNameText;

    [SerializeField] private TMP_Text dialogueText;

    [Header("Referências")]
    [SerializeField] private RecommendationUI recommendationUI;

    private NPCController currentNPC;

    private string[] currentDialogues;

    private int currentDialogueIndex;

    private bool isDialogueOpen;

    // Impede que o mesmo E abra outro diálogo.
    public bool JustClosedDialogue { get; private set; }

    private void Update()
    {
        if (!isDialogueOpen)
            return;

        if (Input.GetKeyDown(KeyCode.E))
        {
            NextDialogue();
        }
    }

    private void LateUpdate()
    {
        // A flag dura apenas um frame.
        JustClosedDialogue = false;
    }

    // Abre um diálogo.
    public void OpenDialogue(
        string npcName,
        string[] dialogues,
        NPCController npc)
    {
        if (isDialogueOpen)
            return;

        currentNPC = npc;

        currentDialogues = dialogues;

        currentDialogueIndex = 0;

        npcNameText.text = npcName;

        dialogueText.text = currentDialogues[currentDialogueIndex];

        dialoguePanel.SetActive(true);

        isDialogueOpen = true;
    }

    // Próxima fala.
    private void NextDialogue()
    {
        currentDialogueIndex++;

        if (currentDialogueIndex < currentDialogues.Length)
        {
            dialogueText.text =
                currentDialogues[currentDialogueIndex];
        }
        else
        {
            CloseDialogue();
        }
    }

    // Fecha o diálogo.
    private void CloseDialogue()
    {
        dialoguePanel.SetActive(false);

        isDialogueOpen = false;

        // Evita reabrir o diálogo no mesmo frame.
        JustClosedDialogue = true;

        if (currentNPC != null)
        {
            recommendationUI.OpenRecommendation(currentNPC);
        }
    }

    // Retorna se existe diálogo aberto.
    public bool IsDialogueOpen()
    {
        return isDialogueOpen;
    }
}