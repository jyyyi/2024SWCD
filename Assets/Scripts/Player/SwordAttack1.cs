using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordAttack1 : MonoBehaviour
{
    Animator animator; // �ִϸ����� ������Ʈ
    BoxCollider swordCollider; // ���� �浹 ����

    public Transform attackOrigin; // Ÿ�� ���� ���� ������Ʈ
    private bool isAttacking = false;

    void Start()
    {
        animator = GetComponent<Animator>();
        swordCollider = GetComponent<BoxCollider>();
        swordCollider.isTrigger = true; // Ʈ���� ����
        swordCollider.enabled = false; // �ʱ⿡�� ��Ȱ��ȭ

        // attackOrigin�� �������� �ʾ����� �⺻������ ���� Transform ���
        if (attackOrigin == null)
        {
            attackOrigin = transform;
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            animator.SetTrigger("swing1Trigger"); // �ִϸ��̼� Ʈ���� ����
            StartAttack();
        }

        if (Input.GetKeyDown(KeyCode.G))
        {
            animator.SetTrigger("swing2Trigger"); // �ִϸ��̼� Ʈ���� ����
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
        swordCollider.enabled = true; // ���� Ȱ��ȭ
        Invoke("EndAttack", 0.3f); // 0.3�� �� ���� ����
    }

    private void EndAttack()
    {
        isAttacking = false;
        swordCollider.enabled = false; // ���� ��Ȱ��ȭ
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

            other.GetComponent<PlantEnemy>().TakeDamage(damage); // �ʿ�� ������ ����
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
