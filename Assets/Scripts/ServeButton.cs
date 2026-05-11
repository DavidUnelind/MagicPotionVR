using UnityEngine;

public class ServeButton : MonoBehaviour
{

    public bool servedGuest; 

    public BarQueueManager barQueueManager;
    public Shaker shaker;

    void Start()
    {
        servedGuest = false; 
    }

    public void PressButton()
    {
        if (shaker.isDoneShaking)
        {
            servedGuest = true;
            Guest guest = barQueueManager.GetFirstInQueue();
            if (guest != null)
            {
                guest.BeingServed();
            }
            shaker.ResetShaker();
        }
    }
}
