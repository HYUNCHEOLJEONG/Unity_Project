using UnityEngine;

public class Potion : MonoBehaviour
{
    public float manaRecoveryAmount = 20f;
    public float fallSpeed = 5f;

    private void Update()
    {
        transform.Translate(Vector3.down * fallSpeed * Time.deltaTime);
        if (transform.position.y < -40f)
        {
            Destroy(gameObject); // 화면 아래로 떨어지면 제거
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
            }
            Destroy(gameObject);
        }
    }
    
   
}
