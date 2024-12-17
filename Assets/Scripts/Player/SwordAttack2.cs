using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordAttack2 : MonoBehaviour
{
    Animator animator;
    BoxCollider swordCollider;

    // Ÿ�� ���� ���� ������Ʈ
    public Transform attackOrigin;

    private bool isAttacking = false;
    private float damageMultiplier = 1.0f; // ���� ����

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
            animator.SetTrigger("fswing1Trigger");
            damageMultiplier = 1.0f; // fswing1�� ������ 2��
            StartAttack();
        }

        if (Input.GetKeyDown(KeyCode.G))
        {
            animator.SetTrigger("fswing2Trigger");
            damageMultiplier = 2.0f; // fswing2�� �⺻ ������
            StartAttack();
        }

        if (Input.GetKeyDown(KeyCode.H))
        {
            animator.SetTrigger("fswing3Trigger");
            damageMultiplier = 1.0f; // fswing3�� ������ 2��
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
            Vector3 weaponPosition = attackOrigin.position; // �� ������ ��ġ ���
            float distance = Vector3.Distance(weaponPosition, hitPosition);

            Debug.Log("Ÿ�� ��ġ: " + hitPosition);
            Debug.Log("weapon ���� ��ġ: " + weaponPosition);
            Debug.Log("Ÿ�� ������ weapon ���� ���� �Ÿ�: " + distance);

            float damage = CalculateDamage(distance) * damageMultiplier; // ���� ����
            Debug.Log("����� ������: " + damage);

            // ����� �޼��� ȣ�� - �浹 ��ġ ����
            StaticEnemy enemy = other.GetComponent<StaticEnemy>();
            if (enemy != null)
            {
                enemy.TakeDamage(damage, hitPosition); // �������� �浹 ��ġ ����
            }
        }
    }

    float CalculateDamage(float distance)
    {
        float baseDamage = 10f;
        float maxDistance = 0.3f; // �������� ���ҵǴ� �ִ� �Ÿ�
        float minDistance = 0.1f; // ũ��Ƽ�� �Ÿ�

        if (distance <= minDistance)
            return 30f; // ũ��Ƽ�� ������

        if (distance > maxDistance)
            return 0f; // ������ �ʰ��� ��� ������ 0

        return Mathf.Lerp(30f, 10f, (distance - minDistance) / (maxDistance - minDistance));
    }
}
