using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField]
    private float movespeed = 20f; // �Ѿ� �ӵ�
    public float damage = 16f;
   
    private void Start()
    {
        Destroy(gameObject, 3f); // 3�� �� �Ѿ� ����
    }

    private void Update()
    {
        transform.Translate(Time.deltaTime * movespeed * Vector3.up);
    }

    // ȸ���� ����Ƽ���� ������ �� �״�� �����ϱ� ���� SetDirection�� �� �Լ��� ��
    public void SetDirection(Quaternion rotation)
    {
       
        // �ƹ��͵� �� ��! ȸ���� ����Ƽ �����Ϳ��� ������ ��θ� ���
    }
}