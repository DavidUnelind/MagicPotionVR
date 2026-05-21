using UnityEngine;

public class EyeAndHeart : MonoBehaviour
{
    public Recipe recipe;
    public string ingredient;
    public Teleportation teleportation;
    public AudioSource eyeAudio;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Cauldron"))
        {
            recipe.addIngredient(ingredient);
            teleportation.MovePosition();

            if (eyeAudio != null)
            {
                eyeAudio.Play();
            }
        }
    }
}