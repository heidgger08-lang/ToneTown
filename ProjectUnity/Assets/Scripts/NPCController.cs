using UnityEngine;

// Controla o comportamento do cliente dentro da loja.
public class NPCController : MonoBehaviour
{
    // Estados possíveis do NPC.
    private enum NPCState
    {
        WalkingToCounter,
        WaitingForService,
        Leaving
    }

    // Estado atual.
    private NPCState currentState;

    // Ponto onde o NPC espera atendimento.
    [SerializeField] private Transform counterPoint;

    // Ponto da porta.
    [SerializeField] private Transform doorPoint;

    // Velocidade de movimentação.
    [SerializeField] private float moveSpeed = 2f;

    // Distância mínima para considerar que chegou.
    [SerializeField] private float stoppingDistance = 0.1f;

    // Nome do NPC.
    [Header("Dados do NPC")]
    [SerializeField] private string npcName;

    // Lista de falas.
    [SerializeField] private string[] dialogues;

    private void Start()
    {
        // Ao nascer, vai para o balcão.
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
                break;

            case NPCState.Leaving:
                MoveToDoor();
                break;
        }
    }

    // Move o NPC até o balcão.
    private void MoveToCounter()
    {
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

            Debug.Log("Cliente aguardando atendimento.");
        }
    }

    // Move o NPC até a porta.
    private void MoveToDoor()
    {
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

    // Chamado quando o atendimento termina.
    public void FinishService()
    {
        Debug.Log("Cliente atendido.");

        currentState = NPCState.Leaving;
    }

    // Retorna o nome do NPC.
    public string GetNPCName()
    {
        return npcName;
    }

    // Retorna todas as falas.
    public string[] GetDialogues()
    {
        return dialogues;
    }

    // Verifica se o NPC está esperando atendimento.
    public bool IsWaitingForService()
    {
        return currentState == NPCState.WaitingForService;
    }
}