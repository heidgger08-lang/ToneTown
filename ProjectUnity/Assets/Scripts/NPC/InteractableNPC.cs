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
        // Busca os componentes necessários.
        npcController = GetComponent<NPCController>();

        dialogueUI = FindObjectOfType<DialogueUI>();

        recommendationUI = FindObjectOfType<RecommendationUI>();

        playerServiceArea = FindObjectOfType<PlayerServiceArea>();
    }

    private void Update()
    {
        // Só permite interação se:
        // - NPC estiver esperando atendimento
        // - Jogador estiver atrás do balcão
        // - Nenhum diálogo estiver aberto
        // - Nenhuma tela de recomendação estiver aberta
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