using UnityEngine;
using System.Collections;

public class MelodyList : MonoBehaviour {

    /// <summary>
    /// Resources/Melody内のフォルダの名前を入れる
    /// </summary>
    [SerializeField]
    string[] SoundsFileNameList = null;

    /// <summary>
    /// 再生されることになったフォルダを指定する
    /// </summary>
    public string SoundFileName { get; private set; }

    /// <summary>
    /// 最初に音楽データの場所を設定する
    /// </summary>
    void Awake ()
    {
        var PathName = "Melody/";
        SoundFileName = PathName + SoundsFileNameList[Random.Range(0, SoundsFileNameList.Length)];
    }
}
