using UnityEngine;
using System;

public class Score : MonoBehaviour
{
    private Stage stage;
    private PinChecker pinChecker;

    private int currentScore;

    [Header("�� ����")]
    [SerializeField] private int pinScore = 100;

    [Header("�ټ� �� �߰� ���� ����")]
    [SerializeField] private double scoreRate = 1.0f;

    private int lastPinCount = 0;

    private int starNum = -1;

    private double scoreDecreaseRate;

    private void Start()
    {
        stage = FindObjectOfType<Stage>();
        pinChecker = FindObjectOfType<PinChecker>();

        lastPinCount = stage.GetStartPinCount();

        scoreRate = 1.0f;

        starNum = -1;

        double value = 1.0 / stage.GetRollCount();

        scoreDecreaseRate = Math.Round(value, 1);
    }

    public void UpdateScore()
    {
        // �̹� ���忡 ģ �� ���� ��������
        int pinCount = lastPinCount - pinChecker.GetPinCount();

        // ���� ���
        currentScore += (int)(pinScore * pinCount * scoreRate);

        // Update
        lastPinCount = stage.GetStartPinCount() - pinCount;

        scoreRate -= scoreDecreaseRate;

        for (int i = 0; i < stage.GetStarCount(); i++)
        {
            Debug.Log("���� ������Ʈ");
            if (stage.GetStarScore(i) <= currentScore)
            {
                Debug.Log(currentScore + "�� " + stage.GetStarScore(i) + "���� ŭ");
                starNum = i;
            }
        }
    }

    public int GetCurrentScore()
    {
        return currentScore;
    }

    public int GetStarNum()
    {
        return starNum;
    }
    
    public bool IsClear()
    {
        if (starNum == -1) return false;
        else return true;
    }

    public int GetPinScore()
    {
        return pinScore;
    }
}
