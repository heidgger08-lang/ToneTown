using TMPro;
using UnityEngine;

public class MoneyUI : MonoBehaviour
{
    [SerializeField] private TMP_Text moneyText;

    private void OnEnable()
    {
        EconomyManager.OnMoneyChanged += UpdateMoney;
    }

    private void OnDisable()
    {
        EconomyManager.OnMoneyChanged -= UpdateMoney;
    }

    private void Start()
    {
        if (EconomyManager.Instance != null)
        {
            UpdateMoney(EconomyManager.Instance.GetMoney());
        }
    }

    private void UpdateMoney(int money)
    {
        moneyText.text = $"R$ {money:N0}";
    }
}