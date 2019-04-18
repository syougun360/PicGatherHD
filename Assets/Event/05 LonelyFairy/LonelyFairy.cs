using UnityEngine;
using System.Collections;

public class LonelyFairy : EventBase
{
    //開始アイコン決定後に実装
    /*
    /// <summary>
    /// イベント開始時に表示するアイコン
    /// </summary>
    [SerializeField]
    private GameObject EventIcon = null;

    /// <summary>
    /// イベントが始まったことをユーザーに教える
    /// </summary>
    private void EventBeginSign()
    {
        var ScreenPosition = Camera.main.ScreenToWorldPoint( new Vector3(Screen.width/2,Screen.height,1.0f));

        Instantiate(EventIcon, ScreenPosition, Quaternion.identity);
    }
    */

    /// <summary>
    /// 生成するイベント用の木の実
    /// </summary>
    [SerializeField]
    private GameObject LonelyFruitPrefab = null;

    /// <summary>
    /// 現在の妖精の状態を得る
    /// </summary>
    private LonelyFairyMover ChildFairy = null;

    // Use this for initialization
    void Start()
    {
        //EventBeginSign();
        CreateLonelyFruit();
        ChildFairy = GetComponentInChildren<LonelyFairyMover>();

    }

    // Update is called once per frame
    void Update()
    {

        if(!ChildFairy)
        {
            ChildFairy = GetComponentInChildren<LonelyFairyMover>();
        }
        else if (ChildFairy.NowMoveMode == LonelyFairyMover.MoveMode.OutOfScreen)
        {
            Finish();
        }

    }

    /// <summary>
    /// 終了時の処理
    /// </summary>
    protected override void Finish()
    {
        base.Finish();
        Camera.main.GetComponent<CameraMover>().enabled = true;
    }

    /// <summary>
    /// 妖精が隠れる木の実を生成する
    /// </summary>
    private void CreateLonelyFruit()
    {
        var clone = (GameObject)Instantiate(LonelyFruitPrefab,GetBranchVertexPosition(),Quaternion.identity);
        clone.transform.parent = this.transform;
    }


    /// <summary>
    /// Rayを飛ばして当たったところに木の実を付ける。
    /// 当たるまで繰り返す。やばい。
    /// </summary>
    /// <returns>当たった場所</returns>
    private Vector3 GetBranchVertexPosition()
    {
        var AlreadyHit = false;

        var branchObject = GameObject.Find("Branch");

        var position = Vector3.zero;

        while(!AlreadyHit)
        {
            var point = new Vector3(Random.Range(0,Screen.width),Random.Range(0,Screen.height),1.0f);
            
            Ray ray = Camera.main.ScreenPointToRay(point);
            RaycastHit hit = new RaycastHit();
            if (Physics.Raycast(ray, out hit, (1 << branchObject.layer)))
            {
                if (hit.collider.gameObject == branchObject)
                {
                    position =  new Vector3(hit.point.x, hit.point.y, hit.point.z);
                    AlreadyHit = true;
                }
            }
        }

        return position;
    }

}
