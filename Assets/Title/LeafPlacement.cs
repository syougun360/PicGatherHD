/// --------------------------------------------------------------------
/// date ： 2015/02/23  
/// brief ： 葉っぱを配置
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

public class LeafPlacement : MonoBehaviour {

//    struct LeafData
//    {
//        public LeafData(Vector3 pos):this()
//        {
//            Pos = new Vec3J(pos.x, pos.y, pos.z);
//        }
//        public Vec3J Pos { get; set; }
//    };

//    [SerializeField]
//    GameObject LeafPrefab = null;

//    [SerializeField]
//    GameObject TreeBranch = null;

//    Vector3 BeforeLeafObjectPos = Vector3.zero;
    
//    List<LeafData> Data = new List<LeafData>();

//    const string FileName = "TitleLeaf";
//    const float CanInstanceDistance = 0.5f;


//    // Use this for initialization
//    void Start () {
//        //List<Texture2D> resList = new List<Texture2D>();

//        //const string Name = "Leaf";

//        //for(int i = 0;i<3;i++)
//        //resList.Add(Resources.Load(Name + "/" + i) as Texture2D);
        
//        //foreach(var leaf in GetLoadData())
//        //{
//        //    var leafClone = (GameObject)Instantiate(LeafPrefab, new Vector3(leaf.Pos.X, leaf.Pos.Y, leaf.Pos.Z), Quaternion.identity);
//        //    leafClone.transform.parent = transform;
//        //    leafClone.gameObject.name = LeafPrefab.name;
//        //    leafClone.renderer.material.mainTexture = resList[Random.Range(0, resList.Count)];
//        //}
//    }
	
//    // Update is called once per frame
//    void Update () {

//        if (TouchManager.IsMouseButton(TreeBranch))
//        {
//            //CreatePrefab();
//        }
//    }

//    /// <summary>
//    /// 配置
//    /// </summary>
//    void CreatePrefab()
//    {
//        var distance = Vector3.Distance(BeforeLeafObjectPos, TouchManager.TapPos);
//        if (distance >= CanInstanceDistance)
//        {
//            var leafClone = (GameObject)Instantiate(LeafPrefab, TouchManager.TapPos, Quaternion.identity);
//            leafClone.transform.parent = gameObject.transform;
//            leafClone.gameObject.name = LeafPrefab.gameObject.name;
//            BeforeLeafObjectPos = TouchManager.TapPos;
//            Data.Add(new LeafData(leafClone.transform.position));
//            FileWrite();
//        }
//    }

//    /// <summary>
//    /// 読み込み
//    /// </summary>
//    /// <returns></returns>
//    List<LeafData> GetLoadData()
//    {
//#if UNITY_METRO && !UNITY_EDITOR
//        var folderPath = "Database/";
//        var filePath = folderPath + FileName + ".json";

//        if (!LibForWinRT.IsFileExistAsync(filePath).Result) return null;
//        var jsonText = LibForWinRT.ReadFileText(filePath).Result;

//#else

//        var folderpath = Application.persistentDataPath + "/Database/";
//        var filePath = folderpath + FileName + ".json";

//        if (!File.Exists(filePath)) return null;

//        var jsonText = File.ReadAllText(filePath);
//#endif
//        var json = LitJson.JsonMapper.ToObject<List<LeafData>>(jsonText);

//        return json;
//    }

//    /// <summary>
//    /// ファイルに書き出す
//    /// </summary>
//    void FileWrite()
//    {
//        string json = LitJson.JsonMapper.ToJson(Data);

//#if UNITY_METRO && !UNITY_EDITOR
//        LibForWinRT.WriteFileText("Database",FileName + ".json",json);
//#else
//        var path = Application.persistentDataPath + "/Database/";
//        if (!Directory.Exists(path))
//        {
//            Directory.CreateDirectory(path);
//        }
//        File.WriteAllText(path + FileName + ".json", json);
//#endif
//    }

}
