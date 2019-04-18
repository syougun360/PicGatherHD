using UnityEngine;
using System.Collections;

public class ResetButtonSoundEffecter : MonoBehaviour {


    [SerializeField]
    SoundEffectPlayer SEPlayer = null;

    [SerializeField]
    string ButtonResName = string.Empty;

    [SerializeField]
    string YesResName = string.Empty;

    [SerializeField]
    string NoResName = string.Empty;

    /// <summary>
    /// ボタン押した
    /// </summary>
    public void ButtonPlay()
    {
        if (UISelectManager.IsNoneMode) return;
        if (SEPlayer.IsPlaying(ButtonResName)) return;

        SEPlayer.Play(ButtonResName);
    }

    /// <summary>
    /// ボタン押した
    /// </summary>
    public void YesPlay()
    {
        if (SEPlayer.IsPlaying(YesResName)) return;

        SEPlayer.Play(YesResName);
    }

    /// <summary>
    /// ボタン押した
    /// </summary>
    public void NoPlay()
    {
        if (SEPlayer.IsPlaying(NoResName)) return;

        SEPlayer.Play(NoResName);
    }
}

