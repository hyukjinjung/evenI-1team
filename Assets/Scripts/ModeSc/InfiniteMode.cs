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
        Debug.Log("���Ѹ�� �ʱ�ȭ");
        // Ÿ�ϸŴ��� ���� �ʱ�ȭ ����
        tileManager.ResetSettingsToDefault();
    }

    public void UpdateMode()
    {
        // ���Ѹ�忡�� �����Ӹ��� ������Ʈ�� ������ ������ ����
    }

    public void ExitMode()
    {
        Debug.Log("���Ѹ�� ����");
    }
}

