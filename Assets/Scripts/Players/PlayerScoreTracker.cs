using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScoreTracker : MonoBehaviour
{
    private float startY;
    private float endY;
    private bool isGameOver = false;

    public int score = 0;

    // Start is called before the first frame update
    void Start()
    {
        startY = transform.position.y;
        Debug.Log($"게임 시작 Y  위치: {startY}");
    }

    // Update is called once per frame
   
    private void Update()
    {
        if (isGameOver) return; // 게임 오버 후에는 계속 계산하지 않음

        // 플레이어의 현재 위치 추적
        endY = transform.position.y;
    }

    public void CalculateScore()
    {
        if (isGameOver) return; // 중복 계산 방지

        isGameOver = true; // 게임 오버 처리
        float distance = endY - startY; // Y축으로 이동한 거리
        score = Mathf.RoundToInt(distance); // 점수 계산 (1 유닛 = 1점)

        Debug.Log($"게임 오버! 총 점수: {score} (이동 거리: {distance} 유닛)");
    }
}





