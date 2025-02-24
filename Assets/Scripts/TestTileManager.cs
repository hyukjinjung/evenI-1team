using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public struct TileGenerationParam
{
    // 0 ~ 100 ������ Ȯ����
    public int probability;

    // �⺻ ����: 1�� ������, -1�� ����
    public int defaultDirection;
}


public class TestTileManager : MonoBehaviour
{
    [SerializeField] private GameObject testTilePrefab;

    //[SerializeField] public GameObject obstacleStairPrefab;

    // Ÿ�� ���� ����
    private float xOffset = 1;
    private float yOffset = 1;

    private int direction = 1;

    // Inspector���� �� Ÿ�Ͽ� ���� �Ű������� ������ �� ����
    [SerializeField] private List<TileGenerationParam> tileParams = new List<TileGenerationParam>();


    // Ÿ�ϵ��� �����ϴ� ����Ʈ
    private List<Tile> tiles = new List<Tile>();

    // x ��ǥ�� ���� ��
    private int currentX = 0;




    // Start is called before the first frame update
    void Start()
    {
        int tileCount = 20; // ������ Ÿ�� ����

        for (int i = 0; i < tileCount; i++)
        {
            GameObject testTile = Instantiate(testTilePrefab);
            testTile.transform.SetParent(transform);

            testTile.transform.localPosition = new Vector3(currentX, yOffset * i, 0);
            testTile.gameObject.SetActive(true);

            tiles.Add(testTile.GetComponent<Tile>());


            // Ÿ�� �Ű������� �����Ǿ� �ִٸ� �̸� ����Ͽ� x ��ǥ ����
            if (i < tileParams.Count)
            {
                TileGenerationParam param = tileParams[i];

                // 0~100 ������ ���� ���� ����
                int randValue = Random.Range(0, 101);

                int chosenDirection;

                // ���� ���� �Ű������� Ȯ������ ������ �⺻ ����, �׷��� ������ �ݴ� ���� ����
                if (randValue < param.probability)
                    chosenDirection = param.defaultDirection;
                else
                    chosenDirection = -param.defaultDirection;


                // ���õ� ���⿡ ���� x ��ǥ ������Ʈ
                currentX += chosenDirection;
            }
            else
            {
                // �Ű������� ���ٸ� �⺻ ������(������ �̵�) ����
                currentX += 1;
            }
        }
    }


    // ���� ���� �ش��ϴ� Ÿ���� ��ȯ
    public Tile GetTile(int currentFloor)
    {
        if (currentFloor < 0 || currentFloor >= tiles.Count - 1) // ���� Ȯ��
            return null;

        return tiles[currentFloor + 1];
    }
}
