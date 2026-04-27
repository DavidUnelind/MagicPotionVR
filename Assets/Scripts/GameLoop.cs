using UnityEngine;
using TMPro;

public class GameLoop : MonoBehaviour
{

    private int points = 0; 
    public ServeButton button; 
    public TextMeshProUGUI textPro; 

    private float remainingTime = 120f; 

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        textPro.text = "Time remaining: " + Mathf.RoundToInt(remainingTime) + "\nCustomers served: " + points;   
    }

    // Update is called once per frame
    void Update()
    {
        if (remainingTime > 0)
        {
            remainingTime -= Time.deltaTime;  
            textPro.text = "Time remaining: " + Mathf.RoundToInt(remainingTime) + "\nCustomers served: " + points;  
        }
        else
        {
           textPro.text = "GAME OVER!!!!";
        }

        if (button.servedGuest)
        {
            points += 1; 
            button.servedGuest = false; 
        }
    }
}
