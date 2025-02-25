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
