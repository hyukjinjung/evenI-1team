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
    [SerializeField] private GameObject testTilePrefab;      // �׽�Ʈ Ÿ�� ������ �߰�
    [SerializeField] private GameObject obstacleStairPrefab; // ���� ������Ʈ ������ �߰�
    [SerializeField] private GameObject ReverseControlPrefab; // ����Ű ���� ������Ʈ ������ �߰�
    [SerializeField] private GameObject TransparentPrefab; // ���� ���� ������Ʈ ������ �߰�
    [SerializeField] private GameObject StickyPrefab; // ������ ���� ������Ʈ ������ �߰�
    [SerializeField] private GameObject HideNextPrefab; // ���� ���� ���� ������Ʈ ������ �߰�

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

    // y ��ǥ�� ���� ��
    private int currentY = 0;

    // Ÿ�� ���� ����
    [SerializeField] private float spawnInterval = 0.5f;

    // ���� ������Ʈ ���� Ȯ�� (0 ~ 1)
    [SerializeField] private float obstacleSpawnChance = 0.3f;

    // �ִ� Ÿ�� ��
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
            // Ÿ�� ����
            GameObject testTile = Instantiate(testTilePrefab, transform);
            testTile.transform.localPosition = new Vector3(currentX, currentY, 0);
            testTile.gameObject.SetActive(true);

            Tile tileComponent = testTile.GetComponent<Tile>();
            tiles.Add(tileComponent);

            // ���� Ȯ���� ���� ������Ʈ ����
            if (Random.value < obstacleSpawnChance)
            {
                CreateObstacleOnTile(tileComponent);
            }

            // �ִ� Ÿ�� ���� �ʰ��ϸ� ���� ������ Ÿ�� ����
            if (tiles.Count > maxTiles)
            {
                Destroy(tiles[0].gameObject);
                tiles.RemoveAt(0);
            }

            // Ÿ�� �Ű������� �����Ǿ� �ִٸ� �̸� ����Ͽ� x ��ǥ ����
            if (tiles.Count < tileParams.Count)
            {
                TileGenerationParam param = tileParams[tiles.Count];

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
                // �Ű������� ���ٸ� �����ϰ� ���� ����
                int randomDirection = Random.Range(0, 2) * 2 - 1; // -1 �Ǵ� 1
                currentX += randomDirection;
            }

            // y ��ǥ ������Ʈ
            currentY += 1;

            yield return new WaitForSeconds(spawnInterval);
        }
    }

    // Ư�� Ÿ�� ���� ��ֹ��� �����ϴ� �Լ�
    private void CreateObstacleOnTile(Tile tile)
    {
        if (tile == null) return;

        // ��ֹ� ���� �� Ÿ���� �ڽ����� ����
        GameObject obstacle = Instantiate(obstacleStairPrefab, tile.transform);

        // ��ֹ��� Ÿ���� �߾�, �ణ ���ʿ� ��ġ�ϰ� Z ��ǥ�� �����Ͽ� ���ʿ� ��ġ��Ŵ
        obstacle.transform.localPosition = new Vector3(0, 0.1f, -0.1f); // Ÿ�Ϻ��� ��¦ ���� ��ġ�ϰ� �������� �̵�
        obstacle.gameObject.SetActive(true);

        // ��ֹ��� Ÿ���� ���� ����
        ObstacleStair obstacleScript = obstacle.GetComponent<ObstacleStair>();
        if (obstacleScript != null)
        {
            obstacleScript.AssignRandomObstacle();
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
