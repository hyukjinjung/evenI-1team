using System.Collections;
using UnityEngine;

public class ObstacleStair : MonoBehaviour
{
    public enum ObstacleType { ReverseControl, Transparent, Sticky, HideNext }
    public ObstacleType obstacleType;

    [Header("Obstacle Materials")]
    public Material[] obstacleMaterials; // 0: ReverseControl, 1: Transparent, 2: Sticky, 3: HideNext

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

        if (rend != null && obstacleMaterials.Length == 4) // �迭 ���� Ȯ��
        {
            rend.material = obstacleMaterials[(int)obstacleType]; // �� Ÿ�Կ� �´� Material ����

            switch (obstacleType)
            {
                case ObstacleType.ReverseControl:
                    // ����Ű ����
                    break;

                case ObstacleType.Transparent:
                    // ���� ����
                    StartCoroutine(ToggleTransparency());
                    break;

                case ObstacleType.Sticky:
                    // ������ ����
                    break;

                case ObstacleType.HideNext:
                    // ���� ���� ����
                    StartCoroutine(HideNextStair());
                    break;
            }
        }
        else
        {
            Debug.LogError($"{gameObject.name} - Material �迭�� �ùٸ��� �������� �ʾҽ��ϴ�!");
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
