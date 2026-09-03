using UnityEngine;

public class FloatingPrompt : MonoBehaviour
{
    [SerializeField] private float height = 0.15f;
    [SerializeField] private float speed = 2f;

    private Vector3 startPosition;

    private void Start()
    {
        startPosition = transform.localPosition;
    }

    private void Update()
    {
        float offset = Mathf.Sin(Time.time * speed) * height;

        transform.localPosition = startPosition + new Vector3(0f, offset, 0f);
    }
}