using UnityEngine;
using UnityEngine.UI;

public class PanelSettingsUI : MonoBehaviour
{
    public GameObject settingsPanel; // Panel Settings
    public Button closeButton; // Tombol Close

    void Start()
    {
        settingsPanel.SetActive(false); // Panel mati saat awal
        closeButton.onClick.AddListener(CloseSettings);
    }

    public void OpenSettings()
    {
        settingsPanel.SetActive(true); // Tampilkan panel settings
    }

    public void CloseSettings()
    {
        settingsPanel.SetActive(false); // Sembunyikan panel settings
    }
}
