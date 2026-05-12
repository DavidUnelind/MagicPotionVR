using UnityEngine;
using System.Collections.Generic;

public class CharacterSkinRandomizer : MonoBehaviour
{
    [Header("Skin Prefabs")]
    public List<GameObject> skinPrefabs;

    [Header("Where skins spawn")]
    public Transform visualsParent;

    private GameObject currentSkin;

    void Awake()
    {
        RandomizeSkin();
    }

    void RandomizeSkin()
    {
        if (skinPrefabs == null || skinPrefabs.Count == 0)
            return;

        int randomIndex = Random.Range(0, skinPrefabs.Count);

        currentSkin = Instantiate(
            skinPrefabs[randomIndex],
            visualsParent
        );

        currentSkin.transform.localPosition = Vector3.zero;
        currentSkin.transform.localRotation = Quaternion.identity;
        currentSkin.transform.localScale = Vector3.one;
    }
}