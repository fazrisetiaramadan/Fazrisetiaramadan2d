using UnityEngine;

public class ShopManager : MonoBehaviour
{
    public GameObject shopPanel;

    public void OpenShop()
    {
        shopPanel.SetActive(true);
    }

    public void CloseShop()
    {
        shopPanel.SetActive(false);
    }
}
