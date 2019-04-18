/// --------------------------------------------------------------------
/// date ： 2015/01/30  
/// brief ： 葉っぱを生成
/// author ： Yamada Masamistu
/// --------------------------------------------------------------------

using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class LeafCreator : MonoBehaviour {

    [SerializeField]
    List<GameObject> LeafPrefabs = new List<GameObject>();

    [SerializeField]
    StampListMover StampList = null;

    [SerializeField]
    StampSelecter Selecter = null;

    [SerializeField]
    SoundEffectPlayer SEPlayer = null;

    [SerializeField]
    string CreateSoundResName = string.Empty;

    GameObject TreeBranch = null;

    LeafStampManagerController Manager = null;

    Vector3 BeforeLeafObjectPos = Vector3.zero;
    Texture SelectTexture = null;

    int TextureID = 0;

    const float CanInstanceDistance = 0.03f;

	// Use this for initialization
	void Start () {
        SelectTexture = renderer.material.mainTexture;
        Manager = GetComponent<LeafStampManagerController>();
        ChangeTreeBranch();
	}
	
	// Update is called once per frame
	void Update () {
        if (!ModeManager.IsGameMode) return;
        if (!StampList.IsCreate) return;

        ChangeTreeBranch();

        if (TouchManager.IsTouching(TreeBranch) || TouchManager.IsMouseButton(TreeBranch))
        {
            CreatePrefab();
            SEPlayer.Play(CreateSoundResName);
        }
	}

    /// <summary>
    /// 木の枝オブジェクトを切り替える
    /// </summary>
    void ChangeTreeBranch()
    {
        if (TreeBranch) return;

        TreeBranch = GameObject.Find("Branch");
    }

    /// <summary>
    /// PrefabをGameObjectとして生成する
    /// </summary>
    void CreatePrefab()
    {
        var distance = Vector3.Distance(BeforeLeafObjectPos, TouchManager.TapPos);
        if (distance >= CanInstanceDistance)
        {
            var index = Random.Range(0, LeafPrefabs.Count);
            var leafClone = (GameObject)Instantiate(LeafPrefabs[index], TouchManager.TapPos, Quaternion.identity);
            leafClone.transform.parent = transform;
            leafClone.gameObject.name = LeafPrefabs[index].gameObject.name;

            foreach (Transform child in leafClone.transform)
            {
                child.renderer.material.mainTexture = SelectTexture;
            }
            
            leafClone.GetComponent<CharacterDataSave>().SetSaveData(TextureID);
            
            Manager.CreateChildrenDataSave(leafClone, TextureID);

            BeforeLeafObjectPos = TouchManager.TapPos;

            Manager.ChildrensDataSave();
        }
    }

    /// <summary>
    /// 選択テクスチャを切り替える
    /// </summary>
    /// <param name="button"></param>
    public void ChangeSelectTexture(GameObject button)
    {
        TextureID = button.GetComponent<LeafIDSetting>().ID;
        SelectTexture = button.GetComponent<Image>().mainTexture;
        Selecter.SelectFrameMove(button);
    }
}
