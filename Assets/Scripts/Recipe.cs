using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class Recipe : MonoBehaviour
{
    public GameObject canvas;
    public GameObject love; 
    public GameObject luck; 
    public GameObject strength; 
    public GameObject health; 
    public Transform playerCamera;
    private int rand;
    public bool recipeDone = false;
    private Dictionary<string, bool> ingredients = new Dictionary<string, bool>();
    void Start()
    {
        guestDone(); 
    }

    void Update()
    {
        Vector3 direction = playerCamera.position - transform.position;
        direction.y = 0;
        transform.rotation = Quaternion.LookRotation(-direction);
    }

    public void newGuest()
    {
        guestDone();
        canvas.SetActive(true);
        rand = Random.Range(0, 4); 

        switch (rand)
        {
            case 0:
                love.SetActive(true);
                ingredients.Add("Purple", false);
                ingredients.Add("Red", false);
                ingredients.Add("Heart", false);
                break; 

            case 1:
                luck.SetActive(true);
                ingredients.Add("Orange", false);
                ingredients.Add("Green", false);
                ingredients.Add("Eye", false);
                break;

            case 2:
                strength.SetActive(true);
                ingredients.Add("Black", false);
                ingredients.Add("Orange", false);
                ingredients.Add("Eye", false);
                break;

            case 3:
                health.SetActive(true);
                ingredients.Add("Yellow", false);
                ingredients.Add("Blue", false);
                ingredients.Add("Heart", false);
                break;

            default:
                break;
        }
    }

    public void guestDone()
    {
        for (int i = 0; i < 3; i++)
        {
            changeVisibleLines(i, false);
        }
        
        canvas.SetActive(false);
        love.SetActive(false);
        luck.SetActive(false);
        strength.SetActive(false);
        health.SetActive(false);
        ingredients.Clear();
        recipeDone = false;
    }

    public void addIngredient(string ingredient)
    {
        Debug.Log("TJO: " + ingredients);
        if (ingredients.ContainsKey(ingredient))
        {
            ingredients[ingredient] = true;
            List<string> keys = new List<string>(ingredients.Keys);
            int index = keys.IndexOf(ingredient);
            changeVisibleLines(index, true);

            if (ingredients.ContainsValue(false) == false)
            {
                recipeDone = true;
            }
        }
    }

    private void changeVisibleLines(int index, bool value)
    {
        GameObject obj = null;
        switch (rand)
        {
            case 0:
                obj = love;
                break; 

            case 1:
                obj = luck;
                break;

            case 2:
                obj = strength;
                break;

            case 3:
                obj = health;
                break;

            default:
                break;
        }
        GameObject redLine = obj.transform.GetChild(index + 4).gameObject;
        UnityEngine.Debug.Log("Changing line " + index + " to " + value);
        redLine.SetActive(value);
    }
}
