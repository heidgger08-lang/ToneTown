using System;
using System.Collections.Generic;
using UnityEngine;

// Controla o estoque de instrumentos do jogador.
public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance;

    // Guarda a quantidade de cada instrumento.
    private Dictionary<InstrumentData, int> inventory =
        new Dictionary<InstrumentData, int>();

    // Evento disparado quando o estoque muda.
    public static event Action OnInventoryChanged;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Adiciona um instrumento ao estoque.
    public void AddInstrument(InstrumentData instrument)
    {
        if (instrument == null)
        {
            Debug.LogWarning("InventoryManager: Instrumento inválido.");
            return;
        }

        if (inventory.ContainsKey(instrument))
        {
            inventory[instrument]++;
        }
        else
        {
            inventory.Add(instrument, 1);
        }

        Debug.Log(
            $"Estoque: {instrument.instrumentName} x{inventory[instrument]}"
        );

        OnInventoryChanged?.Invoke();
    }

    // Remove um instrumento do estoque.
    public bool RemoveInstrument(InstrumentData instrument)
    {
        if (instrument == null)
        {
            Debug.LogWarning("InventoryManager: Instrumento inválido.");
            return false;
        }

        if (!inventory.ContainsKey(instrument))
        {
            Debug.LogWarning(
                $"Não há {instrument.instrumentName} no estoque."
            );

            return false;
        }

        if (inventory[instrument] <= 0)
        {
            Debug.LogWarning(
                $"{instrument.instrumentName} está SOLD OUT."
            );

            return false;
        }

        inventory[instrument]--;

        Debug.Log(
            $"Estoque: {instrument.instrumentName} x{inventory[instrument]}"
        );

        OnInventoryChanged?.Invoke();

        return true;
    }

    // Retorna a quantidade de determinado instrumento.
    public int GetQuantity(InstrumentData instrument)
    {
        if (instrument == null)
        {
            return 0;
        }

        if (inventory.TryGetValue(instrument, out int quantity))
        {
            return quantity;
        }

        return 0;
    }
}