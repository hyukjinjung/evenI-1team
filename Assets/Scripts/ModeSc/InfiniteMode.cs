using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfiniteMode : IGameMode
{
    private TestTileManager tileManager;

    public InfiniteMode(TestTileManager tileManager)
    {
        this.tileManager = tileManager;
    }

    public void InitializeMode()
    {
        Debug.Log("무한모드 초기화");
        // 타일매니저 설정 초기화 예시
        tileManager.ResetSettingsToDefault();
    }

    public void UpdateMode()
    {
        // 무한모드에서 프레임마다 업데이트할 사항이 있으면 구현
    }

    public void ExitMode()
    {
        Debug.Log("무한모드 종료");
    }
}

