using UnityEngine;

// Controla o painel de recomendação.
public class RecommendationUI : MonoBehaviour
{
    // Painel da recomendação.
    [SerializeField] private GameObject recommendationPanel;

    // Cliente atual.
    private NPCController currentNPC;

    // Indica se a tela está aberta.
    private bool isOpen = false;

    // Abre o painel.
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

    // Verifica se está aberto.
    public bool IsOpen()
    {
        return isOpen;
    }

    // Jogador escolheu um instrumento.
    public void SelectInstrument(string selectedInstrument)
    {
        if (currentNPC == null)
        {
            return;
        }

        // Verifica se acertou.
        if (selectedInstrument == currentNPC.GetDesiredInstrument())
        {
            Debug.Log("Venda realizada!");
        }
        else
        {
            Debug.Log("Cliente saiu sem comprar.");
        }

        // Fecha o painel.
        CloseRecommendation();

        // Faz o cliente ir embora.
        currentNPC.FinishService();

        // Limpa referência.
        currentNPC = null;
    }
}