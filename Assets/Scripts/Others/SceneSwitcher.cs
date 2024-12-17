using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class SceneSwitcher : MonoBehaviour
{
    public float delayBeforeSwitch = 2f; // �� ��ȯ ���� ����� �ð� (��)
    public string targetSceneName = "Snow_Setting"; // ��ȯ�� �� �̸�

    private bool isSwitching = false; // �� ��ȯ �ߺ� ����

    void Update()
    {
        // Q Ű �Է��� ����
        if (Input.GetKeyDown(KeyCode.Q) && !isSwitching)
        {
            StartCoroutine(SwitchSceneWithDelay());
        }
    }

    IEnumerator SwitchSceneWithDelay()
    {
        isSwitching = true; // �� ��ȯ �ߺ� ����
        yield return new WaitForSeconds(delayBeforeSwitch); // ������ �ð���ŭ ���
        SceneManager.LoadScene(targetSceneName); // �� ��ȯ
    }
}
