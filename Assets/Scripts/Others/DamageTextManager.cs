using UnityEngine;

public class DamageTextManager : MonoBehaviour
{
    public float lifeTime = 4.0f; // �ؽ�Ʈ ���� �ð�
    private float timer = 0f;

    void Update()
    {
        // lifeTime�� ������ ������Ʈ ����
        timer += Time.deltaTime;
        if (timer >= lifeTime)
        {
            Destroy(gameObject);
        }
    }
}
