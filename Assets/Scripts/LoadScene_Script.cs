using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadScene_Script : MonoBehaviour
{
    static string nextScene;

    [SerializeField]
    Image LoadingBar;

    public static void LoadScene(string sceneName)
    {
        nextScene = sceneName;
        SceneManager.LoadScene("LoadingScene");
    }

    private void Start()
    {
        StartCoroutine(LoadSceneProcess());
    }

    IEnumerator LoadSceneProcess()
    {
        AsyncOperation op = SceneManager.LoadSceneAsync(nextScene);
        op.allowSceneActivation = false;

        float timer = 0f;
        while (!op.isDone)
        {
            yield return null;

            if (op.progress < 0.6f)
            {
                LoadingBar.fillAmount = op.progress;
            }
            else
            {
                timer += Time.unscaledDeltaTime / 5;
                LoadingBar.fillAmount = Mathf.Lerp(0.6f, 1.0f, timer);
                if (LoadingBar.fillAmount >= 1.0f)
                {
                    op.allowSceneActivation = true;
                    yield break;
                }
            }
        }
    }
}
