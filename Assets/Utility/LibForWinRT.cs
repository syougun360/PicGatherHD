using UnityEngine;
using System.Collections;

#if UNITY_METRO && !UNITY_EDITOR
using System;
using LegacySystem.IO;
using Windows.Storage;
using Windows.Storage.Streams;
using System.IO;
using WinRTLegacy.Text;
using Windows.Storage.Pickers;
using System.Collections.Generic;
using Windows.Storage.Provider;
using Windows.UI.Xaml.Media.Imaging;
using System.Threading.Tasks;
#else
#endif


/// <summary>
/// WinRTのライブラリー
/// </summary>
public class LibForWinRT 
{

#if UNITY_METRO && !UNITY_EDITOR

    const string ParentFolder = "PicGather/";

    /// <summary>
    /// ファイルを読み込む
    /// </summary>
    /// <param name="fileName">ファイルパス</param>
    /// <returns>ファイルのbyte型の配列が戻り値</returns>
    public static async Task<string> ReadFileText(string filePath)
    {
        try
        {
            var url = "ms-appdata:///roaming/" + ParentFolder + filePath;
            var file = await StorageFile.GetFileFromApplicationUriAsync(new Uri(url));
            var text = await FileIO.ReadTextAsync(file);
            return text;
        }
        catch
        {
            return string.Empty;
        }
    }

    /// <summary>
    /// ファイルを読み込む
    /// </summary>
    /// <param name="fileName">ファイルパス</param>
    /// <returns>ファイルのbyte型の配列が戻り値</returns>
    public static async Task<byte[]> ReadFileBytes(string filePath)
    {
        try
        {
            var url = "ms-appdata:///roaming/" + ParentFolder + filePath;
            var file = await StorageFile.GetFileFromApplicationUriAsync(new Uri(url));
            var buf = await FileIO.ReadBufferAsync(file);
            var bytes = new byte[buf.Length];
            var dr = DataReader.FromBuffer(buf);
            dr.ReadBytes(bytes);
            return bytes;
        }
        catch
        {
            return null;
        }
    }

    /// <summary>
    /// 書き出す
    /// </summary>
    /// <param name="folderPath">Folderパス</param>
    /// <param name="fileName">ファイルパス</param>
    /// <param name="body">byte配列のデータ</param>
    public static async void WriteFile(string folderPath,string fileName, byte[] body)
    {
        try
        {
            // ローミングフォルダ
            var parentFolder = await ApplicationData.Current.RoamingFolder.CreateFolderAsync(ParentFolder, CreationCollisionOption.OpenIfExists);
            var folder = await parentFolder.CreateFolderAsync(folderPath, CreationCollisionOption.OpenIfExists);

            // ファイル（存在すれば上書き）
            var file = await folder.CreateFileAsync(fileName, CreationCollisionOption.ReplaceExisting);

            // 書き込み
            using (var rStream = await file.OpenAsync(FileAccessMode.ReadWrite))
            using (var oStream = rStream.GetOutputStreamAt(0))
            {
                var writer = new DataWriter(oStream);
                writer.UnicodeEncoding = Windows.Storage.Streams.UnicodeEncoding.Utf8;
                writer.WriteBytes(body);
                await writer.StoreAsync();

            }
        }
        catch 
        {
        
        }

    }

    /// <summary>
    /// 書き出す
    /// </summary>
    /// <param name="folderPath">Folderパス</param>
    /// <param name="fileName">ファイルパス</param>
    /// <param name="body">byte配列のデータ</param>
    public static async void WriteSharePicture(string folderPath, string fileName, byte[] body)
    {
        try
        {
            // ローミングフォルダ
            var folder = await KnownFolders.PicturesLibrary.CreateFolderAsync(folderPath, CreationCollisionOption.OpenIfExists);

            // ファイル（存在すれば上書き）
            var file = await folder.CreateFileAsync(fileName, CreationCollisionOption.ReplaceExisting);

            // 書き込み
            using (var rStream = await file.OpenAsync(FileAccessMode.ReadWrite))
            using (var oStream = rStream.GetOutputStreamAt(0))
            {
                var writer = new DataWriter(oStream);
                writer.UnicodeEncoding = Windows.Storage.Streams.UnicodeEncoding.Utf8;
                writer.WriteBytes(body);
                await writer.StoreAsync();

            }
        }
        catch
        { 
        
        }

    }
    
    /// <summary>
    /// 書き出す
    /// </summary>
    /// <param name="folderPath">Folderパス</param>
    /// <param name="fileName">ファイルパス</param>
    /// <param name="body">stringの文字データ</param>
    public static async void WriteFileText(string folderPath, string fileName, string body)
    {
        try
        {
            // ローミングフォルダ
            var parentFolder = await ApplicationData.Current.RoamingFolder.CreateFolderAsync(ParentFolder, CreationCollisionOption.OpenIfExists);
            var folder = await parentFolder.CreateFolderAsync(folderPath, CreationCollisionOption.OpenIfExists);

            // ファイル（存在すれば上書き）
            var file = await folder.CreateFileAsync(fileName, CreationCollisionOption.ReplaceExisting);

            // 書き込み
            using (var rStream = await file.OpenAsync(FileAccessMode.ReadWrite))
            using (var oStream = rStream.GetOutputStreamAt(0))
            {
                var writer = new DataWriter(oStream);
                writer.UnicodeEncoding = Windows.Storage.Streams.UnicodeEncoding.Utf8;
                writer.WriteString(body);
                await writer.StoreAsync();
            }
        }
        catch
        { 
            
        }
    }


    /// <summary>
    /// 削除する
    /// </summary>
    /// <param name="folderPath">Folderパス</param>
    public static async void FolderDelete()
    {
        try
        {
            var folder = await ApplicationData.Current.RoamingFolder.GetFolderAsync(ParentFolder);
            await folder.DeleteAsync();
        }
        catch
        { 
            
        }
    }
#endif

}
