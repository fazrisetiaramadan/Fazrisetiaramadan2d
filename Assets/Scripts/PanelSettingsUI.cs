using UnityEngine;
using UnityEngine.UI;

public class PanelSettingsUI : MonoBehaviour
{
    public GameObject settingsPanel; 
    public Button closeButton; 

    void Start()
    {
        settingsPanel.SetActive(false); 
        closeButton.onClick.AddListener(CloseSettings);
    }

    public void OpenSettings()
    {
        settingsPanel.SetActive(true); 
    }

    public void CloseSettings()
    {
        settingsPanel.SetActive(false); 
    }
}
