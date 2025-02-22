using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


[System.Serializable]
public struct TileGenerationParam
{
    // 0 ~ 100 사이의 확률값
    public int probability;

    // 기본 방향: 1은 오른쪽, -1은 왼쪽
    public int defaultDirection;

    // 변경 여부
    public bool isChanged;
}


public class TestTileManager : MonoBehaviour
{
    [SerializeField] private GameObject testTilePrefab;

    //[SerializeField] public GameObject obstacleStairPrefab;

    // 타일 간의 간격
    //private float xOffset = 1;
    private float yOffset = 1;

    //private int direction = 1;

    // Inspector에서 각 타일에 대한 매개변수를 설정할 수 있음
    //[SerializeField] private List<TileGenerationParam> tileParams = new List<TileGenerationParam>();


    // 타일들을 저장하는 리스트
    private List<Tile> tiles = new List<Tile>();

    // x 좌표의 현재 값
    private int currentX = 0;

    private const int minX = 35;
    private const int maxX = 65;

    private const int resetX = 50;



    // Start is called before the first frame update
    void Start()
    {
        int tileCount = 20; // 생성할 타일 개수

        for (int i = 0; i < tileCount; i++)
        {
            GameObject testTile = Instantiate(testTilePrefab);
            testTile.transform.SetParent(transform);

            testTile.transform.localPosition = new Vector3(currentX, yOffset * i, 0);
            testTile.SetActive(true);
            //testTile.gameObject.SetActive(true);

            tiles.Add(testTile.GetComponent<Tile>());

            currentX += 1;


            float chance = Mathf.Min(1f, 0.3f + 0.35f * Mathf.Log10(i + 1));

            if (Random.value <= chance)
            {
                int additionalOffset = Random.Range(3, 6) * (Random.value < 0.5f ? 1 : -1);
                int newX = currentX + additionalOffset;

                // 새 좌표가 허용 범위 [minX, maxX]를 벗어나면 resetX로 설정,
                // 그렇지 않으면 범위 내로 조정
                if (newX < minX || newX > maxX)
                {
                    currentX = resetX;
                }
                else
                {
                    currentX = Mathf.Clamp(newX, minX, maxX);
                }
            }
        }
    }


    public Tile GetTile(int currentFloor)
    {
        if (currentFloor < 0 || currentFloor >= tiles.Count - 1) // 범위 확인
            return null;

        return tiles[currentFloor + 1];
    }


    //before
    //// 타일 매개변수가 지정되어 있다면 이를 사용하여 x 좌표 갱신
    //if (i < tileParams.Count)
    //{
    //    TileGenerationParam param = tileParams[i];

    //    // 0~100 사이의 랜덤 숫자 생성
    //    int randValue = Random.Range(0, 101);

    //    int chosenDirection;

    //    // 랜덤 값이 매개변수의 확률보다 작으면 기본 방향, 그렇지 않으면 반대 방향 선택
    //    if (randValue < param.probability)
    //        chosenDirection = param.defaultDirection;
    //    else
    //        chosenDirection = -param.defaultDirection;


    //    // 선택된 방향에 따라 x 좌표 업데이트
    //    currentX += chosenDirection;
    //}
    //else
    //{
    //    // 매개변수가 없다면 기본 오프셋(오른쪽 이동) 적용
    //    currentX += 1;
    //}
}



// 현재 층에 해당하는 타일을 반환


//if (tiles.Count - 1 > currentFloor)
//{
//    return null;
//}

// 다음 타일을 반환 (플레이어가 도착해야 할 타일)

