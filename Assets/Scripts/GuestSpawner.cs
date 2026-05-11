using UnityEngine;

public class GuestSpawner : MonoBehaviour
{
    public static GuestSpawner Instance;

    [Header("Guest Prefabs")]
    public GameObject[] guestPrefabs;

    [Header("Spawn Point")]
    public Transform spawnPoint;

    [Header("Exit Point")]
    public Transform exitPoint;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Debug.LogWarning("[Spawner] Duplicate GuestSpawner found, destroying extra instance.");
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    private void Start()
    {
        Debug.Log("[Spawner] Start called");

        if (BarQueueManager.Instance == null)
        {
            Debug.LogError("[Spawner] No BarQueueManager instance found in scene!");
            return;
        }

        if (guestPrefabs == null || guestPrefabs.Length == 0)
        {
            Debug.LogError("[Spawner] guestPrefabs is empty! Assign guest prefabs in the Inspector.");
            return;
        }

        if (spawnPoint == null)
        {
            Debug.LogError("[Spawner] spawnPoint is not assigned! Assign it in the Inspector.");
            return;
        }

        if (exitPoint == null)
        {
            Debug.LogError("[Spawner] exitPoint is not assigned! Assign it in the Inspector.");
            return;
        }

        FillQueueAtStart();
    }

    void FillQueueAtStart()
    {
        if (BarQueueManager.Instance == null)
            return;

        if (BarQueueManager.Instance.queueSlots == null || BarQueueManager.Instance.queueSlots.Length == 0)
        {
            Debug.LogError("[Spawner] BarQueueManager.queueSlots is not assigned or empty!");
            return;
        }

        int slotCount = BarQueueManager.Instance.queueSlots.Length;
        Debug.Log($"[Spawner] Filling queue at start with {slotCount} slots.");

        for (int i = 0; i < slotCount; i++)
        {
            SpawnGuest();
        }
    }

    public void TrySpawnGuest()
    {
        if (BarQueueManager.Instance == null)
        {
            Debug.LogError("[Spawner] No BarQueueManager instance available when trying to spawn.");
            return;
        }

        if (!BarQueueManager.Instance.HasFreeSlot())
            return;

        SpawnGuest();
    }

    void SpawnGuest()
    {
        if (guestPrefabs == null || guestPrefabs.Length == 0)
        {
            Debug.LogError("[Spawner] guestPrefabs is empty, cannot spawn guest.");
            return;
        }

        if (spawnPoint == null)
        {
            Debug.LogError("[Spawner] spawnPoint is not assigned, cannot spawn guest.");
            return;
        }

        Debug.Log("[Spawner] Spawning guest...");

        int randomIndex = Random.Range(0, guestPrefabs.Length);

        GameObject guestObject = Instantiate(
            guestPrefabs[randomIndex],
            spawnPoint.position,
            Quaternion.identity
        );

        Guest guest = guestObject.GetComponent<Guest>() ?? guestObject.GetComponentInChildren<Guest>();

        if (guest == null)
        {
            Debug.LogError("[Spawner] Guest prefab missing Guest script on root or child!");
            Destroy(guestObject);
            return;
        }

        guest.Init(exitPoint);
        Debug.Log($"[Spawner] Spawned guest '{guest.name}' and initialized exit '{exitPoint.name}'.");
    }
}