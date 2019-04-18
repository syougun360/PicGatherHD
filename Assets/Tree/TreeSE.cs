using UnityEngine;
using System.Collections;

public class TreeSE : MonoBehaviour
{
    [SerializeField]
    string ResName = string.Empty;

    [SerializeField]
    SoundEffectPlayer Player = null;

    public void Play()
    {
        Player.Play(ResName,3.0f);
    }
}
