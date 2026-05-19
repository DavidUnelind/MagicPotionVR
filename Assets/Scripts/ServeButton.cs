using UnityEngine;

public class ServeButton : MonoBehaviour
{
    [SerializeField] private AudioClip buttonSound;

    public bool servedGuest; 

    public BarQueueManager barQueueManager;
    public Shaker shaker;

    void Start()
    {
        servedGuest = false; 
    }

    public void PressButton()
    {
        SoundManager.instance.PlaySound(buttonSound);
        if (shaker.isDoneShaking)
        {
            servedGuest = true;
            Guest guest = barQueueManager.GetFirstInQueue();
            if (guest != null && guest.isAtBar)
            {
                guest.BeingServed();
            }
            shaker.ResetShaker();
        }
    }
}
