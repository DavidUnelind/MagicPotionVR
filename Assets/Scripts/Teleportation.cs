using UnityEngine;

public class Teleportation : MonoBehaviour
{
    private Rigidbody rigidBody;
    private Vector3 startPosition;
    public float maxDistance;
    private bool isTeleporting = false;
    public ParticleSystem teleportEffect;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        startPosition = transform.position;
        rigidBody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isTeleporting) return;

        //float distance = Vector3.Distance(startPosition, transform.position);

        float distanceY = Mathf.Abs(startPosition.y - transform.position.y);

        if (distanceY >= maxDistance)
        {
            StartCoroutine(Teleport());
        }
    }

    private System.Collections.IEnumerator Teleport()
    {
        isTeleporting = true;

        yield return new WaitForSeconds(3.0f);

        var Obj = Instantiate(teleportEffect, transform.position, Quaternion.identity);
        Obj.transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);

        GetComponent<Renderer>().enabled = false;
        transform.position = startPosition;
        transform.rotation = Quaternion.identity;
        rigidBody.linearVelocity = Vector3.zero;
        rigidBody.angularVelocity = Vector3.zero;

        yield return new WaitForSeconds(1.0f);

        Destroy(Obj.gameObject, Obj.main.duration / 2.1f);
        GetComponent<Renderer>().enabled = true;

        isTeleporting = false;
    }
}
