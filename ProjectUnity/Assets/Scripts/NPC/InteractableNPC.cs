using UnityEngine;

// Controla a interação entre o jogador e o NPC.
public class InteractableNPC : MonoBehaviour
{
    // Referência ao controlador do NPC.
    private NPCController npcController;

    // Referência à UI de diálogo.
    private DialogueUI dialogueUI;

    // Referência à UI de recomendação.
    private RecommendationUI recommendationUI;

    // Referência à área de atendimento.
    private PlayerServiceArea playerServiceArea;

    private void Start()
    {
        npcController = GetComponent<NPCController>();

        dialogueUI = FindFirstObjectByType<DialogueUI>();

        recommendationUI = FindFirstObjectByType<RecommendationUI>();

        playerServiceArea = FindFirstObjectByType<PlayerServiceArea>();
    }

    private void Update()
    {
        // Impede que o mesmo E que fecha o diálogo
        // abra outro imediatamente.
        if (dialogueUI.JustClosedDialogue)
            return;

        if (
            npcController.IsWaitingForService() &&
            playerServiceArea.isInServiceArea &&
            !dialogueUI.IsDialogueOpen() &&
            !recommendationUI.IsOpen() &&
            Input.GetKeyDown(KeyCode.E)
        )
        {
            Interact();
        }
    }

    // Abre o diálogo.
    private void Interact()
    {
        dialogueUI.OpenDialogue(
            npcController.GetNPCName(),
            npcController.GetDialogues(),
            npcController
        );
    }
}