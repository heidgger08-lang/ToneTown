using UnityEngine;

public class NPCController : MonoBehaviour
{
    // Estados possíveis do NPC.
    private enum NPCState
    {
        WalkingToCounter,
        WaitingForService,
        Leaving
    }

    // Estado atual do NPC.
    private NPCState currentState;

    // Ponto onde o NPC será atendido.
    [SerializeField] private Transform counterPoint;

    // Ponto da porta por onde ele sai.
    [SerializeField] private Transform doorPoint;

    // Velocidade de movimentação.
    [SerializeField] private float moveSpeed = 2f;

    // Distância mínima para considerar que chegou.
    [SerializeField] private float stoppingDistance = 0.1f;

    private void Start()
    {
        // Quando o NPC nasce ele começa indo para o balcão.
        currentState = NPCState.WalkingToCounter;
    }

    private void Update()
    {
        switch (currentState)
        {
            case NPCState.WalkingToCounter:
                MoveToCounter();
                break;

            case NPCState.WaitingForService:
                // Fica parado esperando ser atendido.
                break;

            case NPCState.Leaving:
                MoveToDoor();
                break;
        }
    }

    // O NPC vai até o balcão.
    private void MoveToCounter()
    {
        transform.position = Vector2.MoveTowards(
            transform.position,
            counterPoint.position,
            moveSpeed * Time.deltaTime
        );

        // Verifica se chegou.
        if (Vector2.Distance(transform.position, counterPoint.position) <= stoppingDistance)
        {
            currentState = NPCState.WaitingForService;

            Debug.Log("Cliente aguardando atendimento.");
        }
    }

    // O NPC vai até a porta.
    private void MoveToDoor()
    {
        transform.position = Vector2.MoveTowards(
            transform.position,
            doorPoint.position,
            moveSpeed * Time.deltaTime
        );

        // Quando chegar na porta ele some.
        if (Vector2.Distance(transform.position, doorPoint.position) <= stoppingDistance)
        {
            Destroy(gameObject);
        }
    }

    // Função que será chamada quando o jogador terminar o atendimento.
    public void FinishService()
    {
        currentState = NPCState.Leaving;

        Debug.Log("Cliente atendido.");
    }
}