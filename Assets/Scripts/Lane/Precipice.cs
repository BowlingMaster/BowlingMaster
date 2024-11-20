using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Precipice : MonoBehaviour
{
    [Header("ũ�Ⱑ �����ϴ� �� �ɸ��� �ð�")]
    [SerializeField] private float shrinkDuration = 2f;

    [Header("�̵� �ӵ� ���� ����")]
    [SerializeField] private float decreaseSpeedRate = 0.05f;

    [Header("���� ���Ḧ �����ϴ� FinishLine ������Ʈ")]
    [SerializeField] private FinishLine finishLine;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Vine Ball") || collision.isTrigger) return;

        Rigidbody2D rigid = collision.GetComponent<Rigidbody2D>();

        if (rigid != null)
        {
            rigid.velocity *= decreaseSpeedRate;
        }

        StartCoroutine(Shrink(collision));
    }

    IEnumerator Shrink(Collider2D collision)
    {
        Vector3 originalScale = collision.transform.localScale;
        float currentTime = 0f;

        while (currentTime < shrinkDuration)
        {
            // �ð��� ���� ũ�� ����
            collision.transform.localScale = Vector3.Lerp(originalScale, Vector3.zero, currentTime / shrinkDuration);
            currentTime += Time.deltaTime;
            yield return null; 
        }

        // ũ�⸦ ������ 0���� ����
        collision.transform.localScale = Vector3.zero;

        if (collision.CompareTag("Ball")) finishLine.DeadBall(collision);
    }
}
