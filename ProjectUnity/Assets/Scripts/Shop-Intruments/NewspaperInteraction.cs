using UnityEngine;

// Controla a interação do jogador com o jornal da loja.
public class NewspaperInteraction : MonoBehaviour
{
    [Header("Referência")]
    [SerializeField] private GameObject purchasePanel;
    [SerializeField] private GameObject interactionPrompt;

    [Header("Configuração")]
    [SerializeField] private KeyCode interactionKey = KeyCode.E;

    private bool playerInRange = false;
    private bool newspaperOpen = false;

    private void Start()
    {
        // Esconde o indicador E no início.
        if (interactionPrompt != null)
        {
            interactionPrompt.SetActive(false);
        }
    }

    private void Update()
    {
        if (playerInRange && Input.GetKeyDown(interactionKey))
        {
            ToggleNewspaper();
        }

        // Permite fechar o jornal com ESC.
        if (newspaperOpen && Input.GetKeyDown(KeyCode.Escape))
        {
            CloseNewspaper();
        }
    }

    private void ToggleNewspaper()
    {
        if (newspaperOpen)
        {
            CloseNewspaper();
        }
        else
        {
            OpenNewspaper();
        }
    }

    private void OpenNewspaper()
    {
        if (purchasePanel == null)
        {
            Debug.LogWarning("NewspaperInteraction: PurchasePanel não foi definido.");
            return;
        }

        purchasePanel.SetActive(true);
        newspaperOpen = true;

        Debug.Log("Jornal aberto.");
    }

    private void CloseNewspaper()
    {
        if (purchasePanel == null)
        {
            return;
        }

        purchasePanel.SetActive(false);
        newspaperOpen = false;

        Debug.Log("Jornal fechado.");
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;

            // Mostra o indicador E.
            if (interactionPrompt != null)
            {
                interactionPrompt.SetActive(true);
            }

            Debug.Log("Jogador está perto do jornal. Pressione E para ler.");
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;

            // Esconde o indicador E.
            if (interactionPrompt != null)
            {
                interactionPrompt.SetActive(false);
            }

            // Fecha o jornal se o jogador sair da área.
            if (newspaperOpen)
            {
                CloseNewspaper();
            }

            Debug.Log("Jogador saiu da área do jornal.");
        }
    }
}