using UnityEngine;
using System.Collections;

public class FruitManagerController : CharacterManager {

    FruitCreator Creator = null;

    // Use this for initialization
    void Awake()
    {
        Name = "Fruit";
        Init();

        Creator = GetComponent<FruitCreator>();
    }

    /// <summary>
    /// 子オブジェクトのデータ保存。
    /// ファイルに書き出す
    /// </summary>
    public override void ChildrensDataSave()
    {
        foreach (Transform children in transform)
        {
            var character = children.GetComponent<CharacterDataSave>();
            var frut = children.GetComponent<FruitFalling>();
            var isDead = frut.IsFall ? true : false;

            SaveData.Write(new CharacterData(character.Data.ID, Name,
                            character.transform.position, character.transform.lossyScale, !isDead));
        }

        SaveData.FileWrite(Name);
    }
    /// <summary>
    /// 子オブジェクトを生成
    /// </summary>
    public void ChildrenCreate(Vector3 pos)
    {
        var clone = Creator.Create(pos);

        CreateChildrenDataSave(clone);
        ChildrensDataSave();
    }
}
