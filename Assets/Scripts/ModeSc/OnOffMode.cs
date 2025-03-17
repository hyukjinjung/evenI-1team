using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnOffMode : IGameMode
{
    private TestTileManager tileManager;

    // OnOff 타일 확률(20%), 일반 타일 확률(80%)
    private float onOffTileProbability = 0.2f;
    private float normalTileProbability = 0.8f;

    public OnOffMode(TestTileManager tileManager)
    {
        this.tileManager = tileManager;
    }

    public void InitializeMode()
    {
        Debug.Log("<color=blue>[OnOffMode]</color> InitializeMode 호출됨");

        // TestTileManager에 “온오프 모드”를 켜 주고,
        // OnOff/일반 타일 확률을 설정해 주는 메서드를 추가했다고 가정
        tileManager.SetOnOffModeActive(true, onOffTileProbability, normalTileProbability);
    }

    public void UpdateMode()
    {
        // 매 프레임 필요한 로직이 있으면 작성
    }

    public void ExitMode()
    {
        Debug.Log("<color=blue>[OnOffMode]</color> ExitMode 호출됨");
        tileManager.SetOnOffModeActive(false, 0f, 1f);
    }
}
