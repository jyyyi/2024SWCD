using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    public float maxHealth = 10000f; // 최대 체력
    public Transform target;
    public bool isChase;

    private float currentHealth;
    private CameraMgr mCamera;

    public GameObject damageEffect;
    public GameObject damageTextPrefab; 

    NavMeshAgent nav;
    Animator anim;

    void Start()
    {
        currentHealth = maxHealth; // 처음 시작 시 최대 체력으로 설정
    }

    void Awake()
    {
        nav = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();

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


        Invoke("ChaseStart", 2);
    }

    void ChaseStart()
    {
        isChase = true;
    }

    void Update()
    {
        if (isChase)
        {
            nav.SetDestination(target.position);
        }
    }

    // 데미지를 받는 메서드
    public void TakeDamage(float damage)
    {
        currentHealth -= damage; // 체력 감소
        //Debug.Log("Enemy took " + damage + " damage. Current Health: " + currentHealth);
        anim.SetTrigger("GetHit");


        // 데미지 이펙트 생성
        if (damageEffect != null)
        {
            Instantiate(damageEffect, transform.position, Quaternion.identity);
        }


        // 데미지 텍스트 생성

        if (damageTextPrefab != null)
        {
            GameObject damageTextObj = Instantiate(damageTextPrefab, transform.position, Quaternion.identity);

            Vector3 screenPosition = Camera.main.WorldToScreenPoint(transform.position);
            damageTextObj.transform.SetParent(GameObject.Find("Canvas").transform, false);
            damageTextObj.transform.position = screenPosition;

            damageTextObj.GetComponent<DamageText>().SetDamageText(damage.ToString());
        }





        // 데미지에 따른 카메라 흔들림
        if (mCamera != null)
        {
            // 흔들림 세기와 지속 시간 설정
            float shakeRange = Mathf.Clamp(damage / 500f, 0.02f, 0.1f); // 데미지에 따라 세기 설정
            float shakeDuration = Mathf.Clamp(damage / 50f, 0.1f, 1.0f); // 데미지에 따라 지속 시간 설정

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





    // 적이 죽었을 때 호출되는 메서드
    private void Die()
    {
        Debug.Log("Enemy died!");
        anim.SetTrigger("doDie");
        isChase = false;
        nav.enabled = false;

        // 일정 시간 후 적 오브젝트 삭제
        Destroy(gameObject, 3f);
    }
}