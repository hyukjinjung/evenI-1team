using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;
using UnityEngine.Rendering;

/*

클래스 설명:
 
*/
public class Tile : MonoBehaviour
{
    private Monster monsterOnTile;
    public Monster MonsterOnTile {get {return monsterOnTile;}}
    
    private GameObject obstacle;
    private GameObject item; // 아이템 추가
    private GameObject transparent; // ✅ 추가 (Transparent 발판 변수)

    [SerializeField] private int index;
    public int Index {get {return index;}}
    
    public void Init(int index)
    {
        this.index = index;
    }
    
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

    // Transparent 발판 추가
    public void SetTransparentTile(GameObject transparentTile)
    {
        this.transparent = transparentTile;
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

    // 몬스터 제거
    public void RemoveMonster()
    {
        if (monsterOnTile != null)
        {
            Destroy(monsterOnTile.gameObject);
            monsterOnTile = null;
            Debug.Log("몬스터 삭제됨. 타일은 유지됨");
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
}
