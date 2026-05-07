/*using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class HideHandOnGrab : MonoBehaviour
{
    public GameObject handModel;
    private XRGrabInteractable grabInteractable;
    void Awake()
    {
        grabInteractable = GetComponent<XRGrabInteractable>();

        grabInteractable.selectEntered.AddListener(HideHand);
        grabInteractable.selectExited.AddListener(ShowHand);
    }

    void HideHand(SelectedEnterEventArgs args)
    {
        handModel.SetActive(false);
    }

    void ShowHand(SelectedExitEventArgs args)
    {
        handModel.SetActive(true);
    }
}
*/