/// ---------------------------------------------------
/// date ： 2015/01/17    
/// brief ： 雨を生成する処理
/// author ： Yamada Masamistu
/// ---------------------------------------------------

using UnityEngine;
using System.Collections;

public class RainCreator : MonoBehaviour {

    [SerializeField]
    GameObject Prefab = null;

    [SerializeField]
    GameObject PrefabManager = null;

    GameObject Manager = null;
    
    int Count = 0;
    bool IsCreate = false;

    int CreateCountSpace = 2;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
        CreateParticle();
	}

    /// <summary>
    /// パーティクルを生成
    /// </summary>
    void CreateParticle()
    {
        if (!IsCreate) return;

        Count++;
        if (Count % CreateCountSpace != 0) return;

        var Scale = transform.localScale / 2;
        var PosX = transform.position.x + Random.Range(-Scale.x, Scale.x);
        var PosY = transform.position.y + -1;
        var PosZ = transform.position.z + Random.Range(-Scale.z, Scale.z);
        var Clone = (GameObject)Instantiate(Prefab, new Vector3(PosX, PosY, PosZ), Prefab.transform.rotation);
        
        Clone.transform.parent = Manager.transform;
        Clone.name = Prefab.name;
    }

    /// <summary>
    /// 生成スタート
    /// 管理オブジェクトを生成
    /// </summary>
    public void StartCreate()
    {
        IsCreate = true;

        Manager = (GameObject)Instantiate(PrefabManager, Vector3.zero, Quaternion.identity);
        Manager.name = PrefabManager.name;
    }

    /// <summary>
    /// ストップ
    /// 管理オブジェクトを10秒後、削除
    /// </summary>
    public void StopCreate()
    {
        IsCreate = false;
        Destroy(Manager, 6);
    }
}
