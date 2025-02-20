using System.Collections;
using UnityEngine;

public class ObstacleStair : MonoBehaviour
{
    public enum ObstacleType { ReverseControl, Transparent, Sticky, HideNext }
    public ObstacleType obstacleType;

    private Renderer rend;
    private Collider col;
    private bool isHidden = false;

    private void Start()
    {
        rend = GetComponentInChildren<Renderer>(); // ���� ������Ʈ���� Ž��
        col = GetComponent<Collider>();

        if (rend == null)
        {
            Debug.LogError($"{gameObject.name} - Renderer�� �������� �ʽ��ϴ�!");
        }
    }

    public void AssignRandomObstacle()
    {
        obstacleType = (ObstacleType)Random.Range(0, 4);

        if (rend != null && rend.material != null)
        {
            Material newMaterial = new Material(rend.material); // ���� �ν��Ͻ� ����
            rend.material = newMaterial; // ���Ӱ� ������ Material�� ����

            switch (obstacleType)
            {
                case ObstacleType.ReverseControl:
                    rend.material.color = Color.blue; // ����Ű ����
                    break;

                case ObstacleType.Transparent:
                    rend.material.color = Color.magenta; // ���� ����
                    StartCoroutine(ToggleTransparency());
                    break;

                case ObstacleType.Sticky:
                    rend.material.color = Color.yellow; // ������ ����
                    break;

                case ObstacleType.HideNext:
                    rend.material.color = Color.black; // ���� ���� ����
                    StartCoroutine(HideNextStair());
                    break;
            }
        }
    }

    private IEnumerator ToggleTransparency()
    {
        while (true)
        {
            yield return new WaitForSeconds(2f);
            if (rend != null)
            {
                Color color = rend.material.color;
                color.a = color.a == 1f ? 0.2f : 1f;
                rend.material.color = color;
            }
        }
    }

    private IEnumerator HideNextStair()
    {
        yield return new WaitForSeconds(1f);

        GameObject nextStair = GameObject.FindWithTag("Stair");
        if (nextStair != null && !isHidden)
        {
            Renderer nextRend = nextStair.GetComponent<Renderer>();
            if (nextRend != null)
            {
                nextRend.enabled = false;
                isHidden = true;
                yield return new WaitForSeconds(3f);
                nextRend.enabled = true;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerController player = other.GetComponent<PlayerController>();

            if (player != null)
            {
                switch (obstacleType)
                {
                    case ObstacleType.ReverseControl:
                        player.InvertControls(3f);
                        break;

                    case ObstacleType.Sticky:
                        player.ModifySpeed(0.7f, 3f);
                        break;
                }
            }
        }
    }
}
