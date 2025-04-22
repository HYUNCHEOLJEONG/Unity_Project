using UnityEngine;

public class BackgroundScroller : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 3f;
    private float resetPositionY = -78f;   // 한 배경이 완전히 내려가면
    private float repositionY = 156f;      // 다시 위로 올려줄 거리 (77.3 * 2)

    void Update()
    {
        transform.position += Vector3.down * moveSpeed * Time.deltaTime;

        if (transform.position.y <= resetPositionY)
        {
            transform.position += new Vector3(0f, repositionY, 0f);
        }
    }
}
