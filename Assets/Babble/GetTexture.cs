using UnityEngine;
using System.Collections;

public class GetTexture : MonoBehaviour
{
    
    public CreateTextureByCamera Target;

    void Start()
    {
    }

    void Update()
    {
        renderer.material.mainTexture = Target.Screenshot;
    }
}