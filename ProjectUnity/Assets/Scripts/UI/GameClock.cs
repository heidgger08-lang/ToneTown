using UnityEngine;
using TMPro;

public class GameClock : MonoBehaviour
{
    public TMP_Text clockText;

    
    [Range(0, 23)]
    public int startHour = 8;

    [Range(0, 59)]
    public int startMinute = 0;

    
    
    public float gameMinutesPerRealMinute = 5f;

    private float currentMinutes;

    void Start()
    {
        currentMinutes = startHour * 60 + startMinute;
        UpdateClockUI();
    }

    void Update()
    {
        
        float gameMinutesPerSecond = gameMinutesPerRealMinute / 60f;

        currentMinutes += gameMinutesPerSecond * Time.deltaTime;

       
        if (currentMinutes >= 1440)
            currentMinutes -= 1440;

        UpdateClockUI();
    }

    void UpdateClockUI()
    {
        int hour = Mathf.FloorToInt(currentMinutes / 60);
        int minute = Mathf.FloorToInt(currentMinutes % 60);

        clockText.text = $"{hour:00}:{minute:00}";
    }
}