using UnityEngine;

public class BombBall : Ball
{
    [Header("���� �ݰ�")]
    [SerializeField] private float explosionRadius = 5f;

    [Header("���߷�")]
    [SerializeField] private float explosionPower = 10f;

    protected override void Update()
    {
        base.Update();

        Bomb(transform.position);
    }

    void Bomb(Vector2 explosinoPosition)
    {
        if (!DetectSkill()) return;

        canSkill = false;

        Collider2D[] colliders = Physics2D.OverlapCircleAll(explosinoPosition, explosionRadius);

        foreach (Collider2D collider in colliders)
        {
            if (collider.gameObject == this.gameObject)
            {
                collider.isTrigger = true;
                continue;   //������ ������ ���� ����
            }

            Rigidbody2D rigid = collider.GetComponent<Rigidbody2D>();

            if (rigid != null)
            {
                Vector2 direction = rigid.position - explosinoPosition; //���� �߽ɿ����� ����

                float distance = direction.magnitude;           //���� �߽ɿ����� ���� ��Į��
                float force = explosionPower / (distance + 1); //�Ÿ��� ���� ���߷� ����
                rigid.AddForce(direction.normalized * force, ForceMode2D.Impulse);

                //���� �浹�� �ƴ� ���߷� ���� �������� �� Fall Down ó��

                Pin pin = collider.GetComponent<Pin>();

                if (pin != null)
                {
                    pin.FallDown();
                }
            }
        }

        HideBall();
    }
}
