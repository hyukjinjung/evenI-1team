using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 
Ŭ���� ����: 
���� �� Ÿ�� ������ ����
 
*/
public class Tile : MonoBehaviour
{
    private List<Monster> monstersOnTile = new List<Monster>();


    // �÷��̾ Ÿ�� ���ʿ� �ִ��� Ȯ��
    private GameObject obstacle;

    public void SetObstacle(GameObject obstacle)
    {
        this.obstacle = obstacle;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && obstacle != null)
        {
            Destroy(obstacle);
            obstacle = null;
        }
    }

    public bool TileOnLeft(Transform position)
    {
        return position.position.x > transform.position.x;
    }


    // ����Ʈ�� ���Ͱ� �ִ��� Ȯ��
    // ù ��° ���͸� ��ȯ
    // NinjaFrog�� Ư�� �ɷ� �ߵ� �� ����
    public Monster GetFirstMonster()
    {
        return monstersOnTile.Count > 0 ? monstersOnTile[0] : null;
    }


    // Ÿ�Ͽ� ���� �߰�
    public void AddMonster(Monster monster)
    {
        if (!monstersOnTile.Contains(monster))
            monstersOnTile.Add(monster);
    }


    // Ÿ�Ͽ��� ���� ����
    public void RemoveMonster(Monster monster)
    {
        if (monstersOnTile.Contains(monster))
            monstersOnTile.Remove(monster);
    }

}
