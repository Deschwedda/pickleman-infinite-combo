using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class GameManager : MonoBehaviour
{
    
    public int comboCount;
    private int lastComboCount;
    private float lastComboTime;
    private float comboResetTime = 3f;
    public int currentGold;
    public int earnedGoldAmount;
    public bool isComboActive;
    AudioSource clickSource;

    [Header("UI")]
    public TextMeshProUGUI comboTextMiddle;
    public TextMeshProUGUI comboCounter;
    public TextMeshProUGUI goldCounter;
    public TextMeshProUGUI notificationScreen;
    public GameObject notificationPanelHolder;
    public float notificationScreenShowTime = 2f;


    [Header("UI Achievements")]
    public TextMeshProUGUI bronzeText;
    public Image bronzeImage;

    public TextMeshProUGUI silverText;
    public Image silverImage;

    public TextMeshProUGUI goldText;
    public Image goldImage;

    public TextMeshProUGUI diamondText;
    public Image diamondImage;

    public GameObject bronzeUnlocked;
    public GameObject silverUnlocked;
    public GameObject goldUnlocked;
    public GameObject diamondUnlocked;

    [Header("Multipliers")]
    public float turretAttackRateMultiplier = 1f;
    public float platformCDMultiplier = 1f;


    void Start()
    {
        clickSource = GetComponent<AudioSource>();
        comboCount = 0;
        lastComboCount = 0;
             
        

    }
        
    void Update()
    {
        ResetCombo();
    }

    public void AddPoints()
    {
        lastComboTime = Time.time;
        comboCount++;
        comboCounter.text = comboCount.ToString();
        comboTextMiddle.text = comboCount.ToString();

        if (comboCount == 1000)
        {
            ActivateBronze();
        }
        else if (comboCount == 10000)
        {
            ActivateSilver();
        }
        else if (comboCount == 100000)
        {
            ActivateGold();
        }
        else if (comboCount == 1000000)
        {
            ActivateDiamond();
        }
              
    }
    
    private void ResetCombo()
    {
        if (Time.time - lastComboTime >= comboResetTime && comboCount > 0)
        {
            lastComboCount = comboCount;            
            comboCount = 0;
            comboTextMiddle.text = "";
            comboCounter.text = comboCount.ToString();
            AddGold();
        }
    }

    private void AddGold()
    {
        if (lastComboCount > 0) 
        {
            earnedGoldAmount = (lastComboCount * 50);
            currentGold += earnedGoldAmount;
            goldCounter.text = currentGold.ToString();
            CallNotificationScreen();
            Invoke(nameof(HideNotificationScreen), notificationScreenShowTime);
        }
       
    }

    private void CallNotificationScreen()
    {
        notificationPanelHolder.SetActive(true);
        notificationScreen.text = "You earned " + earnedGoldAmount + " gold for " + lastComboCount + " combo(s).";
    }

    private void HideNotificationScreen()
    {
        notificationPanelHolder.SetActive(false);
    }   

    public void EndCombo()
    {
        clickSource.Play();
        if (isComboActive)
        {
            isComboActive = false;
        }
        
    }

    private void ActivateBronze()
    {
        ActivateAchievementText(bronzeUnlocked);
        Color imageColor = bronzeImage.color;
        imageColor.a = 1;
        bronzeImage.color = imageColor;
                
        Color textColor = bronzeText.color;
        textColor.a = 1;
        bronzeText.color = textColor;

        
    }

    private void ActivateSilver()
    {
        ActivateAchievementText(silverUnlocked);
        Color imageColor = silverImage.color;
        imageColor.a = 1;
        silverImage.color = imageColor;

        Color textColor = silverText.color;
        textColor.a = 1;
        silverText.color = textColor;
    }

    private void ActivateGold()
    {
        ActivateAchievementText(goldUnlocked);
        Color imageColor = goldImage.color;
        imageColor.a = 1;
        goldImage.color = imageColor;

        Color textColor = goldText.color;
        textColor.a = 1;
        goldText.color = textColor;

    }

    private void ActivateDiamond()
    {
        ActivateAchievementText(diamondUnlocked);
        Color imageColor = diamondImage.color;
        imageColor.a = 1;
        diamondImage.color = imageColor;

        Color textColor = diamondText.color;
        textColor.a = 1;
        diamondText.color = textColor;
    }

    public void ActivateAchievementText(GameObject achievementText)
    {
        StartCoroutine(ActivateAndDeactivate(achievementText, 2f));
    }

    private IEnumerator ActivateAndDeactivate(GameObject achievementText, float delay)
    {
        achievementText.SetActive(true);
        yield return new WaitForSeconds(delay);
        achievementText.SetActive(false);
    }


    
}
