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
    private GameObject item; // 아이템 추가

    // 몬스터 추가
    public void SetMonster(Monster monster)
    {
        monsterOnTile = monster;
    }

    // 장애물 추가
    public void SetObstacle(GameObject obstacle)
    {
        this.obstacle = obstacle;
    }

    // 아이템 추가
    public void SetItem(GameObject item)
    {
        this.item = item;
    }

    private void OnTriggerEnter(Collider other)
    {
        // 아이템 획득 로직 추가
        if (other.CompareTag("Player") && item != null)
        {
            // 아이템 획득 처리
            Destroy(item);
            item = null;
        }
    }

    public bool TileOnLeft(Transform position)
    {
        return transform.position.x < position.position.x;
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
