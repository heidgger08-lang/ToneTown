using UnityEngine;

public class ShopWallManager : MonoBehaviour
{
    [Header("Instrumentos expostos na parede")]
    [SerializeField] private InstrumentDisplay[] displays;

    private void OnEnable()
    {
        InventoryManager.OnInventoryChanged += AtualizarParede;
    }

    private void OnDisable()
    {
        InventoryManager.OnInventoryChanged -= AtualizarParede;
    }

    private void Start()
    {
        AtualizarParede();
    }

    public void AtualizarParede()
    {
        if (InventoryManager.Instance == null)
            return;

        foreach (InstrumentDisplay display in displays)
        {
            if (display != null)
            {
                display.AtualizarVisual();
            }
        }
    }
}
