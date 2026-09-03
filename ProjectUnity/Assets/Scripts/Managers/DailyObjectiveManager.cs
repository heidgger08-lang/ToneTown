using System.Collections;
using TMPro;
using UnityEngine;

public class DailyObjectiveManager : MonoBehaviour
{
    public static DailyObjectiveManager Instance;

    [Header("UI")]
    [SerializeField] private TMP_Text objectiveText;

    [Header("Área de Atendimento")]
    [SerializeField] private PlayerServiceArea playerServiceArea;

    [Header("Objetivo de Vendas")]
    [SerializeField] private int salesGoal = 5;

    [Header("Fade")]
    [SerializeField] private float delayBeforeFade = 1f;
    [SerializeField] private float fadeDuration = 0.5f;

    private bool counterObjectiveCompleted = false;
    private bool firstCustomerObjective = true;

    private int salesCount = 0;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        objectiveText.text = "☐ Vá para trás do balcão";
    }

    private void Update()
    {
        if (counterObjectiveCompleted)
            return;

        if (playerServiceArea != null &&
            playerServiceArea.isInServiceArea)
        {
            counterObjectiveCompleted = true;

            objectiveText.text = "✓ Vá para trás do balcão";

            StartCoroutine(ShowFirstCustomerObjective());
        }
    }

    private IEnumerator ShowFirstCustomerObjective()
    {
        yield return new WaitForSeconds(delayBeforeFade);

        yield return StartCoroutine(FadeOut());

        objectiveText.text = "☐ Atenda seu primeiro cliente";

        yield return StartCoroutine(FadeIn());
    }

    // Chamado quando uma venda é realizada com sucesso.
    public void RegisterSale()
    {
        salesCount++;

        // Primeira venda.
        if (firstCustomerObjective)
        {
            firstCustomerObjective = false;

            StartCoroutine(ShowSalesObjective());
            return;
        }

        // Próximas vendas.
        UpdateSalesText();
    }

    private IEnumerator ShowSalesObjective()
    {
        yield return new WaitForSeconds(delayBeforeFade);

        yield return StartCoroutine(FadeOut());

        UpdateSalesText();

        yield return StartCoroutine(FadeIn());
    }

    private void UpdateSalesText()
    {
        objectiveText.text =
            $"☐ Venda {salesGoal} instrumentos. ({salesCount}/{salesGoal})";
    }

    private IEnumerator FadeOut()
    {
        Color originalColor = objectiveText.color;
        float timer = 0f;

        while (timer < fadeDuration)
        {
            timer += Time.deltaTime;

            float alpha = Mathf.Lerp(
                1f,
                0f,
                timer / fadeDuration
            );

            objectiveText.color = new Color(
                originalColor.r,
                originalColor.g,
                originalColor.b,
                alpha
            );

            yield return null;
        }

        objectiveText.color = new Color(
            originalColor.r,
            originalColor.g,
            originalColor.b,
            0f
        );
    }

    private IEnumerator FadeIn()
    {
        Color color = objectiveText.color;

        float timer = 0f;

        while (timer < fadeDuration)
        {
            timer += Time.deltaTime;

            float alpha = Mathf.Lerp(
                0f,
                1f,
                timer / fadeDuration
            );

            objectiveText.color = new Color(
                color.r,
                color.g,
                color.b,
                alpha
            );

            yield return null;
        }

        // Garante que o texto fique totalmente visível.
        objectiveText.color = new Color(
            color.r,
            color.g,
            color.b,
            1f
        );
    }
}