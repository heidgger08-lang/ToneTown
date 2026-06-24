using UnityEngine;

// Controla a interação entre o jogador e o NPC.
public class InteractableNPC : MonoBehaviour
{
    // Guarda se o jogador está próximo.
    private bool playerInRange = false;

    private void Update()
    {
        // Só permite interação quando o jogador estiver perto.
        if (playerInRange && Input.GetKeyDown(KeyCode.E))
        {
            Interact();
        }
    }

    // Detecta quando o jogador entra na área de interação.
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;

            Debug.Log("Pressione E para interagir.");
        }
    }

    // Detecta quando o jogador sai da área.
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
        }
    }

    // O que acontece ao interagir.
    private void Interact()
    {
        Debug.Log("Cliente atendido!");

        // Procura o NPCController no mesmo objeto.
        NPCController npcController = GetComponent<NPCController>();

        if (npcController != null)
        {
            Debug.Log("NPCController encontrado!");
            npcController.FinishService();
        }
        else
        {
            Debug.Log("NPCController NÃO encontrado!");
        }
    }
}