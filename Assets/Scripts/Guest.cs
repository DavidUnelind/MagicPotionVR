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
    private int queueIndex = 3;
    private bool hasBeenFirstInQueue = false;

    private bool initialized = false;

    public Recipe recipe; 

    public bool isAtBar => animator != null && animator.GetBool("AtBar");
    private UnityEngine.AI.NavMeshAgent agent;

    public void Init(Transform exit, Recipe newRecipe)
    {
        exitPosition = exit;
        recipe = newRecipe;
        initialized = true;
    }

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();

        agent.speed = speed;
        agent.angularSpeed = 720f;
        agent.acceleration = 20f;

        if (rb != null)
        {
            rb.constraints = RigidbodyConstraints.FreezeAll;
        }

        BarQueueManager.Instance.JoinQueue(this);
    }

    void Update()
    {
        if (target == null || !isMoving) return;

        agent.SetDestination(target.position);

        if (exitPosition != null && Vector3.Distance(transform.position, exitPosition.position) < 6.5f)
        {
            animator?.SetBool("Crouch", true);
        }

        if (exitPosition != null && Vector3.Distance(transform.position, exitPosition.position) < 0.5f)
        {
            GuestSpawner.Instance.TrySpawnGuest();
            Destroy(gameObject);
            return;
        }
        if (!agent.pathPending && agent.remainingDistance < 0.2f)
        {
            isMoving = false;
            animator?.SetBool("AtBar", true);

            if (queueIndex == 0 && recipe != null && !hasBeenFirstInQueue)
            {
                recipe.newGuest();
                hasBeenFirstInQueue = true;
            }
        }
    }

    public void AssignSlot(Transform slot, int index)
    {
        target = slot;
        queueIndex = index;
        isMoving = true;

        animator?.SetBool("AtBar", false);
    }

    public void BeingServed()
    {
        if (served) return;

        served = true;
        recipe.guestDone(); 
        BarQueueManager.Instance.LeaveQueue(this);

        target = exitPosition;
        isMoving = true;

        animator?.SetBool("AtBar", false);
    }


    public void UpdateRecipe()
    {
        recipe.newGuest();
    }

    public void Kill()
    {
        if (this == null) return;
        Destroy(gameObject);
    }
}