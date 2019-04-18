using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class StampListMover : MonoBehaviour {

    public enum STATE
    {
        Open,
        Stop,
        Close,
    };

    [SerializeField]
    AnimationClip OpenAnimClip = null;

    [SerializeField]
    AnimationClip CloseAnimClip = null;

    [SerializeField]
    GameObject StampList = null;

    [SerializeField]
    SoundEffectPlayer SEPlayer = null;

    [SerializeField]
    string OpenSoundResName = string.Empty;

    [SerializeField]
    string CloseSoundResName = string.Empty;

    Animation MoveAnimation = null;
    Vector3 ClosePos = Vector3.zero;

    public bool IsCreate { get { return (State == STATE.Stop); } }
    public bool IsClosed { get { return (State == STATE.Close); } }

    public STATE State {get;private set;}

    // Use this for initialization
	void Start () {
        State = STATE.Close;
        MoveAnimation = GetComponent<Animation>();

        var rectTrans = transform as RectTransform;
        ClosePos = rectTrans.anchoredPosition3D;
	}
	
	// Update is called once per frame
	void Update () {

        Opening();
        ChangeNonActive();

	}

    /// <summary>
    /// 開いているとき
    /// </summary>
    void Opening()
    {
        if (State == STATE.Open)
        {
            if (!MoveAnimation.isPlaying)
            {
                State = STATE.Stop;
            }
        }

    }

    /// <summary>
    /// 非アクティブ
    /// </summary>
    void ChangeNonActive()
    {
        if (!ModeManager.IsGameMode)
        {
            CloseAnimation();

            if (!StampList.activeSelf) return;
            StampList.SetActive(false);
        }
    }

    public void Open()
    {
        if (!ModeManager.IsGameMode) return;
        if (State != STATE.Close) return;

        State = STATE.Open;
        MoveAnimation.Blend(OpenAnimClip.name, 0.3f);
        SEPlayer.Play(OpenSoundResName);

        if (StampList.activeSelf) return;
        StampList.SetActive(true);
    }

    public void Close()
    {
        if (State != STATE.Stop) return;

        CloseAnimation();
        SEPlayer.Play(CloseSoundResName);

    }

    public void CloseAnimation()
    {
        if (State == STATE.Close) return;

        State = STATE.Close;
        MoveAnimation.Blend(CloseAnimClip.name, 0.3f);
    }

    /// <summary>
    /// 閉じるに切り替える
    /// </summary>
    public void ChangeClose()
    {
        State = STATE.Close;

        var rectTrans = transform as RectTransform;
        rectTrans.anchoredPosition3D = ClosePos;
    }
}
