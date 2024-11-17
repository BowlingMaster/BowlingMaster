//System
using System.Collections.Generic;

//Unity
using UnityEngine;
using UnityEngine.UI;

//TMPro
using TMPro;

public class BallSelectPopup : MonoBehaviour
{
    [Header("���� �߰��� �� �ִ� ���� ������ �����ֱ� ���� TMP �迭")]
    [SerializeField] private Text[] ballCountTexts;

    [Header("�� �߰� ��ư �迭")]
    [SerializeField] private Button[] addBallButtons;

    [Header("��� ��ư")]
    [SerializeField] private Button deleteButton;

    [Header("Ȯ�� ��ư")]
    [SerializeField] private Button confirmButton;

    [Header("���� ���� ������ ���� ������ �����ֱ� ���� TMP")]
    [SerializeField] private TextMeshProUGUI remainCount;

    [Header("������ ���� �����ֱ� ���� �̹��� �迭")]
    [SerializeField] private Image[] ballImages;

    private int imageIndex;

    [Header("������ ��������Ʈ �迭")]
    [SerializeField] private Sprite[] ballSprites;

    private Stack<int> indexStack;

    private DeckController deckController;

    private void OnEnable()
    {
        deckController = FindObjectOfType<DeckController>();

        confirmButton.interactable = false;

        indexStack = new Stack<int>();

        for(int i = 0; i < ballImages.Length; i++)
        {
            ballImages[i].gameObject.SetActive(false);
        }

        imageIndex = -1;

        UpdateCountTMPs();
        UpdateRemainCount();
    }

    /// <summary>
    /// Update All Count Text Mesh Pro
    /// </summary>
    private void UpdateCountTMPs()
    {
        for (int i = 0; i < ballCountTexts.Length; i++)
        {
            ballCountTexts[i].text = deckController.GetCurrentBallCount(i).ToString() + "�� ����";
        }
    }

    private void UpdateRemainCount()
    {
        if (!deckController.CheckMaxSelect())
            remainCount.text = "���� ���� ���� ���� : " + deckController.GetCurrentMaxSelectableCount().ToString();
        else remainCount.text = "��� ���� �����Ͽ����ϴ�.";
    }

    /// <summary>
    /// Add ball
    /// </summary>
    /// <param name="_index"></param>
    public void AddBall(int _index)
    {
        if (deckController.CheckMaxSelect() || deckController.CheckBallMax(_index)) return;

        bool isMax = deckController.AddBall(_index);

        imageIndex++;
        ballImages[imageIndex].gameObject.SetActive(true);
        ballImages[imageIndex].sprite = ballSprites[_index];

        UpdateCountTMPs();
        UpdateRemainCount();

        indexStack.Push(_index);

        if (isMax)
        {
            confirmButton.interactable = true;
            DeactiveAddButtons();
        }
    }

    /// <summary>
    /// Delete Ball
    /// </summary>
    public void DeleteBall()
    {
        if (indexStack.Count == 0) return;

        int index = indexStack.Pop();

        deckController.DeleteBall(index);


        ballImages[imageIndex].gameObject.SetActive(false);
        imageIndex--;

        UpdateCountTMPs();
        UpdateRemainCount();

        ActiveAddButtons();

        confirmButton.interactable = false;
    }

    /// <summary>
    /// Start Game : Generate Ball and Close Popup
    /// </summary>
    public void StartGame()
    {
        //if not enough select count, return method
        if (!deckController.CheckMaxSelect()) return;

        LaneController laneController = FindObjectOfType<LaneController>();

        laneController.GenerateBall();

        Destroy(gameObject);
    }

    private void ActiveAddButtons()
    {
        for (int i = 0; i < addBallButtons.Length; i++)
        {
            addBallButtons[i].interactable = true;
        }
    }

    private void DeactiveAddButtons()
    {
        for (int i = 0; i < addBallButtons.Length; i++)
        {
            addBallButtons[i].interactable = false;
        }
    }
}
