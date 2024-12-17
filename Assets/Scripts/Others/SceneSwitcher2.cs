using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class SceneSwitcher2 : MonoBehaviour
{
    public float delayBeforeSwitch = 2f; // �� ��ȯ ���� ����� �ð� (��)
    public string targetSceneName = "End_Setting"; // ��ȯ�� �� �̸�
    public int requiredPressCount = 2; // T Ű�� ������ �ϴ� Ƚ��

    private int pressCount = 0; // T Ű�� ���� Ƚ��
    private bool isSwitching = false; // �� ��ȯ �ߺ� ����

    void Update()
    {
        // T Ű �Է��� ����
        if (Input.GetKeyDown(KeyCode.T) && !isSwitching)
        {
            pressCount++; // T Ű ���� Ƚ�� ����

            // ������ Ƚ����ŭ �����ٸ� �� ��ȯ ����
            if (pressCount >= requiredPressCount)
            {
                StartCoroutine(SwitchSceneWithDelay());
            }
        }
    }

    IEnumerator SwitchSceneWithDelay()
    {
        isSwitching = true; // �� ��ȯ �ߺ� ����
        yield return new WaitForSeconds(delayBeforeSwitch); // ������ �ð���ŭ ���
        SceneManager.LoadScene(targetSceneName); // �� ��ȯ
    }
}
