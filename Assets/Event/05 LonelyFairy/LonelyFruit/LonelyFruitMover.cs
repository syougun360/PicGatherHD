using UnityEngine;
using System.Collections;

public class LonelyFruitMover : MonoBehaviour {

    /// <summary>
    /// 落下時に出すパーティクル
    /// </summary>
    [SerializeField]
    private ParticleSystem ParticlePrefab = null;


    /// <summary>
    /// すでにパーティクルが生成されているかどうか
    /// </summary>
    private bool AlreadyCreatedParticle = false;


    /// <summary>
    /// 移動させるかどうかを受け取る参照
    /// </summary>
    private LonelyFairyCreator LFCreator = null;

	// Use this for initialization
	void Start () {
        LFCreator = GetComponent<LonelyFairyCreator>();
	}
	
	// Update is called once per frame
	void Update () {
	    if(LFCreator.IsFallingFruit)
        {
            var GravitySpeed = 0.6f * Time.deltaTime;
            transform.position = new Vector3(transform.position.x,transform.position.y - GravitySpeed,transform.position.z);
            if(!AlreadyCreatedParticle)
            {
                AlreadyCreatedParticle = true;
                var clone = (ParticleSystem)Instantiate(ParticlePrefab, transform.position, Quaternion.identity);
                clone.transform.parent = transform;
                clone.Play();
            }
        }
	}
}
