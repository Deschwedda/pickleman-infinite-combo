using TMPro;
using UnityEngine;

public class Shop : MonoBehaviour
{
      
    [Header("Shop Settings")]
    private float fixedYPosition;
    public GameObject shopUI;
    public GameManager gameManagerSc;
    public int priceMultiplier = 750;
    private int turretPrice;
    private int turretUpgradePrice;
    private int platformUpgradePrice;
    private int platformPrice;
    private int spawner1Price;
    private int spawner2Price;
    private int spawner3Price;
    private int proximityPrice;
    private bool haveSpawner3;


    [Header("Spawner Levels // Items bought")]
    public int spawner1Level = 1;
    public int spawner2Level = 1;
    public int spawner3Level = 1;
    public int turretLevel = 1;
    public int platformLevel = 1;
    public int turretsBought = 0;
    public int platformsBought = 0;
    public int proximityBought = 0;

    [Header("Items")]
    public GameObject itemPrefab;
    public GameObject currentItem;
    public GameObject turretPrefab;
    public GameObject platformPrefab;
    public GameObject proximityPrefab;
    public GameObject spawner1;
    public GameObject spawner2;
    public GameObject spawner3;

    [Header("Other")]    
    public TextMeshProUGUI goldCounter;
    public TextMeshProUGUI turretPriceText;
    public TextMeshProUGUI platformPriceText;
    public TextMeshProUGUI proximityPriceText;
    public TextMeshProUGUI spawner1PriceText;
    public TextMeshProUGUI spawner2PriceText;
    public TextMeshProUGUI spawner3PriceText;
    public TextMeshProUGUI spawner3BuyPrice;
    public TextMeshProUGUI turretUpgradePriceText;
    public TextMeshProUGUI platformUpgradePriceText;
    public GameObject How2PlayScreen;
    AudioSource clickSource;

    


    void Start()
    {
        GameObject gameManagerObject = GameObject.Find("GameManager");
        gameManagerSc = gameManagerObject.GetComponent<GameManager>();
        clickSource = GetComponent<AudioSource>();
        How2PlayScreen.SetActive(true);
    }

   
    void Update()
    {
        PlaceItem();
    }

    public void PlaceItem()
    {
        
        if (currentItem != null)
        {
            
            // Convert mouse position to world position
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePosition.z = 0;
            mousePosition.y = fixedYPosition;

            currentItem.transform.position = mousePosition;

            if (Input.GetMouseButtonDown(0))
            {
                clickSource.Play();
                currentItem = null;
                OpenShop();
            }

        }
    }

    public void UpgradeSpawner1()
    {
        clickSource.Play();
        Spawner spawner1Sc = spawner1.GetComponent<Spawner>();
        if (spawner1Sc.spawnRate <= 0.1f)
        {
            spawner1PriceText.text = "Max";
        }

        else
        {
            spawner1Price = spawner1Level * priceMultiplier;
            if (gameManagerSc.currentGold >= spawner1Price)
            {
                gameManagerSc.currentGold -= spawner1Price;
                UpdateGoldAmount();
                spawner1Sc.spawnRate -= 0.1f;
                spawner1Level++;
                spawner1Price = spawner1Level * priceMultiplier;
                spawner1PriceText.text = spawner1Price.ToString();
            }

        }
        
    }

    public void UpgradeSpawner2()
    {
        clickSource.Play();
        Spawner spawner2Sc = spawner2.GetComponent<Spawner>();
        if (spawner2Sc.spawnRate <= 0.1f)
        {
            spawner2PriceText.text = "Max";
        }
        else
        {
            spawner2Price = spawner2Level * priceMultiplier;
            if (gameManagerSc.currentGold >= spawner2Price)
            {                
                gameManagerSc.currentGold -= spawner2Price;
                UpdateGoldAmount();
                spawner2Sc.spawnRate -= 0.1f;
                spawner2Level++;
                spawner2Price = spawner2Level * priceMultiplier;
                spawner2PriceText.text = spawner2Price.ToString();
            }
            
        }
    }

    public void UpgradeSpawner3()
    {
        clickSource.Play();
        Spawner spawner3Sc = spawner3.GetComponent<Spawner>();
        if (spawner3Sc.spawnRate <= 0.1f)
        {
            spawner3PriceText.text = "Max";
        }
        else
        {
            if (haveSpawner3 == false)
            { return; }

            else
            {
                spawner3Price = spawner3Level * priceMultiplier;
                if (gameManagerSc.currentGold >= spawner3Price)
                {
                    gameManagerSc.currentGold -= spawner3Price;
                    UpdateGoldAmount();
                    spawner3Sc.spawnRate -= 0.1f;
                    spawner3Level++;
                    spawner3Price = spawner3Level * priceMultiplier;
                    spawner3PriceText.text = spawner3Price.ToString();
                }
            }
        }
    }

    public void UpgradeTurrets()
    {
        clickSource.Play();
        turretUpgradePrice = turretLevel * (priceMultiplier * 2);
        if (gameManagerSc.currentGold >= turretUpgradePrice)
        {
            if (gameManagerSc.turretAttackRateMultiplier <= 0.1f)
            {
                turretUpgradePriceText.text = "Max";
            }
            else
            {
                gameManagerSc.currentGold -= turretUpgradePrice;
                UpdateGoldAmount();
                gameManagerSc.turretAttackRateMultiplier -= 0.1f;
                turretLevel++;
                turretUpgradePrice = turretLevel * (priceMultiplier * 2 );
                turretUpgradePriceText.text = turretUpgradePrice.ToString();
            }

        }
    }

    public void UpgradePlatforms()
    {
        clickSource.Play();
        platformUpgradePrice = platformLevel * (priceMultiplier * 2);
        if (gameManagerSc.currentGold >= platformUpgradePrice)
        {
            if (gameManagerSc.platformCDMultiplier <= 0.1f)
            {
                platformUpgradePriceText.text = "Max";
            }

            else
            {
                gameManagerSc.currentGold -= platformUpgradePrice;
                UpdateGoldAmount();
                gameManagerSc.platformCDMultiplier -= 0.1f;
                platformLevel++;
                platformUpgradePrice = platformLevel * (priceMultiplier * 2);
                platformUpgradePriceText.text = platformUpgradePrice.ToString();
            }

        }
    }

    public void BuyTurret()
    {
        clickSource.Play();
        turretPrice = turretsBought * priceMultiplier;
        if (gameManagerSc.currentGold >= turretsBought * priceMultiplier)
        {
            CloseShop();
            gameManagerSc.currentGold -= turretsBought * priceMultiplier;
            UpdateGoldAmount();            
            currentItem = Instantiate(turretPrefab);
            fixedYPosition = 3.98f;
            turretsBought++;
            turretPrice = turretsBought * priceMultiplier;
            turretPriceText.text = turretPrice.ToString();
        }               
    }

    public void BuyPlatform()
    {
        clickSource.Play();
        platformPrice = platformsBought * priceMultiplier;
        if (gameManagerSc.currentGold >= platformsBought * priceMultiplier)
        {
            CloseShop();
            gameManagerSc.currentGold -= platformsBought * priceMultiplier;
            UpdateGoldAmount();
            currentItem = Instantiate(platformPrefab);
            fixedYPosition = -4.312f;
            platformsBought++;
            platformPrice = platformsBought * priceMultiplier;
            platformPriceText.text = platformPrice.ToString();
        }
             
    }

    public void BuySpawner()
    {
        if (haveSpawner3)
        {
            spawner3BuyPrice.text = "Max";
        }
        else
        {
            clickSource.Play();
            if (spawner3.gameObject.activeSelf == false && gameManagerSc.currentGold >= 5000)
            {
                gameManagerSc.currentGold -= 5000;
                UpdateGoldAmount();
                spawner3.gameObject.SetActive(true);
                haveSpawner3 = true;
            }

        }

    }

    public void BuyProximity()
    {
        clickSource.Play();
        proximityPrice = proximityBought * priceMultiplier;
        if (gameManagerSc.currentGold >= proximityBought * priceMultiplier)
        {
            gameManagerSc.currentGold -= proximityBought * priceMultiplier;
            UpdateGoldAmount();
            currentItem = Instantiate(proximityPrefab);
            fixedYPosition = -4.275f;
            proximityBought++;
            proximityPrice = proximityBought * priceMultiplier;
            proximityPriceText.text = proximityPrice.ToString();
        }
    }

    public void OpenShop()
    {
        if (shopUI != null)
        {
            clickSource.Play();
            shopUI.SetActive(true);
        }
    }

    public void CloseShop()
    {
        if (shopUI != null) 
        {
            clickSource.Play();
            shopUI.SetActive(false);
        }
    }

    public void UpdateGoldAmount()
    {
        goldCounter.text = gameManagerSc.currentGold.ToString();
    }


    public void ActivateHow2PlayScreen()
    {
        clickSource.Play();
        How2PlayScreen.SetActive(true);
    }

    public void DeactivateHow2PlayScreen()
    {
        clickSource.Play();
        How2PlayScreen.SetActive(false);
    }

}
