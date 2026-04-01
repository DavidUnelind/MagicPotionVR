using UnityEngine;

public class Guest : MonoBehaviour
{
    private Rigidbody rigidbody;
    [SerializeField] public float speed = 0.0f;
    [SerializeField] public GameObject barPosition;
    [SerializeField] public GameObject exitPosition;
    private Transform target;
    private bool served = false;
    private bool isMoving = true;

    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        target = barPosition.transform;
    }

    void Update()
    {
        if (!isMoving) return;
        
        transform.position = Vector3.MoveTowards(transform.position, target.position, Time.deltaTime * speed);

        if (Vector3.Distance(transform.position, target.position) < 0.1f)
        {
            isMoving = false;
        }
    }

    public void BeingServed()
    {
        served = true;
        target = exitPosition.transform;
        isMoving = true;
    }
}
