using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordAttack1 : MonoBehaviour
{
    Animator animator; // 애니메이터 컴포넌트
    BoxCollider swordCollider; // 검의 충돌 영역

    public Transform attackOrigin; // 타격 지점 기준 오브젝트
    private bool isAttacking = false;

    void Start()
    {
        animator = GetComponent<Animator>();
        swordCollider = GetComponent<BoxCollider>();
        swordCollider.isTrigger = true; // 트리거 설정
        swordCollider.enabled = false; // 초기에는 비활성화

        // attackOrigin이 설정되지 않았으면 기본값으로 현재 Transform 사용
        if (attackOrigin == null)
        {
            attackOrigin = transform;
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            animator.SetTrigger("swing1Trigger"); // 애니메이션 트리거 설정
            StartAttack();
        }

        if (Input.GetKeyDown(KeyCode.G))
        {
            animator.SetTrigger("swing2Trigger"); // 애니메이션 트리거 설정
            StartAttack();
        }

        if (Input.GetKeyDown(KeyCode.H))
        {
            animator.SetTrigger("swing3Trigger");
            StartAttack();
        }
    }

    private void StartAttack()
    {
        isAttacking = true;
        swordCollider.enabled = true; // 공격 활성화
        Invoke("EndAttack", 0.3f); // 0.3초 후 공격 종료
    }

    private void EndAttack()
    {
        isAttacking = false;
        swordCollider.enabled = false; // 공격 비활성화
    }

    private void OnTriggerEnter(Collider other)
    {
        if (isAttacking && other.CompareTag("Enemy"))
        {
            Vector3 hitPosition = other.ClosestPoint(transform.position);
            Vector3 weaponPosition = attackOrigin.position;  // 새 기준점 위치 사용
            float distance = Vector3.Distance(weaponPosition, hitPosition);

            Debug.Log("타격 위치: " + hitPosition);
            Debug.Log("weapon 기준 위치: " + weaponPosition);
            Debug.Log("타격 지점과 weapon 기준 간의 거리: " + distance);

            float damage = CalculateDamage(distance);
            Debug.Log("적용된 데미지: " + damage);

            other.GetComponent<PlantEnemy>().TakeDamage(damage); // 필요시 데미지 전달
        }
    }

    private float CalculateDamage(float distance)
    {
        float baseDamage = 1f;
        float criticalDistance = 0.1f;
        float criticalDamage = 10f;

        return distance < criticalDistance ? criticalDamage : baseDamage;
    }
}
