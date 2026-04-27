using UnityEngine;
using UnityEngine.UI;
using Oculus.Interaction;

public class Shaker : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private Rigidbody shaker;
    public Guest guest; 
    [SerializeField] public Slider progressBar; 
    public GameObject canvas; 
    private float shakingTime = 0f;
    private float shakingIntensity = 2f;
    public bool isDoneShaking = false; 
    private Vector3 lastPos;
    private Vector3 startPosition;

    void Start()
    {
        shaker = GetComponent<Rigidbody>();
        shaker.collisionDetectionMode = CollisionDetectionMode.Continuous;
        lastPos = transform.position;
        canvas.SetActive(false);
        progressBar.minValue = 0f; 
        progressBar.maxValue = 1f; 
        progressBar.value = 0f; 
        startPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(transform.position, startPosition) > 1.0f)
        {
            shaker.linearVelocity = Vector3.zero;
            shaker.angularVelocity = Vector3.zero;

            transform.position = Vector3.MoveTowards(
                transform.position,
                startPosition,
                2.0f * Time.deltaTime
            );
        }

        float speed = (transform.position - lastPos).magnitude / Time.deltaTime;
        lastPos = transform.position;

        if (speed > shakingIntensity)
        {
            shakingTime += Time.deltaTime;
        } else
        {
            shakingTime += 0f;
        }

        shakingTime = Mathf.Clamp(shakingTime, 0f, 3f);
        progressBar.value = shakingTime / 3f; 

        if (shakingTime >= 3f)
        {
            isDoneShaking = true;
            canvas.SetActive(false);
        }

        if (guest.respawned)
        {
            shakingTime = 0f;
            isDoneShaking = false;
            progressBar.value = 0f;
        }
    }
}
