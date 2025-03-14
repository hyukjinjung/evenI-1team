using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MonsterTile : MonoBehaviour
{
    // ���� ������ �迭
    [SerializeField] private GameObject[] monsterPrefabs;

    // ���� ���� ��ġ
    [SerializeField] private Transform monsterSpawnPoint;

    // ���� ���� ���� ������
    [SerializeField] private float spawnHeightOffset = 1.0f;

    // ���Ͱ� �����Ǿ����� ����
    private bool hasSpawnedMonster = false;

    // �̹� ������ ���� ����
    private GameObject spawnedMonster;

    // ���� ������ ���� ���� ���� ���� (������)
    private static int monsterCount = 0;

    private void Start()
    {
        // ����� �α�
        Debug.Log($"MonsterTile Start() ȣ���. ���� ���� ��: {monsterCount}");

        // ���� ���� ��ġ�� ������ �ڵ����� ����
        if (monsterSpawnPoint == null)
        {
            monsterSpawnPoint = transform;
        }

        // ���� ���� (�� ����)
        if (!hasSpawnedMonster)
        {
            SpawnRandomMonster();
        }
    }

    // ���� �ڽ� ������Ʈ Ȯ�� �� ����
    private void CheckExistingMonsters()
    {
        // �̹� �ڽ����� ���Ͱ� �ִ��� Ȯ��
        foreach (Transform child in transform)
        {
            // �ڽ� ������Ʈ�� �������� Ȯ�� (�±׳� �̸����� Ȯ��)
            if (child.CompareTag("Monster") || child.name.Contains("Fly") ||
                child.name.Contains("Mantis") || child.name.Contains("Spider") ||
                child.name.Contains("Butterfly") || child.name.Contains("Dragonfly"))
            {
                Debug.LogWarning($"�̹� ���Ͱ� �����մϴ�: {child.name}. �����մϴ�.");
                Destroy(child.gameObject);
            }
        }
    }

    // ���� ���� ����
    private void SpawnRandomMonster()
    {
        if (hasSpawnedMonster)
        {
            Debug.LogWarning("�̹� ���Ͱ� �����Ǿ����ϴ�.");
            return;
        }

        // ���� ���� Ȯ�� �� ����
        CheckExistingMonsters();

        if (monsterPrefabs == null || monsterPrefabs.Length == 0)
        {
            Debug.LogError("���� �������� �������� �ʾҽ��ϴ�.");
            return;
        }

        // ���� ���� ����
        int randomIndex = Random.Range(0, monsterPrefabs.Length);
        GameObject selectedMonsterPrefab = monsterPrefabs[randomIndex];

        if (selectedMonsterPrefab == null)
        {
            Debug.LogWarning($"���õ� ���� �������� null�Դϴ�. �ε���: {randomIndex}");
            return;
        }

        // ���� ����
        Vector3 spawnPosition = monsterSpawnPoint.position + new Vector3(0f, spawnHeightOffset, 0f);
        spawnedMonster = Instantiate(selectedMonsterPrefab, spawnPosition, Quaternion.identity);

        // ���͸� Ÿ���� �ڽ����� ����
        spawnedMonster.transform.SetParent(transform);

        // ���� �±� ���� (���� �������� ���� ���)
        if (string.IsNullOrEmpty(spawnedMonster.tag) || spawnedMonster.tag == "Untagged")
        {
            spawnedMonster.tag = "Monster";
        }

        hasSpawnedMonster = true;
        monsterCount++;

        Debug.Log($"���� ����: {selectedMonsterPrefab.name}, �� ���� ��: {monsterCount}");
    }

    // ������Ʈ�� ���ŵ� �� ȣ��
    private void OnDestroy()
    {
        if (spawnedMonster != null)
        {
            monsterCount--;
            Debug.Log($"���� Ÿ�� ���ŵ�. ���� ���� ��: {monsterCount}");
        }
    }
}

