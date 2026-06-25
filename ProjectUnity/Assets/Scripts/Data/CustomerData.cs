using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Customer", menuName = "Tone Town/Customer")]
public class CustomerData : ScriptableObject
{
    [Header("Informações")]
    public string customerName;

    [TextArea]
    public List<string> dialogues;

    [Header("Preferências")]
    public InstrumentData desiredInstrument;
}