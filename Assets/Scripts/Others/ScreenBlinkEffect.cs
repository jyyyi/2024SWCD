using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScreenBlinkEffect : MonoBehaviour
{
    public Image blackScreen;         // ���� ȭ�� �̹���
    public float blinkDuration = 0.5f; // ������ ���� �ð�
    public bool use255Range = true;    // 255 ���� ��� ����

    private bool isBlinking = false;

    public void TriggerBlink()
    {
        if (!isBlinking)
        {
            StartCoroutine(BlinkEffect());
        }
    }

    private IEnumerator BlinkEffect()
    {
        isBlinking = true;

        // ���̵� �� (ȭ���� �˰� ����)
        float alpha = 0f;
        while (alpha < 1f)
        {
            alpha += Time.deltaTime / (blinkDuration / 2);
            SetAlpha(alpha);
            yield return null;
        }

        // ���̵� �ƿ� (ȭ���� ��������)
        while (alpha > 0f)
        {
            alpha -= Time.deltaTime / (blinkDuration / 2);
            SetAlpha(alpha);
            yield return null;
        }

        isBlinking = false;
    }

    private void SetAlpha(float alpha)
    {
        if (blackScreen != null)
        {
            if (use255Range)
            {
                alpha = Mathf.Clamp(alpha * 255, 0, 255); // 0~1 ���� 0~255�� ��ȯ
            }
            else
            {
                alpha = Mathf.Clamp(alpha, 0, 1); // 0~1 ���� ����
            }

            Color color = blackScreen.color;

            if (use255Range)
            {
                color.a = alpha / 255f; // 255 ������ 0~1�� ��ȯ
            }
            else
            {
                color.a = alpha; // �״�� ����
            }

            blackScreen.color = color;
        }
    }
}
