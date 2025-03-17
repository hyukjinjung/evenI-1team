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
        Debug.Log("���ǵ��� �ʱ�ȭ");
        tileManager.ResetSettingsToDefault();
        tileManager.AdjustSpeed(1.5f); // ���ǵ� ����
        uiManager.StartTimer(duration);
    }

    public void UpdateMode()
    {
        // ���ǵ��� ���� ������Ʈ (�ʿ� ��)
    }

    public void ExitMode()
    {
        Debug.Log("���ǵ��� ����");
        tileManager.AdjustSpeed(1f); // �ӵ� ������� ����
    }
}

