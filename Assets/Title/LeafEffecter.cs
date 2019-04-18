using UnityEngine;
using System.Collections;

public class LeafEffecter : MonoBehaviour {

    TitleStartter Startter = null;
    Vector3 Velocity = Vector3.zero;

	// Use this for initialization
	void Start () {

        renderer.material.mainTexture = Resources.Load("Leaf" + "/" + Random.Range(0,3)) as Texture2D;

        Startter = GameObject.FindObjectOfType(typeof(TitleStartter)) as TitleStartter;

        Velocity = new Vector3(Startter.WindDirection + Random.Range(0, 10), Random.Range(-5, 5), Random.Range(-5, 5));
	}

	// Update is called once per frame
	void Update () {
        if (!Startter.IsStart) return;

        transform.Translate(Velocity * Time.deltaTime);

        Velocity *= 0.99f;

        transform.Rotate(new Vector3(0, 0, Velocity.z * Time.deltaTime));
	}
}
