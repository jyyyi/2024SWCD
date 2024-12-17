using UnityEngine;
using TMPro; // TextMeshPro ���ӽ����̽� �߰�

public class DamageText : MonoBehaviour
{ 
    public float moveSpeed = 2f;   // �ؽ�Ʈ�� ���� �ö󰡴� �ӵ�
    public float duration = 1f;   // �ؽ�Ʈ�� ������� �ð�

    private TextMeshProUGUI damageText; 

    void Awake()
    {
        damageText = GetComponent<TextMeshProUGUI>();
    }

    public void SetDamageText(string damageValue)
    {
        damageText.text = damageValue; // ������ �� ����
    }

    void Update()
    {
        transform.Translate(Vector3.up * moveSpeed * Time.deltaTime); // �ؽ�Ʈ�� ���� �̵�
        Destroy(gameObject, duration); // ���� �ð��� ������ ������Ʈ ����
    }
}
