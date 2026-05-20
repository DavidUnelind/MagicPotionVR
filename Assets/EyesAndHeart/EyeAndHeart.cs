using UnityEngine;

public class EyeAndHeart : MonoBehaviour
{
    public Recipe recipe;
    public string ingredient;
    public Teleportation teleportation;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Cauldron"))
        {
            Debug.Log("EYEEYEYEYE");
            recipe.addIngredient(ingredient);
            teleportation.MovePosition();
        }
    }
}