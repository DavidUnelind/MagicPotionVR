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
    public AudioSource gameOverSound;
    public AudioSource pointsSound;
    public MenuManager menu;
    private bool started = false;
    private float remainingTime; 
    private float defaultPlayTime;
    public GameObject cauldron;
    public GameObject stick;
    public float cutEndSeconds = 0.1f;

    void Start()
    {
        pointsCanvas.SetActive(false);
        clockTickingSound.pitch = 0.8f;
        defaultPlayTime = 120f;
    }

    void Update()
    {
        if (!started) return;
        if (remainingTime > 0)
        {
            remainingTime -= Time.deltaTime;  

            string timeString = string.Format("{0:00}:{1:00}", Mathf.Floor(remainingTime / 60), Mathf.Floor(remainingTime % 60));

            textPro.text = "Time remaining: " + timeString + "\nCustomers served: " + points;  
            if (remainingTime < 20f)
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
            gameOverSound.time = 1f;
            gameOverSound.Play();
            StopGame();
        }

        if (button.servedGuest)
        {
            points += 1; 
            button.servedGuest = false; 
            pointsSound.Play();
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
        remainingTime = defaultPlayTime;
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

    public void SetDefaultPlayTime(float time)
    {
        defaultPlayTime = time;
    }
}