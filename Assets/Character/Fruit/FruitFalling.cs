using UnityEngine;
using System.Collections;

public class FruitFalling : MonoBehaviour {

    [SerializeField]
    float FallSpeed = 1;

    public bool IsFall{get; private set;}


	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
        if (!IsFall) return;

        transform.Translate(Vector3.down * FallSpeed * Time.deltaTime);

        var cameraMain = Camera.main;
        var screenDown = cameraMain.ScreenToWorldPoint(new Vector3(0, -Screen.height/2, -cameraMain.transform.position.z)).y;

        if (transform.position.y < screenDown)
        {
            Destroy(gameObject);
        }
	}

    public void OnFall()
    {
        IsFall = true;

        var Manager = GameObject.FindObjectOfType(typeof(FruitManagerController)) as FruitManagerController;
        Manager.DataClear();
    }
}
