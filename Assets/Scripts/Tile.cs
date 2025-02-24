using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

/*

클래스 설명:
 
*/
public class Tile : MonoBehaviour
{
    private Monster monsterOnTile;
    private GameObject obstacle;


    // 몬스터 추가
    public void SetMonster(Monster monster)
    {
        monsterOnTile = monster;
    }
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




    // 몬스터 반환
    public Monster GetFirstMonster()
    {
        return monsterOnTile;
    }

    // 몬스터 제거
    public void RemoveMonster()
    {
        monsterOnTile = null;
    }

    // 해당 타일에 몬스터가 있는지 확인
    public bool HasMonster()
    {
        return monsterOnTile != null;
    }

}
