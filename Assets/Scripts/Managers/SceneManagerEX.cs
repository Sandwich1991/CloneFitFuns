using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneManagerEX
{
    private GameObject _loadingScreen;
    private Slider _progressbar;
    
    public void LoadScene(Define.Scene type)
    {
        Managers.Clear();
        Managers.CoroutineHelper(LoadSceneProgress(type));
        
    }

    IEnumerator LoadSceneProgress(Define.Scene type)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(GetSceneName(type));

        operation.allowSceneActivation = false;

        float timer = 0f;
        while (!operation.isDone)
        {
            yield return null;
            
            if (operation.progress < 0.9f)
            {
                _progressbar.value = operation.progress;
            }
            else
            {
                timer += Time.unscaledDeltaTime;
                _progressbar.value = Mathf.Lerp(0.9f, 1f, timer);
                if (_progressbar.value >= 1f)
                {
                    operation.allowSceneActivation = true;
                    Managers.Resource.Destroy(_loadingScreen);
                    yield break;
                }
            }
        }
    }

    string GetSceneName(Define.Scene type)
    {
        string name = System.Enum.GetName(typeof(Define.Scene), type);
        return name;
    }

    public void init()
    {
        _loadingScreen = Managers.Resource.Instantiate("LoadingScreen");
        _progressbar = _loadingScreen.transform.Find("ProgressionBar").GetComponent<Slider>();
    }
}
