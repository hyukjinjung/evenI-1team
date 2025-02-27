using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScoreTracker : MonoBehaviour
{
    private float startY;
    private float endY;
    private bool isGameOver = false;

    public int score = 0;

    // Start is called before the first frame update
    void Start()
    {
        startY = transform.position.y;
        Debug.Log($"���� ���� Y  ��ġ: {startY}");
    }

    // Update is called once per frame
   
    private void Update()
    {
        if (isGameOver) return; // ���� ���� �Ŀ��� ��� ������� ����

        // �÷��̾��� ���� ��ġ ����
        endY = transform.position.y;
    }

    public void CalculateScore()
    {
        if (isGameOver) return; // �ߺ� ��� ����

        isGameOver = true; // ���� ���� ó��
        float distance = endY - startY; // Y������ �̵��� �Ÿ�
        score = Mathf.RoundToInt(distance); // ���� ��� (1 ���� = 1��)

        Debug.Log($"���� ����! �� ����: {score} (�̵� �Ÿ�: {distance} ����)");
    }
}





