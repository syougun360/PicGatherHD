
/// ---------------------------------------------------
/// date ： 2015/01/19 
/// brief ： キャプチャー保存
/// author ： Yamada Masamistu
/// ---------------------------------------------------
/// 
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

#if UNITY_METRO && !UNITY_EDITOR
using LegacySystem.IO;
#else
using System.IO;
#endif


public class CampusCaptureController : MonoBehaviour
{
    [SerializeField]
    CampusTemplateSetting CampusTemplate = null;

    [SerializeField]
    GameObject Campus = null;

    public Rect CaptureRect {get;private set;}
    
    Button ClickButton = null;

    public CharacterManager CharaManager {get;private set;}

    void Start()
    {
        ClickButton = GetComponent<Button>();
        ClickButton.onClick.AddListener(Save);
        SetCampusRect();
    }

    /// <summary>
    /// キャプチャーするキャンパスのRect型を設定する。
    /// </summary>
    void SetCampusRect()
    {
        var FrameRect = Campus.GetComponent<RectTransform>().rect;

#if UNITY_METRO && !UNITY_EDITOR
        var rightshift = 180;
        var downshift = 120;
        FrameRect.x += FrameRect.width / 2 + rightshift;
        FrameRect.y += FrameRect.height / 2 + downshift;
        FrameRect.width -= rightshift * 3.5f;
        FrameRect.height -= downshift * 3.0f;
#else
        var RightShift = 130;
        var DownShift = 90;
        FrameRect.x += FrameRect.width / 2 + RightShift;
        FrameRect.y += FrameRect.height / 2 + DownShift;
        FrameRect.width -= RightShift*3;
        FrameRect.height -= DownShift*2.7f;

#endif
        CaptureRect = FrameRect;
    }

    /// <summary>
    /// 保存する。
    /// </summary>
    void Save()
    {
        if (!CampusTemplate.IsSelect) return;
        if (!CharaManager.CanSave) return;

        StartCoroutine("SaveTexture");
    }

    /// <summary>
    /// キャプチャー処理
    /// キャプチャーしたテクスチャデータをまず、pngデータにエンコードする。
    /// バイト配列で画像を読み込みをしています。
    /// 読み込んだ画像をキャラクターが持っておるキャンパステクスチャに設定する。
    /// ファイルとして書き出す
    /// </summary>
    IEnumerator SaveTexture()
    {
        yield return new WaitForEndOfFrame();

        var texture = new Texture2D((int)CaptureRect.width, (int)CaptureRect.height, TextureFormat.ARGB32, false);

        texture.ReadPixels(CaptureRect, 0, 0);
        texture.Apply();

        var bytes = texture.EncodeToPNG();

        texture = new Texture2D(256, 256);

        texture.LoadImage(bytes);

        CharaManager.SetTexture2D(texture);

        WriteFile(bytes);

        CharaManager.Entry();

    }

    /// <summary>
    /// ファイルを書き出す
    /// </summary>
    /// <param name="bytes"></param>
    void WriteFile(byte[] bytes)
    {

#if UNITY_METRO && !UNITY_EDITOR
        var fileName = CharaManager.ID + ".png";

        LibForWinRT.WriteFile(CharaManager.Name, fileName, bytes);
#else
        var path = Application.persistentDataPath + "/" + CharaManager.Name + "/";
        if (!Directory.Exists(path))
        {
            Directory.CreateDirectory(path);
        }
        var filePath = path + CharaManager.ID + ".png";
        File.WriteAllBytes(filePath, bytes);
#endif

    }
    
    /// <summary>
    /// 保存するキャラクターデータを切り替える
    /// </summary>
    /// <param name="character"></param>
    public void ChangeSaveCharacter(CharacterManager character)
    {
        CharaManager = character;
    }

    /// <summary>
    /// 保存するキャラクターデータをNullにする
    /// </summary>
    public void NullSaveCharacter()
    {
        CharaManager = null;
    }

}