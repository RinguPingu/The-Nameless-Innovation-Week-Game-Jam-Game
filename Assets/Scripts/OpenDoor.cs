using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class OpenDoor : MonoBehaviour
{
    [SerializeField]
    Tilemap tilemap;

    [SerializeField]
    Sprite doorTile;

    // Start is called before the first frame update
    void Start()
    {
        for (int x = tilemap.cellBounds.min.x; x < tilemap.cellBounds.max.x; x++)
        {
            for (int y = tilemap.cellBounds.min.y; y < tilemap.cellBounds.max.y; y++)
            {
                var tile = tilemap.GetTile(new Vector3Int(x, y, 0));
                Debug.Log(tilemap.GetSprite(new Vector3Int(x, y, 0)) == doorTile);

                
            }

        }
    }

    public void OpenDoorAt(Vector2 pos)
    {
        var cpos = tilemap.WorldToCell(pos);

        if (tilemap.GetSprite(cpos) == doorTile)
        {
            tilemap.SetTile(cpos, null);

            var dirs = new Vector2[] { Vector2.left, Vector2.right, Vector2.up, Vector2.down };

            for (int i = 0; i < 4; i++)
            {
                OpenDoorAt(pos + dirs[i]);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            OpenDoorAt(Camera.main.ScreenToWorldPoint(Input.mousePosition));
        }
    }
}
