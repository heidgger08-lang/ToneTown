using TMPro;
using UnityEngine;

public class InstrumentStockUI : MonoBehaviour
{
    [Header("Instrumento")]
    [SerializeField] private InstrumentData instrumentData;

    [Header("Texto")]
    [SerializeField] private TMP_Text stockText;

    private void OnEnable()
    {
        InventoryManager.OnInventoryChanged += AtualizarEstoque;
        AtualizarEstoque();
    }

    private void OnDisable()
    {
        InventoryManager.OnInventoryChanged -= AtualizarEstoque;
    }

    private void AtualizarEstoque()
    {
        if (instrumentData == null)
            return;

        if (stockText == null)
            return;

        if (InventoryManager.Instance == null)
            return;

        int quantidade =
            InventoryManager.Instance.GetQuantity(instrumentData);

        stockText.text = $"Estoque: {quantidade}";
    }
}
