using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthManager : MonoBehaviour
{
    [SerializeField]
    private int maxHealth = 100;
    private int currentHealth;

    public float RemainingHealthPercentage
    {
        get
        {
            return currentHealth / maxHealth;
        }
    }

    public Renderer playerBody;
    public Renderer playerJoints;
    private float flashLength = 0.1f;
    private float totalFlashDuration = 0.6f;
    public GameManager gameManager;
    public HealthBarUI healthBarUI;

    void Start()
    {
        ResetHealth();
    }

    private void UpdateHealthUI()
    {
        if (healthBarUI != null)
        {
            healthBarUI.UpdateHealthBar((float)currentHealth / maxHealth);
        }
    }

    public void HurtPlayer(int damage)
    {
        currentHealth -= damage;
        
        if (currentHealth <= 0)
        {
            SoundManager.sndMan.PlayDeathFromFallSound();
            gameManager.RespawnPlayer();
        }
        else
        {
            SoundManager.sndMan.PlayDamagesSound();
            StartCoroutine(FlashEffect());
        }

        UpdateHealthUI();
    }

    public void ResetHealth()
    {
        currentHealth = maxHealth;
        UpdateHealthUI();
    }

    public void StartFlashEffect()
    {
        StartCoroutine(FlashEffect());
    }

    private IEnumerator FlashEffect()
    {
        float flashCounter = totalFlashDuration;

        while (flashCounter > 0)
        {
            playerBody.enabled = !playerBody.enabled;
            playerJoints.enabled = !playerJoints.enabled;
            yield return new WaitForSeconds(flashLength);
            flashCounter -= flashLength;
        }
        playerBody.enabled = true;
        playerJoints.enabled = true;
    }

    public void HealPlayer(int healAmount)
    {
        currentHealth += healAmount;
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
        UpdateHealthUI();
    }
}
