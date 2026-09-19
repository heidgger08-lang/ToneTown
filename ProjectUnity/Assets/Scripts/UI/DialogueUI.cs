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

    [Header("Continuar Diálogo")]
    [SerializeField] private GameObject continueText;

    [SerializeField] private TMP_Text continueTextLabel;

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

        if (Input.GetMouseButtonDown(0))
        {
            NextDialogue();
        }
    }

    private void LateUpdate()
    {
       
        JustClosedDialogue = false;
    }

   
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

        if (continueText != null)
        {
            continueText.SetActive(true);

            if (continueTextLabel != null)
                continueTextLabel.text = "Botão esquerdo - Continuar";
        }

        isDialogueOpen = true;
    }

    
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

    
    private void CloseDialogue()
    {
        dialoguePanel.SetActive(false);

        
        if (continueText != null)
            continueText.SetActive(false);

        isDialogueOpen = false;

    
        JustClosedDialogue = true;

        if (currentNPC != null)
        {
            recommendationUI.OpenRecommendation(currentNPC);
        }
    }

   
    public bool IsDialogueOpen()
    {
        return isDialogueOpen;
    }
}
