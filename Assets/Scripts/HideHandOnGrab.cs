using UnityEngine;

public class HideHandOnGrab : MonoBehaviour
{
    [Tooltip("Assign the hand visuals root to hide when this object is grabbed.")]
    public GameObject handVisuals;

    private OVRGrabbable grabbable;
    private bool lastGrabState;

    void Awake()
    {
        grabbable = GetComponent<OVRGrabbable>();
    }

    void Update()
    {
        if (grabbable == null) return;

        bool isGrabbed = grabbable.isGrabbed;
        if (isGrabbed == lastGrabState) return;
        lastGrabState = isGrabbed;

        if (handVisuals != null)
        {
            handVisuals.SetActive(!isGrabbed);
            return;
        }

        if (grabbable.grabbedBy != null)
        {
            ToggleRenderers(grabbable.grabbedBy.gameObject, !isGrabbed);
        }
    }

    private void ToggleRenderers(GameObject root, bool visible)
    {
        if (root == null) return;

        Renderer[] renderers = root.GetComponentsInChildren<Renderer>(true);
        foreach (Renderer renderer in renderers)
        {
            if (renderer != null)
            {
                renderer.enabled = visible;
            }
        }
    }
}