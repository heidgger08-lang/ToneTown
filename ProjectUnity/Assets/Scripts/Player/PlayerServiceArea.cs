using UnityEngine;

// Verifica se o jogador está atrás do balcão.
public class PlayerServiceArea : MonoBehaviour
{
    // Indica se o jogador está na área de atendimento.
    public bool isInServiceArea = false;

    [Header("Prompt de interação")]
    [SerializeField] private GameObject interactionPrompt;

    private void Start()
    {
        if (interactionPrompt != null)
            interactionPrompt.SetActive(false);
    }

    private void Update()
    {
        AtualizarPrompt();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("ServiceArea"))
        {
            isInServiceArea = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("ServiceArea"))
        {
            isInServiceArea = false;
        }
    }

    private void AtualizarPrompt()
    {
        if (interactionPrompt == null)
            return;

        if (!isInServiceArea)
        {
            interactionPrompt.SetActive(false);
            return;
        }

        NPCController[] npcs =
            FindObjectsByType<NPCController>(FindObjectsSortMode.None);

        bool existeNPCNoBalcao = false;

        foreach (NPCController npc in npcs)
        {
            if (npc.IsWaitingForService())
            {
                existeNPCNoBalcao = true;
                break;
            }
        }

        interactionPrompt.SetActive(existeNPCNoBalcao);
    }
}
