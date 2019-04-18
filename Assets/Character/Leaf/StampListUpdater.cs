/// ---------------------------------------------------
/// date ： 2015/01/29  
/// brief ： スタンプ用の葉っぱを生成
/// author ： Yamada Masamistu
/// ---------------------------------------------------
/// 
using UnityEngine;
using System.Collections;

public class StampListUpdater : MonoBehaviour
{

    [SerializeField]
    CharacterManager Manager = null;


    void Update()
    {
        if (!Manager.IsCreate) return;

        StartCoroutine("Create");

        Manager.Created();
    }

    /// <summary>
    /// 少し待ってから生成するようにする
    /// </summary>
    /// <returns></returns>
    IEnumerator Create()
    {
        yield return new WaitForSeconds(2.0f);

        Manager.ChildrensDataSave();
        Manager.NoneState();

    }
}
