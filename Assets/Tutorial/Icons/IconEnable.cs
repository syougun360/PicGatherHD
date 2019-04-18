using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class IconEnable : MonoBehaviour {

    /// <summary>
    /// 親のImage
    /// </summary>
    private Image ParentImage = null;

    /// <summary>
    /// gameObjectのImage
    /// </summary>
    private Image ThisImage = null;

    void Start()
    {
        ParentImage = transform.parent.transform.parent.GetComponent<Image>();
        ThisImage = GetComponent<Image>();
    }

	// Update is called once per frame
	void Update () {
        if (!ParentImage.enabled)
        {
            ThisImage.enabled = ParentImage.enabled;
        }
	}
}
