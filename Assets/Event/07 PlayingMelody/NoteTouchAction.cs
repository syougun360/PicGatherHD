using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class NoteTouchAction : MonoBehaviour {

    /// <summary>
    /// 死ぬまでの時間
    /// </summary>
    private const float DyingTime = 2.0f;

    /// <summary>
    /// イベントのプレハブのスクリプトを得る
    /// </summary>
    private PlayingMelody Manager = null;

    /// <summary>
    /// すでにタッチされているかどうか
    /// されている...true されていない...false
    /// </summary>
    private bool AlreadyTouched = false;

    /// <summary>
    /// タッチした際に出てくるエフェクト
    /// </summary>
    [SerializeField]
    private GameObject EffectPrefab = null;

    void Start()
    {
        Manager = transform.parent.transform.parent.GetComponent<PlayingMelody>();
    }

    void Update()
    {
        if (AlreadyTouched) return;

        if(TouchManager.IsMouseButtonDown(gameObject) || TouchManager.IsTouching(gameObject))
        {
            Manager.AddSoundNumber();
            DisableNote();
            var clone = (GameObject)Instantiate(EffectPrefab);
            clone.transform.SetParent(transform);

        }
    }

    /// <summary>
    /// 音符の機能を消す
    /// </summary>
    private void DisableNote()
    {        
        Destroy(gameObject, DyingTime);
        AlreadyTouched = true;
        iTween.FadeTo(gameObject, iTween.Hash("alpha", 0, "time", DyingTime, "easetype", iTween.EaseType.easeOutExpo));

    }
}
