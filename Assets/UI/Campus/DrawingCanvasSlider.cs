/// ---------------------------------------------------
/// date ： 2015/01/20 
/// brief ： お絵かきするキャンパスをスライドさせる
/// author ： Yamada Masamistu
/// ---------------------------------------------------
/// 
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class DrawingCanvasSlider : MonoBehaviour {

    [SerializeField]
    AnimationClip OpenAnimClip = null;

    [SerializeField]
    AnimationClip CloseAnimClip = null;

    [SerializeField]
    CampusTemplateSetting CampusTemplate = null;

    [SerializeField]
    CharacterCampusDestroy CampusDes = null;

    [SerializeField]
    DrawingCampusBackGroundController CampusBackGround = null;

    [SerializeField]
    StampListMover StampList = null;

    [SerializeField]
    UIDrawingModeChanger UIModeChanger = null;

    [SerializeField]
    ChangeCameraPositionController CameraPosChange = null;

    [SerializeField]
    BGMManager BGM = null;

    [SerializeField]
    SoundEffectPlayer SEPlayer = null;

    [SerializeField]
    string OpenSoundResName = string.Empty;

    [SerializeField]
    string CloseSoundResName = string.Empty;

    [SerializeField]
    string SaveSoundResName = string.Empty;

    Animation MoveAnimation = null;
    
    enum STATE
    {
        Stop,   /// 停止
        Open,   /// 開く
        Close,  /// 閉じる
    };

    STATE State = STATE.Stop;

    // Use this for initialization
    void Start()
    {
        MoveAnimation = GetComponent<Animation>();
    }

    // Update is called once per frame
    void Update()
    {
        Opening();

        if (!ModeManager.IsDrawingMode) return;
        
        Closed();
    }

    void Opening()
    {
        if (State != STATE.Open) return;
        StartCoroutine("WaitOpend");
    }

    IEnumerator WaitOpend()
    {
        yield return new WaitForSeconds(1.0f);
        if (!MoveAnimation.isPlaying)
        {
            State = STATE.Stop;
            CameraPosChange.ChangeDrawingCampus();
            CampusBackGround.Unavailable();
            ModeManager.ChangeDrawingMode();
        }
    }

    /// <summary>
    /// 閉じたを押したかどうかの判定
    /// </summary>
    /// <returns></returns>
    public bool IsCloseOnClick()
    {
        if (State != STATE.Close) return false;
        return true;
    }

    /// <summary>
    /// 閉じたアニメーション終わった時の処理
    /// </summary>
    /// <returns></returns>
    void Closed()
    {
        if (State != STATE.Close) return;
        StartCoroutine("WaitClosed");
        State = STATE.Stop;
    }

    IEnumerator WaitClosed()
    {
        yield return new WaitForSeconds(1.4f);

        CameraPosChange.ChangeGameMain();
        CampusBackGround.Enabled();
        CampusTemplate.NonSelect();
        ModeManager.ChangeGameMode();
        CampusDes.Des();
        BGM.Stop();
        UIModeChanger.Enable(true);
    }

    /// <summary
    /// 開くアニメーションの処理
    /// </summary>
    public void Open()
    {
        if (State == STATE.Open) return;
        if (!ModeManager.IsGameMode) return;
        if (MoveAnimation.isPlaying) return;

        State = STATE.Open;
        MoveAnimation.Blend(OpenAnimClip.name, 0.3f);
        StampList.ChangeClose();
        BGM.Stop();
        SEPlayer.Play(OpenSoundResName);
        UIModeChanger.Enable(false);

    }

    /// <summary>
    /// 閉じるアニメーションの処理
    /// </summary>
    public void Close()
    {
        CloseAnimation(CloseSoundResName);
    }

    /// <summary>
    /// 閉じるアニメーションの処理
    /// </summary>
    public void SaveClose()
    {
        if (!CampusTemplate.IsSelect) return;

        CloseAnimation(SaveSoundResName);

    }

    void CloseAnimation(string resName)
    {
        if (State == STATE.Close) return;
        if (MoveAnimation.isPlaying) return;

        State = STATE.Close;
        MoveAnimation.Blend(CloseAnimClip.name, 0.3f);
        SEPlayer.Play(resName);
    }

}
