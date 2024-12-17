using UnityEngine;

public class PlantEffect : MonoBehaviour
{
    public GameObject player; // Player ������Ʈ
    public ScreenBlinkEffect screenBlinkEffect; // ������ ȿ�� ��ũ��Ʈ

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == player)
        {
            // ������ ȿ�� ����
            if (screenBlinkEffect != null)
            {
                screenBlinkEffect.TriggerBlink();
            }
        }
    }
}
