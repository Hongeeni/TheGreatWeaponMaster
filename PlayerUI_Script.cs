using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerUI_Script : MonoBehaviour
{
    public GameObject playerMenu;
    public GameObject lineArea;
    public GameObject NPCName;
    public GameObject NPCLine;

    private GameObject NPCImage;
    private GameObject playerImage;

    private Text characterNameStr;
    private Text characterLineStr;

    private int lineCount;

    List<string> lines = new List<string>
    {
        "선대 영웅", "너가 내 뒤를 잇게 될 아이구나.",
        "나", "앗 누구시죠?",
        "선대 영웅", "너의 선대 영웅이였던 사람이지. 하지만 내 힘이 부족해서 드래곤에게 져버렸어..",
        "선대 영웅", "죽어서라도 사람들을 위해 성불하지 못하고 너희를 도우기 위해 남아있던 거야.",
        "선대 영웅", "이번에야말로 마왕을 무찌를 수 있게 내가 너를 도와줄게.",
        "선대 영웅", "맵에 있는 오아시스까지 와보렴.",
        "나", "알겠습니다.",
        "",
        "선대 영웅", "무사히 도착했구나. 하지만 상처를 좀 입은것 같네.",
        "나", "네. 꽤 힘드네요.",
        "선대 영웅", "그럴줄 알고 여기로 오라고 했던거야. 이 곳에선 회복을 할 수 있어.",
        "선대 영웅", "앞으로 있을 지역에서도 이런 곳이 있을거야. 꼭 체력을 회복하고 다니도록 해.",
        "나", "...이제 어디로 가야하죠?",
        "선대 영웅", "이제 다음 지역으로 넘어가는 포탈이 있을거야. 그곳으로 가면 돼.",
        "",
        "나", "으으 추워. 어떻게 사막 바로 옆에 설원이 있는 거지?",
        "선대 영웅", "드래곤때문에 날씨가 이상해져서 사막과 설원이 붙어 있는 이상한 세계가 되어버린거야.",
        "나", "그렇군요.",
        "선대 영웅", "여기도 지역은 다르지만 사막과 별반 다를게 없어. 계속 나아가렴.",
        "",
        "나", "여긴.. 안개 때문에 앞이 잘 안보이잖아?",
        "선대 영웅", "원래 이 숲은 아름다운 곳이였는데... 드래곤때문에 이 숲까지 이상해져버렸어.",
        "선대 영웅", "조심해. 이 숲 어딘가에서 아까와는 다르게 강한 힘이 느껴져. 아까 전까지 만났던 몬스터들보다 훨씬 강한 녀석이야.",
        "나", "네. 주의할게요.",
        "",
        "나", "여... 여긴..?",
        "선대 영웅", "여기가 바로 이 사태의 원흉인 드래곤이 사는 둥지야.",
        "선대 영웅", "저 제단 가운데에 있는 보석을 만지면 놈이 나타날거야.",
        "선대 영웅", "마음 단단히 먹어. 지금까지 만난 놈들과는 차원이 다르게 강할거야.",
        "나", "제가 이길 수 있을까요?",
        "선대 영웅", "넌 충분히 해낼 수 있어. 너를 믿는 나를 믿는거야.",
        "나", "네. 꼭 해낼게요.",
        "",
        "선대 영웅", "해냈어!",
        "나", "쓰러뜨렸다..",
        "선대 영웅", "너가 세상을 구한거야!!",
        "선대 영웅", "드디어 세계가 평화로워지겠어.",
        "나", "도와주셔서 감사합니다.",
        "선대 영웅", "무슨 소리야. 쓰러뜨린건 너인걸. 이 무대의 주인공은 너야.",
        "선대 영웅", "모두가 기다리고 있을테니 어서 돌아가보렴.",
        "나", "감사합니다..",
        "System", "그렇게 드래곤은 쓰러지고 세계는 평화를 되찾게 되었다.",
        "End"
    };

    private void Start()
    {
        NPCImage = lineArea.transform.GetChild(2).gameObject;
        playerImage = lineArea.transform.GetChild(3).gameObject;

        characterNameStr = NPCName.GetComponent<Text>();
        characterLineStr = NPCLine.GetComponent<Text>();

        if (PlayerPrefs.GetString("PlayStage") == "Stage_01")
        {
            lineCount = 0;

            characterNameStr.text = lines[lineCount];
            lineCount++;
            characterLineStr.text = lines[lineCount];
            lineCount++;
        }
        else if (PlayerPrefs.GetString("PlayStage") == "Stage_02")
        {
            lineCount = 28;

            characterNameStr.text = lines[lineCount];
            lineCount++;
            characterLineStr.text = lines[lineCount];
            lineCount++;
        }
        else if (PlayerPrefs.GetString("PlayStage") == "Stage_03")
        {
            lineCount = 37;

            characterNameStr.text = lines[lineCount];
            lineCount++;
            characterLineStr.text = lines[lineCount];
            lineCount++;
        }
        else
        {
            lineCount = 46;

            characterNameStr.text = lines[lineCount];
            lineCount++;
            characterLineStr.text = lines[lineCount];
            lineCount++;
        }

        Time.timeScale = 0.0f;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (playerMenu.activeSelf)
            {
                if (lineArea.activeSelf)
                {
                    Cursor.lockState = CursorLockMode.Locked;
                    Cursor.visible = false;

                    playerMenu.SetActive(false);
                }
                else
                {
                    Cursor.lockState = CursorLockMode.Locked;
                    Cursor.visible = false;

                    playerMenu.SetActive(false);

                    Time.timeScale = 1.0f;
                }
            }
            else
            {
                if (Time.timeScale == 0.0f)
                {
                    playerMenu.SetActive(true);

                    Cursor.lockState = CursorLockMode.Confined;
                    Cursor.visible = true;
                }
                else
                {
                    Time.timeScale = 0.0f;

                    playerMenu.SetActive(true);

                    Cursor.lockState = CursorLockMode.Confined;
                    Cursor.visible = true;
                }
            }
        }

        if (lineArea.activeSelf && Input.GetKeyDown(KeyCode.Space))
        {
            if (lines[lineCount] == "")
            {
                lineArea.SetActive(false);
                Time.timeScale = 1.0f;

                lineCount++;
                characterNameStr.text = lines[lineCount];
                lineCount++;
                characterLineStr.text = lines[lineCount];
                lineCount++;

                if (characterNameStr.text == "선대 영웅")
                {
                    NPCImage.SetActive(true);
                    playerImage.SetActive(false);
                }
                else
                {
                    NPCImage.SetActive(false);
                    playerImage.SetActive(true);
                }
            }
            else if (lines[lineCount] == "End")
            {
                StartCoroutine(EndGame());
            }
            else
            {
                characterNameStr.text = lines[lineCount];
                lineCount++;
                characterLineStr.text = lines[lineCount];
                lineCount++;

                if (characterNameStr.text == "선대 영웅")
                {
                    NPCImage.SetActive(true);
                    playerImage.SetActive(false);
                }
                else if (characterNameStr.text == "나")
                {
                    NPCImage.SetActive(false);
                    playerImage.SetActive(true);
                }
                else
                {
                    playerImage.SetActive(false);
                }
            }
        }
    }

    public void RetryGame()
    {
        LoadScene_Script.LoadScene(PlayerPrefs.GetString("PlayStage"));
    }

    public void ResetGame()
    {
        PlayerPrefs.SetString("PlayStage", "Stage_01");

        Time.timeScale = 1.0f;
        LoadScene_Script.LoadScene("TitleScene");
    }

    public void ExitGame()
    {
        Application.Quit();
        //UnityEditor.EditorApplication.isPlaying = false;
    }

    private IEnumerator EndGame()
    {
        lineArea.SetActive(false);

        Time.timeScale = 1.0f;
        yield return new WaitForSeconds(3.0f);
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;

        PlayerPrefs.SetString("PlayStage", "Stage_01");
        LoadScene_Script.LoadScene("TitleScene");
    }
}
