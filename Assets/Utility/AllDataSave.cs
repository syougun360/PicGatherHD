/// ---------------------------------------------------
/// date ： 2015/03/01  
/// brief ：オブジェクトのすべてをセーブする処理
/// author ： Yamada Masamistu
/// ---------------------------------------------------
/// 
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AllDataSave : MonoBehaviour {

    [SerializeField]
    List<CharacterManager> Characters = new List<CharacterManager>();

    [SerializeField]
    TreeChanger Tree = null;

    [SerializeField]
    FeverManager Fever = null;

    public void AllSave()
    {
        foreach (var chara in Characters)
        {
            chara.ChildrensDataSave();
        }
        Tree.Save();
        Fever.Save();
    }
}
