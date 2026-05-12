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
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    private void Start()
    {

        if (BarQueueManager.Instance == null 
            || guestPrefabs == null 
            || guestPrefabs.Length == 0
            || spawnPoint == null
            || exitPoint == null
            || guestPrefabs == null
            || guestPrefabs.Length == 0)
        {
            return;
        }

        /*if (guestPrefabs == null || guestPrefabs.Length == 0)
        {
            return;
        }

        if (spawnPoint == null)
        {
            return;
        }

        if (exitPoint == null)
        {
            return;
        } */

        FillQueueAtStart();
    }

    void FillQueueAtStart()
    {
        /*
        if (BarQueueManager.Instance == null)
            return;*/

        if (BarQueueManager.Instance.queueSlots == null || BarQueueManager.Instance.queueSlots.Length == 0)
        {
            return;
        }

        int slotCount = BarQueueManager.Instance.queueSlots.Length;

        for (int i = 0; i < slotCount; i++)
        {
            SpawnGuest();
        }
    }

    public void TrySpawnGuest()
    {
        if (BarQueueManager.Instance == null || !BarQueueManager.Instance.HasFreeSlot())
        {
            return;
        }

        /*if (!BarQueueManager.Instance.HasFreeSlot())
            return;*/

        SpawnGuest();
    }

    void SpawnGuest()
    {
        /*if (guestPrefabs == null || guestPrefabs.Length == 0 || spawnPoint == null)
        {
            return;
        }*/

        int randomIndex = Random.Range(0, guestPrefabs.Length);

        GameObject guestObject = Instantiate(
            guestPrefabs[randomIndex],
            spawnPoint.position,
            Quaternion.identity
        );

        Guest guest = guestObject.GetComponent<Guest>() ?? guestObject.GetComponentInChildren<Guest>();

        if (guest == null)
        {
            Destroy(guestObject);
            return;
        }

        guest.Init(exitPoint);
    }
}

/* Debug options
 Debug.LogWarning("[Spawner] Duplicate GuestSpawner found, destroying extra instance.");
Debug.Log("[Spawner] Start called");
Debug.LogError("[Spawner] No BarQueueManager instance found in scene!");
Debug.LogError("[Spawner] guestPrefabs is empty! Assign guest prefabs in the Inspector.");
Debug.LogError("[Spawner] spawnPoint is not assigned! Assign it in the Inspector.");
Debug.LogError("[Spawner] exitPoint is not assigned! Assign it in the Inspector.");
Debug.LogError("[Spawner] BarQueueManager.queueSlots is not assigned or empty!");
Debug.Log($"[Spawner] Filling queue at start with {slotCount} slots.");
Debug.LogError("[Spawner] No BarQueueManager instance available when trying to spawn.");
Debug.LogError("[Spawner] guestPrefabs is empty, cannot spawn guest.");
Debug.LogError("[Spawner] spawnPoint is not assigned, cannot spawn guest.");
Debug.Log("[Spawner] Spawning guest...");
Debug.LogError("[Spawner] Guest prefab missing Guest script on root or child!");
Debug.Log($"[Spawner] Spawned guest '{guest.name}' and initialized exit '{exitPoint.name}'.");*/
