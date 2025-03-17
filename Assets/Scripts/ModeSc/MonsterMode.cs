using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterMode : IGameMode
{
    private TestTileManager tileManager;

    // ���� Ÿ�� Ȯ��(20%), �Ϲ� Ÿ�� Ȯ��(80%)
    private float monsterTileProbability = 0.2f;
    private float normalTileProbability = 0.8f;

    public MonsterMode(TestTileManager tileManager)
    {
        this.tileManager = tileManager;
    }

    public void InitializeMode()
    {
        Debug.Log("<color=green>[MonsterMode]</color> InitializeMode ȣ���");

        // TestTileManager�� ������ ��塱�� �� �ְ�,
        // ����/�Ϲ� Ÿ�� Ȯ���� ������ �ִ� �޼��带 �߰��ߴٰ� ����
        tileManager.SetMonsterModeActive(true, monsterTileProbability, normalTileProbability);
    }

    public void UpdateMode()
    {
        // �� ������ �ʿ��� ������ ������ �ۼ�
    }

    public void ExitMode()
    {
        Debug.Log("<color=green>[MonsterMode]</color> ExitMode ȣ���");
        // ��� ���� ��, �ٽ� ��Ȱ��ȭ (���� ����)
        tileManager.SetMonsterModeActive(false, 0f, 1f);
    }
}

