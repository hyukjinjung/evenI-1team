using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 
클래스 설명: 
게임 내 타일 정보를 관리
 
*/
public class Tile : MonoBehaviour
{
    private List<Monster> monstersOnTile = new List<Monster>();


    // 플레이어가 타일 왼쪽에 있는지 확인
    public bool TileOnLeft(Transform position)
    {
        return position.position.x > transform.position.x;
    }


    // 리스트에 몬스터가 있는지 확인
    // 첫 번째 몬스터를 반환
    // NinjaFrog의 특수 능력 발동 시 사용됨
    public Monster GetFirstMonster()
    {
        return monstersOnTile.Count > 0 ? monstersOnTile[0] : null;
    }


    // 타일에 몬스터 추가
    public void AddMonster(Monster monster)
    {
        if (!monstersOnTile.Contains(monster))
            monstersOnTile.Add(monster);
    }


    // 타일에서 몬스터 제거
    public void RemoveMonster(Monster monster)
    {
        if (monstersOnTile.Contains(monster))
            monstersOnTile.Remove(monster);
    }

}
