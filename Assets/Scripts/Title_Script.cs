using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class Title_Script : MonoBehaviour
{
    public GameObject startVideoScreen;

    private Animator gameStartAnimator;
    private VideoPlayer startVideo;

    private string startStage;
    private bool onClickButton;

    private void Awake()
    {
        if (PlayerPrefs.HasKey("PlayStage"))
        {
            startStage = PlayerPrefs.GetString("PlayStage");
        }
        else
        {
            PlayerPrefs.SetString("PlayStage", "Stage_01");
            startStage = "Stage_01";
        }
    }

    private void Start()
    {
        gameStartAnimator = this.GetComponent<Animator>();
        startVideo = startVideoScreen.GetComponent<VideoPlayer>();

        onClickButton = false;
    }

    //Start 버튼 클릭 시 게임이 시작되는 함수
    public void StartGame()
    {
        if (!onClickButton)
        {
            onClickButton = true;
            StartCoroutine(OnGameStart());
        }
    }

    //Exit 버튼 클릭 시 게임이 종료되는 함수
    public void ExitGame()
    {
        if (!onClickButton)
        {
            onClickButton = true;

            Application.Quit();
            //UnityEditor.EditorApplication.isPlaying = false;
        }
    }

    private IEnumerator OnGameStart()
    {
        gameStartAnimator.SetBool("isStart", true);
        yield return new WaitForSeconds(2.0f);

        //만약 현재 스테이지가 Stage_01일 경우 인트로 동영상 재생
        if (PlayerPrefs.GetString("PlayStage") == "Stage_01")
        {
            startVideo.Play();
            startVideoScreen.GetComponent<Image>().color = new Color(0, 0, 0, 0);
            yield return new WaitForSeconds(8.0f);
        }

        LoadScene_Script.LoadScene(startStage);
    }
}