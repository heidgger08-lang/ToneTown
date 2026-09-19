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
        if (Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log(
                $"E APERTADO | " +
                $"Waiting={npcController.IsWaitingForService()} | " +
                $"Area={playerServiceArea.isInServiceArea} | " +
                $"Dialogue={dialogueUI.IsDialogueOpen()} | " +
                $"Recommendation={recommendationUI.IsOpen()} | " +
                $"WasServed={npcController.WasServed()}"
            );

            if (
                npcController.IsWaitingForService() &&
                playerServiceArea.isInServiceArea &&
                !dialogueUI.IsDialogueOpen() &&
                !recommendationUI.IsOpen()
            )
            {
                Debug.Log(">>> ENTROU NO IF DO INTERACTABLENPC");

                if (!npcController.WasServed())
                {
                    Interact();
                }
                else
                {
                    Debug.Log(">>> TENTANDO REABRIR VENDA");

                    recommendationUI.OpenRecommendation(
                        npcController
                    );
                }
            }
            else
            {
                Debug.Log(">>> NÃO ENTROU NO IF");
            }
        }
    }

    // Inicia o primeiro atendimento e abre o diálogo.
    private void Interact()
    {
        npcController.StartService();

        dialogueUI.OpenDialogue(
            npcController.GetNPCName(),
            npcController.GetDialogues(),
            npcController
        );
    }
}