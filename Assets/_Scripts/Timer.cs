using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    public float timeElapsed = 0f;
    public bool timerIsRunning = false;
    public TextMeshProUGUI timerText;
  
    // Update is called once per frame
    void Update()
    {
        if (timerIsRunning)
        {
            timeElapsed += Time.deltaTime;
            UpdateTimerDisplay();
        }
    }

    void UpdateTimerDisplay()
    {
        int hours = Mathf.FloorToInt(timeElapsed / 3600); // Saati hesapla
        int minutes = Mathf.FloorToInt((timeElapsed % 3600) / 60); // Dakikayý hesapla
        int seconds = Mathf.FloorToInt(timeElapsed % 60); // Saniyeyi hesapla

        // Saat, dakika ve saniyeyi formatla
        timerText.text = string.Format("{0:00}:{1:00}:{2:00}", hours, minutes, seconds);
    }
}
