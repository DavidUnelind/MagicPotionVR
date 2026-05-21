using UnityEngine;

public class DoorController : MonoBehaviour
{
    [Header("Door Settings")]
    public float openAngle = 90f;
    public float openSpeed = 2f;

    [Header("Detection")]
    public float detectionRadius = 3f;
    public string guestTag = "Guest";
    public Vector3 detectionOffset;

    private Quaternion closedRotation;
    private Quaternion openRotation;

    private bool isOpen = false;

    public AudioSource doorOpeningSound; 
    public AudioSource doorClosingSound; 

    void Start()
    {
        closedRotation = transform.rotation;

        openRotation = Quaternion.Euler(
            transform.eulerAngles + new Vector3(0, openAngle, 0)
        );
    }

    void Update()
    {
        bool temp = isOpen; 

        CheckForGuests();

        if (doorOpeningSound != null && temp == false && isOpen == true)
        {
            doorOpeningSound.Play(); 
        } else if (doorClosingSound != null && temp == true && isOpen == false)
        {
            doorClosingSound.time = 4f;
            doorClosingSound.Play();
        }

        Quaternion targetRotation = isOpen ? openRotation : closedRotation;

        transform.rotation = Quaternion.Slerp(
            transform.rotation,
            targetRotation,
            Time.deltaTime * openSpeed
        );
    }

    void CheckForGuests()
    {
        isOpen = false;

        Collider[] nearbyObjects = Physics.OverlapSphere(
            transform.position + detectionOffset,
            detectionRadius
        );

        foreach (Collider col in nearbyObjects)
        {
            if (col.CompareTag(guestTag))
            {
                isOpen = true;
                break;
            }
        }
    }

    // Shows detection radius in Scene view
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position + detectionOffset, detectionRadius);
    }
}