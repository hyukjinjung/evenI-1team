using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterMode : IGameMode
{
    private TestTileManager tileManager;

    // 몬스터 타일 확률(20%), 일반 타일 확률(80%)
    private float monsterTileProbability = 0.2f;
    private float normalTileProbability = 0.8f;

    public MonsterMode(TestTileManager tileManager)
    {
        this.tileManager = tileManager;
    }

    public void InitializeMode()
    {
        Debug.Log("<color=green>[MonsterMode]</color> InitializeMode 호출됨");

        // TestTileManager에 “몬스터 모드”를 켜 주고,
        // 몬스터/일반 타일 확률을 설정해 주는 메서드를 추가했다고 가정
        tileManager.SetMonsterModeActive(true, monsterTileProbability, normalTileProbability);
    }

    public void UpdateMode()
    {
        // 매 프레임 필요한 로직이 있으면 작성
    }

    public void ExitMode()
    {
        Debug.Log("<color=green>[MonsterMode]</color> ExitMode 호출됨");
        // 모드 종료 시, 다시 비활성화 (선택 사항)
        tileManager.SetMonsterModeActive(false, 0f, 1f);
    }
}

