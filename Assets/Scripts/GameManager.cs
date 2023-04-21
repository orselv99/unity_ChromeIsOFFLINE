using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
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

            if (this.highScore < score)
            {
                this.highScore++;
                scores[0].text = score.ToString("HI 00000.##");
            }
            else
            {
                scores[0].text = this.highScore.ToString("HI 00000.##");
            }

            scores[1].text = score.ToString("00000.##");
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
        this.isGameOver = false;
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

        // ���� clone ����
        var clones = GameObject.FindGameObjectsWithTag("Clone");
        foreach (var clone in clones)
        {
            DestroyImmediate(clone);
        }

        // �÷��̾� ������ġ
        var player = GameObject.FindGameObjectWithTag("Player");
        Debug.Log(player.name);
        player.transform.position = new Vector2(-5f, -2.5f);

        // ���ھ� �ʱ�ȭ
        var scores = this.uis[2].GetComponentsInChildren<Text>();
        scores[1].text = 0.ToString("00000.##");

        StartCoroutine(StartTimer());
    }
}
