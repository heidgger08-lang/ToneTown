using UnityEngine;

// Controla a interação entre o jogador e o NPC.
public class InteractableNPC : MonoBehaviour
{
    // Referência ao controlador do NPC.
    private NPCController npcController;

    // Referência à UI de diálogo.
    private DialogueUI dialogueUI;

    // Referência ao sistema da área de atendimento do jogador.
    private PlayerServiceArea playerServiceArea;

    private void Start()
    {
        // Busca o controlador do NPC.
        npcController = GetComponent<NPCController>();

        // Busca a UI de diálogo na cena.
        dialogueUI = FindObjectOfType<DialogueUI>();

        // Busca o Player e seu script de atendimento.
        playerServiceArea =
            FindObjectOfType<PlayerServiceArea>();
    }

    private void Update()
    {
        // Só permite interação se:
        // - NPC estiver esperando atendimento
        // - Jogador estiver atrás do balcão
        // - Nenhum diálogo estiver aberto
        if (
            npcController.IsWaitingForService() &&
            playerServiceArea.isInServiceArea &&
            !dialogueUI.IsDialogueOpen() &&
            Input.GetKeyDown(KeyCode.E)
        )
        {
            Interact();
        }
    }

    // Abre o diálogo do NPC.
    private void Interact()
    {
        dialogueUI.OpenDialogue(
            npcController.GetNPCName(),
            npcController.GetDialogues(),
            npcController
        );
    }
}