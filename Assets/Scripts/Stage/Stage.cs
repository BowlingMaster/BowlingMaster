using UnityEngine;

public class Stage : MonoBehaviour
{
    [Header("�������� �� �� ���� ���� Ƚ�� (���� ����)")]
    [SerializeField] private int[] selectableCount;

    [Header("�������� �� ���� ���� �� �ִ� Ƚ�� (���� ����)")]
    [SerializeField] private int rollCount;

    [Header("�� ���� �˾�")]
    [SerializeField] private GameObject ballSelectPopUp;

    private void Start()
    {
        Instantiate(ballSelectPopUp);
    }

    public int GetSelectableCount(int _index)
    {
        return selectableCount[_index];
    }

    public int GetRollCount()
    {
        return rollCount;
    }
}
