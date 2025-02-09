using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileChecker : MonoBehaviour
{
    public LayerMask targetLayer; // Ÿ�� ���̾�

    public float checkDistanceX = 0.75f; // Ÿ�� x��
    public float checkDistanceY = 0.45f; // Ÿ�� y��



    // ���� �밢�� ���� Ÿ���� �ִ��� Ȯ���ϴ� �Լ�
    public bool IsTileInFrontLeft()
    {
        // ���� �밢�� �� Ÿ�� ��ġ ���
        Vector2 leftDiagonalTile = new Vector2(transform.position.x - checkDistanceX, transform.position.y + checkDistanceY);

        float radius = 0.5f; // ���� ���� ����
        Collider2D[] hits = Physics2D.OverlapCircleAll(leftDiagonalTile, radius, targetLayer);

        Debug.Log("���� �밢�� üũ ��ǥ: " + leftDiagonalTile + ", ������ ������Ʈ ��: " + hits.Length);

        // Ÿ���� �����Ǿ����� ���� ��ȯ
        return hits.Length > 0;
    }


    // ������ �밢�� ���� Ÿ���� �ִ��� Ȯ���ϴ� �Լ�
    public bool IsTileInFrontRight()
    {
        // ������ �밢�� �� Ÿ�� ��ġ ���
        Vector2 rightDiagonalTile = new Vector2(transform.position.x + checkDistanceX, transform.position.y + checkDistanceY);

        float radius = 0.5f; // ���� ���� ����
        Collider2D[] hits = Physics2D.OverlapCircleAll(rightDiagonalTile, radius, targetLayer);

        Debug.Log("������ �밢�� üũ ��ǥ: " + rightDiagonalTile + ", ������ ������Ʈ ��: " + hits.Length);

        // Ÿ���� �����Ǿ����� ���� ��ȯ
        return hits.Length > 0;
    }


    // �÷��̾� �ٷ� �Ʒ��� Ÿ���� �ִ��� Ȯ���ϴ� �Լ�
    public bool IsTileBelow()
    {
        Vector2 belowTilePosition = new Vector2(transform.position.x, transform.position.y - checkDistanceY);

        float radius = 0.5f; // ���� ���� ����
        Collider2D[] hits = Physics2D.OverlapCircleAll(belowTilePosition, radius, targetLayer);

        Debug.Log("�Ʒ� Ÿ�� üũ ��ǥ: " + belowTilePosition + ", ������ ������Ʈ ��: " + hits.Length);

        // Ÿ���� �����Ǿ����� ���� ��ȯ
        return hits.Length > 0;
    }
}
