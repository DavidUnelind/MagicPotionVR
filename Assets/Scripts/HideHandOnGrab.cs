using UnityEngine;

public class HideHandOnGrab : MonoBehaviour
{
    public GameObject handVisuals;

    private OVRGrabbable grabbable;

    void Awake()
    {
        grabbable = GetComponent<OVRGrabbable>();
    }

    void Update()
    {
        if (grabbable == null || handVisuals == null) return;

        if (grabbable.isGrabbed)
        {
            if (handVisuals.activeSelf)
                handVisuals.SetActive(false);
        }
        else
        {
            if (!handVisuals.activeSelf)
                handVisuals.SetActive(true);
        }
    }
}