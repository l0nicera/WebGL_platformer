using UnityEngine;

public class FallDamage : MonoBehaviour
{
    public PlayerController playerController;
    public HealthManager healthManager;
    private float highestPoint;
    private bool wasInAir;

    void Update()
    {
        CheckFallDamage();
    }

    void CheckFallDamage()
    {
        if (!playerController.IsGrounded)
        {
            if (!wasInAir)
            {
                highestPoint = playerController.transform.position.y;
                wasInAir = true;
            }
            else if (playerController.transform.position.y > highestPoint)
            {
                highestPoint = playerController.transform.position.y;
            }
        }
        else if (wasInAir)
        {
            float fallDistance = highestPoint - playerController.transform.position.y;
            if (fallDistance > minHeightForDamage)
            {
                int damage = Mathf.RoundToInt(fallDistance * damageMultiplier);
                healthManager.HurtPlayer(damage);
            }
            wasInAir = false;
        }
    }

    public void ResetFallDamage()
    {
        wasInAir = false;
        highestPoint = 0f;
    }

    public float minHeightForDamage = 5f;
    public float damageMultiplier = 10f;
}
