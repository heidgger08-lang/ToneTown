using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // Velocidade de movimentação do jogador.
    public float moveSpeed = 5f;

    // Direção atual do movimento.
    private Vector2 moveDirection;

    // Referência ao Rigidbody2D.
    private Rigidbody2D rb;

    // Executado quando o objeto é criado.
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        ReadInput();
        Move();
    }

    // Lê os inputs do teclado.
    private void ReadInput()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        moveDirection = new Vector2(horizontal, vertical);
        moveDirection = moveDirection.normalized;
    }

    // Move o jogador utilizando o Rigidbody2D.
    private void Move()
    {
        rb.linearVelocity = moveDirection * moveSpeed;
    }
}