using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class SceneAnimCoverManager : MonoBehaviour
{
    public static SceneAnimCoverManager Instance;

    public Action actionBeforeLoadScene;

    public Animator coverAnim;

    private string sceneName;

    private bool isAnimating;

    private void Awake()
    {
        SceneAnimCoverManager.Instance = this;
        SceneManager.sceneLoaded += new UnityAction<Scene, LoadSceneMode>(this.OnLevelFinishedLoading);
    }

    private void OnDestroy()
    {
        SceneAnimCoverManager.Instance = null;
        SceneManager.sceneLoaded -= new UnityAction<Scene, LoadSceneMode>(this.OnLevelFinishedLoading);
    }

    private void OnLevelFinishedLoading(Scene scence, LoadSceneMode mode)
    {
        if (scence.name == this.sceneName)
        {
            this.coverAnim.SetTrigger("end");
            this.isAnimating = false;
        }
    }

    public void onFadeOutEnd()
    {
        if (this.actionBeforeLoadScene != null)
        {
            this.actionBeforeLoadScene();
        }
        this.fadeIn();
    }

    public void loadScene(string sceneName)
    {
        Debug.Log("loadScene: " + sceneName);
        if (this.isAnimating)
        {
            return;
        }
        this.isAnimating = true;
        this.coverAnim.SetTrigger("start");
        this.sceneName = sceneName;
    }

    private void fadeIn()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(this.sceneName);
    }
}
