using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    private GameObject obstacle;

    public void SetObstacle(GameObject obstacle)
    {
        this.obstacle = obstacle;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && obstacle != null)
        {
            Destroy(obstacle);
            obstacle = null;
        }
    }

    public bool TileOnLeft(Transform position)
    {
        return position.position.x > transform.position.x;
    }
}
