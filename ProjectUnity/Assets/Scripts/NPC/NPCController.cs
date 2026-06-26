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
        transform.position = Vector2.MoveTowards(
            transform.position,
            counterPoint.position,
            moveSpeed * Time.deltaTime);

        if (Vector2.Distance(
            transform.position,
            counterPoint.position) <= stoppingDistance)
        {
            currentState = NPCState.WaitingForService;
        }
    }

    private void MoveToDoor()
    {
        transform.position = Vector2.MoveTowards(
            transform.position,
            doorPoint.position,
            moveSpeed * Time.deltaTime);

        if (Vector2.Distance(
            transform.position,
            doorPoint.position) <= stoppingDistance)
        {
            Destroy(gameObject);
        }
    }

    public void FinishService()
    {
        Debug.Log("FINISH SERVICE");
        if (currentState == NPCState.Leaving)
            return;

        currentState = NPCState.Leaving;

        // Nunca mais permite interação com esse NPC.
        InteractableNPC interactable = GetComponent<InteractableNPC>();

        if (interactable != null)
            interactable.enabled = false;

        Debug.Log($"{customerData.customerName} foi atendido.");
    }

    public bool WasServed()
    {
        return wasServed;
    }

    public bool IsWaitingForService()
    {
        return currentState == NPCState.WaitingForService;
    }

    public void SetPoints(Transform counter, Transform door)
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