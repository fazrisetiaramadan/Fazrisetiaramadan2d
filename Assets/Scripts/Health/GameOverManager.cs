using UnityEngine;

public class GameOverManager : MonoBehaviour
{
    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private Health playerHealth;

    private void Start()
    {
        if (gameOverPanel != null)
            gameOverPanel.SetActive(false);

        if (playerHealth != null)
            playerHealth.onDeath += ShowGameOver; // Subscribe event
    }

    private void ShowGameOver()
    {
        if (gameOverPanel != null)
        {
            Invoke("EnableGameOverPanel", 1.5f); // Delay sebelum menampilkan panel
        }
    }

    private void EnableGameOverPanel()
    {
        gameOverPanel.SetActive(true);
    }

    private void OnDestroy()
    {
        if (playerHealth != null)
            playerHealth.onDeath -= ShowGameOver; // Unsubscribe event
    }
}
