using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using TMPro;

public class GameTimer : MonoBehaviour
{
    public float initialTime = 180f;
    public float timeRemaining;
    public TextMeshProUGUI timerText;
    public GameManager gameManager;
    private bool isTimerEffectActive = false;
    public PlayerController playerController;
    private Vector3 initialPlayerPosition;
    private Color originalTextColor;
    private float originalFontSize;
    private bool isLargeTextActive;


    void Start()
    {
        timeRemaining = initialTime;
        initialPlayerPosition = playerController.transform.position;
        originalTextColor = timerText.color;
        originalFontSize = timerText.fontSize;
        UpdateTimerDisplay();
    }

    void Update()
    {
        if (Vector3.Distance(playerController.transform.position, initialPlayerPosition) > 0.1f)
        {
            if (timeRemaining > 0)
{
                timeRemaining -= Time.deltaTime;
                if (timeRemaining < 0)
                {
                    timeRemaining = 0;
                }
                if (timeRemaining % 60f <= 1f && timeRemaining > 1f && !isTimerEffectActive)
                {
                    StartCoroutine(GrowTextEffect());
                }

                if (timeRemaining <= 30f && timeRemaining > 29.5f && !isTimerEffectActive)
                {
                    StartCoroutine(GrowTextEffect());
                }

                if (timeRemaining <= 11f )
                {
                    if (!isLargeTextActive)
                    {
                        isLargeTextActive = true;
                        timerText.fontSize = originalFontSize * 1.5f;
                    }

                    if (Mathf.FloorToInt(timeRemaining) != Mathf.FloorToInt(timeRemaining + Time.deltaTime))
                    {
                        StartCoroutine(ChangeTextColorEffect());
                    }
                }

                UpdateTimerDisplay();
            }
            else
            {
                timeRemaining = 0;
                SoundManager.sndMan.PlayTimesOverSound();
                gameManager.RespawnPlayer();
            }
        }
    }

    public void StartTimer()
    {
        UpdateTimerDisplay();
    }

    private void UpdateTimerDisplay()
    {
        int minutes = Mathf.FloorToInt(timeRemaining / 60);
        int seconds = Mathf.FloorToInt(timeRemaining % 60);
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    public void ResetTimer()
    {
        timeRemaining = initialTime;
        UpdateTimerDisplay();
    }

    private IEnumerator GrowTextEffect()
    {
        isTimerEffectActive = true;
        float effectDuration = 1f;
        float elapsedTime = 0;
        Vector3 originalRotation = timerText.transform.localEulerAngles;
        float trembleAngle = 5.0f;
        float trembleSpeed = 50.0f;

        timerText.color = Color.red;

        while (elapsedTime < effectDuration)
        {
            timerText.fontSize = Mathf.Lerp(originalFontSize, originalFontSize * 1.5f, elapsedTime / effectDuration);

            float trembleEffect = Mathf.Sin(elapsedTime * trembleSpeed) * trembleAngle;
            timerText.transform.localEulerAngles = originalRotation + new Vector3(0, 0, trembleEffect);

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        timerText.fontSize = originalFontSize;
        timerText.color = originalTextColor;
        timerText.transform.localEulerAngles = originalRotation;
        isTimerEffectActive = false;
    }

    private IEnumerator ChangeTextColorEffect()
    {
        isTimerEffectActive = true;
        timerText.color = Color.red;
        yield return new WaitForSeconds(0.5f);
        timerText.color = Color.black;
        isTimerEffectActive = false;
    }

    public void ResetLargeTextEffect()
    {
        isLargeTextActive = false;
        timerText.fontSize = originalFontSize;
    }

}
