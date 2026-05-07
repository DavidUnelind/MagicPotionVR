using System.Collections.Generic;
using UnityEngine;

public class BarQueueManager : MonoBehaviour
{
    public static BarQueueManager Instance;

    public Transform[] queueSlots;
    private List<Guest> guests = new List<Guest>();

    void Awake()
    {
        Instance = this;
    }

    public void JoinQueue(Guest guest)
    {
        guests.Add(guest);
        UpdateQueue();
    }

    public void LeaveQueue(Guest guest)
    {
        guests.Remove(guest);
        UpdateQueue();
    }

    public void UpdateQueue()
    {
        for (int i = 0; i < guests.Count; i++)
        {
            if (i < queueSlots.Length)
            {
                guests[i].AssignSlot(queueSlots[i], i);
            }
        }
    }
}