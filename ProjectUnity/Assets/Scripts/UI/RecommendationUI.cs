using UnityEngine;

public class RecommendationUI : MonoBehaviour
{
    [Header("Painel")]
    [SerializeField] private GameObject recommendationPanel;

    private NPCController currentNPC;

    private bool isOpen;

    // Abre o painel de recomendação.
    public void OpenRecommendation(NPCController npc)
    {
        currentNPC = npc;

        recommendationPanel.SetActive(true);

        isOpen = true;
    }

    // Fecha o painel.
    public void CloseRecommendation()
    {
        recommendationPanel.SetActive(false);

        isOpen = false;
    }

    // Retorna se o painel está aberto.
    public bool IsOpen()
    {
        return isOpen;
    }

    // Jogador escolheu um instrumento.
    public void SelectInstrument(InstrumentData selectedInstrument)
    {
        if (currentNPC == null)
            return;

        InstrumentData desiredInstrument =
            currentNPC.GetDesiredInstrument();

        // Acertou a recomendação.
        if (selectedInstrument == desiredInstrument)
        {
            EconomyManager.Instance.AddMoney(selectedInstrument.price);

            NotificationManager.Instance.Show(
                $"+R$ {selectedInstrument.price:N0}",
                Color.green
            );

            Debug.Log("Venda realizada!");
        }
        // Errou a recomendação.
        else
        {
            NotificationManager.Instance.Show(
                "Venda recusada",
                Color.red
            );

            Debug.Log("Cliente saiu sem comprar.");
        }

        // Fecha o painel.
        CloseRecommendation();

        // Faz o cliente ir embora.
        currentNPC.FinishService();

        // Limpa a referência.
        currentNPC = null;
    }
}