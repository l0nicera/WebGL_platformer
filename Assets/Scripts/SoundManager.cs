using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager sndMan;

    private AudioSource audioSource;
    private AudioClip gemPickupSound;
    private AudioClip deathFromFallSound;
    private AudioClip jumpSound;
    private AudioClip timesOverSound;
    private AudioClip damagesSound;
    private AudioClip[] footstepSounds;

    void Start()
    {
        sndMan = this;
        audioSource = GetComponent<AudioSource>();
        gemPickupSound = Resources.Load<AudioClip>("GemPickupSound");
        deathFromFallSound = Resources.Load<AudioClip>("DeathFromFallSound");
        jumpSound = Resources.Load<AudioClip>("JumpSound");
        timesOverSound = Resources.Load<AudioClip>("TimesOverSound");
        damagesSound = Resources.Load<AudioClip>("DamagesSound");
        footstepSounds = Resources.LoadAll<AudioClip>("FootstepSounds");
    }
    public void PlayGemSound()
    {
        audioSource.PlayOneShot(gemPickupSound);
    }
    public void PlayDeathFromFallSound()
    {
        audioSource.PlayOneShot(deathFromFallSound);
    }
    public void PlayJumpSound()
    {
        audioSource.PlayOneShot(jumpSound);
    }
    public void PlayTimesOverSound()
    {
        audioSource.PlayOneShot(timesOverSound);
    }
    public void PlayDamagesSound()
    {
        audioSource.PlayOneShot(damagesSound);
    }
    public void PlayFootstepSound()
    {
        if (footstepSounds.Length > 0)
        {
            int index = Random.Range(0, footstepSounds.Length);
            audioSource.PlayOneShot(footstepSounds[index]);
        }
    }
}
