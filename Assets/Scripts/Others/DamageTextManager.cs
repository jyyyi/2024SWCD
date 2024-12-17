using UnityEngine;

public class DamageTextManager : MonoBehaviour
{
    public float lifeTime = 4.0f; // 텍스트 유지 시간
    private float timer = 0f;

    void Update()
    {
        // lifeTime이 지나면 오브젝트 삭제
        timer += Time.deltaTime;
        if (timer >= lifeTime)
        {
            Destroy(gameObject);
        }
    }
}
