using UnityEngine;

// Used a video called "PERLIN NOISE in Unity - Procedural Generation Tutorial" by Brackeys on YouTube to create this script
// with modifications to make it unique.
public class PerlinMaterial : MonoBehaviour
{

    public int width = 256;
    public int height = 256;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Update()
    {
        Renderer renderer = GetComponent<Renderer>();
        renderer.material.mainTexture = GenerateTexture();
    }

    Texture2D GenerateTexture()
    {
        Texture2D texture = new Texture2D(width, height);

        // Generate a perlin noise map for the texture
        // this nested loop is essential going and filling in each pixel of the texture
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                Color color = CalculateColor(x, y);
                texture.SetPixel(x, y, color);
            }
        }
        texture.Apply();                // this is important to apply the changes of the texture
        return texture;
    }

    Color CalculateColor (int x, int y)
    {
        // having these being decimal values creates better variations and perlin coordinations
        float xCoord = (float)x / width * 3.0f;
        float yCoord = (float)y / height * 7.0f;

        // this would create either white, black, or a shade of gray when generating
        float sample = Mathf.PerlinNoise(xCoord, yCoord);
        return new Color(sample, sample, sample);
    }
}

