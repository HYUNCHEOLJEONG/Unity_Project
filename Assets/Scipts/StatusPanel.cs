using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerUI : MonoBehaviour
{
    [Header("UI Components")]
    public Image healthBar;
    public TextMeshProUGUI healthText;

    public Image manaBar;
    public TextMeshProUGUI manaText;

    [Header("Status Values")]
    private float maxHealth = 100f;
    private float currentHealth = 100f;
    private float displayedHealth = 100f;

    private float maxMana = 100f;
    private float currentMana = 100f;
    private float displayedMana = 100f;

    [Header("Lerp Speed")]
    public float lerpSpeed = 5f;
    void Start()
    {
        currentMana = 0f;
        displayedMana = 0f; // UI�� ó���� 0����
    }
    void Update()
    {
        // ü�°� ���� UI �� �ε巴�� ��ȭ
        displayedHealth = Mathf.Lerp(displayedHealth, currentHealth, Time.deltaTime * lerpSpeed);
        displayedMana = Mathf.Lerp(displayedMana, currentMana, Time.deltaTime * lerpSpeed);

        healthBar.fillAmount = displayedHealth / maxHealth;
        healthText.text = $"HP {Mathf.CeilToInt(displayedHealth)} /{maxHealth}";

        manaBar.fillAmount = displayedMana / maxMana;
        manaText.text = $"MP {Mathf.CeilToInt(displayedMana)} /{maxMana}";
    }

    // ���� ó��
    public void TakeDamage(float amount)
    {
        currentHealth = Mathf.Clamp(currentHealth - amount, 0, maxHealth);
    }

    // ���� ���
    public void UseMana(float amount)
    {
        currentMana = Mathf.Clamp(currentMana - amount, 0, maxMana);
    }

    // ü�� ȸ��
    public void Heal(float amount)
    {
        currentHealth = Mathf.Clamp(currentHealth + amount, 0, maxHealth);
    }

    // ���� ȸ��
    public void RegainMana(float amount)
    {
        currentMana = Mathf.Clamp(currentMana + amount, 0, maxMana);
    }

    // ���� ü�� ��ȯ
    public float GetCurrentHealth()
    {
        return currentHealth;
    }

    // ���� ���� ��ȯ
    public float GetCurrentMana()
    {
        return currentMana;
    }

    // �ִ� ���� ��ȯ
    public float GetMaxMana()
    {
        return maxMana;
    }
}
