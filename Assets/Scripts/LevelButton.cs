using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelButton : MonoBehaviour
{
    public int levelIndex; 

    public void LoadLevel()
    {
        HealthManager healthManager = FindObjectOfType<HealthManager>();

        if (healthManager != null && healthManager.currentHealth > 0)
        {
            healthManager.UseHealth(); 
            SceneManager.LoadScene("Level" + levelIndex); 
        }
        else
        {
            Debug.Log("Health habis! Tunggu cooldown.");
            // Bisa ditambahkan UI pop-up untuk memberitahu pemain
        }
    }
}