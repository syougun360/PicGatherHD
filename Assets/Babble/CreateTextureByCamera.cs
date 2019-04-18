using UnityEngine;
using System.Collections;
using System.IO;
using System;

public class CreateTextureByCamera : MonoBehaviour {

    public Texture2D Screenshot { get; private set; }

    void Awake()
    {
        int width = Screen.width;
        int height = Screen.height;
        Screenshot = new Texture2D(width, height, TextureFormat.RGB24, false);
        this.camera.fieldOfView = 20;
        this.camera.aspect = 1.0f;

    }

    void OnPostRender()
    {
        Screenshot.ReadPixels(new Rect(0, 0, Screen.width, Screen.height), 0, 0);
        Screenshot.Apply();
    }

    void OnDestroy()
    {
        Destroy(Screenshot);
    }
}
