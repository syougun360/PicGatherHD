using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PageForwardAction : MonoBehaviour {

    private StoryBoardAnimator StoryBoardAnim = null;

    [SerializeField]
    private GameObject EffectPrefab = null;

    void Start()
    {
        StoryBoardAnim = FindObjectOfType(typeof(StoryBoardAnimator)) as StoryBoardAnimator;

    }

	// Update is called once per frame
	void Update () {
      
        foreach(var touch in Input.touches)
        {
            if(touch.phase == TouchPhase.Ended)
            {
                StoryBoardAnim.AddIndex(1);
                StoryBoardAnim.RefreshSprite();
                CreateTouchEffect(touch.position);
            }
        }

        if(Input.GetMouseButtonUp(0))
        {
            StoryBoardAnim.AddIndex(1);
            StoryBoardAnim.RefreshSprite();
            CreateTouchEffect(Input.mousePosition);
        }
	}

    private void CreateTouchEffect(Vector3 screenPosition)
    {
        screenPosition.z = 0.7f;

        var WorldPosition = Camera.main.ScreenToWorldPoint(screenPosition);

        var clone = (GameObject)Instantiate(EffectPrefab, WorldPosition, Quaternion.identity);

        clone.transform.SetParent(transform);
    }
}
