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

        shopPanel.SetActive(false); // Panel shop tidak langsung tampil
    }

    public void OpenShop()
    {
        shopPanel.SetActive(true); // Tampilkan panel shop
    }

    public void CloseShop()
    {
        shopPanel.SetActive(false); // Sembunyikan panel shop
    }

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

            CloseShop(); // Setelah beli, langsung tutup shop
        }
    }

    private void UpdateCoinUI()
    {
        int currentCoins = PlayerPrefs.GetInt("TotalCoins", 0);
        coinText.text = currentCoins.ToString();
    }
}
