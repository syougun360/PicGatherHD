using UnityEngine;
using System.Collections;

public class EffecterMoveSetting : MonoBehaviour {

    [System.Serializable]
    public struct RandomVelocity
    {
        public RandomVelocity(float max, float min):this()
        {
            Max = max;
            Min = min; 
        }

        public float Speed { get { return Random.Range(Min, Max); } }
        public float Min;
        public float Max;
    }

    [SerializeField]
    RandomVelocity VelocityX = new RandomVelocity(0, 0);

    [SerializeField]
    RandomVelocity VelocityY = new RandomVelocity(0, 0);

    [SerializeField]
    RandomVelocity VelocityZ = new RandomVelocity(0, 0);

    Vector3 Velocity = Vector3.zero;

    // Use this for initialization
    void Start()
    {
        Velocity = new Vector3(VelocityX.Speed, VelocityY.Speed, VelocityZ.Speed);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Velocity * Time.deltaTime);

        StartCoroutine("AlphaControl");
    }

    /// <summary>
    /// アルファ値の制御
    /// </summary>
    /// <returns></returns>
    IEnumerator AlphaControl()
    {
        yield return new WaitForSeconds(1.0f);

        Velocity *= 0.98f;

        var alpha = new Color(0, 0, 0, 0.05f);
        renderer.material.color -= alpha;
        if (renderer.material.color.a <= 0)
        {
            Destroy(gameObject);
        }
    }
}
