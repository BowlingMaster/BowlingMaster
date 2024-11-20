using UnityEngine;

public class SplitBall : Ball
{
    [Header("�п� ���� �ӵ�")]
    [SerializeField] private float splitSpeed = 200f;

    [Header("�������� ��ȯ�Ǵ� �θ���ġ �迭")]
    [SerializeField] private Transform[] parents;

    [Header("��ų ���� ���� ����")]
    [SerializeField] private float decreaseDegree = 0.5f;

    private Vector3 currentPosition;

    private SpriteRenderer spriteRenderer;

    protected override void Update()
    {
        base.Update();
        Split();
    }

    void Split()
    {
        if (!DetectSkill()) return;

        canSkill = false;

        currentPosition = transform.position;

        col.isTrigger = true;

        rb.velocity *= decreaseDegree;

        for (int i = 0; i < parents.Length; i++)
        {
            GameObject newBall = Instantiate(GetSplitBall());

            newBall.transform.SetParent(parents[i]);
            newBall.transform.localPosition = Vector3.zero;

            Vector2 directionVector = parents[i].position - currentPosition;

            Rigidbody2D newBallRigid = newBall.GetComponent<Rigidbody2D>();

            if (newBallRigid != null)
                newBallRigid.AddForce(directionVector * splitSpeed);

        }

        HideBall();
    }

    private GameObject GetSplitBall()
    {
        string path = "Skill/Split Ball";

        GameObject splitBall = Resources.Load<GameObject>(path);

        return splitBall;
    }

    private void HideBall()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.color = new Color(1, 1, 1, 0);
    }
}


