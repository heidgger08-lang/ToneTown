using UnityEngine;
using UnityEngine.EventSystems;

public class InstrumentChoice : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private string instrumentName;

    private RecommendationUI recommendationUI;

    private void Start()
    {
        recommendationUI = FindObjectOfType<RecommendationUI>();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        recommendationUI.SelectInstrument(instrumentName);
    }
}