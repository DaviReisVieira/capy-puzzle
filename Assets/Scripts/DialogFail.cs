using System;
using System.Collections.Generic;
//using Umeng;
using UnityEngine;
using UnityEngine.UI;

public class DialogFail : Dialog
{
    public Text title;

    public Text starCountTxt;

    public GameObject adIcon;

    public ButtonVideo btnVideo;

    public GameObject hand;

    private bool isVideoClicked;

    protected override void Awake()
    {
        base.Awake();
        //AdsManager expr_0B = AdsManager.Instance;
        // AdsControl.Instance.onRewardVideoComplete = (Action<string>)Delegate.Combine(AdsControl.Instance.onRewardVideoComplete, new Action<string>(this.onRewardVideoComplete));
        SceneAnimCoverManager expr_31 = SceneAnimCoverManager.Instance;
        expr_31.actionBeforeLoadScene = (Action)Delegate.Combine(expr_31.actionBeforeLoadScene, new Action(this.Close));
    }

    private void OnDestroy()
    {
        //AdsManager expr_05 = AdsManager.Instance;
        // AdsControl.Instance.onRewardVideoComplete = (Action<string>)Delegate.Remove(AdsControl.Instance.onRewardVideoComplete, new Action<string>(this.onRewardVideoComplete));
        SceneAnimCoverManager expr_2B = SceneAnimCoverManager.Instance;
        expr_2B.actionBeforeLoadScene = (Action)Delegate.Remove(expr_2B.actionBeforeLoadScene, new Action(this.Close));
    }

    protected override void Start()
    {
        base.Start();
        DialogWin.showTimes++;
        this.title.text = LocalizationManager.Instance.getLocalizeStringFormat("LEVEL_dialog_title", LevelManager.getLevelName());
        this.starCountTxt.text = LevelManager.totalStarGeted + "/" + LevelManager.totalStars;
        bool flag = PrefManager.isLotteryUsed() && RemoteConfig.Instance.freePrize_status == 0;
        this.adIcon.SetActive(flag);
        this.btnVideo.enabled = flag;
        if (!PrefManager.isWinHandShowed())
        {
            this.hand.SetActive(true);
            PrefManager.setWinHandShowed();
        }

        SoundManager.Instance.playSound(SoundManager.Sound.fail);
        Timer.Register(0.1f, delegate
        {
            this.showInterstitial();
        }, null, false, true, this);
    }

    private bool showInterstitial()
    {
        if (this.shouldShowInterstital())
        {
            DialogWin.showTimes = 0;
            //bool flag = (float)(Util.GetCurrentTime() - AdsManager.Instance.lastInterstitialShowTime) >= RemoteConfig.Instance.interstitial_time_interval;
            //string condition = (!flag) ? "time" : "level";
            // AdsControl.Instance.showAds();
            return true;
        }
        return false;
    }

    private bool shouldShowInterstital()
    {
        if (this.isVideoClicked)
        {
            return false;
        }
        if (PrefManager.getLevelPassed() < RemoteConfig.Instance.interstitial_level_show)
        {
            return false;
        }

        // if (AdsControl.Instance.GetInterstitalAvailable())
        // {
        // 	return true;
        // }



        return false;
    }

    public void onVideoClick()
    {
        this.isVideoClicked = true;
        if (RemoteConfig.Instance.freePrize_status == 0)
        {
            if (!PrefManager.isLotteryUsed())
            {
                PrefManager.setLotteryUsed();
                this.adIcon.SetActive(true);
                this.btnVideo.enabled = true;
                this.hand.SetActive(false);
                this.onRewardVideoComplete("Win");
            }
        }
        else
        {
            this.hand.SetActive(false);
            this.onRewardVideoComplete("Fail");
        }
    }

    public void onReplay()
    {
        LevelManager.playGame();

    }

    public void onLevel()
    {
        SceneAnimCoverManager.Instance.loadScene("Level");
    }

    public void onHome()
    {
        SceneAnimCoverManager.Instance.loadScene("Home");
    }

    private void onRewardVideoComplete(string from)
    {
        if (from == "Fail")
        {
            // DialogController.instance.ShowDialog(DialogType.lottery, DialogShow.OVER_CURRENT, string.Empty);
        }
    }
}
