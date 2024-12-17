using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.LegacyInputHelpers;

public class SwordAttack : MonoBehaviour
{
    Animator animator;
    BoxCollider swordCollider;

    // Ÿ�� ���� ���� ������Ʈ
    public Transform attackOrigin;

    private bool isAttacking = false;

    void Start()
    {
        animator = GetComponent<Animator>();
        swordCollider = GetComponent<BoxCollider>();
        swordCollider.isTrigger = true;
        swordCollider.enabled = false; // �ʱ⿡�� ��Ȱ��ȭ
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
        Invoke("EndAttack", 0.1f); // 0.5�� �� ���� ����
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
            Vector3 weaponPosition = attackOrigin.position;  // �� ������ ��ġ ���
            float distance = Vector3.Distance(weaponPosition, hitPosition);

            Debug.Log("Ÿ�� ��ġ: " + hitPosition);
            Debug.Log("weapon ���� ��ġ: " + weaponPosition);
            Debug.Log("Ÿ�� ������ weapon ���� ���� �Ÿ�: " + distance);

            float damage = CalculateDamage(distance);
            Debug.Log("����� ������: " + damage);

            other.GetComponent<Enemy>().TakeDamage(damage); // �ʿ�� ������ ����
        }
    }

    float CalculateDamage(float distance)
    {
        float baseDamage = 30f;
        float maxDistance = 0.3f; // �������� ���ҵǴ� �ִ� �Ÿ�
        float minDistance = 0.1f; // ũ��Ƽ�� �Ÿ�

        if (distance <= minDistance)
            return 50f; // ũ��Ƽ�� ������

        if (distance > maxDistance)
            return 0f; // ������ �ʰ��� ��� ������ 0

        return Mathf.Lerp(50f, 30f, (distance - minDistance) / (maxDistance - minDistance));
    }

}