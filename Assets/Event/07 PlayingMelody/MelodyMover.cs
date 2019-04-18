using UnityEngine;
using System.Collections;

public class MelodyMover : MonoBehaviour {

    [SerializeField,Range(1,5)]
    private int NoteID = 0;

    /// <summary>
    /// イベントのプレハブのスクリプトを得る
    /// </summary>
    private PlayingMelody Manager = null;

    /// <summary>
    /// スクリーン上での座標
    /// </summary>
    private Vector3 PositionInScreen = Vector3.zero;

    /// <summary>
    /// 移動する目的地のスクリーン座標
    /// </summary>
    private Vector3 Target = Vector3.zero;

    /// <summary>
    /// 移動量
    /// </summary>
    private Vector3 Velocity = Vector3.zero;

    /// <summary>
    /// 音符の開始地点
    /// </summary>
    private float[] StartX = { -Screen.width * 0.2f, Screen.width * 1.2f };

    // Use this for initialization
	void Start () {
        ///親(Canvas)の親(PlayingMelody)を見る
        Manager = transform.parent.transform.parent.GetComponent<PlayingMelody>();

        var StartXNumber = Random.Range(0, StartX.Length);

        SetStartPosition(StartXNumber);

        SetTargetPosition(StartXNumber);

        Velocity = 0.3f * (Target - PositionInScreen);
	}
	
    /// <summary>
    /// 初期位置を決める
    /// </summary>
    /// <param name="startXNumber">Ｘ座標のテーブル情報</param>
    private void SetStartPosition(int startXNumber)
    {
        var StartY = Random.Range(-transform.lossyScale.y, Screen.height + transform.lossyScale.y);

        PositionInScreen = new Vector3(StartX[startXNumber], StartY, 1.0f);

        transform.position = Camera.main.ScreenToWorldPoint(PositionInScreen);
    }

    /// <summary>
    /// 進行先の位置を決める
    /// </summary>
    /// <param name="startXNumber">Ｘ座標のテーブル情報</param>
    private void SetTargetPosition(int startXNumber)
    {
        if (startXNumber == 0)
        {
            Target = new Vector3(StartX[1], Random.Range(-transform.lossyScale.y, Screen.height + transform.lossyScale.y), 1.0f);
        }
        else
        {
            Target = new Vector3(StartX[0], Random.Range(-transform.lossyScale.y, Screen.height + transform.lossyScale.y), 1.0f);
        }

    }

	// Update is called once per frame
	void Update () {
        if(Manager.CountTime > (NoteID - 1) * PlayingMelody.SoundSegmentSecond)
        {
            renderer.enabled = true;
            Move();
        }
        else
        {
            renderer.enabled = false;
        }
	}

    /// <summary>
    /// 移動
    /// </summary>
    private void Move()
    {
        PositionInScreen += Velocity * Time.deltaTime;

        transform.position = Camera.main.ScreenToWorldPoint(PositionInScreen);
        
    }
}
