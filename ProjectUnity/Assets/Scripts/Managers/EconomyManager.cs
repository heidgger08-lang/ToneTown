using System;
using UnityEngine;

// Controla toda a economia do jogo.
public class EconomyManager : MonoBehaviour
{
    // Singleton.
    public static EconomyManager Instance;

    // Evento disparado sempre que o dinheiro mudar.
    public static event Action<int> OnMoneyChanged;

    [Header("Economia")]
    [SerializeField] private int money = 5000;

    [SerializeField] private int maxMoney = 10000000;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        // Atualiza a HUD quando o jogo começa.
        OnMoneyChanged?.Invoke(money);
    }

    // Adiciona dinheiro.
    public void AddMoney(int amount)
    {
        money += amount;

        // Limita ao valor máximo.
        money = Mathf.Min(money, maxMoney);

        Debug.Log($"Recebeu R$ {amount}");
        Debug.Log($"Dinheiro atual: R$ {money}");

        // Atualiza a HUD.
        OnMoneyChanged?.Invoke(money);
    }

    // Remove dinheiro.
    public bool SpendMoney(int amount)
    {
        if (money < amount)
        {
            Debug.Log("Dinheiro insuficiente.");
            return false;
        }

        money -= amount;

        Debug.Log($"Gastou R$ {amount}");
        Debug.Log($"Dinheiro atual: R$ {money}");

        // Atualiza a HUD.
        OnMoneyChanged?.Invoke(money);

        return true;
    }

    // Retorna o dinheiro atual.
    public int GetMoney()
    {
        return money;
    }

    // Retorna o dinheiro máximo.
    public int GetMaxMoney()
    {
        return maxMoney;
    }
}