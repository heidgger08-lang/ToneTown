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
        if (npc == null)
            return;

        currentNPC = npc;

        recommendationPanel.SetActive(true);

        isOpen = true;

        Debug.Log("Painel de recomendação aberto.");
    }

    // Fecha o painel sem finalizar o atendimento.
    public void CloseRecommendation()
    {
        recommendationPanel.SetActive(false);

        isOpen = false;

        Debug.Log(">>> PAINEL FECHADO | isOpen = " + isOpen);
    }

    // Retorna se o painel está aberto.
    public bool IsOpen()
    {
        return recommendationPanel != null &&
               recommendationPanel.activeSelf;
    }

    // Jogador escolheu um instrumento.
    public void SelectInstrument(InstrumentData selectedInstrument)
    {
        if (currentNPC == null)
        {
            Debug.LogWarning("Nenhum NPC está sendo atendido.");
            return;
        }

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
                return;

            // Adiciona o dinheiro da venda.
            EconomyManager.Instance.AddMoney(
                selectedInstrument.salePrice
            );

            // Registra a venda no objetivo diário.
            DailyObjectiveManager.Instance.RegisterSale(
                selectedInstrument.salePrice
            );

            NotificationManager.Instance.Show(
                $"+R$ {selectedInstrument.salePrice:N0}",
                Color.green
            );

            Debug.Log(
                $"Venda realizada: {selectedInstrument.instrumentName}"
            );

            // Fecha o painel.
            CloseRecommendation();

            // Finaliza o atendimento e manda o cliente embora.
            currentNPC.FinishService();

            // Agora sim não precisamos mais do NPC.
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

            // Finaliza o atendimento.
            currentNPC.FinishService();

            // Limpa a referência.
            currentNPC = null;
        }
    }
}