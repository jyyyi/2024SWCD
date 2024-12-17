using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StaticEnemy : MonoBehaviour
{
    public float maxHealth = 100f; // �ִ� ü��

    private float currentHealth;
    private CameraMgr mCamera;

    public GameObject damageEffect;
    public GameObject damageTextPrefab;

    void Start()
    {
        currentHealth = maxHealth; // ó�� ���� �� �ִ� ü������ ����
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

    // �������� �޴� �޼���
    public void TakeDamage(float damage, Vector3 hitPosition)
    {
        currentHealth -= damage; // ü�� ����
        Debug.Log("Enemy took " + damage + " damage. Current Health: " + currentHealth);

        // ������ ����Ʈ ����
        if (damageEffect != null)
        {
            Instantiate(damageEffect, hitPosition, Quaternion.identity);
        }

        // ������ �ؽ�Ʈ ����
        if (damageTextPrefab != null)
        {
            GameObject damageTextObj = Instantiate(damageTextPrefab, transform.position, Quaternion.identity);

            Vector3 screenPosition = Camera.main.WorldToScreenPoint(hitPosition);
            damageTextObj.transform.SetParent(GameObject.Find("Canvas").transform, false);
            damageTextObj.transform.position = screenPosition;

            damageTextObj.GetComponent<DamageText>().SetDamageText(damage.ToString());
        }

        // �������� ���� ī�޶� ��鸲
        if (mCamera != null)
        {
            // ��鸲 ����� ���� �ð� ����
            float shakeRange = Mathf.Clamp(damage / 500f, 0.02f, 0.1f); // �������� ���� ���� ����
            float shakeDuration = Mathf.Clamp(damage / 50f, 0.1f, 1.0f); // �������� ���� ���� �ð� ����

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

    // ���� �׾��� �� ȣ��Ǵ� �޼���
    private void Die()
    {
        Debug.Log("Enemy died!");

        // ���� �ð� �� �� ������Ʈ ����
        Destroy(gameObject, 3f);
    }
}
