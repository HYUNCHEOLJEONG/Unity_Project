using UnityEngine;

public class BackgroundScroller : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 3f;
    private float resetPositionY = -78f;   // �� ����� ������ ��������
    private float repositionY = 156f;      // �ٽ� ���� �÷��� �Ÿ� (77.3 * 2)

    void Update()
    {
        transform.position += Vector3.down * moveSpeed * Time.deltaTime;

        if (transform.position.y <= resetPositionY)
        {
            transform.position += new Vector3(0f, repositionY, 0f);
        }
    }
}
