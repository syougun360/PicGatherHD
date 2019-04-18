using UnityEngine;
using System.Collections;

public class FeverGaugeParticle : MonoBehaviour {

    void Start()
    {
        Stop();
    }

    public void Stop()
    {
        if (!particleSystem.isPlaying) return;

        particleSystem.Stop(false);
    }

    public void Play()
    {
        particleSystem.Play(true);
    }
}
