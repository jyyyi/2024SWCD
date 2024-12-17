using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlantEnemy : MonoBehaviour
{
    public float maxHealth = 100f; // 최대 체력
    public GameObject damageEffect; // 데미지 이펙트 프리팹

    private float currentHealth;
    private Animator animator;
    private CameraMgr mCamera;


    void Start()
    {
        currentHealth = maxHealth; // 시작 시 최대 체력으로 설정
        animator = GetComponent<Animator>();
    }

    void Awake()
    {

        // CameraMgr 초기화
        if (Camera.main != null)
        {
            mCamera = Camera.main.GetComponent<CameraMgr>();
            if (mCamera == null)
            {
                Debug.LogError("CameraMgr 컴포넌트를 Main Camera에서 찾을 수 없습니다!");
            }
        }
        else
        {
            Debug.LogError("Main Camera가 설정되지 않았습니다!");
        }

    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage; // 체력 감소
        Debug.Log("Enemy took " + damage + " damage. Current Health: " + currentHealth);

        // 데미지 이펙트 생성
        if (damageEffect != null)
        {
            Instantiate(damageEffect, transform.position, Quaternion.identity);
        }

        // 데미지에 따른 카메라 흔들림
        if (mCamera != null)
        {
            // 흔들림 세기와 지속 시간 설정
            float shakeRange = Mathf.Clamp(damage / 100f, 0.02f, 0.1f); // 데미지에 따라 세기 설정
            float shakeDuration = Mathf.Clamp(damage / 10f, 0.1f, 1.0f); // 데미지에 따라 지속 시간 설정

            mCamera.ShakeCamera(shakeRange, shakeDuration);
        }
        else
        {
            Debug.LogWarning("CameraMgr이 초기화되지 않았습니다.");
        }

        if (currentHealth <= 0)
        {
            Die(); // 체력이 0 이하일 때 죽음 처리
        }
    }

    private void Die()
    {
        Debug.Log("적이 사망했습니다.");
        //animator.SetTrigger("doDie"); // 죽는 애니메이션 트리거
        Destroy(gameObject, 2f); // 2초 후 오브젝트 삭제
    }
}
