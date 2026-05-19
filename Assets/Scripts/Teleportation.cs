
using UnityEngine;

public class Teleportation : MonoBehaviour
{
    private Rigidbody rigidBody;
    private Vector3 startPosition;
    private Quaternion startRotation;
    public float maxDistance;
    private bool isTeleporting = false;
    public ParticleSystem teleportEffect;

    public Wobble wobble;

    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
        startPosition = transform.position;
        startRotation = transform.rotation;
    }

    void Update()
    {
        if (isTeleporting) {
            if (wobble != null) wobble.fillLevel = 0.2f;
            return;
        }
        
        if (transform.position.y < maxDistance)
        {
            StartCoroutine(Teleport());
        }
    }

    private System.Collections.IEnumerator Teleport()
    {
        isTeleporting = true;
        
    
        yield return new WaitForSeconds(0.1f);
        
        var Obj = Instantiate(teleportEffect, transform.position, Quaternion.identity);
        Obj.transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);

        GetComponent<Renderer>().enabled = false;
        MovePosition();

        yield return new WaitForSeconds(1.0f);

        Destroy(Obj.gameObject, Obj.main.duration / 2.1f);
        GetComponent<Renderer>().enabled = true;

        isTeleporting = false;
    }

    public void MovePosition()
    {
        
        transform.position = startPosition;
        transform.rotation = startRotation;
        rigidBody.linearVelocity = Vector3.zero;
        rigidBody.angularVelocity = Vector3.zero;
    }
}
