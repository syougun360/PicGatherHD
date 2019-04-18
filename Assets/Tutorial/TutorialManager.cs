using UnityEngine;
using System.Collections;

public class TutorialManager : MonoBehaviour {


    /// <summary>
    /// チュートリアルのリスト
    /// </summary>
    public enum TutorialList
    {
        NULL = -1,
        SelectStampList,
        PutStamp,
        DrawLeaf,
        DrawFairy,
        DrawCloud,
        GUARD
    };

    /// <summary>
    /// すでに終えているか 終えている...true 終えていない...false
    /// </summary>
    private bool[] alreadyEndedList = new bool[(int)TutorialList.GUARD];

    public bool[] AlreadyEndedList { 
        get { return this.alreadyEndedList; }
        set { this.alreadyEndedList = value; }
    }

    /// <summary>
    /// キャンパスモードかどうかを得る
    /// </summary>
    public bool IsCampusMode = false;

    // Use this for initialization
	void Start () {

        for (int i = 0; i < (int)TutorialList.DrawCloud; i++)
        {
            AlreadyEndedList[i] = false;
        }
    }

    /// <summary>
    /// スタンプリストのチュートリアルが終えた
    /// </summary>
    public void AlreadySelectStampList()
    {
        AlreadyEndedList[(int)TutorialList.SelectStampList] = true;
    }

    // Draw関係

    
    /// <summary>
    /// 葉の絵を描き終えた
    /// </summary>
    public void AlreadyDrawLeaf()
    {
        AlreadyEndedList[(int)TutorialList.DrawLeaf] = true;
    }

    /// <summary>
    /// 妖精の絵をかき終えた
    /// </summary>
    public void AlreadyDrawFairy()
    {
        AlreadyEndedList[(int)TutorialList.DrawFairy] = true;
    }

    /// <summary>
    /// 雲の絵をかき終えた
    /// </summary>
    public void AlreadyDrawCloud()
    {
        AlreadyEndedList[(int)TutorialList.DrawCloud] = true;
    }

    /// <summary>
    /// 書き終えたものを判断しフラグを切り替える
    /// </summary>
    public void ChangeState()
    {
        var CCController = FindObjectOfType<CampusCaptureController>();
        if (CCController.CharaManager == null) return;
        var CampusTemplate = FindObjectOfType(typeof(CampusTemplateSetting)) as CampusTemplateSetting;
        if (CampusTemplate == null || CampusTemplate.IsSelect == false) return;

        switch(CCController.CharaManager.Name)
        {
            case "Leaf":
                AlreadyEndedList[(int)TutorialList.DrawLeaf] = true;
                break;
            case "Cloud":
                AlreadyEndedList[(int)TutorialList.DrawCloud] = true;
                break;
            case "Fairy":
                AlreadyEndedList[(int)TutorialList.DrawFairy] = true;
                break;
            default:
                return;
        }

        return;
    }

    /// <summary>
    /// チュートリアル表示のためキャンパスを開いているかどうかをチェックする
    /// </summary>
    /// <param name="_changed">表示中...true 表示していない...false</param>
    public void ChangeCampusMode(bool _changed)
    {
        var TutorialMngr = FindObjectOfType<TutorialManager>();

        if (TutorialMngr == null) return;

        TutorialMngr.IsCampusMode = _changed;
    }

    /// <summary>
    /// セーブしたうえでキャンパスを離れたかどうか
    /// </summary>
    /// <param name="_changed">セーブしてキャンパスを閉じた...true 閉じていない...false</param>
    public void SaveAndChangeCampusMode(bool _changed)
    {
        var TutorialMngr = FindObjectOfType<TutorialManager>();
        
        if (TutorialMngr == null) return;

        var CampusTemplate = FindObjectOfType(typeof(CampusTemplateSetting)) as CampusTemplateSetting;

        if (CampusTemplate == null || CampusTemplate.IsSelect == false) return;

        TutorialMngr.IsCampusMode = _changed;
    }

}
