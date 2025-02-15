using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestTileManager : MonoBehaviour
{
    [SerializeField] private GameObject testTilePrefab;

    private float xOffset = 1;
    private float yOffset = 1;
    
    private int direction = 1;
    
    private List<Tile>  tiles = new List<Tile>();
    
    
    // Start is called before the first frame update
    void Start()
    {
        int x = 0;
        for (int i = 0; i < 10; i++)
        {
            GameObject testTile = Instantiate(testTilePrefab);
            testTile.transform.SetParent(transform);

            testTile.transform.localPosition = new Vector3(x, yOffset * i, 0);
            testTile.gameObject.SetActive(true);
            
            tiles.Add(testTile.GetComponent<Tile>());
            
            
            if (x <= -2)
            {
                direction = 1;
            }
            else if (x >= 2)
            {
                direction = -1;
            }

            x += direction;
        }
        
        
    }

    public Tile GetTile(int currentFloor)
    {
        if (currentFloor >= tiles.Count || currentFloor < 0) // 범위 확인
            return null;

        //if (tiles.Count - 1 > currentFloor)
        //{
        //    return null;
        //}

        return tiles[currentFloor +1];
    }
}
