using UnityEngine;

public class Potion : MonoBehaviour
{
    public float manaRecoveryAmount = 20f; // 포션이 회복하는 양
    public float fallSpeed = 5f; // 포션이 떨어지는 속도

    private void Update()
    {
        transform.Translate(Vector3.down * fallSpeed * Time.deltaTime);
        if (transform.position.y < -40f)
        {
            Destroy(gameObject); // 포션이 화면 아래로 떨어지면 파괴괴
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Player player = collision.GetComponent<Player>();
            if (player != null && player.ui != null)
            {
                player.ui.RegainMana(manaRecoveryAmount);
                // 플레이어의 UI에서 마나 회복
            }
            Destroy(gameObject);
        }
    }
    
   
}
