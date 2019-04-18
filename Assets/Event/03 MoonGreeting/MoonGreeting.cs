using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MoonGreeting : EventBase
{
    //　月の情報
    GameObject moon;
    float animationCount = 0;
    readonly Vector3 STARTPOS = new Vector3(Screen.width - Screen.width * 0.1f, Screen.height - Screen.height * 0.165f, 22);
    const float fieldOfViewMax = 60;
    const float fieldOfViewMin = 24;
    const float fieldOfViewAddValue = 1.0f;

    [SerializeField]
    GameObject moonPrefab = null;

    [SerializeField]
    float greetTime = 5.0f;

    [SerializeField]
    float lookTime = 2.0f;

    GameObject CelestialBody = null;


    //　カメラ回転を保存する変数
    Quaternion cameraRotation;

    enum State
    {
        ATTENSION,
        ZOOMUP,
        GREET,
        ZOOMOUT,
        RETURN
    };
    State state;

    // Use this for initialization
    void Start()
    {
        var pos = Camera.main.ScreenToWorldPoint(STARTPOS);

        moon = Instantiate(moonPrefab, pos, Quaternion.identity) as GameObject;
        moon.gameObject.transform.parent = transform;
        moon.name = moonPrefab.name;
        moon.transform.LookAt(transform.position + Camera.main.transform.rotation * Vector3.back);
        moon.transform.rotation *= Quaternion.Euler(0, 180, 0);

        state = State.ATTENSION;

        CelestialBody = GameObject.Find("CelestialBody");
        CelestialBody.SetActive(false);

        cameraRotation = Camera.main.transform.rotation;
        Camera.main.GetComponent<CameraMover>().enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        Attension();
        ZoomUp();
        Greet();
        ZoomOut();
        Return();
        Finish();
    }
    
    /// <summary>
    /// 月に注目する
    /// </summary>
    void Attension()
    {
        if (state != State.ATTENSION) return;

        var targetRotation = Quaternion.LookRotation(moon.transform.position - Camera.main.transform.position);
        Camera.main.transform.rotation = Quaternion.Lerp(Camera.main.transform.rotation, targetRotation, lookTime * Time.deltaTime);
        if (Camera.main.transform.rotation == targetRotation)
        {
            state = State.ZOOMUP;
        }

    }

    /// <summary>
    /// 月にズームアップする
    /// </summary>
    void ZoomUp()
    {
        if (state != State.ZOOMUP) return;

        if (Camera.main.fieldOfView > fieldOfViewMin)
        {
            Camera.main.fieldOfView += -fieldOfViewAddValue;
        }
        else
        {
            state = State.GREET;
        }
    }

    /// <summary>
    /// 挨拶
    /// </summary>
    void Greet()
    {
        if (state != State.GREET) return;
       
        animationCount += Time.deltaTime;

        if (animationCount >= greetTime)
        {
            state = State.ZOOMOUT;
        }
    }

    /// <summary>
    /// ズームアウト
    /// </summary>
    void ZoomOut()
    {
        if (state != State.ZOOMOUT) return;

        Camera.main.fieldOfView += fieldOfViewAddValue;
        if (Camera.main.fieldOfView > fieldOfViewMax)
        {
            Camera.main.fieldOfView = fieldOfViewMax;
            state = State.RETURN;
        }
    }

    /// <summary>
    /// カメラを元に戻す
    /// </summary>
    void Return()
    {
        if (state != State.RETURN) return;
        Camera.main.transform.rotation = Quaternion.Lerp(Camera.main.transform.rotation, cameraRotation, Time.deltaTime);
    }

    /// <summary>
    /// 終了時の処理
    /// </summary>
    protected override void Finish()
    {
        if (state != State.RETURN) return;
        if (Camera.main.transform.rotation != cameraRotation) return;

        Camera.main.GetComponent<CameraMover>().enabled = true;

        Destroy(moon);

        CelestialBody.SetActive(true);

        base.Finish();

    }
}
