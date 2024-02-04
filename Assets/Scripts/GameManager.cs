using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public PlayerController playerController;
    public HealthManager healthManager;
    public ScoreManager scoreManager;
    public GameTimer gameTimer;
    public FallDamage fallDamage;
    private Vector3 respawnPoint;
    private Quaternion respawnRotation;
    private bool isRespawning;
    private List<Gem> allGems = new List<Gem>();
    private int initialGemCount = 0;
    private float respawnLength  = .2f;
    public GameObject deathEffect;
    public Image blackScreen;
    private bool isFadeToBlack;
    private bool isFadeFromBlack;
    private float fadeSpeed = 2f;
    private float waitForFade = .5f;



    void Start()
    {
        respawnPoint = playerController.transform.position;
        respawnRotation = playerController.transform.rotation;
        allGems.AddRange(FindObjectsOfType<Gem>());
    }

    void Update()
    {
        if(isFadeToBlack)
        {
            blackScreen.color = new Color(blackScreen.color.r, blackScreen.color.g, blackScreen.color.b, Mathf.MoveTowards(blackScreen.color.a, 1f, fadeSpeed * Time.deltaTime));
            if(blackScreen.color.a == 1f)
            {
                isFadeToBlack = false;
            }
        }

        if(isFadeFromBlack)
        {
            blackScreen.color = new Color(blackScreen.color.r, blackScreen.color.g, blackScreen.color.b, Mathf.MoveTowards(blackScreen.color.a, 0f, fadeSpeed * Time.deltaTime));
            if(blackScreen.color.a == 0f)
            {
                isFadeFromBlack = false;
            }
        }

    }

    public void RespawnPlayer()
    {
        if (!isRespawning)
        {
            StartCoroutine(RespawnCo());
        }
        
    }

    private IEnumerator RespawnCo()
    {
        isRespawning = true;
        playerController.gameObject.SetActive(false);
        Instantiate(deathEffect, playerController.transform.position, playerController.transform.rotation);

        yield return new WaitForSeconds(respawnLength);

        isFadeToBlack = true;

        yield return new WaitForSeconds(waitForFade);

        isFadeToBlack = false;
        isFadeFromBlack = true;

        if (playerController != null)
        {
            playerController.enabled = false;
        }

        playerController.gameObject.SetActive(false);
        yield return new WaitForSeconds(respawnLength);

        playerController.transform.position = respawnPoint;
        playerController.transform.rotation = respawnRotation;

        playerController.gameObject.SetActive(true);

        if (playerController != null)
        {
            playerController.enabled = true;
        }

        if (healthManager != null)
        {

            if (fallDamage != null)
            {
                fallDamage.ResetFallDamage();
            }

            healthManager.ResetHealth();
            healthManager.StartFlashEffect();
        }
        ResetGameObjects();
        ResetScore();
        if (gameTimer)
        {
            gameTimer.ResetTimer();
            gameTimer.ResetLargeTextEffect();
        }
        isRespawning = false;
    }

    private void ResetGameObjects()
    {
        foreach (var gem in allGems)
        {
            gem.gameObject.SetActive(true);
        }
    }

    private void ResetScore()
    {
        scoreManager.ResetGems(initialGemCount);
        scoreManager.UpdateGemText();
    }

}
