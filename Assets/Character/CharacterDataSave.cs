/// ---------------------------------------------------
/// date ： 2015/02/12  
/// brief ： キャラクターの保存するデータ 
/// author ： Yamada Masamistu
/// ---------------------------------------------------

using UnityEngine;
using System.Collections;

public struct CharacterData
{
    public CharacterData(int id, string name, Vector3 pos, Vector3 scale, bool isCreateLoad = true)
        : this()
    {
        ID = id;
        Name = name;
        Pos = new Vec3J(pos.x, pos.y, pos.z);
        Scale = new Vec3J(scale.x, scale.y, scale.z);
        IsCreateLoad = isCreateLoad;
    }

    public Vec3J Scale { get; set; }
    public Vec3J Pos { get; set; }
    public string Name { get; set; }
    public int ID { get; set; }
    public bool IsCreateLoad { get; set; }
};

public class CharacterDataSave : MonoBehaviour
{

    public CharacterData Data { get; private set; }

    /// <summary>
    /// 保存するデータを格納する。
    /// </summary>
    /// <param name="textureFilePath">生成したテクスチャパス</param>
    public void SetSaveData(int id)
    {
        Data = new CharacterData(id,name,transform.position, transform.lossyScale);
    }

}
