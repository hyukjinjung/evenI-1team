using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnOffMode : IGameMode
{
    private TestTileManager tileManager;

    // OnOff Ÿ�� Ȯ��(20%), �Ϲ� Ÿ�� Ȯ��(80%)
    private float onOffTileProbability = 0.2f;
    private float normalTileProbability = 0.8f;

    public OnOffMode(TestTileManager tileManager)
    {
        this.tileManager = tileManager;
    }

    public void InitializeMode()
    {
        Debug.Log("<color=blue>[OnOffMode]</color> InitializeMode ȣ���");

        // TestTileManager�� ���¿��� ��塱�� �� �ְ�,
        // OnOff/�Ϲ� Ÿ�� Ȯ���� ������ �ִ� �޼��带 �߰��ߴٰ� ����
        tileManager.SetOnOffModeActive(true, onOffTileProbability, normalTileProbability);
    }

    public void UpdateMode()
    {
        // �� ������ �ʿ��� ������ ������ �ۼ�
    }

    public void ExitMode()
    {
        Debug.Log("<color=blue>[OnOffMode]</color> ExitMode ȣ���");
        tileManager.SetOnOffModeActive(false, 0f, 1f);
    }
}
