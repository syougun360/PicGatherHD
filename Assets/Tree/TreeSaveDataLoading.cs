/// --------------------------------------------------------------------
/// date ： 2015/02/23  
/// brief ： 木のセーブデータを読み込み
/// author ： Yamada Masamistu
/// --------------------------------------------------------------------

using UnityEngine;
using System.Collections;

#if UNITY_METRO && !UNITY_EDITOR
using LegacySystem.IO;
#else
using System.IO;
using System.Collections.Generic;
#endif



public class TreeSaveDataLoading : MonoBehaviour {

    public TreeData GetLoadData()
    {
#if UNITY_METRO && !UNITY_EDITOR
        var folderPath = "Database/";
        var filePath = folderPath + name + ".json";

        var jsonText = LibForWinRT.ReadFileText(filePath).Result;

#else

        var folderpath = Application.persistentDataPath + "/Database/";
        var filePath = folderpath + name + ".json";

        if (!File.Exists(filePath)) return new TreeData(-1,Vector3.zero,Vector3.zero);

        var jsonText = File.ReadAllText(filePath);
#endif
        if (string.IsNullOrEmpty(jsonText)) return new TreeData(-1, Vector3.zero, Vector3.zero);

        var json = LitJson.JsonMapper.ToObject<TreeData>(jsonText);

        return json;
    }
}
