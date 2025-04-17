using UnityEngine;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour
{
    public int heartPrice = 10;
    public Text coinText;
    public GameObject shopPanel;

    private HealthManager healthManager;

    void Start()
    {
        healthManager = FindObjectOfType<HealthManager>();
        UpdateCoinUI();

        shopPanel.SetActive(false); 
    }

    public void OpenShop()
    {
        shopPanel.SetActive(true); 
    }

    public void CloseShop()
    {
        shopPanel.SetActive(false); 

    public void BuyHeart()
    {
        int currentCoins = PlayerPrefs.GetInt("TotalCoins", 0);

        if (currentCoins >= heartPrice)
        {
            currentCoins -= heartPrice;
            PlayerPrefs.SetInt("TotalCoins", currentCoins);
            UpdateCoinUI();

            if (healthManager != null)
            {
                if (healthManager.IsCooldownActive())
                {
                    healthManager.StopCooldown();
                }

                healthManager.AddHealth(1);
                healthManager.UpdateUIExternal();
            }

            CloseShop(); 
        }
    }

    private void UpdateCoinUI()
    {
        int currentCoins = PlayerPrefs.GetInt("TotalCoins", 0);
        coinText.text = currentCoins.ToString();
    }
}
