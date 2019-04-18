using UnityEngine;
using System.Collections;

public class IconCreator : MonoBehaviour {

    [SerializeField]
    private GameObject Prefab = null;

	// Use this for initialization
	void Start () {
        var clone = (GameObject)Instantiate(Prefab,transform.position,Quaternion.identity);
        clone.transform.SetParent(transform);
        clone.transform.localScale = Prefab.transform.localScale;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
