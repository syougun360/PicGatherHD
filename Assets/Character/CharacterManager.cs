/// ---------------------------------------------------
/// date ： 2015/01/30  
/// brief ： キャラクターの親クラス
/// author ： Yamada Masamistu
/// ---------------------------------------------------
/// 
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

#if UNITY_METRO && !UNITY_EDITOR
using LegacySystem.IO;
#else
using System.IO;
#endif

public class CharacterManager : MonoBehaviour
{
    public int ID { get; protected set; }
    public string Name { get; protected set; }
    public bool IsCreate { get { return (State == STATE.Create); } }
    public bool CanSave { get { return (State == STATE.None); } }
    public bool CanDrawing { get; protected set; }
    public Texture2D CampusTexture { get; private set; }

    [SerializeField]
    protected CampusTemplateSetting Template = null;

    [SerializeField]
    protected Sprite TemplateSprite = null;

    [SerializeField]
    List<GameObject> ChildrenPrefabs = new List<GameObject>();

    protected CharacterDataWriting SaveData = null;
    private int CreateNum = 0;
    protected int LimitCreateNum = 0;

    enum STATE
    {
        None,       //  なにもない
        Create,     //  生成
        Appearance, //  登場
    }

    STATE State;

    /// <summary>
    /// データベースからIDをロードする。
    /// </summary>
    public virtual void Init()
    {
        CreateNum = LimitCreateNum;
        CanDrawing = true;

        SaveData = GetComponent<CharacterDataWriting>();
        State = STATE.None;
        CampusTexture = null;
        ChildrensLoading();
    }

    /// <summary>
    /// 子オブジェクト達を読み込む
    /// </summary>
    void ChildrensLoading()
    { 
#if UNITY_METRO && !UNITY_EDITOR
        var folderPath = "Database/";
        var filePath = folderPath + Name + ".json";

        var jsonText = LibForWinRT.ReadFileText(filePath).Result;

#else
        var folderpath = Application.persistentDataPath + "/Database/" ;
        var filePath = folderpath + Name + ".json";
        
        if (!File.Exists(filePath)) return;

        var jsonText = File.ReadAllText(filePath);
#endif
        if (string.IsNullOrEmpty(jsonText)) return;
        var json = LitJson.JsonMapper.ToObject<CharacterData[]>(jsonText);

        foreach(var chara in json)
        {
            if (chara.Name == name)
            {
                ID = chara.ID;
            }
            else
            {
                ChildrenCreate(chara);
            }

        }
    }

    /// <summary>
    /// 子オブジェクトを生成
    /// </summary>
    /// <param name="chara"></param>
    void ChildrenCreate(CharacterData chara)
    {
        if (!chara.IsCreateLoad) return;

        var index = Random.Range(0, ChildrenPrefabs.Count);
        var clone = (GameObject)Instantiate(ChildrenPrefabs[index], Vector3.zero, Quaternion.identity);
        clone.name = chara.Name;
        clone.transform.parent = transform;
        clone.transform.position = new Vector3(chara.Pos.X, chara.Pos.Y, chara.Pos.Z);
        clone.transform.lossyScale.Scale(new Vector3(chara.Pos.X, chara.Pos.Y, chara.Pos.Z));
        TextureLoad(clone, chara);
        CreateChildrenDataSave(clone,chara.ID);

        LimitCreate();
    }

    protected virtual void TextureLoad(GameObject clone,CharacterData chara)
    {

#if UNITY_METRO && !UNITY_EDITOR
        
        var filePath = chara.Name + "/" + (chara.ID - 1) + ".png";

        var bytes = LibForWinRT.ReadFileBytes(filePath).Result;
#else
        var filePath = Application.persistentDataPath + "/" + chara.Name + "/" + (chara.ID - 1) + ".png";

        if (!File.Exists(filePath)) return;
        var bytes = File.ReadAllBytes(filePath);

#endif
        if (bytes == null) return;

        var texture = new Texture2D(128, 128);
        texture.LoadImage(bytes);

        if (Name == "Fairy")
        {
            clone.transform.FindChild("fairy").renderer.material.mainTexture = texture;
        }
        else
        {
            clone.renderer.material.mainTexture = texture;
        }
    }

    /// <summary>
    /// 登録する
    /// </summary>
    public void Entry()
    {
        if (State != STATE.None) return;

        LimitCreate();
        ID++;
        State = STATE.Create;
        SaveData.Write(new CharacterData(ID, name,transform.position, transform.lossyScale));
        
    }

    /// <summary>
    /// 限界生成処理
    /// </summary>
    void LimitCreate()
    {
        CreateNum--;
        if (CreateNum <= 0)
        {
            CanDrawing = false;
            CreateNum = 0;
        }
    }

    /// <summary>
    /// 生成できる数を増加
    /// </summary>
    public void LimitCreateNumIncrease()
    {
        if (CreateNum > LimitCreateNum) return;

        CanDrawing = true;
        CreateNum ++;
    }


    /// <summary>
    /// テンプレート(Sample)を設定する
    /// </summary>
    public virtual void SetTemplate()
    {
        Template.SetSprite(TemplateSprite);
    }

    /// <summary>
    /// 生成された時の処理
    /// </summary>
    public void Created()
    {
        State = STATE.Appearance;
    }

    /// <summary>
    /// なにもない状態にする
    /// </summary>
    public void NoneState()
    {
        State = STATE.None;
    }

    /// <summary>
    /// お絵かきしたテクスチャデータを設定する。
    /// </summary>
    /// <param name="texture">テクスチャデータ</param>
    public void SetTexture2D(Texture2D texture)
    {
        CampusTexture = texture;
    }

    /// <summary>
    /// 生成した子オブジェクトのデータを保存する。
    /// </summary>
    /// <param name="clone">生成するオブジェクト</param>
    public void CreateChildrenDataSave(GameObject clone)
    {
        var children = clone.GetComponent<CharacterDataSave>();
        children.SetSaveData(ID);
    }

    /// <summary>
    /// 生成した子オブジェクトのデータを保存する。
    /// </summary>
    /// <param name="clone">生成するオブジェクト</param>
    public void CreateChildrenDataSave(GameObject clone,int id)
    {
        var children = clone.GetComponent<CharacterDataSave>();
        children.SetSaveData(id);
    }

    /// <summary>
    /// 子オブジェクトのデータ保存。
    /// ファイルに書き出す
    /// </summary>
    public virtual void ChildrensDataSave()
    {
        var childrens = GameObject.FindGameObjectsWithTag(Name);

        foreach (var children in childrens)
        {
            var character = children.GetComponent<CharacterDataSave>();
            SaveData.Write(new CharacterData(character.Data.ID, Name,
                            character.transform.position, character.transform.lossyScale));
        }

        SaveData.FileWrite(Name);
    }

    /// <summary>
    /// データをクリアする
    /// </summary>
    public void DataClear()
    {
        SaveData.Clear();
        SaveData.FileWrite(Name);
    }
}