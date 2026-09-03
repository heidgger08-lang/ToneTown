using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PurchaseItem : MonoBehaviour
{
    [Header("Instrumento")]
    public InstrumentData instrumentData;

    [Header("Referências da UI")]
    public Image instrumentImage;
    public TMP_Text instrumentName;
    public TMP_Text instrumentDescription;
    public TMP_Text priceText;
    public Button orderButton;

    private void Start()
    {
        UpdateUI();

        if (orderButton != null)
        {
            orderButton.onClick.AddListener(OnOrderClicked);
        }
    }

    // Preenche o anúncio com os dados do instrumento.
    private void UpdateUI()
    {
        if (instrumentData == null)
        {
            Debug.LogWarning("PurchaseItem: Nenhum InstrumentData foi definido.");
            return;
        }

        if (instrumentName != null)
        {
            instrumentName.text = instrumentData.instrumentName;
        }

        if (instrumentDescription != null)
        {
            instrumentDescription.text = instrumentData.description;
        }

        if (priceText != null)
        {
            priceText.text = $"R$ {instrumentData.purchasePrice:N0}";
        }

        if (instrumentImage != null)
        {
            instrumentImage.sprite = instrumentData.icon;
            instrumentImage.preserveAspect = true;
        }
    }

    // Executado quando o jogador clica em ORDER.
    private void OnOrderClicked()
    {
        if (instrumentData == null)
        {
            Debug.LogWarning(
                "PurchaseItem: Não há instrumento definido para este pedido."
            );
            return;
        }

        if (EconomyManager.Instance == null)
        {
            Debug.LogError(
                "PurchaseItem: EconomyManager não encontrado na cena."
            );
            return;
        }

        if (InventoryManager.Instance == null)
        {
            Debug.LogError(
                "PurchaseItem: InventoryManager não encontrado na cena."
            );
            return;
        }

        // Tenta pagar pelo instrumento.
        bool purchaseSuccessful =
            EconomyManager.Instance.SpendMoney(instrumentData.purchasePrice);

        // Se não tiver dinheiro, não adiciona ao estoque.
        if (!purchaseSuccessful)
        {
            Debug.Log("Compra não realizada: dinheiro insuficiente.");
            return;
        }

        // Adiciona o instrumento ao estoque.
        InventoryManager.Instance.AddInstrument(instrumentData);

        Debug.Log(
            $"Compra realizada: {instrumentData.instrumentName}"
        );
    }
}