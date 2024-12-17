using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class SceneSwitcher2 : MonoBehaviour
{
    public float delayBeforeSwitch = 2f; // 씬 전환 전에 대기할 시간 (초)
    public string targetSceneName = "End_Setting"; // 전환할 씬 이름
    public int requiredPressCount = 2; // T 키를 눌러야 하는 횟수

    private int pressCount = 0; // T 키를 누른 횟수
    private bool isSwitching = false; // 씬 전환 중복 방지

    void Update()
    {
        // T 키 입력을 감지
        if (Input.GetKeyDown(KeyCode.T) && !isSwitching)
        {
            pressCount++; // T 키 누른 횟수 증가

            // 지정된 횟수만큼 눌렀다면 씬 전환 실행
            if (pressCount >= requiredPressCount)
            {
                StartCoroutine(SwitchSceneWithDelay());
            }
        }
    }

    IEnumerator SwitchSceneWithDelay()
    {
        isSwitching = true; // 씬 전환 중복 방지
        yield return new WaitForSeconds(delayBeforeSwitch); // 설정된 시간만큼 대기
        SceneManager.LoadScene(targetSceneName); // 씬 전환
    }
}
