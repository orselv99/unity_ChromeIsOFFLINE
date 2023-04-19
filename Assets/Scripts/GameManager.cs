using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;
    
    // 0: ����ȭ��, 1: ����Ÿ�̸�, 2: ���ھ�, 3: ���ӿ��� & �����
    [SerializeField]
    private GameObject[] uis = null;

    [SerializeField]
    private GameObject cloud = null;

    private int highScore = 0;
    public bool isGameStart = false;
    public bool isGameOver = false;

    private void Awake()
    {
        instance = this;
        DontDestroyOnLoad(this);
    }

    private void Update()
    {
        if (this.isGameStart == true)
        {

        }
    }

    private void LateUpdate()
    {
        if (this.isGameStart == true)
        {

        }
    }

    // ui �̺�Ʈ
    IEnumerator StartGame()
    {
        // ���ھ� ��� (0: high score, 1: current score)
        var scores = this.uis[2].GetComponentsInChildren<Text>();
        int score = 0;
        while (this.isGameOver == false)
        {
            yield return new WaitForSeconds(1f);

            score++;
            scores[1].text = score.ToString("00000.##");

            if (this.highScore < score)
            {
                scores[0].text = score.ToString("HI 00000.##");
            }
        }

        this.uis[3].SetActive(true);   // ���ӿ��� & ����� ǥ��
    }
    IEnumerator StartTimer()
    {
        for (int i = 1; i <= 3; i++)
        {
            yield return new WaitForSeconds(1f);
            var text = this.uis[1].GetComponentInChildren<Text>();

            if (i < 3)
            {
                text.text = (3 - i).ToString();
            }
            else
            {
                text.text = "START!!";
            }
        }

        // ���� ����
        this.isGameStart = true;
        StartCoroutine(StartGame());

        // Ÿ�̸Ӱ� ������ ����
        yield return new WaitForSeconds(1f);
        this.uis[1].SetActive(false);
    }
    public void OnClickPlayButton()
    { 
        this.uis[0].SetActive(false);   // ����ȭ�� ����
        this.uis[3].SetActive(false);   // ���ӿ��� & ����� ����

        this.uis[1].SetActive(true);    // Ÿ�̸� ���̱�
        this.uis[2].SetActive(true);    // ���ھ� ���̱�

        StartCoroutine(StartTimer());
    }
    public void OnClickRetryButton()
    {
        this.uis[3].SetActive(false);   // ���ӿ��� & ����� ����
        
        this.uis[1].SetActive(true);    // Ÿ�̸� ���̱�
        this.uis[2].SetActive(true);    // ���ھ� ���̱�

        StartCoroutine(StartTimer());
    }
}
