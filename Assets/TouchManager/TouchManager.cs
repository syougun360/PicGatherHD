using UnityEngine;
using System.Collections;

public class TouchManager : MonoBehaviour {

    public static bool IsPhaseTap { get; private set; }
    public static bool IsPhaseSwipe { get; private set; }
    public static Vector3 TapPos { get; private set; }

    static GameObject RayCastHitObject = null;

	// Use this for initialization
	void Start () 
    {
	}
	
	// Update is called once per frame
	void Update () 
    {

	}

    static void SetNonPhase()
    {
        IsPhaseTap = IsPhaseSwipe = false;
    }


    static void TouchPhaseJudgment(Touch touch)
    {
        if (touch.phase == TouchPhase.Began) IsPhaseTap = true;
        if (touch.phase == TouchPhase.Moved) IsPhaseSwipe = true;
    }

    public static bool IsTouching(GameObject gameObject_)
    {
        SetNonPhase();

        foreach (var touch in Input.touches)
        {
            if (touch.phase == TouchPhase.Began)
            {
                Ray ray = Camera.main.ScreenPointToRay(touch.position);
                RaycastHit hit = new RaycastHit();

                if (IsRayCatsHit(ray, hit, touch, gameObject_, (1 << gameObject_.layer)))
                {
                    return true;
                }
            }
        }
        return false;
    }

    /// <summary>
    /// マウス左ボタンが押されてるときの処理
    /// Rayを飛ばして当たってたら、trueを返す
    /// </summary>
    /// <returns>当たったかどうか?の判定</returns>
    public static bool IsMouseButton(GameObject gameObject_)
    {
        if (Input.GetMouseButton(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit = new RaycastHit();
            if (IsRayCatsHit(ray, hit, gameObject_, (1 << gameObject_.layer)))
            {
                return true;
            }
        }
        return false;
    }

    /// <summary>
    /// マウス左ボタンが押されたときの処理
    /// Rayを飛ばして当たってたら、trueを返す
    /// </summary>
    /// <returns>当たったかどうか?の判定</returns>
    public static bool IsMouseButtonDown(GameObject gameObject_)
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit = new RaycastHit();
            if (IsRayCatsHit(ray, hit, gameObject_, (1 << gameObject_.layer)))
            {
                return true;
            }
        }
        return false;
    }


    /// <summary>
    /// RayCastで当たったかどうかの判定
    /// タップした座標を取得する。
    /// </summary>
    /// <param name="ray"></param>
    /// <param name="hit"></param>
    /// <returns>当たったかどうか</returns>
    static bool IsRayCatsHit(Ray ray, RaycastHit hit, GameObject gameObject_, int layerMask)
    {
        if (Physics.Raycast(ray, out hit, layerMask))
        {
            RayCastHitObject = hit.collider.gameObject;

            if (RayCastHitObject == gameObject_)
            {
                TapPos = new Vector3(hit.point.x, hit.point.y, hit.point.z);
                return true;
            }
        }
        return false;
    }

    /// <summary>
    /// RayCastで当たったかどうかの判定
    /// タップした座標を取得する。
    /// </summary>
    /// <param name="ray"></param>
    /// <param name="hit"></param>
    /// <returns>当たったかどうか</returns>
    static bool IsRayCatsHit(Ray ray, RaycastHit hit, Touch touch, GameObject gameObject_, int layerMask)
    {
        if (Physics.Raycast(ray, out hit, layerMask))
        {
            TouchPhaseJudgment(touch);
            RayCastHitObject = hit.collider.gameObject;

            if (RayCastHitObject == gameObject_)
            {
                TapPos = new Vector3(hit.point.x, hit.point.y, hit.point.z);
                return true;
            }
        }
        return false;
    }
}
