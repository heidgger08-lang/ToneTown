using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DailyObjectiveManager : MonoBehaviour
{
    public static DailyObjectiveManager Instance;

    [Header("UI")]
    [SerializeField] private TMP_Text objectiveText;

    [Header("Área de Atendimento")]
    [SerializeField] private PlayerServiceArea playerServiceArea;

    [Header("Objetivo de Vendas")]
    [SerializeField] private int salesGoal = 3;

    [Header("Fade")]
    [SerializeField] private float delayBeforeFade = 1f;
    [SerializeField] private float fadeDuration = 0.5f;

    private bool counterObjectiveCompleted = false;
    private bool firstCustomerObjective = true;

    private int salesCount = 0;
    private int totalMoneyEarned = 0;
    public static int FinalSalesCount;
    public static int FinalMoneyEarned;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        objectiveText.text = "☐ Compre um instrumento no jornal";
    }

    private void Update()
    {
        if (counterObjectiveCompleted)
            return;

        if (playerServiceArea != null &&
            playerServiceArea.isInServiceArea)
        {
            counterObjectiveCompleted = true;

            objectiveText.text = "✓ Compre um instrumento no jornal";

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
    public void RegisterSale(int saleAmount)
    {
        salesCount++;
        totalMoneyEarned += saleAmount;

        Debug.Log(
            $"Venda registrada: {salesCount}/{salesGoal}"
        );

        // Meta atingida.
        if (salesCount >= salesGoal)
        {
            UpdateSalesText();

            StartCoroutine(EndGame());

            return;
        }

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

    private IEnumerator EndGame()
    {
        yield return new WaitForSeconds(1.5f);

        FinalSalesCount = salesCount;
        FinalMoneyEarned = totalMoneyEarned;

        SceneManager.LoadScene("Credits");
    }

    public int GetSalesCount()
    {
        return salesCount;
    }

    public int GetTotalMoneyEarned()
    {
        return totalMoneyEarned;
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

        objectiveText.color = new Color(
            color.r,
            color.g,
            color.b,
            1f
        );
    }
}