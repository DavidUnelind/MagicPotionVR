using UnityEngine;
using TMPro;
using System.Diagnostics;
using System.Collections.Generic;

public class Recipe : MonoBehaviour
{
    public GameObject canvas;
    public GameObject love; 
    public GameObject luck; 
    public GameObject strength; 
    public Transform playerCamera;
    private List<string> ingredients = new List<string>();
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        guestDone(); 
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 direction = playerCamera.position - transform.position;
        direction.y = 0;
        transform.rotation = Quaternion.LookRotation(-direction);
        //transform.LookAt(transform.position + playerCamera.rotation * Vector3.forward, playerCamera.rotation * Vector3.up);
    }

    public void newGuest()
    {
        canvas.SetActive(true);
        int rand = Random.Range(0, 3); 

        switch (rand)
        {
            case 0:
                love.SetActive(true);
                ingredients.Add("Green");
                ingredients.Add("Pink");
                break; 

            case 1:
                luck.SetActive(true);
                ingredients.Add("Green");
                ingredients.Add("Orange");
                break;

            case 2:
                strength.SetActive(true);
                ingredients.Add("Red");
                ingredients.Add("Purple");
                break;

            default:
                break;
        }
    }

    public void guestDone()
    {
        canvas.SetActive(false);
        love.SetActive(false);
        luck.SetActive(false);
        strength.SetActive(false);
    }

    public void addIngredient(string ingredient)
    {
        if (ingredients.Contains(ingredient))
        {
            ingredients.Remove(ingredient);
        }

        if (ingredients.Count == 0)
        {
        }
    }
}
