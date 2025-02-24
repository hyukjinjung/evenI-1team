using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct TileGenerationParam
{
    // 0 ~ 100 사이의 확률값
    public int probability;

    // 기본 방향: 1은 오른쪽, -1은 왼쪽
    public int defaultDirection;
}

public class TestTileManager : MonoBehaviour
{
    [SerializeField] private GameObject testTilePrefab;      // 테스트 타일 프리팹 추가
    [SerializeField] private GameObject obstacleStairPrefab; // 방해 오브젝트 프리팹 추가
    [SerializeField] private GameObject ReverseControlPrefab; // 방향키 반전 오브젝트 프리팹 추가
    [SerializeField] private GameObject TransparentPrefab; // 투명 발판 오브젝트 프리팹 추가
    [SerializeField] private GameObject StickyPrefab; // 끈끈이 발판 오브젝트 프리팹 추가
    [SerializeField] private GameObject HideNextPrefab; // 다음 발판 숨김 오브젝트 프리팹 추가

    // 타일 간의 간격
    private float xOffset = 1;
    private float yOffset = 1;

    private int direction = 1;

    // Inspector에서 각 타일에 대한 매개변수를 설정할 수 있음
    [SerializeField] private List<TileGenerationParam> tileParams = new List<TileGenerationParam>();

    // 타일들을 저장하는 리스트
    private List<Tile> tiles = new List<Tile>();

    // x 좌표의 현재 값
    private int currentX = 0;

    // y 좌표의 현재 값
    private int currentY = 0;

    // 타일 생성 간격
    [SerializeField] private float spawnInterval = 0.5f;

    // 방해 오브젝트 생성 확률 (0 ~ 1)
    [SerializeField] private float obstacleSpawnChance = 0.3f;

    // 최대 타일 수
    [SerializeField] private int maxTiles = 20;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(GenerateTiles());
    }

    private IEnumerator GenerateTiles()
    {
        while (true)
        {
            // 타일 생성
            GameObject testTile = Instantiate(testTilePrefab, transform);
            testTile.transform.localPosition = new Vector3(currentX, currentY, 0);
            testTile.gameObject.SetActive(true);

            Tile tileComponent = testTile.GetComponent<Tile>();
            tiles.Add(tileComponent);

            // 일정 확률로 방해 오브젝트 생성
            if (Random.value < obstacleSpawnChance)
            {
                CreateObstacleOnTile(tileComponent);
            }

            // 최대 타일 수를 초과하면 가장 오래된 타일 삭제
            if (tiles.Count > maxTiles)
            {
                Destroy(tiles[0].gameObject);
                tiles.RemoveAt(0);
            }

            // 타일 매개변수가 지정되어 있다면 이를 사용하여 x 좌표 갱신
            if (tiles.Count < tileParams.Count)
            {
                TileGenerationParam param = tileParams[tiles.Count];

                // 0~100 사이의 랜덤 숫자 생성
                int randValue = Random.Range(0, 101);

                int chosenDirection;

                // 랜덤 값이 매개변수의 확률보다 작으면 기본 방향, 그렇지 않으면 반대 방향 선택
                if (randValue < param.probability)
                    chosenDirection = param.defaultDirection;
                else
                    chosenDirection = -param.defaultDirection;

                // 선택된 방향에 따라 x 좌표 업데이트
                currentX += chosenDirection;
            }
            else
            {
                // 매개변수가 없다면 랜덤하게 방향 선택
                int randomDirection = Random.Range(0, 2) * 2 - 1; // -1 또는 1
                currentX += randomDirection;
            }

            // y 좌표 업데이트
            currentY += 1;

            yield return new WaitForSeconds(spawnInterval);
        }
    }

    // 특정 타일 위에 장애물을 생성하는 함수
    private void CreateObstacleOnTile(Tile tile)
    {
        if (tile == null) return;

        // 장애물 생성 후 타일의 자식으로 설정
        GameObject obstacle = Instantiate(obstacleStairPrefab, tile.transform);

        // 장애물을 타일의 중앙, 약간 위쪽에 배치하고 Z 좌표를 조정하여 앞쪽에 위치시킴
        obstacle.transform.localPosition = new Vector3(0, 0.1f, -0.1f); // 타일보다 살짝 위에 위치하고 앞쪽으로 이동
        obstacle.gameObject.SetActive(true);

        // 장애물의 타입을 랜덤 지정
        ObstacleStair obstacleScript = obstacle.GetComponent<ObstacleStair>();
        if (obstacleScript != null)
        {
            obstacleScript.AssignRandomObstacle();
        }
    }

    // 현재 층에 해당하는 타일을 반환
    public Tile GetTile(int currentFloor)
    {
        if (currentFloor < 0 || currentFloor >= tiles.Count - 1) // 범위 확인
            return null;

        return tiles[currentFloor + 1];
    }
}
