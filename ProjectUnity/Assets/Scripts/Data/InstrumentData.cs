using UnityEngine;

[CreateAssetMenu(fileName = "New Instrument", menuName = "Tone Town/Instrument")]
public class InstrumentData : ScriptableObject
{
    [Header("Informações")]
    public string instrumentName;

    [TextArea(3, 5)]
    public string description;

    public Sprite icon;

    [Header("Características")]
    public MusicGenre mainGenre;

    [Range(1, 10)]
    public int quality = 5;

    [Range(1, 10)]
    public int difficulty = 5;

    [Range(1, 10)]
    public int popularity = 5;

    [Header("Economia")]
    public int price = 1000;
}