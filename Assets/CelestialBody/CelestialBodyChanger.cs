/// ---------------------------------------------------
/// date ： 2015/02/19  
/// brief ： 天体(太陽、月)を切り替える処理
/// author ： Yamada Masamistu
/// ---------------------------------------------------
using UnityEngine;
using System.Collections;

public class CelestialBodyChanger : MonoBehaviour {

    [SerializeField]
    float RotationValue = 10.0f;

    RectTransform RectTrans = null;

    bool IsChange = false;

    const float ChangeRotation = 180;

    enum STATE
    { 
        Sun,
        Moon,
    };
    STATE State = STATE.Sun;

	// Use this for initialization
	void Start () {
        RectTrans = transform as RectTransform;

        if (DateTimeController.IsMorning || DateTimeController.IsNoon)
        {
            State = STATE.Sun;
            RectTrans.localEulerAngles.Set(0, 0, 0);
        }
        if (DateTimeController.IsNight || DateTimeController.IsSleep)
        {
            State = STATE.Moon;
            RectTrans.rotation = Quaternion.Euler(0, 0, ChangeRotation);
        }
	}
	
	// Update is called once per frame
	void Update () {
        if (IsChange)
        {
            MoonChangeControl();
            SunChangeControl();
        }
        else
        {
            StartChange(DateTimeController.IsMorning, STATE.Sun);
            StartChange(DateTimeController.IsNoon, STATE.Sun);
            StartChange(DateTimeController.IsNight, STATE.Moon);
            StartChange(DateTimeController.IsSleep, STATE.Moon);
        }
    }
    
    /// <summary>
    /// 切り替える
    /// </summary>
    void StartChange(bool isTime,STATE state )
    {
        if (isTime && State != state)
        {
            State = state;
            IsChange = true;
        }
    }

    /// <summary>
    /// 月の切り替え処理
    /// </summary>
    void MoonChangeControl()
    {
        if (State != STATE.Moon) return;
            
        if (RectTrans.localEulerAngles.z >= ChangeRotation)
        {
            IsChange = false;
            RectTrans.localEulerAngles.Set(0, 0, ChangeRotation);
            return;
        }
        RectTrans.Rotate(Vector3.forward * RotationValue * Time.deltaTime);
    }

    /// <summary>
    /// 太陽の切り替え処理
    /// </summary>
    void SunChangeControl()
    {
        if (State != STATE.Sun) return;

        if (RectTrans.localEulerAngles.z <= RotationValue * Time.deltaTime)
        {
            IsChange = false;
            RectTrans.localEulerAngles.Set(0, 0, 0);
            return;
        }
        RectTrans.Rotate(Vector3.forward * -RotationValue * Time.deltaTime);
    }

}
