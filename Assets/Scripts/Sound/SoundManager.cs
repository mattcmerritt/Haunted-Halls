using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public AudioSource Source;
    public AudioClip FootstepClip, LandingClip;
    public Transform PlayerTransform;

    public event EventHandler<SoundEventArgs> OnSoundMade;

    public void PlayFootstep()
    {
        Source.PlayOneShot(FootstepClip);
        OnSoundMade?.Invoke(this, new SoundEventArgs { Location = PlayerTransform.position });
    }

    public void PlayLandingSound()
    {
        Source.PlayOneShot(LandingClip);
        OnSoundMade?.Invoke(this, new SoundEventArgs { Location = PlayerTransform.position });
    }
}
