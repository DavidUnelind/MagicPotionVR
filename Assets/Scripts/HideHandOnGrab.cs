/*using UnityEngine;
using Oculus.Interaction;
using Oculus.Interaction.HandGrab;

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

    void HideHand(SelectEnterEventArgs args)
    {
        handModel.SetActive(false);
    }

    void ShowHand(SelectExitEventArgs args)
    {
        handModel.SetActive(true);
    }
}

{
    private OVRHandGrabInteractable handGrabInteractable;
    private GameObject handVisuals;

    void Awake()
    {
        handGrabInteractable = GetComponent<OVRHandGrabInteractable>();
        handVisuals = GameObject.Find("HandVisuals");
    }

    void OnEnable()
    {
        handGrabInteractable.GrabBegin += OnGrabBegin;
        handGrabInteractable.GrabEnd += OnGrabEnd;
    }

    void OnDisable()
    {
        handGrabInteractable.GrabBegin -= OnGrabBegin;
        handGrabInteractable.GrabEnd -= OnGrabEnd;
    }

    void OnGrabBegin()
    {
        handVisuals.SetActive(false);
    }

    void OnGrabEnd()
    {
        handVisuals.SetActive(true);
    }
}*/
 