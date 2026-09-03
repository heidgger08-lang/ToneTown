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

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("ServiceArea"))
        {
            isInServiceArea = true;

            if (interactionPrompt != null)
                interactionPrompt.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("ServiceArea"))
        {
            isInServiceArea = false;

            if (interactionPrompt != null)
                interactionPrompt.SetActive(false);
        }
    }
}