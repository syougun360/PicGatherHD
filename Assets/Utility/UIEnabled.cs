using UnityEngine;
using System.Collections;

public class UIEnabled : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}


    public static void Enabled()
    {
        var objs = GameObject.FindGameObjectsWithTag("UI");

        foreach (var obj in objs)
        {
            var ui = obj.GetComponent<ChangeDrawUIController>();
            ui.Enabled();
        }
    }

    public static void Unavailable()
    {
        var objs = GameObject.FindGameObjectsWithTag("UI");

        foreach (var obj in objs)
        {
            var ui = obj.GetComponent<ChangeDrawUIController>();
            ui.Unavailable();
        }
    }
}
