using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Collections;
using System.Collections.Generic;

public class ChangeLeafStampShowList : MonoBehaviour
{

    /// <summary>
    /// Resouces内の画像データパス
    /// </summary>
    [SerializeField]
    LeafStampManagerController GraphicsPath = null;

    [SerializeField]
    SoundEffectPlayer Player = null;

    [SerializeField]
    string ChangeSoundResName = string.Empty;

    /// <summary>
    /// 表示しているスタンプの最大数
    /// </summary>
    private const int MaxStampIconNumber = 3;

    /// <summary>
    /// スタンプオブジェクトを得る
    /// </summary>
    [SerializeField]
    private GameObject[] Leaf = new GameObject[MaxStampIconNumber];

    /// <summary>
    /// スタンプのリストがいくつスクロールされているのかのカウント
    /// </summary>
    private int ScrollValue = 0;

    // Use this for initialization
    void Start()
    {
        /// 開始時に表示するスタンプ一覧を用意する
        IconsInitialize(GraphicsPath.ID);
    }

    /// <summary>
    /// アイコンの数が増加時、不明時に呼び、有効な数だけアイコンを初期化する
    /// </summary>
    /// <param name="canReadNumber">アイコンがある数を渡す</param>
    private void IconsInitialize(int canReadNumber = MaxStampIconNumber)
    {
        IconsReset(canReadNumber);
        ButtonsReset(canReadNumber);
    }


    // Update is called once per frame
    void Update()
    {

    }


    /// <summary>
    /// スタンプのアイコンを更新する
    /// </summary>
    /// <param name="canReadNumber">アイコンがある数を渡す</param>
    private void IconsReset(int canReadNumber = MaxStampIconNumber)
    {
        if (canReadNumber >= MaxStampIconNumber)
        {
            canReadNumber = MaxStampIconNumber;
        }

        for (int i = 0; i < canReadNumber; i++)
        {
            SetLeafSprite(Leaf[i],ScrollValue + i);
        }
    }


    /// <summary>
    /// スタンプアイコンがあるかどうかをチェックして
    /// 無い場合はボタンを無効化する
    /// </summary>
    /// <param name="canReadNumber">アイコンがある数を渡す</param>
    private void ButtonsReset(int canReadNumber = MaxStampIconNumber)
    {
        for (int i = 0; i < MaxStampIconNumber; i++)
        {
            if (i < canReadNumber)
            {
                SetButtonEnabled(Leaf[i], true);
            }
            else
            {
                SetButtonEnabled(Leaf[i], false);
            }
        }
    }



    /// <summary>
    /// 第一引数の葉オブジェクトのテクスチャを第二引数のパスにあるものに変更する
    /// </summary>
    /// <param name="_gameObject">変更される葉</param>
    /// <param name="textureID">変更するテクスチャのパス</param>
    private void SetLeafSprite(GameObject _gameObject, int textureID)
    {
        var MainImage = _gameObject.GetComponent<Image>();
        var NewTexture = new Texture2D(128, 128);

        if (textureID < MaxStampIconNumber)
        {
            NewTexture = Resources.Load(GraphicsPath.Name + "/" + textureID) as Texture2D;
        }
        else
        {

#if UNITY_METRO && !UNITY_EDITOR
            var FolderName = GraphicsPath.Name + "/";
            var FileName = textureID + ".png";
            var Bytes = LibForWinRT.ReadFileBytes(FolderName + FileName).Result;
            NewTexture.LoadImage(Bytes);
#else
            var FolderName = Application.persistentDataPath + "/" + GraphicsPath.Name + "/";
            var FileName = textureID + ".png";
            var Bytes = File.ReadAllBytes(FolderName + FileName);
            NewTexture.LoadImage(Bytes);
#endif
        }

        _gameObject.GetComponent<LeafIDSetting>().ID = textureID;

        var NewSprite = Sprite.Create(NewTexture, new Rect(0, 0, NewTexture.width, NewTexture.height), Vector2.zero);
        MainImage.sprite = NewSprite;


    }


    /// <summary>
    /// Buttonの有効化、無効化をする
    /// </summary>
    /// <param name="_gameObject">変化を与えるGameObject</param>
    /// <param name="isEnabled">有効...true 無効...false</param>
    private void SetButtonEnabled(GameObject _gameObject, bool isEnabled)
    {
        var button = _gameObject.GetComponent<Button>();
        button.enabled = isEnabled;
    }


    /// <summary>
    /// 右にあるボタンの押し込んだ際の処理
    /// </summary>
    public void RightScroll()
    {
        ScrollValue -= (ScrollValue > 0) ? 1 : 0;

        IconsInitialize();

        Player.Play(ChangeSoundResName);
    }


    /// <summary>
    /// 右にあるボタンの押し込んだ際の処理
    /// </summary>
    public void LeftScroll()
    {
        ScrollValue += (GraphicsPath.ID > ScrollValue + MaxStampIconNumber) ? 1 : 0;

        IconsInitialize();

        Player.Play(ChangeSoundResName);

    }

}