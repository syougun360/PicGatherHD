using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ShareListCloseController : MonoBehaviour {

    [SerializeField]
    GameObject ShareList = null;

    [SerializeField]
    CaptureController Capture = null;

    Button CloseButton = null;

	// Use this for initialization
	void Start () {
        CloseButton = GetComponent<Button>();
        CloseButton.onClick.AddListener(None);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void None()
    {
        Capture.ButtonEnable();
        UISelectManager.ChangeNoneMode();
    }
}
