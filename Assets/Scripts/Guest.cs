using UnityEngine;

public class Guest : MonoBehaviour
{
    private Rigidbody rb;          
    private Animator animator;

    [SerializeField] public float speed = 3.0f;

    private Transform exitPosition;
    private Transform target;
    private bool served = false;
    private bool isMoving = false;  
    private int queueIndex = 0;

    private bool initialized = false;

    public Recipe recipe; 

    public bool isAtBar => animator != null && animator.GetBool("AtBar");

    public void Init(Transform exit, Recipe newRecipe)
    {
        exitPosition = exit;
        recipe = newRecipe;
        initialized = true;
    }

    void Start()
    {
        Debug.Log($"[Guest] Spawned: {gameObject.name}");

        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();

        if (rb != null)
        {
            rb.constraints = RigidbodyConstraints.FreezeAll;
        }

        if (!initialized)
        {
            Debug.LogError("[Guest] Init() was never called before Start()!");
            return;
        }

        if (BarQueueManager.Instance == null)
        {
            Debug.LogError("[Guest] No QueueManager in scene!");
            return;
        }

        BarQueueManager.Instance.JoinQueue(this);
        Debug.Log("[Guest] Joined queue");
    }

    void Update()
    {
        if (target == null || !isMoving) return;

        Vector3 newPos = Vector3.MoveTowards(
            transform.position,
            target.position,
            Time.deltaTime * speed
        );
        transform.position = newPos;

        Vector3 dir = (target.position - transform.position);
        dir.y = 0;
        if (dir.sqrMagnitude > 0.001f)
            transform.rotation = Quaternion.LookRotation(dir);

        if (exitPosition != null &&
            Vector3.Distance(transform.position, exitPosition.position) < 0.5f)
        {
            GuestSpawner.Instance.TrySpawnGuest();
            Destroy(gameObject);
            return;
        }
        if (Vector3.Distance(transform.position, target.position) < 0.1f)
        {
            isMoving = false;
            animator?.SetBool("AtBar", true);
            if (queueIndex == 0)
            {
                recipe.newGuest();
            }
        }
    }

    public void AssignSlot(Transform slot, int index)
    {
        target = slot;
        queueIndex = index;
        isMoving = true;

        Debug.Log($"{gameObject.name} assigned to slot {queueIndex}");
        animator?.SetBool("AtBar", false);
    }

    public void BeingServed()
    {
        if (served) return;

        served = true;
        recipe.guestDone(); 
        BarQueueManager.Instance.LeaveQueue(this);

        target = exitPosition;
        Debug.Log($"{exitPosition.position} is pos");
        isMoving = true;

        animator?.SetBool("AtBar", false);
    }
}