using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


[System.Serializable]
public struct TileGenerationParam
{
    // 0 ~ 100 ������ Ȯ����
    public int probability;

    // �⺻ ����: 1�� ������, -1�� ����
    public int defaultDirection;

    // ���� ����
    public bool isChanged;
}


public class TestTileManager : MonoBehaviour
{
    [SerializeField] private GameObject testTilePrefab;

    //[SerializeField] public GameObject obstacleStairPrefab;

    // Ÿ�� ���� ����
    //private float xOffset = 1;
    private float yOffset = 1;

    //private int direction = 1;

    // Inspector���� �� Ÿ�Ͽ� ���� �Ű������� ������ �� ����
    //[SerializeField] private List<TileGenerationParam> tileParams = new List<TileGenerationParam>();


    // Ÿ�ϵ��� �����ϴ� ����Ʈ
    private List<Tile> tiles = new List<Tile>();

    // x ��ǥ�� ���� ��
    private int currentX = 0;

    private const int minX = 35;
    private const int maxX = 65;

    private const int resetX = 50;



    // Start is called before the first frame update
    void Start()
    {
        int tileCount = 20; // ������ Ÿ�� ����

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

                // �� ��ǥ�� ��� ���� [minX, maxX]�� ����� resetX�� ����,
                // �׷��� ������ ���� ���� ����
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
        if (currentFloor < 0 || currentFloor >= tiles.Count - 1) // ���� Ȯ��
            return null;

        return tiles[currentFloor + 1];
    }


    //before
    //// Ÿ�� �Ű������� �����Ǿ� �ִٸ� �̸� ����Ͽ� x ��ǥ ����
    //if (i < tileParams.Count)
    //{
    //    TileGenerationParam param = tileParams[i];

    //    // 0~100 ������ ���� ���� ����
    //    int randValue = Random.Range(0, 101);

    //    int chosenDirection;

    //    // ���� ���� �Ű������� Ȯ������ ������ �⺻ ����, �׷��� ������ �ݴ� ���� ����
    //    if (randValue < param.probability)
    //        chosenDirection = param.defaultDirection;
    //    else
    //        chosenDirection = -param.defaultDirection;


    //    // ���õ� ���⿡ ���� x ��ǥ ������Ʈ
    //    currentX += chosenDirection;
    //}
    //else
    //{
    //    // �Ű������� ���ٸ� �⺻ ������(������ �̵�) ����
    //    currentX += 1;
    //}
}



// ���� ���� �ش��ϴ� Ÿ���� ��ȯ


//if (tiles.Count - 1 > currentFloor)
//{
//    return null;
//}

// ���� Ÿ���� ��ȯ (�÷��̾ �����ؾ� �� Ÿ��)

