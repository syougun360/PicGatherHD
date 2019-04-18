using UnityEngine;
using System.Collections;

public class FairyEating : MonoBehaviour {


    [SerializeField]
    float MaxScale = 5.0f;

    [SerializeField]
    float AddScale = 0.5f;

    FairyMover Move = null;
 
    float Scale = 0;
    float ScaleTime = 5.0f;

    [SerializeField]
    string eatSE = string.Empty;

    SoundEffectPlayer Player = null;

	// Use this for initialization
	void Start () {
        Move = GetComponent<FairyMover>();
        Scale = transform.lossyScale.x;
        Player = GameObject.Find("SEPlayer").GetComponent<SoundEffectPlayer>();
	}
	
	// Update is called once per frame
	void Update () {


        if (transform.lossyScale.x >= MaxScale)
        {
            Move.SetStateAbsorption();
        }

	}

    void OnCollisionStay(Collision collision)
    {

        if (Move.IsMove) return;

        if (collision.gameObject.name == "Fruit")
        {
            Move.ChangeMoveAnimation();

            Scale += AddScale;
            iTween.ScaleTo(gameObject,new Vector3(Scale, Scale, Scale),ScaleTime);

            Destroy(collision.gameObject);

            Player.Play(eatSE);

            var Manager = GameObject.FindObjectOfType(typeof(FruitManagerController)) as FruitManagerController;
            Manager.ChildrensDataSave();
        }

    }
}
