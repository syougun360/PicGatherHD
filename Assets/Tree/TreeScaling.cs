/// ---------------------------------------------------
/// date ： 2015/02/17  
/// brief ： 木を大きくする処理
/// author ： Yamada Masamistu
/// ---------------------------------------------------

using UnityEngine;
using System.Collections;

public class TreeScaling : MonoBehaviour {

    [SerializeField]
    float MaxScale = 100.0f;

    [SerializeField]
    float AddScale = 1.0f;

    [SerializeField]
    float ScaleToTime = 3.0f;

    float Scale = 0;
    float Count = 0;
    TreeChanger Changer = null;

    enum STATE
    {
        None,
        End,
    };
    STATE State = STATE.None;

	// Use this for initialization
	void Start () {
        Scale = transform.lossyScale.x;
        Changer = GetComponent<TreeChanger>();
	}
	
    void Update()
    {
        ScaleTo();
        Finish();
    }

    /// <summary>
    /// 次のスケールにする
    /// </summary>
    public void NextScale()
    {
        Scale += AddScale;
        transform.lossyScale.Scale(new Vector3(Scale, Scale, Scale));
    }

    void ScaleTo()
    {
        if (!Changer.IsScaling) return;
        if (ModeManager.IsFerverMode) return;
        if (Scale >= MaxScale) return;

        Scale += AddScale;
        iTween.ScaleTo(gameObject, iTween.Hash("scale", new Vector3(Scale, Scale, Scale),
                        "time", ScaleToTime, "easetype", iTween.EaseType.easeInOutExpo));

        Changer.ChangeNormalState();
        State = STATE.End;
    }

    void Finish()
    {
        if (State != STATE.End) return;

        Count += Time.deltaTime;
        if (Count >= ScaleToTime * 2)
        {
            State = STATE.None;
            Count = 0;
            Changer.Save();
        }
    }
}
