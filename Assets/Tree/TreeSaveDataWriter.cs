/// --------------------------------------------------------------------
/// date ： 2015/02/23  
/// brief ： 木のセーブデータを書き出す
/// author ： Yamada Masamistu
/// --------------------------------------------------------------------

using UnityEngine;
using System.Collections;

#if UNITY_METRO && !UNITY_EDITOR
using LegacySystem.IO;
#else
using System.IO;
#endif

public struct TreeData
{
    public TreeData(int id,Vector3 pos,Vector3 scale):this()
    {
        ID = id;
        Pos = new Vec3J(pos.x, pos.y, pos.z);
        Scale = new Vec3J(scale.x, scale.y, scale.z);
    }

    public Vec3J Pos { get; set; }
    public Vec3J Scale { get; set; }
    public int ID { get; set; }
}

public class TreeSaveDataWriter : MonoBehaviour {

    TreeData Data;

    /// <summary>
    /// 書き込み
    /// </summary>
    /// <param name="id"></param>
    /// <param name="pos"></param>
    public void Write(int id,Vector3 pos)
    {
        Data = new TreeData(id,pos,transform.lossyScale);

        FileWrite();
    }

    /// <summary>
    /// ファイルに書き出す
    /// </summary>
    void FileWrite()
    {
        string json = LitJson.JsonMapper.ToJson(Data);

#if UNITY_METRO && !UNITY_EDITOR
        LibForWinRT.WriteFileText("Database",name + ".json",json);
#else
        var path = Application.persistentDataPath + "/Database/";
        if (!Directory.Exists(path))
        {
            Directory.CreateDirectory(path);
        }
        File.WriteAllText(path + name + ".json", json);
#endif
    }
}
