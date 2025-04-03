using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HealthManager : MonoBehaviour
{
    public int maxHealth = 3;
    public int currentHealth;
    public float cooldownTime = 240f; // 4 menit dalam detik
    public Text healthText;
    public Text cooldownText;

    private float cooldownTimer;
    private bool isCooldownActive;

    void Start()
    {
        currentHealth = PlayerPrefs.GetInt("Health", maxHealth);
        cooldownTimer = PlayerPrefs.GetFloat("CooldownTimer", 0);
        isCooldownActive = PlayerPrefs.GetInt("IsCooldownActive", 0) == 1;
        UpdateHealthUI();

        if (currentHealth == 0 && isCooldownActive)
        {
            StartCoroutine(CooldownCoroutine());
        }
    }

    public void UseHealth()
    {
        if (currentHealth > 0)
        {
            currentHealth--;
            PlayerPrefs.SetInt("Health", currentHealth);
            UpdateHealthUI();
            if (currentHealth == 0)
            {
                StartCooldown();
            }
        }
    }

    private void StartCooldown()
    {
        isCooldownActive = true;
        PlayerPrefs.SetInt("IsCooldownActive", 1);
        StartCoroutine(CooldownCoroutine());
    }

    private IEnumerator CooldownCoroutine()
    {
        cooldownTimer = cooldownTime;
        PlayerPrefs.SetFloat("CooldownTimer", cooldownTimer);
        healthText.gameObject.SetActive(false); // Sembunyikan health saat cooldown
        while (cooldownTimer > 0)
        {
            yield return new WaitForSeconds(1);
            cooldownTimer -= 1;
            PlayerPrefs.SetFloat("CooldownTimer", cooldownTimer);
            cooldownText.text = Mathf.Ceil(cooldownTimer).ToString();
        }
        isCooldownActive = false;
        PlayerPrefs.SetInt("IsCooldownActive", 0);
        currentHealth = maxHealth;
        PlayerPrefs.SetInt("Health", currentHealth);
        UpdateHealthUI();
    }

    private void UpdateHealthUI()
    {
        healthText.text = currentHealth.ToString();
        healthText.gameObject.SetActive(currentHealth > 0);
        cooldownText.gameObject.SetActive(currentHealth == 0);
    }
}