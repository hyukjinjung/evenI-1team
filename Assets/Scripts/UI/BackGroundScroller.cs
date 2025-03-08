using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGroundScroller : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    public float scrollSpeed = 2f;
    private float backgroundHeight;
    public bool isGameStarted = false;

    void Start()
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
            backgroundHeight = spriteRenderer.bounds.size.y;
        }

        void Update()
        {
        if (isGameStarted) // ������ ���۵Ǿ��� ���� ��ũ��
        {
            transform.Translate(Vector3.down * scrollSpeed * Time.deltaTime);

            if (transform.position.y < -backgroundHeight)
            {
                RepositionBackground();
            }
        }
    }

        void RepositionBackground()
        {
            Vector3 offset = new Vector3(0, backgroundHeight * 3f);
            transform.position += offset;
        }


        //private SpriteRenderer spriteRenderer;
        //public float scrollSpeed = 2f;
        //private float backgroundHeight;



        //// Start is called before the first frame update
        //void Start()
        //{
        //    spriteRenderer = GetComponent<SpriteRenderer>();
        //    backgroundHeight = spriteRenderer.bounds.size.y;

        //}



        //void RepositionBackground()
        //{
        //    Vector3 offset = new Vector3(0, backgroundHeight * 3f - 0.01f); //������ ���� �߰� ���� ���ȼ����� ��ġ�� ������� 0.01���� 
        //    transform.position += offset;
        //}

        //private void OnTriggerEnter2D(Collider2D other)
        //{
        //    RepositionBackground();
        //}
}
