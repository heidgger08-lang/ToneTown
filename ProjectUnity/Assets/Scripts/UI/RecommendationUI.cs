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

        if (selectedInstrument == null)
        {
            Debug.LogWarning("Nenhum instrumento foi selecionado.");
            return;
        }

        if (InventoryManager.Instance == null)
        {
            Debug.LogError("InventoryManager não encontrado na cena.");
            return;
        }

        if (EconomyManager.Instance == null)
        {
            Debug.LogError("EconomyManager não encontrado na cena.");
            return;
        }

        InstrumentData desiredInstrument =
            currentNPC.GetDesiredInstrument();

        // Verifica se o instrumento está no estoque.
        int stock =
            InventoryManager.Instance.GetQuantity(selectedInstrument);

        if (stock <= 0)
        {
            NotificationManager.Instance.Show(
                "SOLD OUT",
                Color.red
            );

            Debug.Log(
                $"{selectedInstrument.instrumentName} está SOLD OUT."
            );

            return;
        }

        // Acertou a recomendação.
        if (selectedInstrument == desiredInstrument)
        {
            // Remove o instrumento do estoque.
            bool removed =
                InventoryManager.Instance.RemoveInstrument(
                    selectedInstrument
                );

            if (!removed)
            {
                return;
            }

            // Adiciona o dinheiro da venda.
            EconomyManager.Instance.AddMoney(
                selectedInstrument.price
            );

            NotificationManager.Instance.Show(
                $"+R$ {selectedInstrument.price:N0}",
                Color.green
            );

            Debug.Log(
                $"Venda realizada: {selectedInstrument.instrumentName}"
            );

            // Fecha o painel.
            CloseRecommendation();

            // Faz o cliente ir embora.
            currentNPC.FinishService();

            // Limpa a referência.
            currentNPC = null;
        }
        // Errou a recomendação.
        else
        {
            NotificationManager.Instance.Show(
                "Venda recusada",
                Color.red
            );

            Debug.Log("Cliente saiu sem comprar.");

            // Fecha o painel.
            CloseRecommendation();

            // Faz o cliente ir embora.
            currentNPC.FinishService();

            // Limpa a referência.
            currentNPC = null;
        }
    }
}