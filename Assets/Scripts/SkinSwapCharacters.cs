using UnityEngine;
using System.Collections.Generic;

public class CharacterSkinRandomizer : MonoBehaviour
{
    public List<GameObject> skinPrefabs;
    public Transform visualsParent;

    private Animator rootAnimator;

    void Awake()
    {
        rootAnimator = GetComponent<Animator>();

        RandomizeSkin();
    }

    void RandomizeSkin()
    {
        int randomIndex = Random.Range(0, skinPrefabs.Count);

        GameObject skin = Instantiate(
            skinPrefabs[randomIndex],
            visualsParent
        );

        skin.transform.localPosition = Vector3.zero;
        skin.transform.localRotation = Quaternion.identity;
        skin.transform.localScale = Vector3.one;

        Animator skinAnimator = skin.GetComponent<Animator>();

        if (skinAnimator == null)
            return;

        // humanoid case
        if (skinAnimator != null &&skinAnimator.avatar != null)
        {
            rootAnimator.avatar = skinAnimator.avatar;
            rootAnimator.Rebind();
            rootAnimator.Update(0f);
            skinAnimator.enabled = false;
        }

    }
}