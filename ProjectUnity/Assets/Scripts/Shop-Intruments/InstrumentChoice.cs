using UnityEngine;
using UnityEngine.EventSystems;

public class InstrumentChoice : MonoBehaviour, IPointerClickHandler
{
    [Header("Instrumento")]
    [SerializeField] private InstrumentData instrumentData;

    [Header("Referências")]
    [SerializeField] private RecommendationUI recommendationUI;

    public void OnPointerClick(PointerEventData eventData)
    {
        if (recommendationUI == null)
            return;

        recommendationUI.SelectInstrument(instrumentData);
    }
}