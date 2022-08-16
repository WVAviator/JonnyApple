using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ScrollableContent : MonoBehaviour {

    RectTransform rt;
    public int checkTime = 20;
    int moveCheck = 0;

    void Start()
    {
        rt = GetComponent<RectTransform>();
    }

    void Update()
    {
        foreach (Touch touch in Input.touches)
        {
            if (RectTransformUtility.RectangleContainsScreenPoint(rt, touch.position))
            {
                if (touch.phase == TouchPhase.Began) moveCheck = 0;               
                if (touch.phase == TouchPhase.Ended && moveCheck < checkTime) SetActiveSelection();
                moveCheck++;
                
            }
        }
    }

    void SetActiveSelection()
    {
        GetComponentInParent<InventoryManager>().SetActiveSelection(gameObject);
    }
}
