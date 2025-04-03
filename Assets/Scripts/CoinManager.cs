using UnityEngine;
using UnityEngine.UI;

public class CoinManager : MonoBehaviour
{
    public Text totalCoinText;

    void Start()
    {
        int totalCoins = PlayerPrefs.GetInt("TotalCoins", 0);
        totalCoinText.text = totalCoins.ToString(); // Hanya menampilkan angka
    }
}
