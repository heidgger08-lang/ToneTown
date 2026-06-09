using UnityEngine;

// Classe responsável por controlar a movimentação do jogador.
public class PlayerMovement : MonoBehaviour
{
    // Velocidade de movimento do personagem.
    // Como é public, pode ser alterada pelo Inspector da Unity.
    public float moveSpeed = 5f;

    // Guarda a direção em que o jogador deseja se mover.
    private Vector2 moveDirection;

    // Update é executado uma vez por frame.
    private void Update()
    {
        // Lê os inputs do teclado.
        ReadInput();

        // Move o personagem.
        Move();
    }

    // Responsável apenas por ler os controles do jogador.
    private void ReadInput()
    {
        // Horizontal:
        // A = -1
        // D = +1
        float horizontal = Input.GetAxisRaw("Horizontal");

        // Vertical:
        // S = -1
        // W = +1
        float vertical = Input.GetAxisRaw("Vertical");

        // Cria um vetor de direção baseado nos inputs.
        moveDirection = new Vector2(horizontal, vertical);

        // Normaliza para evitar movimento mais rápido na diagonal.
        moveDirection = moveDirection.normalized;
    }

    // Responsável apenas pela movimentação.
    private void Move()
    {
        // Move o objeto usando:
        // direção * velocidade * tempo do frame.
        transform.position += (Vector3)moveDirection * moveSpeed * Time.deltaTime;
    }
}