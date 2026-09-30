using UnityEngine;

public class InstrumentDisplay : MonoBehaviour
{
    [Header("Instrumento")]
    [SerializeField] private InstrumentData instrumentData;

    public InstrumentData GetInstrumentData()
    {
        return instrumentData;
    }

    public void AtualizarVisual()
    {
        if (instrumentData == null)
            return;

        if (InventoryManager.Instance == null)
            return;

        int quantidade =
            InventoryManager.Instance.GetQuantity(instrumentData);

        gameObject.SetActive(quantidade > 0);
    }
}
