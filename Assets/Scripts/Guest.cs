using System.Reflection;
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

    private int queueIndex = 0;

    Animator animator; 
    

    void Start()
    {
        startPosition = transform.position;
        rigidbody = GetComponent<Rigidbody>();
        target = barPosition.transform;
        delay = Random.Range(0f, 10f); 
        Debug.Log("delay: " + delay);
        animator = GetComponent<Animator>();
        BarQueueManager.Instance.JoinQueue(this);
    }

    void Respawn()
    {
        transform.position = startPosition;
        served = false;
        target = barPosition.transform;
        isMoving = true;
        respawned = true; 
        Invoke(nameof(UpdateRespawnVariable), 0.5f);
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

        if (Vector3.Distance(transform.position, target.position) < 1.0f)
        {
            isMoving = false;
            animator.SetBool("AtBar", true); 
        }
    }

    public void BeingServed()
    {
        if (served) return;

        served = true;

        BarQueueManager.Instance.LeaveQueue(this);

        target = exitPosition.transform;
        isMoving = true;
        animator.SetBool("AtBar", false); 
    }

    public void AssignSlot(Transform slot, int index)
    {
        target = slot;
        queueIndex = index;
        isMoving = true;

        Debug.Log($"{gameObject.name} is now number {queueIndex} in queue");

        //animator.SetBool("AtBar", index == 0);
    }
}
