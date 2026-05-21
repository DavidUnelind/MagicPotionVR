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
    public AudioSource clockTickingSound;
    public MenuManager menu;
    private bool started = false;
    private float remainingTime; 
    public GameObject cauldron;
    public GameObject stick;
    public float cutEndSeconds = 0.1f;

    void Start()
    {
        pointsCanvas.SetActive(false);
        clockTickingSound.pitch = 0.8f;
    }

    void Update()
    {
        if (!started) return;
        if (remainingTime > 0)
        {
            remainingTime -= Time.deltaTime;  
            textPro.text = "Time remaining: " + Mathf.RoundToInt(remainingTime) + "\nCustomers served: " + points;  
            if (remainingTime < 25)
            {
                if (!clockTickingSound.isPlaying)
                {
                    clockTickingSound.Play();
                }
                else if (clockTickingSound.isPlaying && clockTickingSound.time >= clockTickingSound.clip.length - cutEndSeconds)
                {
                    clockTickingSound.Stop();
                }
            }
        }
        else
        {
            if (clockTickingSound.isPlaying)
            {
                clockTickingSound.Stop();
            }
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
        remainingTime = 35f;
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