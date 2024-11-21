using UnityEngine;

using System.Collections;

public class VineBall : Ball
{
    [Header("���� ������")]
    [SerializeField] private GameObject vinePrefab;

    [Header("���� ���� �ֱ�")]
    [SerializeField] private float vineInterval = 0.1f;

    private bool isVine = false;

    protected override void Update()
    {
        base.Update();
    }

    private void StartVine()
    {
        if (!isVine)
        {
            isVine = true;
            StartCoroutine(VineCoroutine());
        }
    }

    private void StopVine()
    {
        isVine = false;
    }

    IEnumerator VineCoroutine()
    {
        while (isVine)
        {
            Instantiate(vinePrefab, transform.position, Quaternion.identity);

            yield return new WaitForSeconds(vineInterval);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Precipice")) StartVine();
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Precipice")) StopVine();

    }
}
