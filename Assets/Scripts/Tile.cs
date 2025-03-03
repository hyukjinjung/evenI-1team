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

    // 해당 타일에 몬스터가 있는지 확인
    public bool HasMonster()
    {
        return monsterOnTile != null;
    }

    // 타일 삭제 시, 위에 플레이어가 있으면 게임 오버 처리
    public void DestroyTile()
    {
        if (IsPlayerOnTile())
        {
            Debug.Log("타일이 사라짐. 캐릭터도 게임 오버");
            GameManager.Instance.GameOver();
        }

        Destroy(gameObject);
    }

    // 타일 위에 플레이어가 있는지 확인
    private bool IsPlayerOnTile()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");

        if (player != null)
        {
            float distanceX = Mathf.Abs(player.transform.position.x - transform.position.x);
            float distanceY = Mathf.Abs(player.transform.position.y - transform.position.y);

            return (distanceX < 0.5f && distanceY < 0.5f);
        }

        return false;
    }
}
