using UnityEngine;

// Controla o comportamento do cliente dentro da loja.
public class NPCController : MonoBehaviour
{
    private enum NPCState
    {
        WalkingToCounter,
        WaitingForService,
        Leaving
    }

    [Header("Movimentação")]
    [SerializeField] private float moveSpeed = 2f;
    [SerializeField] private float stoppingDistance = 0.1f;

    [SerializeField] private Transform player;
    private float playerStoppingDistance = 1.5f;

    [Header("Animação")]
    [SerializeField] private Animator animator;

    [Header("Dados")]
    [SerializeField] private CustomerData customerData;

    private NPCState currentState;

    private Transform counterPoint;
    private Transform doorPoint;

    // Impede que o cliente seja atendido duas vezes.
    private bool wasServed = false;

    private void Start()
    {
        currentState = NPCState.WalkingToCounter;

        if (animator == null)
        {
            animator = GetComponent<Animator>();
        }

        if (player == null)
        {
            GameObject playerObject =
                GameObject.FindGameObjectWithTag("Player");

            if (playerObject != null)
            {
                player = playerObject.transform;
            }
        }

        // Começa andando.
        animator.SetBool("Walking", true);

        // Define a direção inicial automaticamente.
        AtualizarDirecao(counterPoint);
    }

    private void Update()
    {
        switch (currentState)
        {
            case NPCState.WalkingToCounter:
                MoveToCounter();
                break;

            case NPCState.WaitingForService:
                break;

            case NPCState.Leaving:
                MoveToDoor();
                break;
        }
    }

    private void MoveToCounter()
    {
        if (PlayerEstaPerto())
            return;

        // Descobre a direção antes de andar.
        AtualizarDirecao(counterPoint);

        transform.position = Vector2.MoveTowards(
            transform.position,
            counterPoint.position,
            moveSpeed * Time.deltaTime
        );

        if (Vector2.Distance(
            transform.position,
            counterPoint.position) <= stoppingDistance)
        {
            currentState = NPCState.WaitingForService;

            // Para a animação de caminhada.
            animator.SetBool("Walking", false);

            // Sempre fica de costas no balcão.
            animator.SetInteger("Directions", 1);

            // Garante que não fique virado para o lado errado.
            animator.SetBool("Walking", false);
        }
    }

    private void MoveToDoor()
    {
        if (PlayerEstaPerto())
            return;

        // Descobre automaticamente para onde está indo.
        AtualizarDirecao(doorPoint);

        // Continua andando.
        animator.SetBool("Walking", true);

        transform.position = Vector2.MoveTowards(
            transform.position,
            doorPoint.position,
            moveSpeed * Time.deltaTime
        );

        if (Vector2.Distance(
            transform.position,
            doorPoint.position) <= stoppingDistance)
        {
            Destroy(gameObject);
        }
    }

    // Descobre a direção em que o NPC está indo.
    private void AtualizarDirecao(Transform destino)
    {
        if (destino == null)
            return;

        Vector2 direcao =
            destino.position - transform.position;

        // Movimento horizontal.
        if (Mathf.Abs(direcao.x) > Mathf.Abs(direcao.y))
        {
            animator.SetInteger("Directions", 2);

            // WalkSide usa o sprite virado para a direita.
            // FlipX = true faz ele olhar para a esquerda.
            if (direcao.x < 0)
                GetComponent<SpriteRenderer>().flipX = true;
            else
                GetComponent<SpriteRenderer>().flipX = false;
        }
        // Movimento para cima.
        else if (direcao.y > 0)
        {
            animator.SetInteger("Directions", 1);

            GetComponent<SpriteRenderer>().flipX = false;
        }
        // Movimento para baixo.
        else
        {
            animator.SetInteger("Directions", 0);

            GetComponent<SpriteRenderer>().flipX = false;
        }
    }

    private bool PlayerEstaPerto()
    {
        if (player == null)
            return false;

        float distancia = Vector2.Distance(
            transform.position,
            player.position
        );

        return distancia <= playerStoppingDistance;
    }

    // Marca que o atendimento deste cliente já começou.
    public void StartService()
    {
        wasServed = true;
    }

    public void FinishService()
    {
        Debug.Log("FINISH SERVICE");

        if (currentState == NPCState.Leaving)
            return;

        currentState = NPCState.Leaving;

        InteractableNPC interactable =
            GetComponent<InteractableNPC>();

        if (interactable != null)
            interactable.enabled = false;

        Debug.Log(
            $"{customerData.customerName} foi atendido."
        );
    }

    public bool WasServed()
    {
        return wasServed;
    }

    public bool IsWaitingForService()
    {
        return currentState == NPCState.WaitingForService;
    }

    public void SetPoints(
        Transform counter,
        Transform door)
    {
        counterPoint = counter;
        doorPoint = door;
    }

    public string GetNPCName()
    {
        return customerData.customerName;
    }

    public string[] GetDialogues()
    {
        return customerData.dialogues.ToArray();
    }

    public InstrumentData GetDesiredInstrument()
    {
        return customerData.desiredInstrument;
    }
}