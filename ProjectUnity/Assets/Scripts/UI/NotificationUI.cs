using TMPro;
using UnityEngine;

public class NotificationUI : MonoBehaviour
{
    [SerializeField] private TMP_Text notificationText;

    [SerializeField] private float moveSpeed = 60f;

    [SerializeField] private float lifeTime = 1f;

    private CanvasGroup canvasGroup;

    private RectTransform rectTransform;

    private float timer;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();

        canvasGroup = GetComponent<CanvasGroup>();

        if (canvasGroup == null)
        {
            canvasGroup = gameObject.AddComponent<CanvasGroup>();
        }
    }

    public void Setup(string message, Color color)
    {
        notificationText.text = message;

        notificationText.color = color;
    }

    private void Update()
    {
        timer += Time.deltaTime;

        rectTransform.anchoredPosition +=
            Vector2.up * moveSpeed * Time.deltaTime;

        canvasGroup.alpha =
            Mathf.Lerp(1f, 0f, timer / lifeTime);

        if (timer >= lifeTime)
        {
            Destroy(gameObject);
        }
    }
}