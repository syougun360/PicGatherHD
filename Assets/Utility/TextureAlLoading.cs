/// ---------------------------------------------------
/// date ： 2015/02/01    
/// brief ： 指定したFolderからテクスチャをすべて読み、保存する。
///         一度読み込んだファイルパスの場合は、保存してあるテクスチャから出す。
/// author ： Yamada Masamistu
/// ---------------------------------------------------

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

#if UNITY_METRO && !UNITY_EDITOR
using LegacySystem.IO;
#else
using System.IO;
#endif

public class TextureAlLoading : MonoBehaviour {

    struct TextureData
    {
        public TextureData(string filePath, Texture2D texture):this()
        {
            FilePath = filePath;
            ReadTexture = texture;
        }

        public Texture2D ReadTexture;
        public string FilePath;

    };

    static List<TextureData> ReadTextureList = new List<TextureData>();

    static Texture2D TempTexture = null;

    /// <summary>
    /// ランダムでテクスチャを読み込む。
    /// </summary>
    /// <param name="character">各キャラクターのインスタンス</param>
    /// <returns>テクスチャ</returns>
    public static Texture2D RandomLoadTexture(CharacterManager character)
    {
        var randomID = Random.Range(0, character.ID);

        return LoadTexture(character, randomID);
    }

    /// <summary>
    /// 指定したIDでテクスチャを読み込む
    /// </summary>
    /// <param name="character">各キャラクターのインスタンス</param>
    /// <param name="ID">読み込みたいID</param>
    /// <returns></returns>
    public static Texture2D LoadTexture(CharacterManager character,int ID)
    {
        if (character.ID == 0) return FerverLoadImage();

        if (character.ID < ID)
        {
            ID = character.ID;
        }

#if UNITY_METRO && !UNITY_EDITOR
        if (CheckFilePathSame(character.Name, ID))
        {
            return TempTexture;
        }

        return LoadImage(character.Name, ID);
#else
        var folderPath = Application.persistentDataPath + "/" + character.Name;

        if (CheckFilePathSame(folderPath,ID))
        {
            return TempTexture;
        }

        return LoadImage(folderPath, ID);
#endif

    }

    /// <summary>
    /// 同じファイルパスが保存してあるかをチェックする
    /// </summary>
    /// <param name="filePath">読み込むファイルパス</param>
    /// <returns>trrue : ある false : ない</returns>
    static bool CheckFilePathSame(string folderPath ,int ID)
    {
        var filePath = GetFilePath(folderPath, ID);
        foreach (var data in ReadTextureList)
        {
            if (data.FilePath == filePath)
            {
                TempTexture = data.ReadTexture;
                return true;
            }
        }

        return false;
    }


    /// <summary>
    /// 画像を読み込む
    /// </summary>
    /// <param name="filePath">ファイルパス</param>
    /// <returns>テクスチャ</returns>
    static Texture2D LoadImage(string folderPath, int ID)
    {
        var filePath = GetFilePath(folderPath, ID);
        var bytes = GetFileBytes(filePath);
        
        if (bytes == null)
        {
            return FerverLoadImage();
        }

        var texture = new Texture2D(128, 128);
        texture.LoadImage(bytes);

        ReadTextureList.Add(new TextureData(filePath, texture));

        return texture;
    }

    /// <summary>
    /// ファイルパスを取得
    /// </summary>
    /// <param name="folderName">フォルダー名</param>
    /// <param name="ID"></param>
    /// <returns></returns>
    static string GetFilePath(string folderName,int ID)
    {
        return folderName + "/" + ID + ".png";
    }

    /// <summary>
    /// ファイルのバイト配列を取得
    /// </summary>
    /// <param name="folderPath">フォルダーパス</param>
    /// <param name="ID">ID</param>
    /// <returns></returns>
    static byte[] GetFileBytes(string filePath)
    {
#if UNITY_METRO && !UNITY_EDITOR
        return LibForWinRT.ReadFileBytes(filePath).Result;
#else
        if (!File.Exists(filePath)) return null;

        return File.ReadAllBytes(filePath);
#endif
    }

    /// <summary>
    /// 指定した場所に画像がないならここが呼ばれる。
    /// フィーバー用の画像を読み込む
    /// </summary>
    /// <returns></returns>
    static Texture2D FerverLoadImage()
    {
        var randomID = Random.Range(0, 2);
        return Resources.Load("FeverGraphic/" + randomID) as Texture2D;
    }
}
