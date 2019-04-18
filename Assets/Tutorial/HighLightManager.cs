using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HighLightManager : MonoBehaviour {

    private Image ThisImage = null;

    [SerializeField]
    private float LoopTime = 1.0f;

    [SerializeField]
    private float DelayTime = 1.0f;

    [SerializeField,Range(0.0f,1.0f)]
    private float MinValue = 0.5f;

    [SerializeField, Range(0.0f, 1.0f)]
    private float MaxValue = 1.0f;

    /// <summary>
    /// 左にスライドするボタンを光らせるかどうか
    /// 光らせる...true 光らせない...false
    /// </summary>
    public bool IsHighLightMode { get; private set;}

	// Use this for initialization
	void Start () {
        IsHighLightMode = false;
        ThisImage = GetComponent<Image>();

        var value = MinValue;
        ThisImage.color = new Color(value, value, value);
    }
	
    void Update()
    {
    }

	// Update is called once per frame
    private void UpdateHandler(float value)
    {
        ThisImage.color = new Color(value,value,value);
    }

    /// <summary>
    /// 新しく作った葉を確認したことを伝え、光らないようにする
    /// </summary>
    public void AlreadyCheckedNewLeaf()
    {
        IsHighLightMode = false;
        iTween.Stop(gameObject);
        var value = MinValue;
        ThisImage.color = new Color(value, value, value);
    }

    /// <summary>
    /// 新しい葉が生成されたことを伝え、光るようにする
    /// </summary>
    public void CreatedNewLeaf()
    {
        var CCController = FindObjectOfType(typeof(CampusCaptureController)) as CampusCaptureController;
        var CampusTemplate = FindObjectOfType(typeof(CampusTemplateSetting)) as CampusTemplateSetting;

        if (CCController.CharaManager == null) return;

        if (!CampusTemplate.IsSelect || CCController.CharaManager.Name != "Leaf") return;

        IsHighLightMode = true;
        iTween.ValueTo(gameObject, iTween.Hash("from", MinValue, "to", MaxValue, "time", LoopTime, "delay", DelayTime, "easetype", iTween.EaseType.easeInSine, "looptype", iTween.LoopType.pingPong, "onupdate", "UpdateHandler"));
    }
}
