using UnityEngine;

// Verifica se o jogador está atrás do balcão.
public class PlayerServiceArea : MonoBehaviour
{
    // Indica se o jogador está na área de atendimento.
    public bool isInServiceArea = false;

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
}