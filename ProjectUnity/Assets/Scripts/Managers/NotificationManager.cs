using UnityEngine;

public class NotificationManager : MonoBehaviour
{
    public static NotificationManager Instance;

    [Header("Prefab")]
    [SerializeField] private NotificationUI notificationPrefab;

    [Header("Pai das notificações")]
    [SerializeField] private Transform notificationParent;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    public void Show(string message, Color color)
    {
        NotificationUI notification =
            Instantiate(
                notificationPrefab,
                notificationParent);

        notification.Setup(message, color);
    }
}