using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TimeSlider : MonoBehaviour
{
    public GameLoop gameLoop;  
    public TextMeshProUGUI text; 
    public Slider slider;
    public float minutes = 2f;
    void Start()
    {
        text.SetText("PLAY TIME: " + minutes + " min");
    }
    public void Update()
    {
        gameLoop.SetDefaultPlayTime(slider.value * 60f);
        text.SetText("PLAY TIME: " + Mathf.RoundToInt(slider.value) + " min");
    }
}
