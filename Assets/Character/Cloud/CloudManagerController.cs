/// ---------------------------------------------------
/// date ： 2015/01/14  
/// brief ： 雲管理クラス
/// author ： Yamada Masamistu
/// ---------------------------------------------------
/// 
using UnityEngine;
using System.Collections;

public class CloudManagerController : CharacterManager
{

	// Use this for initialization
    void Awake()
    {
        LimitCreateNum = 5;

        Name = "Cloud";
        Init();
    }
	// Update is called once per frame
	void Update () {

	}
}
