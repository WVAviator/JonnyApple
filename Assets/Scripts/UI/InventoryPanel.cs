using UnityEngine;
using System.Collections;

public class InventoryPanel : MonoBehaviour {

    public float panelOpenSpeed = 0.1f;
    private bool isOpen = false;
    private bool isTransiting = false;
    private Vector3 desiredPosition;
    private RectTransform rt;
    public RectTransform panel;

    void Start()
    {
        rt = GetComponent<RectTransform>();
    }

    public void ToggleInventory()
    {
        if (isOpen) CloseInventory();
        if (!isOpen) OpenInventory();
    }

    void OpenInventory()
    {
        if (isTransiting) return;
        
        desiredPosition = new Vector3(rt.localPosition.x - 100, rt.localPosition.y, rt.localPosition.z);
        isTransiting = true;
    }

    void CloseInventory()
    {
        if (isTransiting) return;

        desiredPosition = new Vector3(rt.localPosition.x + 100, rt.localPosition.y, rt.localPosition.z);
        isTransiting = true;
    }

    void Update()
    {

        if (isTransiting)
        {
            rt.localPosition = Vector3.Lerp(rt.localPosition, desiredPosition, panelOpenSpeed);
            panel.localPosition = Vector3.Lerp(rt.localPosition, desiredPosition, panelOpenSpeed);

            if (Mathf.Abs(rt.localPosition.x - desiredPosition.x) < 0.1)
            {
                isOpen = !isOpen;
                isTransiting = false;
            }
        }
    }

}
