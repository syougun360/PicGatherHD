using UnityEngine;
using System.Collections;

public class CameraZoomUpper : MonoBehaviour {
    
	// Use this for initialization
	void Start () {
	
	}

    private const int MinFieldOfView = 1;
    private const int MaxFieldOfView = 60;
  
    // Update is called once per frame
	void Update () {

        if (ModeManager.IsDrawingMode) return;

        MouseWheelCommand();

       // TouchDeltaYCommand();
    
        PinchCommand();
	}
   
    private float LastDistance = 0;
    private void PinchCommand()
    {
        if (Input.touchCount != 2) return;

        if(Input.GetTouch(0).phase == TouchPhase.Began ||Input.GetTouch(1).phase == TouchPhase.Began)
        {
            LastDistance = Vector2.Distance(Input.GetTouch(0).position, Input.GetTouch(1).position);
        }

        if (Input.GetTouch(0).phase != TouchPhase.Moved || Input.GetTouch(1).phase != TouchPhase.Moved) return;

        float Distance = Vector2.Distance(Input.GetTouch(0).position, Input.GetTouch(1).position);

        Camera.main.fieldOfView += (LastDistance - Distance) * 0.1f;

        LastDistance = Vector2.Distance(Input.GetTouch(0).position, Input.GetTouch(1).position);

        if (Camera.main.fieldOfView < MinFieldOfView) Camera.main.fieldOfView = MinFieldOfView;
        if (Camera.main.fieldOfView > MaxFieldOfView) Camera.main.fieldOfView = MaxFieldOfView;
 
    }

    private void MouseWheelCommand()
    {
        const float AddValue = 0.5f;

        float RelativeMouseWheelValue = Input.GetAxis("Mouse ScrollWheel");

        if (RelativeMouseWheelValue > 0)
        {
            if (Camera.main.fieldOfView > MinFieldOfView)
            {
                Camera.main.fieldOfView -= AddValue;
                if (Camera.main.fieldOfView < MinFieldOfView) Camera.main.fieldOfView = MinFieldOfView;
            }
        }

        if (RelativeMouseWheelValue < 0)
        {
            if (Camera.main.fieldOfView < MaxFieldOfView)
            {
                Camera.main.fieldOfView += AddValue;
                if (Camera.main.fieldOfView > MaxFieldOfView) Camera.main.fieldOfView = MaxFieldOfView;
            }
        }
    }

    private void TouchDeltaYCommand()
    {
        const float ScaleDownValue = 0.5f;
        //タッチパネルを１箇所タッチしていて
        //Y軸移動量分だけズームイン、ズームアウトする。
        if (Input.touchCount == 1)
        {
            Camera.main.fieldOfView -= Input.GetTouch(0).deltaPosition.y * ScaleDownValue;
            if (Camera.main.fieldOfView < MinFieldOfView) Camera.main.fieldOfView = MinFieldOfView;
            if (Camera.main.fieldOfView > MaxFieldOfView) Camera.main.fieldOfView = MaxFieldOfView;

        }
    }

}

