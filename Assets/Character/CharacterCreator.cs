/// ---------------------------------------------------
/// date ： 2015/01/28    
/// brief ： キャラクターを生成する
/// author ： Yamada Masamistu
/// ---------------------------------------------------

using UnityEngine;
using System.Collections;

public class CharacterCreator : MonoBehaviour {

    [SerializeField]
    GameObject Prefab = null;

    [SerializeField]
    CharacterManager Manager = null;

    [SerializeField]
    bool IsFairy = false;

    [SerializeField]
    SoundEffectPlayer SEPlayer = null;

    [SerializeField]
    string AppearanceSoundResName = string.Empty;

    void Update()
    {
        if (!Manager.IsCreate) return;

        StartCoroutine("Create");

        Manager.Created();
    }

    /// <summary>
    /// 少し待ってから生成するようにする
    /// もし、Resourcesで読み込みが失敗した場合は絶対パスで取得するようにしている。
    /// </summary>
    /// <returns></returns>
    IEnumerator Create()
    {
        yield return new WaitForSeconds(2.0f);

        var Clone = (GameObject)Instantiate(Prefab, new Vector3(0, 100, 0), Prefab.transform.rotation);
        Clone.transform.parent = Manager.transform;
        Clone.name = Prefab.name;
        if (IsFairy)
        {
            Clone.transform.FindChild("fairy").renderer.material.mainTexture = Manager.CampusTexture;
        }
        else
        {
            Clone.renderer.material.mainTexture = Manager.CampusTexture;
        }

        Manager.CreateChildrenDataSave(Clone);
        Manager.ChildrensDataSave();
        Manager.NoneState();

        SEPlayer.Play(AppearanceSoundResName);
    }

}
