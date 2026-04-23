using UnityEngine;

public class Guest : MonoBehaviour
{
    private Rigidbody rigidbody;
    [SerializeField] public float speed = 0.0f;
    private Vector3 startPosition;
    [SerializeField] public GameObject barPosition;
    [SerializeField] public GameObject exitPosition;
    private Transform target;
    private bool served = false;
    private bool isMoving = true;

    public bool respawned = false; 

    private float delay = 0f; 

    void Start()
    {
        startPosition = transform.position;
        rigidbody = GetComponent<Rigidbody>();
        target = barPosition.transform;
        delay = Random.Range(0f, 10f); 
        Debug.Log("delay: " + delay);
    }

    void Respawn()
    {
        transform.position = startPosition;
        served = false;
        target = barPosition.transform;
        isMoving = true;
        respawned = true; 
        Invoke(nameof(UpdateRespawnVariable), 1f);
    }

    void UpdateRespawnVariable()
    {
        respawned = false; 
    }

    void Update()
    {
        if (Vector3.Distance(transform.position, exitPosition.transform.position) < 0.5f)
        {
            isMoving = false; 
            Invoke(nameof(Respawn), delay);
        }

        if (!isMoving) return;
        
        transform.position = Vector3.MoveTowards(transform.position, target.position, Time.deltaTime * speed);

        if (Vector3.Distance(transform.position, target.position) < 0.1f)
        {
            isMoving = false;
        }
    }

    public void BeingServed()
    {
        if (served) return;

        served = true;
        target = exitPosition.transform;
        isMoving = true;
    }
}
