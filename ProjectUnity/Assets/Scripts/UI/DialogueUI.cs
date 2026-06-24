using TMPro;
using UnityEngine;

// Controla a janela de diálogo.
public class DialogueUI : MonoBehaviour
{
    // Painel principal da UI.
    [SerializeField] private GameObject dialoguePanel;

    // Texto do nome do NPC.
    [SerializeField] private TMP_Text npcNameText;

    // Texto da fala atual.
    [SerializeField] private TMP_Text dialogueText;

    // Guarda todas as falas do NPC atual.
    private string[] currentDialogues;

    // Índice da fala atual.
    private int currentDialogueIndex;

    // Indica se existe um diálogo aberto.
    private bool isDialogueOpen;

    // Referência ao NPC que está sendo atendido.
    private NPCController currentNPC;

    private void Update()
    {
        // Se não existir diálogo aberto, não faz nada.
        if (!isDialogueOpen)
            return;

        // Avança para a próxima fala ao apertar E.
        if (Input.GetKeyDown(KeyCode.E))
        {
            NextDialogue();
        }
    }

    // Abre uma conversa.
    public void OpenDialogue(
        string npcName,
        string[] dialogues,
        NPCController npc)
    {
        // Guarda referência do NPC.
        currentNPC = npc;

        // Guarda todas as falas.
        currentDialogues = dialogues;

        // Começa na primeira fala.
        currentDialogueIndex = 0;

        // Define o nome na UI.
        npcNameText.text = npcName;

        // Define a primeira fala.
        dialogueText.text = currentDialogues[0];

        // Mostra o painel.
        dialoguePanel.SetActive(true);

        // Marca que o diálogo está aberto.
        isDialogueOpen = true;
    }

    // Passa para a próxima fala.
    private void NextDialogue()
    {
        // Vai para a próxima posição.
        currentDialogueIndex++;

        // Verifica se ainda existem falas.
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
        // Esconde a UI.
        dialoguePanel.SetActive(false);

        // Marca que não existe diálogo aberto.
        isDialogueOpen = false;

        // Faz o cliente ir embora.
        if (currentNPC != null)
        {
            RecommendationUI recommendationUI =
                FindObjectOfType<RecommendationUI>();

            recommendationUI.OpenRecommendation(
                currentNPC
            );
        }
    }

    // Permite que outros scripts verifiquem
    // se um diálogo está aberto.
    public bool IsDialogueOpen()
    {
        return isDialogueOpen;
    }
}