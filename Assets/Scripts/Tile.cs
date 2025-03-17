using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Rendering;

/*

클래스 설명:
 
*/
public class Tile : MonoBehaviour
{
    private Monster monsterOnTile;
    public Monster MonsterOnTile {get {return monsterOnTile;}}

    public GameObject GetObstacle() {return obstacle;}
    public GameObject GetItem() {return item;}
    
    private GameObject obstacle;
    private GameObject item;
    private GameObject transparent;

    [SerializeField] private int index;
    public int Index {get {return index;}}
    
    public void Init(int index)
    {
        this.index = index;
    }
    
    public void SetMonster(Monster monster)
    {
        monsterOnTile = monster;
    }

    public void SetObstacle(GameObject obstacle)
    {
        this.obstacle = obstacle;
    }

    public void SetItem(GameObject item)
    {
        this.item = item;
    }

    public void SetTransparentTile(GameObject transparentTile)
    {
        this.transparent = transparentTile;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && item != null)
        {
            Destroy(item);
            item = null;
        }
    }

    public bool TileOnLeft(Transform position)
    {
        return transform.position.x < position.position.x;
    }


    public void RemoveMonster()
    {
        if (monsterOnTile != null)
        {
            Destroy(monsterOnTile.gameObject);
            monsterOnTile = null;
            Debug.Log("몬스터 삭제됨. 타일은 유지됨");
        }
    }


    public void RemoveObstacle()
    {
        if (obstacle != null)
        {
            Destroy(obstacle);
            obstacle = null;
        }
    }


    public void RemoveItem()
    {
        if (item != null)
        {
            Destroy(item);
            item = null;
        }
    }


    public bool HasMonster()
    {
        return monsterOnTile != null;
    }


    public bool HasObstacle()
    {
        return obstacle != null;
    }

    public bool HasItem()
    {
        return item != null;
    }
}
