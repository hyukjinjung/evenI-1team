using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGroundScroller : MonoBehaviour
{
    public float scrollSpeed = 2f;//
    private float backgroundHeight;//���ȭ���� ���� ����� ������ �־�ߵ�



    // Start is called before the first frame update
    void Start()//���� ù ������ ���� ���� ����� �ҷ��� 
    {
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();//������ �ȿ� �ִ� ������ ����ž� 
        backgroundHeight = spriteRenderer.bounds.size.y;//���� ����� �ҷ���
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.up * scrollSpeed * Time.deltaTime);
        //��ġ�� ����ϴ� Ʈ������, ���� * 

        if(transform.position.y < -backgroundHeight)//�������� ���ٰ� �ڱ��� ����ŭ �����̸� �ű��

        {
            RepositionBackground();
        }
    }

    void RepositionBackground()//���ƿ��� ��� 
    {
        Vector2 offset = new Vector2(backgroundHeight * 3f - 0.01f, 0); //������ ���� �߰� ���� ���ȼ����� ��ġ�� ������� 0.01���� 
        transform.position= (Vector2)transform.position + offset;//�������� 2���������ʹ� 2��  �鼭2�� ��ƾߵ� 
    }


}
