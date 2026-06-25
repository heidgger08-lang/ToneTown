using UnityEngine;

[CreateAssetMenu(fileName = "New Instrument",
menuName = "Tone Town/Instrument")]
public class InstrumentData : ScriptableObject
{
    [Header("Informações")]

    public string instrumentName;

    public Sprite icon;

    [TextArea]
    public string description;

    [Header("Características")]

    public MusicGenre mainGenre;

    [Range(1, 10)]
    public int quality = 5;

    [Range(1, 10)]
    public int difficulty = 5;

    [Range(1, 10)]
    public int popularity = 5;
}