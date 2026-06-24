using UnityEngine;
using System.Collections;

// Controla o aparecimento dos clientes.
public class NPCSpawner : MonoBehaviour
{
    // Lista de NPCs possíveis.
    [SerializeField] private GameObject[] npcPrefabs;

    // Porta da loja.
    [SerializeField] private Transform doorPoint;

    // Balcão da loja.
    [SerializeField] private Transform counterPoint;

    // Tempo entre clientes.
    [SerializeField] private float spawnDelay = 3f;

    // Cliente atual.
    private GameObject currentNPC;

    // Último cliente gerado.
    private int lastNPCIndex = -1;

    private void Start()
    {
        StartCoroutine(SpawnRoutine());
    }

    private IEnumerator SpawnRoutine()
    {
        while (true)
        {
            if (currentNPC == null)
            {
                yield return new WaitForSeconds(spawnDelay);

                SpawnNPC();
            }

            yield return null;
        }
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