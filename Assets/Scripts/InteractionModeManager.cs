using UnityEngine;
using Oculus.Interaction;
using Oculus.Interaction.Input;

public class InteractionModeManager : MonoBehaviour
{
    [Header("Left Hand Groups")]
    public GameObject leftHand;                 
    public GameObject leftControllerAndNoHand; 
    public GameObject leftHandAndNoController;  

    [Header("Right Hand Groups")]
    public GameObject rightHand;
    public GameObject rightControllerAndNoHand;
    public GameObject rightHandAndNoController;

    public void SetMenuMode()
    {
        SetActive(leftControllerAndNoHand, true);
        SetActive(rightControllerAndNoHand, true);

        SetActive(leftHand, false);
        SetActive(rightHand, false);
        SetActive(leftHandAndNoController, false);
        SetActive(rightHandAndNoController, false);
    }

    public void SetGameplayMode()
    {
        SetActive(leftHand, true);
        SetActive(rightHand, true);
        SetActive(leftHandAndNoController, true);
        SetActive(rightHandAndNoController, true);

        SetActive(leftControllerAndNoHand, false);
        SetActive(rightControllerAndNoHand, false);
    }

    private void SetActive(GameObject obj, bool active)
    {
        if (obj != null) obj.SetActive(active);
    }
}