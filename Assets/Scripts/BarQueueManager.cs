using System.Collections.Generic;
using UnityEngine;

public class BarQueueManager : MonoBehaviour
{
    public static BarQueueManager Instance;

    public Transform[] queueSlots;
    private List<Guest> guests = new List<Guest>();

    void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        }

    public bool HasFreeSlot()
    {
        if (queueSlots == null)
        {
            Debug.LogError("[Queue] queueSlots array is null! Assign queue slots in the BarQueueManager Inspector.");
            return false;
        }

        return guests.Count < queueSlots.Length;
    }

    public void JoinQueue(Guest guest)
    {
        Debug.Log($"[Queue] JoinQueue: {guest.name}");

        if (guests.Contains(guest))
            return;

        guests.Add(guest);

        Debug.Log($"[Queue] Guests count: {guests.Count}");

        UpdateQueue();
    }
    public void LeaveQueue(Guest guest)
    {
        guests.Remove(guest);
        UpdateQueue();
    }

    public void UpdateQueue()
    {
        Debug.Log("[Queue] UpdateQueue called");

        if (queueSlots == null || queueSlots.Length == 0)
        {
            Debug.LogError("[Queue] queueSlots is not assigned or empty. Cannot update guest queue.");
            return;
        }

        for (int i = 0; i < guests.Count; i++)
        {
            if (i < queueSlots.Length)
            {
                Debug.Log($"[Queue] Assigning slot {i} to {guests[i].name}");
                guests[i].AssignSlot(queueSlots[i], i);
            }
        }
    }

    public Guest GetFirstInQueue()
    {
        if (guests.Count > 0)
            return guests[0];
        return null;
    }
}