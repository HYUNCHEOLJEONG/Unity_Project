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
    private float maxHealth = 100f; // 최대 체력
    private float currentHealth = 100f; // 현재 체력
    private float displayedHealth = 100f; // 표시되는 체력

    private float maxMana = 100f;
    private float currentMana = 100f;
    private float displayedMana = 100f;

    [Header("Lerp Speed")] // 보간 속도
    // 보간 속도는 UI가 얼마나 빠르게 업데이트되는지를 결정합니다.
    public float lerpSpeed = 5f;
    void Start()
    {
        currentMana = 0f;
        displayedMana = 0f; 
    }
    void Update()
    {
        
        displayedHealth = Mathf.Lerp(displayedHealth, currentHealth, Time.deltaTime * lerpSpeed);
        // 화면에 표시되는 체력, 실제 게임에서의 현재체력
        // 부드러운 에니메이션 속도를 시간 기준으로 조절절
        displayedMana = Mathf.Lerp(displayedMana, currentMana, Time.deltaTime * lerpSpeed);
        healthBar.fillAmount = displayedHealth / maxHealth;
        // displatedHealth =50f,maxHealth=100f -> fillAmount = 0.5f -> 50%로 채워짐
        healthText.text = $"HP {Mathf.CeilToInt(displayedHealth)} /{maxHealth}";
        // "HP" 문자열 뒤애 displayedHealth를 올림
        manaBar.fillAmount = displayedMana / maxMana;
        manaText.text = $"MP {Mathf.CeilToInt(displayedMana)} /{maxMana}";
    }

    // 
    public void TakeDamage(float amount)
    {
        currentHealth = Mathf.Clamp(currentHealth - amount, 0, maxHealth);
    }
    // 0<currentHealth<maxHealth 
    
    public void UseMana(float amount)
    {
        currentMana = Mathf.Clamp(currentMana - amount, 0, maxMana);
    }
    /// 0<currentMana<maxMana
    public void RegainMana(float amount)
    {
        currentMana = Mathf.Clamp(currentMana + amount, 0, maxMana);
    }
  // // 0<currentMana<maxMana
    public float GetCurrentHealth()
    {
        return currentHealth;
    }
   // 현재 체력 반환
    public float GetCurrentMana()
    {
        return currentMana;
    }
  //  현재 마나 반환 (private 변수땜에)
    public float GetMaxMana()
    {
        return maxMana;
    }
    // 최대 마나 반환(private 변수 땜에)
}
