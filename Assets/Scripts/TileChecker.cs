using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileChecker : MonoBehaviour
{
    public LayerMask targetLayer; // 타일 레이어

    public float checkDistanceX = 0.75f; // 타일 x축
    public float checkDistanceY = 0.45f; // 타일 y축



    // 왼쪽 대각선 위에 타일이 있는지 확인하는 함수
    public bool IsTileInFrontLeft()
    {
        // 왼쪽 대각선 위 타일 위치 계산
        Vector2 leftDiagonalTile = new Vector2(transform.position.x - checkDistanceX, transform.position.y + checkDistanceY);

        float radius = 0.5f; // 감지 범위 조정
        Collider2D[] hits = Physics2D.OverlapCircleAll(leftDiagonalTile, radius, targetLayer);

        Debug.Log("왼쪽 대각선 체크 좌표: " + leftDiagonalTile + ", 감지된 오브젝트 수: " + hits.Length);

        // 타일이 감지되었는지 여부 반환
        return hits.Length > 0;
    }


    // 오른쪽 대각선 위에 타일이 있는지 확인하는 함수
    public bool IsTileInFrontRight()
    {
        // 오른쪽 대각선 위 타일 위치 계산
        Vector2 rightDiagonalTile = new Vector2(transform.position.x + checkDistanceX, transform.position.y + checkDistanceY);

        float radius = 0.5f; // 감지 범위 조정
        Collider2D[] hits = Physics2D.OverlapCircleAll(rightDiagonalTile, radius, targetLayer);

        Debug.Log("오른쪽 대각선 체크 좌표: " + rightDiagonalTile + ", 감지된 오브젝트 수: " + hits.Length);

        // 타일이 감지되었는지 여부 반환
        return hits.Length > 0;
    }


    // 플레이어 바로 아래에 타일이 있는지 확인하는 함수
    public bool IsTileBelow()
    {
        Vector2 belowTilePosition = new Vector2(transform.position.x, transform.position.y - checkDistanceY);

        float radius = 0.5f; // 감지 범위 조정
        Collider2D[] hits = Physics2D.OverlapCircleAll(belowTilePosition, radius, targetLayer);

        Debug.Log("아래 타일 체크 좌표: " + belowTilePosition + ", 감지된 오브젝트 수: " + hits.Length);

        // 타일이 감지되었는지 여부 반환
        return hits.Length > 0;
    }
}
