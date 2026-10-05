using UnityEngine;
using System.Collections;

// Controla o aparecimento dos clientes.
public class NPCSpawner : MonoBehaviour
{
    
    [SerializeField] private GameObject[] npcPrefabs;

   
    [SerializeField] private Transform doorPoint;

    
    [SerializeField] private Transform counterPoint;

    
    [SerializeField] private float spawnDelay = 3f;

    
    private GameObject currentNPC;

    
    private int lastNPCIndex = -1;

    
    private bool firstInstrumentPurchased = false;

    private void OnEnable()
    {
        DailyObjectiveManager.OnFirstPurchaseCompleted += UnlockFirstNPC;
    }

    private void OnDisable()
    {
        DailyObjectiveManager.OnFirstPurchaseCompleted -= UnlockFirstNPC;
    }

    private void Start()
    {
        StartCoroutine(SpawnRoutine());
    }

    private IEnumerator SpawnRoutine()
    {
        while (true)
        {
            // Espera o jogador comprar o primeiro instrumento.
            if (!firstInstrumentPurchased)
            {
                yield return null;
                continue;
            }

            if (currentNPC == null)
            {
                yield return new WaitForSeconds(spawnDelay);

                SpawnNPC();
            }

            yield return null;
        }
    }

    private void UnlockFirstNPC()
    {
        firstInstrumentPurchased = true;
    }

    private void SpawnNPC()
    {
        int randomIndex;

        // Impede repetir o mesmo NPC.
        do
        {
            randomIndex =
                Random.Range(0, npcPrefabs.Length);
        }
        while (
            npcPrefabs.Length > 1 &&
            randomIndex == lastNPCIndex
        );

        lastNPCIndex = randomIndex;

        currentNPC = Instantiate(
            npcPrefabs[randomIndex],
            doorPoint.position,
            Quaternion.identity
        );

        // Configura os pontos do NPC.
        NPCController npcController =
            currentNPC.GetComponent<NPCController>();

        npcController.SetPoints(
            counterPoint,
            doorPoint
        );
    }
}
