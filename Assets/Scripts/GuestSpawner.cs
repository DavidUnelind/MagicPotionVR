using System.Collections;
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
    public Recipe recipe; 

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    public void StartGame()
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

        StartCoroutine(FillQueueAtStart());
    }

    IEnumerator FillQueueAtStart()
    {
        int slotCount = BarQueueManager.Instance.queueSlots.Length;

        for (int i = 0; i < slotCount; i++)
        {
            SpawnGuest();

            // delay between spawns
            yield return new WaitForSeconds(2f);
        }
    }

    public void TrySpawnGuest()
    {
        if (BarQueueManager.Instance == null || !BarQueueManager.Instance.HasFreeSlot())
        {
            return;
        }

        SpawnGuest();
    }

    void SpawnGuest()
    {

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

        guest.Init(exitPoint, recipe);
    }
}