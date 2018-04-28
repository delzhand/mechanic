using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Room : MonoBehaviour {

    public Texture2D geometry;
    public Tile blockTile;

    public void Regenerate()
    {
        Tilemap tileMap = GetComponentInChildren<Tilemap>();

        // Delete all existing geometry
        tileMap.ClearAllTiles();

        // Generate new geometry
        int width = geometry.width;
        int height = geometry.height;
        Color32[] pixels = geometry.GetPixels32();
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                Color32 pixel = pixels[(y * width) + x];
                if (pixel.r == 0)
                {
                    tileMap.SetTile(new Vector3Int(x, y, 0), blockTile);
                }
            }
                
        }

        transform.GetComponent<BoxCollider2D>().size = new Vector2(width, height);
        transform.GetComponent<BoxCollider2D>().offset = new Vector2(width / 2, height / 2);
    }
}
