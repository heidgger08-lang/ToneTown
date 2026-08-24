using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public float Speed;

    private Animator anim;
    private Rigidbody2D rb;
    private SpriteRenderer sprite;

    void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        Vector2 direction  = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

        rb.linearVelocity = direction.normalized*Speed;

        if (direction.x != 0)
        {
            ResetLayer();

            anim.SetLayerWeight(2, 1);

            if (direction.x > 0)
            {
                sprite.flipX = false;
            }
            else if (direction.x < 0)
            {
                sprite.flipX = true;
            }
        }

        if (direction.y > 0 && direction.x == 0) 
        {
            ResetLayer();
            anim.SetLayerWeight(1, 1);
        }

        if (direction.y < 0 && direction.x == 0)
        {
            ResetLayer();
            anim.SetLayerWeight(0, 1);
        }

        if (direction != Vector2.zero) 
        {
            anim.SetBool("Walking", true);
        }else
        {
            anim.SetBool("Walking", false);
        }
    }
    private void ResetLayer()
    {
        anim.SetLayerWeight(0, 0);
        anim.SetLayerWeight(1, 0);
        anim.SetLayerWeight(2, 0);
    }
}
