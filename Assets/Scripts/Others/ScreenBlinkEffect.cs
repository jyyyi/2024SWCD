using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScreenBlinkEffect : MonoBehaviour
{
    public Image blackScreen;         // 검은 화면 이미지
    public float blinkDuration = 0.5f; // 깜빡임 지속 시간
    public bool use255Range = true;    // 255 범위 사용 여부

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

        // 페이드 인 (화면이 검게 변함)
        float alpha = 0f;
        while (alpha < 1f)
        {
            alpha += Time.deltaTime / (blinkDuration / 2);
            SetAlpha(alpha);
            yield return null;
        }

        // 페이드 아웃 (화면이 투명해짐)
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
                alpha = Mathf.Clamp(alpha * 255, 0, 255); // 0~1 값을 0~255로 변환
            }
            else
            {
                alpha = Mathf.Clamp(alpha, 0, 1); // 0~1 범위 유지
            }

            Color color = blackScreen.color;

            if (use255Range)
            {
                color.a = alpha / 255f; // 255 범위를 0~1로 변환
            }
            else
            {
                color.a = alpha; // 그대로 적용
            }

            blackScreen.color = color;
        }
    }
}
