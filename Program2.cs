using UnityEngine;
using UnityEngine.UI;

public class ObjectChanger : MonoBehaviour
{
    public GameObject[] objects;   // Assign Cube, Sphere, and Plane
    public Button colorButton, materialButton, textureButton;
    public Material[] materials;   // Assign materials in the Inspector
    public Texture[] textures;     // Assign textures in the Inspector

    private int materialIndex = 0;
    private int textureIndex = 0;

    void Start()
    {
        colorButton.onClick.AddListener(ChangeAllColors);
        materialButton.onClick.AddListener(ChangeAllMaterials);
        textureButton.onClick.AddListener(ChangeAllTextures);
    }

    void ChangeAllColors()
    {
        foreach (GameObject obj in objects)
        {
            if (obj != null)
                obj.GetComponent<Renderer>().material.color = new Color(Random.value, Random.value, Random.value);
        }
    }

    void ChangeAllMaterials()
    {
        if (materials.Length > 0)
        {
            materialIndex = (materialIndex + 1) % materials.Length;
            foreach (GameObject obj in objects)
            {
                if (obj != null)
                    obj.GetComponent<Renderer>().material = materials[materialIndex];
            }
        }
    }

    void ChangeAllTextures()
    {
        if (textures.Length > 0)
        {
            textureIndex = (textureIndex + 1) % textures.Length;
            foreach (GameObject obj in objects)
            {
                if (obj != null)
                    obj.GetComponent<Renderer>().material.mainTexture = textures[textureIndex];
            }
        }
    }
}
