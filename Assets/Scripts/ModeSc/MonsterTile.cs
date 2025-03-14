using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MonsterTile : MonoBehaviour
{
    // 몬스터 프리팹 배열
    [SerializeField] private GameObject[] monsterPrefabs;

    // 몬스터 생성 위치
    [SerializeField] private Transform monsterSpawnPoint;

    // 몬스터 생성 높이 오프셋
    [SerializeField] private float spawnHeightOffset = 1.0f;

    // 몬스터가 생성되었는지 여부
    private bool hasSpawnedMonster = false;

    // 이미 생성된 몬스터 참조
    private GameObject spawnedMonster;

    // 정적 변수로 몬스터 생성 여부 추적 (디버깅용)
    private static int monsterCount = 0;

    private void Start()
    {
        // 디버깅 로그
        Debug.Log($"MonsterTile Start() 호출됨. 현재 몬스터 수: {monsterCount}");

        // 몬스터 생성 위치가 없으면 자동으로 설정
        if (monsterSpawnPoint == null)
        {
            monsterSpawnPoint = transform;
        }

        // 몬스터 생성 (한 번만)
        if (!hasSpawnedMonster)
        {
            SpawnRandomMonster();
        }
    }

    // 기존 자식 오브젝트 확인 및 제거
    private void CheckExistingMonsters()
    {
        // 이미 자식으로 몬스터가 있는지 확인
        foreach (Transform child in transform)
        {
            // 자식 오브젝트가 몬스터인지 확인 (태그나 이름으로 확인)
            if (child.CompareTag("Monster") || child.name.Contains("Fly") ||
                child.name.Contains("Mantis") || child.name.Contains("Spider") ||
                child.name.Contains("Butterfly") || child.name.Contains("Dragonfly"))
            {
                Debug.LogWarning($"이미 몬스터가 존재합니다: {child.name}. 제거합니다.");
                Destroy(child.gameObject);
            }
        }
    }

    // 랜덤 몬스터 생성
    private void SpawnRandomMonster()
    {
        if (hasSpawnedMonster)
        {
            Debug.LogWarning("이미 몬스터가 생성되었습니다.");
            return;
        }

        // 기존 몬스터 확인 및 제거
        CheckExistingMonsters();

        if (monsterPrefabs == null || monsterPrefabs.Length == 0)
        {
            Debug.LogError("몬스터 프리팹이 설정되지 않았습니다.");
            return;
        }

        // 랜덤 몬스터 선택
        int randomIndex = Random.Range(0, monsterPrefabs.Length);
        GameObject selectedMonsterPrefab = monsterPrefabs[randomIndex];

        if (selectedMonsterPrefab == null)
        {
            Debug.LogWarning($"선택된 몬스터 프리팹이 null입니다. 인덱스: {randomIndex}");
            return;
        }

        // 몬스터 생성
        Vector3 spawnPosition = monsterSpawnPoint.position + new Vector3(0f, spawnHeightOffset, 0f);
        spawnedMonster = Instantiate(selectedMonsterPrefab, spawnPosition, Quaternion.identity);

        // 몬스터를 타일의 자식으로 설정
        spawnedMonster.transform.SetParent(transform);

        // 몬스터 태그 설정 (아직 설정되지 않은 경우)
        if (string.IsNullOrEmpty(spawnedMonster.tag) || spawnedMonster.tag == "Untagged")
        {
            spawnedMonster.tag = "Monster";
        }

        hasSpawnedMonster = true;
        monsterCount++;

        Debug.Log($"몬스터 생성: {selectedMonsterPrefab.name}, 총 몬스터 수: {monsterCount}");
    }

    // 오브젝트가 제거될 때 호출
    private void OnDestroy()
    {
        if (spawnedMonster != null)
        {
            monsterCount--;
            Debug.Log($"몬스터 타일 제거됨. 남은 몬스터 수: {monsterCount}");
        }
    }
}

