/// --------------------------------------------------------------------
/// date ： 2015/01/12  
/// brief ： 葉のスタンプ処理。　木の枝をタップしたら生成される
/// author ： Yamada Masamistu
/// --------------------------------------------------------------------

using UnityEngine;
using UnityEngine.UI;
using System.Collections;

#if UNITY_METRO && !UNITY_EDITOR
using LegacySystem.IO;
#else
using System.IO;
#endif

public class LeafStampManagerController : CharacterManager
{

    const int MaxResroucesLeaf = 3;

    // Use this for initialization
    void Awake()
    {
        LimitCreateNum = 100;

        Name = "Leaf";
        ID = 3;
        Init();
    }

    void Update()
    {

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
            var leafWither = children.GetComponent<LeafWitherController>();
            var leafFall = children.GetComponent<LeafFalling>();
            var isDead = leafWither.IsDead || leafFall.IsFall ? true : false;
            
            SaveData.Write(new CharacterData(character.Data.ID, Name,
                            character.transform.position, character.transform.lossyScale, !isDead));
        }

        SaveData.FileWrite(Name);
    }

    protected override void TextureLoad(GameObject clone, CharacterData chara)
    {
        var texture = new Texture2D(128, 128);


        if (chara.ID < MaxResroucesLeaf)
        {
            texture = Resources.Load(chara.Name + "/" + chara.ID) as Texture2D;
        }
        else
        {
#if UNITY_METRO && !UNITY_EDITOR
            var bytes = LibForWinRT.ReadFileBytes(chara.Name + "/" + chara.ID + ".png").Result;
#else
            var bytes = File.ReadAllBytes(Application.persistentDataPath + "/" + chara.Name + "/" + chara.ID + ".png");
#endif
            texture.LoadImage(bytes);
        }

        foreach (Transform child in clone.transform)
        {
            child.renderer.material.mainTexture = texture;
        }
            
    }

}