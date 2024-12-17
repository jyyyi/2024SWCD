using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.LegacyInputHelpers;

public class SwordAttack : MonoBehaviour
{
    Animator animator;
    BoxCollider swordCollider;

    // 타격 지점 기준 오브젝트
    public Transform attackOrigin;

    private bool isAttacking = false;

    void Start()
    {
        animator = GetComponent<Animator>();
        swordCollider = GetComponent<BoxCollider>();
        swordCollider.isTrigger = true;
        swordCollider.enabled = false; // 초기에는 비활성화
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            animator.SetTrigger("swing1Trigger");
            StartAttack();
        }

        if (Input.GetKeyDown(KeyCode.G))
        {
            animator.SetTrigger("swing2Trigger");
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
        swordCollider.enabled = true;
        Invoke("EndAttack", 0.1f); // 0.5초 후 공격 종료
    }

    private void EndAttack()
    {
        isAttacking = false;
        swordCollider.enabled = false;
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

            other.GetComponent<Enemy>().TakeDamage(damage); // 필요시 데미지 전달
        }
    }

    float CalculateDamage(float distance)
    {
        float baseDamage = 30f;
        float maxDistance = 0.3f; // 데미지가 감소되는 최대 거리
        float minDistance = 0.1f; // 크리티컬 거리

        if (distance <= minDistance)
            return 50f; // 크리티컬 데미지

        if (distance > maxDistance)
            return 0f; // 범위를 초과한 경우 데미지 0

        return Mathf.Lerp(50f, 30f, (distance - minDistance) / (maxDistance - minDistance));
    }

}