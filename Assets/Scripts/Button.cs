using UnityEngine;

public class ServeButton : MonoBehaviour
{

    public bool servedGuest; 

    public Guest guest; 
    public Shaker shaker;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        servedGuest = false; 
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && shaker.isDoneShaking)
        {
            servedGuest = true; 
            guest.BeingServed(); 
        }
    }
}
