using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HealthManager : MonoBehaviour
{
    public int maxHealth = 3;
    public int currentHealth;
    public float cooldownTime = 240f;
    public Text healthText;
    public Text cooldownText;
    public GameObject shopPanel;    
    private float cooldownTimer;
    private bool isCooldownActive;
    private Coroutine cooldownRoutine;

    void Start()
    {
        currentHealth = PlayerPrefs.GetInt("Health", maxHealth);
        cooldownTimer = PlayerPrefs.GetFloat("CooldownTimer", 0);
        isCooldownActive = PlayerPrefs.GetInt("IsCooldownActive", 0) == 1;
        UpdateHealthUI();

        if (currentHealth == 0 && isCooldownActive)
        {
            cooldownRoutine = StartCoroutine(CooldownCoroutine());
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
        cooldownRoutine = StartCoroutine(CooldownCoroutine());
    }

    private IEnumerator CooldownCoroutine()
    {
        cooldownTimer = cooldownTime;
        PlayerPrefs.SetFloat("CooldownTimer", cooldownTimer);

        cooldownText.gameObject.SetActive(true);

        while (cooldownTimer > 0)
        {
            yield return new WaitForSeconds(1);
            cooldownTimer -= 1;
            PlayerPrefs.SetFloat("CooldownTimer", cooldownTimer);
            cooldownText.text = "CoolDown : " + Mathf.CeilToInt(cooldownTimer).ToString() + "s";
        }

        isCooldownActive = false;
        PlayerPrefs.SetInt("IsCooldownActive", 0);
        currentHealth = maxHealth;
        PlayerPrefs.SetInt("Health", currentHealth);
        UpdateHealthUI();
    }

    public void StopCooldown()
    {
        if (cooldownRoutine != null)
        {
            StopCoroutine(cooldownRoutine);
            cooldownRoutine = null;
        }
        isCooldownActive = false;
        cooldownTimer = 0;
        PlayerPrefs.SetInt("IsCooldownActive", 0);
        PlayerPrefs.SetFloat("CooldownTimer", 0);
        UpdateHealthUI();
    }

    private void UpdateHealthUI()
    {
        healthText.text = currentHealth.ToString();
        healthText.gameObject.SetActive(true); 

        if (currentHealth == 0 && isCooldownActive)
        {
            cooldownText.gameObject.SetActive(true);
            cooldownText.text = "CoolDown : " + Mathf.CeilToInt(cooldownTimer).ToString() + "s";
        }
        else
        {
            cooldownText.gameObject.SetActive(false);
        }
    }

    public void OpenShopManually()
    {
        shopPanel.SetActive(true);
    }

    public void UpdateUIExternal()
    {
        UpdateHealthUI();
    }

    public void AddHealth(int amount)
    {
        currentHealth += amount;
        if (currentHealth > maxHealth)
            currentHealth = maxHealth;

        PlayerPrefs.SetInt("Health", currentHealth);
        UpdateHealthUI();
    }

    public bool IsCooldownActive()
    {
        return isCooldownActive;
    }
}
