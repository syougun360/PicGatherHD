using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CanNotCharacterDrawing : MonoBehaviour {

    [SerializeField]
    CharacterManager Manager = null;

    [SerializeField]
    GameObject NotPrefab = null;

    Button CharaButton = null;

    void Start()
    {
        CharaButton = GetComponent<Button>();
    }

    void Update()
    {
        if (!Manager.CanDrawing)
        {
            CharaButton.enabled = false;
            NotPrefab.SetActive(true);
        }
        else
        {
            CharaButton.enabled = true;
            NotPrefab.SetActive(false);
        }
    }


}
