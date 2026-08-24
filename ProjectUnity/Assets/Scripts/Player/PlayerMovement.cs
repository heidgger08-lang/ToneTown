using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // Velocidade de movimentação do jogador.
    public float moveSpeed = 5f;

    // Direção atual do movimento.
    private Vector2 moveDirection;

    // Referência ao Rigidbody2D.
    private Rigidbody2D rb;

    // Referência ao SpriteRenderer.
    private SpriteRenderer spriteRenderer;

    // Executado quando o objeto é criado.
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        ReadInput();
        FlipSprite();
    }

    private void FixedUpdate()
    {
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

    // Vira o sprite de acordo com a direção horizontal.
    private void FlipSprite()
    {
        if (moveDirection.x > 0)
        {
            spriteRenderer.flipX = false;
        }
        else if (moveDirection.x < 0)
        {
            spriteRenderer.flipX = true;
        }
    }
}