using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelButton : MonoBehaviour
{
    public int levelIndex; // Nomor level yang akan dibuka

    public void LoadLevel()
    {
        HealthManager healthManager = FindObjectOfType<HealthManager>();

        if (healthManager != null && healthManager.currentHealth > 0)
        {
            healthManager.UseHealth(); // Mengurangi health sebelum masuk level
            SceneManager.LoadScene("Level" + levelIndex); // Memulai level
        }
        else
        {
            Debug.Log("Health habis! Tunggu cooldown.");
            // Bisa ditambahkan UI pop-up untuk memberitahu pemain
        }
    }
}