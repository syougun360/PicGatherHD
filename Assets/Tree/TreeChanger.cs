/// ---------------------------------------------------
/// date ： 2015/02/14  
/// brief ： 木を切り替える処理
/// author ： Yamada Masamistu
/// ---------------------------------------------------
/// 
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TreeChanger : MonoBehaviour 
{
     
    /// <summary>
    /// 木を切り替えるデータ
    /// </summary>
    [System.Serializable]
    public struct ChangeTreeData
    {
        public ChangeTreeData(GameObject prefab, int feverNumTimes)
        {
            Prefab = prefab;
            FeverNumTimes = feverNumTimes;
        }

        public GameObject Prefab;
        public int FeverNumTimes;
    };

    public bool IsScaling { get { return State == STATE.Scaling; } }

    [SerializeField]
    List<ChangeTreeData> TreeData = new List<ChangeTreeData>();

    [SerializeField]
    FeverManager Fever = null;

    [SerializeField]
    CameraMover CameraMain = null;

    [SerializeField]
    AllDataSave DataSave = null;

    [SerializeField]
    float BroadenValue = 1.0f;

    [SerializeField]
    GameObject GrowthEffect = null;


    TreeSE SEPlayer = null;
    GameObject Tree = null;
    TreeSaveDataWriter Writer = null;
    Vector3 LocalPos = Vector3.zero;
    TreeScaling Scaling = null;

    enum STATE
    {
        Normal,
        Change,
        Scaling,
        Destroy,
    };

    STATE State = STATE.Normal;

    int CreateIndex = 0;
    bool IsStartFirst = true;

    // Use this for initialization
	void Start () {
        Scaling = GetComponent<TreeScaling>();
        SEPlayer = GetComponent<TreeSE>();
        Writer = GetComponent<TreeSaveDataWriter>();
        Tree = GameObject.Find("Tree");

        if (CanLoading()) return;

        IsStartFirst = false;
	}


    /// <summary>
    /// 読み込めるかどうか
    /// </summary>
    /// <returns>true : 読み込み / false 不要</returns>
    bool CanLoading()
    {
        var Loading = GetComponent<TreeSaveDataLoading>().GetLoadData();

        if (Loading.ID >= 0)
        {
            CreateIndex = Loading.ID;
            transform.lossyScale.Set(Loading.Scale.X, Loading.Scale.Y, Loading.Scale.Z);
            CreateChildren();
            BroadenValue *= CreateIndex;

            return true ;
        }

        return false;
    }
    
	// Update is called once per frame
	void Update () {
        StartChange();
        ChangeChildren();
        DestroyChildren();
	}

    /// <summary>
    /// 通常の状態に戻す
    /// 
    /// </summary>
    public void ChangeNormalState()
    {
        DataSave.AllSave();
        Save();
        CameraMain.BroadenMoveRadius(BroadenValue);
        State = STATE.Normal;

        if (!IsStartFirst)
        {
            SEPlayer.Play();
            FruitsFall();
            LeafsFall();
            var clone = (GameObject)Instantiate(GrowthEffect, GrowthEffect.transform.position, Quaternion.identity);
            clone.transform.parent = transform;
        }

    }

    /// <summary>
    /// 木の実が落ちる処理
    /// </summary>
    void FruitsFall()
    {
        var fruits = GameObject.FindGameObjectsWithTag("Fruit");
        if (fruits.Length == 0) return;

        foreach (var fruit in fruits)
        {
            var falling = fruit.GetComponent<FruitFalling>();
            falling.OnFall();
        }
    }

    /// <summary>
    /// 葉っぱが落ちる処理
    /// </summary>
    void LeafsFall()
    {
        var leafs = GameObject.FindGameObjectsWithTag("Leaf");
        if (leafs.Length == 0) return;

        foreach (var leaf in leafs)
        {
            var falling = leaf.GetComponent<LeafFalling>();
            falling.OnFall();
        }
    }
    /// <summary>
    /// 次の木へ切り替える
    /// </summary>
    public void NextChange()
    {
        if (TreeData[CreateIndex].FeverNumTimes != Fever.NumTimes)
        {
            Scaling.NextScale();
            Save();
        }
        else if (CreateIndex <= TreeData.Count)
        {
            CreateIndex++;
            Save();
            CreateIndex--;
        }
    }

    /// <summary>
    /// セーブ
    /// </summary>
    public void Save()
    {
        Writer.Write(CreateIndex - 1, LocalPos);
    }

    /// <summary>
    /// 変更開始する
    /// </summary>
    void StartChange()
    {
        if (!ModeManager.IsFerverMode) return;
        if (State != STATE.Normal) return;
        if (CreateIndex >= TreeData.Count) return;

        ChangeJudgment();
    }

    /// <summary>
    /// 切り替えるのかを判断する場所
    /// </summary>
    void ChangeJudgment()
    {
        if (TreeData[CreateIndex].FeverNumTimes != Fever.NumTimes)
        {
            State = STATE.Scaling;
        }
        else
        {
            State = STATE.Change;
            Tree = GameObject.Find("Tree");
        }
    }

    /// <summary>
    /// 子オブジェクトを変更
    /// </summary>
    void ChangeChildren()
    {
        if (ModeManager.IsFerverMode ) return;
        if (State != STATE.Change) return;

        CreateChildren();
    }

    /// <summary>
    /// 子オブジェクトを生成
    /// </summary>
    void CreateChildren()
    {
        var clone = (GameObject)Instantiate(TreeData[CreateIndex].Prefab);

        clone.name = "Tree";
        clone.transform.parent = transform;
        clone.transform.localPosition = clone.transform.position;
        clone.transform.localScale = new Vector3(1, 1, 1);

        CreateIndex++;

        LocalPos = clone.transform.position;
        
        State = STATE.Destroy;
    }


    /// <summary>
    /// 子オブジェクトを削除
    /// </summary>
    void DestroyChildren()
    {
        if (State != STATE.Destroy) return;

        ChangeNormalState();

        IsStartFirst = false;

        Destroy(Tree);

    }

}
