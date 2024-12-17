using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlantEnemy : MonoBehaviour
{
    public float maxHealth = 100f; // �ִ� ü��
    public GameObject damageEffect; // ������ ����Ʈ ������

    private float currentHealth;
    private Animator animator;
    private CameraMgr mCamera;


    void Start()
    {
        currentHealth = maxHealth; // ���� �� �ִ� ü������ ����
        animator = GetComponent<Animator>();
    }

    void Awake()
    {

        // CameraMgr �ʱ�ȭ
        if (Camera.main != null)
        {
            mCamera = Camera.main.GetComponent<CameraMgr>();
            if (mCamera == null)
            {
                Debug.LogError("CameraMgr ������Ʈ�� Main Camera���� ã�� �� �����ϴ�!");
            }
        }
        else
        {
            Debug.LogError("Main Camera�� �������� �ʾҽ��ϴ�!");
        }

    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage; // ü�� ����
        Debug.Log("Enemy took " + damage + " damage. Current Health: " + currentHealth);

        // ������ ����Ʈ ����
        if (damageEffect != null)
        {
            Instantiate(damageEffect, transform.position, Quaternion.identity);
        }

        // �������� ���� ī�޶� ��鸲
        if (mCamera != null)
        {
            // ��鸲 ����� ���� �ð� ����
            float shakeRange = Mathf.Clamp(damage / 100f, 0.02f, 0.1f); // �������� ���� ���� ����
            float shakeDuration = Mathf.Clamp(damage / 10f, 0.1f, 1.0f); // �������� ���� ���� �ð� ����

            mCamera.ShakeCamera(shakeRange, shakeDuration);
        }
        else
        {
            Debug.LogWarning("CameraMgr�� �ʱ�ȭ���� �ʾҽ��ϴ�.");
        }

        if (currentHealth <= 0)
        {
            Die(); // ü���� 0 ������ �� ���� ó��
        }
    }

    private void Die()
    {
        Debug.Log("���� ����߽��ϴ�.");
        //animator.SetTrigger("doDie"); // �״� �ִϸ��̼� Ʈ����
        Destroy(gameObject, 2f); // 2�� �� ������Ʈ ����
    }
}
