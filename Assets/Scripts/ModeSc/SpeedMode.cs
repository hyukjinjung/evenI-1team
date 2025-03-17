using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedMode : IGameMode
{
    private TestTileManager tileManager;
    private float duration;
    private UIManager uiManager;

    public SpeedMode(TestTileManager tileManager, UIManager uiManager, float duration)
    {
        this.tileManager = tileManager;
        this.uiManager = uiManager;
        this.duration = duration;
    }

    public void InitializeMode()
    {
        Debug.Log("스피드모드 초기화");
        tileManager.ResetSettingsToDefault();
        tileManager.AdjustSpeed(1.5f); // 스피드 증가
        uiManager.StartTimer(duration);
    }

    public void UpdateMode()
    {
        // 스피드모드 로직 업데이트 (필요 시)
    }

    public void ExitMode()
    {
        Debug.Log("스피드모드 종료");
        tileManager.AdjustSpeed(1f); // 속도 원래대로 복귀
    }
}

