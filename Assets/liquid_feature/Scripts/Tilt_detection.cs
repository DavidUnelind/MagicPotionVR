using UnityEngine;

public class Tilt_detection : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform bottle;
    [SerializeField] private Transform pourPoint;
    [SerializeField] private GameObject streamObject;

    // Detta gör så att variabler syns i inspektorn
    [Header("Tilt Settings")]
    [SerializeField] private float pourThreshold = 0;

    
    private bool isPouring;
    private Stream currentStream = null;
    

    private void Start()
    {
        if (bottle == null)
            bottle = transform;
        streamObject = Instantiate(streamObject,pourPoint.position, Quaternion.identity, transform );
        streamObject.transform.localScale = Vector3.one;
        currentStream = streamObject.GetComponent<Stream>();
        streamObject.SetActive(false);
    }

    private void Update()
    {
        // 1 = helt upprätt, 0 = ungefär 90 graders tilt, < 0 = upp och ner
        float uprightDot = Vector3.Dot(bottle.up, Vector3.up);
        bool shouldPour = uprightDot < pourThreshold;

        //Debug.Log($"uprightDot: {uprightDot}, shouldPour: {shouldPour}");

        if (shouldPour != isPouring)
        {
            isPouring = shouldPour;
        }

        if(isPouring )
        {
            StartPour();
        }
        else
        {
            EndPour();
        }
    }

    private void StartPour()
    {
        Debug.Log("POURING STARTED");
        streamObject.SetActive(true);
        currentStream.Begin();
    }

    private void EndPour()
    {
        Debug.Log("POURING STOPPED");
        //streamObject = null;
        streamObject.SetActive(false);
    }
}