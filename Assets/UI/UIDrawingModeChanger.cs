using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class UIDrawingModeChanger : MonoBehaviour {

    [SerializeField]
    GameObject CelestialBody = null;

    [SerializeField]
    List<GameObject> UIButtons = new List<GameObject>();

    public void Enable(bool active)
    {
        CelestialBody.SetActive(active);

        foreach (var button in UIButtons)
        {
            if (button == null) continue;
            button.SetActive(active);
        }
    }
}
