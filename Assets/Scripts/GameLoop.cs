using UnityEngine;
using TMPro;

public class GameLoop : MonoBehaviour
{

    private int points; 
    public ServeButton button; 
    public TextMeshProUGUI textPro; 
    public TextMeshProUGUI textEndPro;
    public GameObject pointsCanvas;
    public GuestSpawner guestSpawner;
    public BarQueueManager barQueueManager;
    public GameObject recipeCanvas;
    public GameObject progressBar;

    public MenuManager menu;
    private bool started = false;

    private float remainingTime; 

    public GameObject cauldron;
    public GameObject stick;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        pointsCanvas.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (!started) return;
        if (remainingTime > 0)
        {
            remainingTime -= Time.deltaTime;  
            textPro.text = "Time remaining: " + Mathf.RoundToInt(remainingTime) + "\nCustomers served: " + points;  
        }
        else
        {
            StopGame();
        }

        if (button.servedGuest)
        {
            points += 1; 
            button.servedGuest = false; 
        }
    }

    public void StartGame()
    {
        started = true;
        guestSpawner = GuestSpawner.Instance;
        guestSpawner.StartGame();
        pointsCanvas.SetActive(true);
        menu.ExitMenu();
        barQueueManager = BarQueueManager.Instance;
        barQueueManager?.RestartGame();
        remainingTime = 120f;
        points = 0;
        cauldron.SetActive(true);
        stick.SetActive(true);
    }

    private void StopGame() {
        cauldron.SetActive(false);
        stick.SetActive(false);
        started = false;
        pointsCanvas.SetActive(false);
        menu.GameOverMenu();
        barQueueManager = BarQueueManager.Instance;
        barQueueManager?.StopGame();
        textEndPro.text = "Game over! \n Customers served: " + points;
        recipeCanvas.SetActive(false);
        progressBar.SetActive(false);
    }
}
