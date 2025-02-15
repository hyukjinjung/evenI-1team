using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public bool TileOnLeft(Transform position)
    {
        return position.position.x > transform.position.x;
    }
}
