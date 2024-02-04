using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gem : MonoBehaviour
{
    public GameObject pickupEffect;

    private void OnTriggerEnter(Collider other)
    {
        ScoreManager scoreManager = FindObjectOfType<ScoreManager>();

        if (scoreManager != null)
        {
            scoreManager.GemCollected();

            Vector3 effectPosition = GetComponent<Collider>().bounds.center;

            Instantiate(pickupEffect, effectPosition, transform.rotation);
            gameObject.SetActive(false);
            SoundManager.sndMan.PlayGemSound();
        }
    }
}