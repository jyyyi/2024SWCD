using UnityEngine;

public class PlantEffect : MonoBehaviour
{
    public GameObject player; // Player 오브젝트
    public ScreenBlinkEffect screenBlinkEffect; // 깜빡임 효과 스크립트

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == player)
        {
            // 깜빡임 효과 실행
            if (screenBlinkEffect != null)
            {
                screenBlinkEffect.TriggerBlink();
            }
        }
    }
}
