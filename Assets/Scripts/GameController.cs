using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class GameController : BaseController
{
    public static int starCount;

    public Text timeLeft;

    public Image progress;

    public GameObject[] stars;

    public GameObject spark;

    public GameObject sparkEnd;

    public GameObject failTry;

    private Man man;

    private TimeLimit timeLimit;

    private float timeLimitTotal;

    private Vector3 sparkInitPos;

    private float _timeUsed;

    private static Action __f__am_cache0;

    private static Action __f__am_cache1;

    private static Action __f__am_cache2;

    private float timeUsed
    {
        get
        {
            return this._timeUsed;
        }
        set
        {
            this._timeUsed = value;
            this.timeLeft.text = LocalizationManager.Instance.getLocalizeStringFormat("time", Mathf.CeilToInt(this.timeLimitTotal - this._timeUsed));
            if (this._timeUsed <= this.timeLimit.Time3)
            {
                GameController.starCount = 3;
                this.progress.fillAmount = (this.timeLimit.Time3 - this._timeUsed) / this.timeLimit.Time3 / 3f + 0.6666667f;
            }
            else if (this._timeUsed <= this.timeLimit.Time3 + this.timeLimit.Time2)
            {
                GameController.starCount = 2;
                this.progress.fillAmount = (this.timeLimit.Time2 - (this._timeUsed - this.timeLimit.Time3)) / this.timeLimit.Time2 / 3f + 0.333333343f;
            }
            else if (this.timeLimitTotal - this._timeUsed > 0f)
            {
                GameController.starCount = 1;
                this.progress.fillAmount = (this.timeLimit.Time1 - (this._timeUsed - this.timeLimit.Time3 - this.timeLimit.Time2)) / this.timeLimit.Time1 / 3f;
            }
            else
            {
                GameController.starCount = 0;
                this.progress.fillAmount = 0f;
            }
            for (int i = 0; i < this.stars.Length; i++)
            {
                this.stars[i].SetActive(i < GameController.starCount);
            }
            this.spark.transform.position = Vector3.Lerp(this.sparkEnd.transform.position, this.sparkInitPos, this.progress.fillAmount);
        }
    }

    public override void Awake()
    {
        base.Awake();
        FailDetector failDetector = UnityEngine.Object.FindObjectOfType<FailDetector>();
        if (failDetector != null)
        {
            failDetector.gameObject.SetActive(false);
        }
    }

    private void Start()
    {

        this.timeLimit = UnityEngine.Object.FindObjectOfType<TimeLimit>();
        this.timeLimitTotal = this.timeLimit.Time1 + this.timeLimit.Time2 + this.timeLimit.Time3;
        this.man = UnityEngine.Object.FindObjectOfType<Man>();
        this.sparkInitPos = this.spark.transform.position;
        this.timeUsed = 0f;
        //GA.StartLevel(LevelManager.getAnalyticLevelName());
    }

    public void updateTime(float timeUsed)
    {
        this.timeUsed = timeUsed;
    }

    public void onFail()
    {
        FailDetector failDetector = UnityEngine.Object.FindObjectOfType<FailDetector>();
        if (failDetector != null)
        {
            failDetector.gameObject.SetActive(false);
        }
        this.failTry.SetActive(false);
        Timer.Register(2f, delegate
        {
            DialogController.instance.ShowDialog(DialogType.lose, DialogShow.REPLACE_CURRENT, string.Empty);
        }, null, false, false, this);
    }

    public void onLevelComplete()
    {
        this.man.onWin();
        FailDetector failDetector = UnityEngine.Object.FindObjectOfType<FailDetector>();
        if (failDetector != null)
        {
            failDetector.gameObject.SetActive(false);
        }
        this.failTry.SetActive(false);
        Timer.Register(2f, delegate
        {
            DialogType type = DialogType.win;
            int collectStarShowCount = PrefManager.getCollectStarShowCount();
            if (collectStarShowCount < RemoteConfig.Instance.collectStarShowCount && GameController.starCount < 3)
            {
                PrefManager.setCollectStarShowCount(collectStarShowCount + 1);
                type = DialogType.collectStar;
            }
            DialogController.instance.ShowDialog(type, DialogShow.REPLACE_CURRENT, string.Empty);
        }, null, false, false, this);
    }

    public void onMenu()
    {
        // DialogController.instance.ShowDialog(DialogType.pause, DialogShow.REPLACE_CURRENT, string.Empty);
    }

    public void onReset()
    {
        LevelManager.playGame();

    }

    public void onFailDetected()
    {
        this.failTry.SetActive(true);
    }

    public void onSkip()
    {
        Time.timeScale = 1f;
        this.man.onWin();
        FailDetector failDetector = UnityEngine.Object.FindObjectOfType<FailDetector>();
        if (failDetector != null)
        {
            failDetector.gameObject.SetActive(false);
        }
        this.failTry.SetActive(false);
        GameController.starCount = 3;
        Timer.Register(0.3f, delegate
        {
            DialogController.instance.ShowDialog(DialogType.win, DialogShow.REPLACE_CURRENT, string.Empty);
        }, null, false, false, this);

    }

    public void onHint()
    {
        if (LevelManager.sectionIndex == 0 && LevelManager.levelIndex == 0)
        {
            return;
        }
        PrefManager.setHintUsed();
        // UnityEngine.Object.FindObjectOfType<HintHelpter>().onHint();

    }
}
