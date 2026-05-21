using UnityEngine;

public class MenuManager : MonoBehaviour
{
    public GameObject startMenu;
    public GameObject instructionMenu;
    public GameObject gameOverMenu;
    
    void Start()
    {
        StartMenu();
        
    }

    public void StartMenu()
    {
        gameOverMenu.SetActive(false);
        instructionMenu.SetActive(false);
        startMenu.SetActive(true);
    }

    public void InstructionMenu()
    {
        gameOverMenu.SetActive(false);
        instructionMenu.SetActive(true);
        startMenu.SetActive(false);
    }

    public void GameOverMenu()
    {
        gameOverMenu.SetActive(true);
        instructionMenu.SetActive(false);
        startMenu.SetActive(false);
    }

    public void ExitMenu()
    {
        gameOverMenu.SetActive(false);
        instructionMenu.SetActive(false);
        startMenu.SetActive(false);
    }
    /*
    private void EnableStartMenu()
    {
        Debug.Log("Menue enabled");
        startMenuTitle.SetActive(true);
        startButton.SetActive(true);
        howToButton.SetActive(true);
    }

    private void EnableInstructionMenu()
    {
        instructionText.SetActive(true);
        instructionTitle.SetActive(true);
        returnButton.SetActive(true);
    }

    private void DisableInstruction()
    {
        Debug.Log("Instructions disabled");
        instructionText.SetActive(false);
        instructionTitle.SetActive(false);
        returnButton.SetActive(false);
    }

    private void DisableStartMenu()
    {
        startMenuTitle.SetActive(false);
        startButton.SetActive(false);
        howToButton.SetActive(false);
    }
*/
}