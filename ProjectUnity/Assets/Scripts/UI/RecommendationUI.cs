using UnityEngine;

public class RecommendationUI : MonoBehaviour
{
    [Header("Painel")]
    [SerializeField] private GameObject recommendationPanel;

    private NPCController currentNPC;

    private bool isOpen;

    public void OpenRecommendation(NPCController npc)
    {
        currentNPC = npc;

        recommendationPanel.SetActive(true);

        isOpen = true;
    }

    public void CloseRecommendation()
    {
        recommendationPanel.SetActive(false);

        isOpen = false;
    }

    public bool IsOpen()
    {
        return isOpen;
    }

    public void SelectInstrument(InstrumentData selectedInstrument)
    {
        if (currentNPC == null)
            return;

        InstrumentData desiredInstrument =
            currentNPC.GetDesiredInstrument();

        if (selectedInstrument == desiredInstrument)
        {
            Debug.Log("Venda realizada!");
        }
        else
        {
            Debug.Log("Cliente saiu sem comprar.");
        }

        CloseRecommendation();

        currentNPC.FinishService();

        currentNPC = null;
    }
}