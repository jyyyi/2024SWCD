using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class SceneSwitcher : MonoBehaviour
{
    public float delayBeforeSwitch = 2f; // 씬 전환 전에 대기할 시간 (초)
    public string targetSceneName = "Snow_Setting"; // 전환할 씬 이름

    private bool isSwitching = false; // 씬 전환 중복 방지

    void Update()
    {
        // Q 키 입력을 감지
        if (Input.GetKeyDown(KeyCode.Q) && !isSwitching)
        {
            StartCoroutine(SwitchSceneWithDelay());
        }
    }

    IEnumerator SwitchSceneWithDelay()
    {
        isSwitching = true; // 씬 전환 중복 방지
        yield return new WaitForSeconds(delayBeforeSwitch); // 설정된 시간만큼 대기
        SceneManager.LoadScene(targetSceneName); // 씬 전환
    }
}
