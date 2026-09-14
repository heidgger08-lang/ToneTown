using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CreditsUI : MonoBehaviour
{
    [Header("Textos")]
    [SerializeField] private TMP_Text salesText;
    [SerializeField] private TMP_Text moneyText;

    private void Start()
    {
        salesText.text =
            $"Instrumentos vendidos: {DailyObjectiveManager.FinalSalesCount}";

        moneyText.text =
            $"Dinheiro ganho: R$ {DailyObjectiveManager.FinalMoneyEarned:N0}";
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}