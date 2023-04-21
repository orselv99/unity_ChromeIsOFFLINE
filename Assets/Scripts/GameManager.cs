using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;
    
    // 0: 시작화면, 1: 시작타이머, 2: 스코어, 3: 게임오버 & 재시작
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

    // ui 이벤트
    IEnumerator StartGame()
    {
        // 스코어 계산 (0: high score, 1: current score)
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

        this.uis[3].SetActive(true);   // 게임오버 & 재시작 표시
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

        // 게임 시작
        this.isGameStart = true;
        this.isGameOver = false;
        StartCoroutine(StartGame());

        // 타이머가 끝나면 숨김
        yield return new WaitForSeconds(1f);
        this.uis[1].SetActive(false);
    }
    public void OnClickPlayButton()
    { 
        this.uis[0].SetActive(false);   // 시작화면 숨김
        this.uis[3].SetActive(false);   // 게임오버 & 재시작 숨김

        this.uis[1].SetActive(true);    // 타이머 보이기
        this.uis[2].SetActive(true);    // 스코어 보이기

        StartCoroutine(StartTimer());
    }
    public void OnClickRetryButton()
    {
        this.uis[3].SetActive(false);   // 게임오버 & 재시작 숨김
        
        this.uis[1].SetActive(true);    // 타이머 보이기
        this.uis[2].SetActive(true);    // 스코어 보이기

        // 기존 clone 제거
        var clones = GameObject.FindGameObjectsWithTag("Clone");
        foreach (var clone in clones)
        {
            DestroyImmediate(clone);
        }

        // 플레이어 시작위치
        var player = GameObject.FindGameObjectWithTag("Player");
        Debug.Log(player.name);
        player.transform.position = new Vector2(-5f, -2.5f);

        // 스코어 초기화
        var scores = this.uis[2].GetComponentsInChildren<Text>();
        scores[1].text = 0.ToString("00000.##");

        StartCoroutine(StartTimer());
    }
}
