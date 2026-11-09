using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerPersistence : MonoBehaviour
{
    private static PlayerPersistence instance;

    private static string pendingSpawnID;

    private Rigidbody2D rb;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;

        rb = GetComponent<Rigidbody2D>();

        DontDestroyOnLoad(gameObject);

        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    public static void SetSpawnPoint(string spawnID)
    {
        pendingSpawnID = spawnID;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (string.IsNullOrEmpty(pendingSpawnID))
            return;

        SceneSpawnPoint[] spawnPoints =
            FindObjectsByType<SceneSpawnPoint>(
                FindObjectsSortMode.None
            );

        foreach (SceneSpawnPoint point in spawnPoints)
        {
            if (point.SpawnID == pendingSpawnID)
            {
                transform.position = point.transform.position;

                if (rb != null)
                    rb.position = point.transform.position;

                pendingSpawnID = null;
                return;
            }
        }

        Debug.LogError("Ponto de entrada não encontrado: " + pendingSpawnID);
    }

    private void OnDestroy()
    {
        if (instance == this)
        {
            SceneManager.sceneLoaded -= OnSceneLoaded;
            instance = null;
        }
    }
}