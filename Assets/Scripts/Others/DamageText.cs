using UnityEngine;
using TMPro; // TextMeshPro 네임스페이스 추가

public class DamageText : MonoBehaviour
{ 
    public float moveSpeed = 2f;   // 텍스트가 위로 올라가는 속도
    public float duration = 1f;   // 텍스트가 사라지는 시간

    private TextMeshProUGUI damageText; 

    void Awake()
    {
        damageText = GetComponent<TextMeshProUGUI>();
    }

    public void SetDamageText(string damageValue)
    {
        damageText.text = damageValue; // 데미지 값 설정
    }

    void Update()
    {
        transform.Translate(Vector3.up * moveSpeed * Time.deltaTime); // 텍스트를 위로 이동
        Destroy(gameObject, duration); // 일정 시간이 지나면 오브젝트 삭제
    }
}
