using UnityEngine;
using System.Collections;

public class BabbleInstantiate : MonoBehaviour
{
    /// <summary>
    /// 生成するプレハブ
    /// </summary>
    [SerializeField]
    private GameObject CreateObjectPrefab = null;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

        CreateBabble();
	}

    /// <summary>
    /// インスタンスを生成するかどうかを決定する
    /// </summary>
    /// <returns>生成する...true 生成しない,できない...false</returns>
    private bool CanInstantiate()
    {
        /// 条件
        {
            /// 生成できるシャボン玉の最大数
            const int MaxBabbleNumber = 1;

            /// 現在生成されているシャボン玉の数を得るために検索し取得する
            var Babbles = GameObject.FindGameObjectsWithTag("Babble");

            if (Babbles.Length >= MaxBabbleNumber) return false;
        }

        return true;
    }

    private void CreateBabble()
    {
   	    if(CanInstantiate())
        {
            Instantiate(CreateObjectPrefab,Vector3.zero,Quaternion.identity);
        }
    }

}
