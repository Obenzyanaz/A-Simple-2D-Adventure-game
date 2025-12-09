using UnityEngine;

public class SetLitMaterial : MonoBehaviour
{
    public Material litMaterial; // assign your Sprite-Lit-Default material here

    void Start()
    {
        // Find all SpriteRenderers in the scene
        SpriteRenderer[] sprites = FindObjectsOfType<SpriteRenderer>();
        foreach (SpriteRenderer sr in sprites)
        {
            sr.material = litMaterial;
        }
    }
}