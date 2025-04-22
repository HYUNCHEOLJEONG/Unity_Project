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
        displayedMana = 0f; // UI도 처음에 0으로
    }
    void Update()
    {
        // 체력과 마나 UI 값 부드럽게 변화
        displayedHealth = Mathf.Lerp(displayedHealth, currentHealth, Time.deltaTime * lerpSpeed);
        displayedMana = Mathf.Lerp(displayedMana, currentMana, Time.deltaTime * lerpSpeed);

        healthBar.fillAmount = displayedHealth / maxHealth;
        healthText.text = $"HP {Mathf.CeilToInt(displayedHealth)} /{maxHealth}";

        manaBar.fillAmount = displayedMana / maxMana;
        manaText.text = $"MP {Mathf.CeilToInt(displayedMana)} /{maxMana}";
    }

    // 피해 처리
    public void TakeDamage(float amount)
    {
        currentHealth = Mathf.Clamp(currentHealth - amount, 0, maxHealth);
    }

    // 마나 사용
    public void UseMana(float amount)
    {
        currentMana = Mathf.Clamp(currentMana - amount, 0, maxMana);
    }

    // 체력 회복
    public void Heal(float amount)
    {
        currentHealth = Mathf.Clamp(currentHealth + amount, 0, maxHealth);
    }

    // 마나 회복
    public void RegainMana(float amount)
    {
        currentMana = Mathf.Clamp(currentMana + amount, 0, maxMana);
    }

    // 현재 체력 반환
    public float GetCurrentHealth()
    {
        return currentHealth;
    }

    // 현재 마나 반환
    public float GetCurrentMana()
    {
        return currentMana;
    }

    // 최대 마나 반환
    public float GetMaxMana()
    {
        return maxMana;
    }
}
