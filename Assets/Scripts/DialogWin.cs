using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class DialogWin : Dialog
{
    private sealed class _init_c__Iterator0 : IEnumerator, IDisposable, IEnumerator<object>
    {
        internal bool _withAds___0;

        internal int _levelUnlocked___0;

        internal int _starCount___0;

        internal int _i___1;

        internal bool _interstitialShowed___0;

        internal GameObject _btnShowMan___0;

        internal DialogWin _this;

        internal object _current;

        internal bool _disposing;

        internal int _PC;

        object IEnumerator<object>.Current
        {
            get
            {
                return this._current;
            }
        }

        object IEnumerator.Current
        {
            get
            {
                return this._current;
            }
        }

        public _init_c__Iterator0()
        {
        }

        public bool MoveNext()
        {
            uint num = (uint)this._PC;
            this._PC = -1;
            switch (num)
            {
                case 0u:
                    this._this.title.text = LocalizationManager.Instance.getLocalizeStringFormat("LEVEL_dialog_title", LevelManager.getLevelName());
                    this._withAds___0 = (PrefManager.isLotteryUsed() && RemoteConfig.Instance.freePrize_status == 0);
                    this._this.adIcon.SetActive(this._withAds___0);
                    this._this.btnVideo.enabled = this._withAds___0;
                    this._levelUnlocked___0 = PrefManager.getUnlockLevel(LevelManager.sectionIndex);
                    if (LevelManager.levelIndex >= this._levelUnlocked___0)
                    {
                        PrefManager.setUnlockLevel(LevelManager.sectionIndex, LevelManager.levelIndex + 1);
                        PrefManager.setLevelPassed(PrefManager.getLevelPassed() + 1);
                        MessageManager.instance.Publish(MessageManager.Event.NEW_LEVEL_PASSED);
                    }
                    this._starCount___0 = PrefManager.getStar(LevelManager.sectionIndex, LevelManager.levelIndex);
                    if (GameController.starCount > this._starCount___0)
                    {
                        PrefManager.setStar(LevelManager.sectionIndex, LevelManager.levelIndex, GameController.starCount);
                        MessageManager.instance.Publish<int>(MessageManager.Event.STARS_UPDATE, LevelManager.sectionIndex);
                    }
                    this._this.timeGold.SetActive(false);
                    this._this.videoBtn.SetActive(false);
                    this._this.retryBtn.SetActive(false);
                    this._this.nextBtn.SetActive(false);
                    this._this.levelBtn.SetActive(false);
                    this._this.manBtn.SetActive(false);
                    this._this.starCountTxt.text = LevelManager.totalStarGeted + "/" + LevelManager.totalStars;
                    this._i___1 = 0;
                    break;
                case 1u:
                    this._current = new WaitForSecondsRealtime(0.1f);
                    if (!this._disposing)
                    {
                        this._PC = 2;
                    }
                    return true;
                case 2u:
                    this._i___1++;
                    break;
                case 3u:
                    this._current = new WaitForSecondsRealtime(0.083f);
                    if (!this._disposing)
                    {
                        this._PC = 4;
                    }
                    return true;
                case 4u:
                    if (!PrefManager.isWinHandShowed())
                    {
                        PrefManager.setWinHandShowed();
                    }
                    this._this.shakeRewardBtn();
                    this._current = new WaitForSecondsRealtime(1f);
                    if (!this._disposing)
                    {
                        this._PC = 5;
                    }
                    return true;
                case 5u:
                    this._btnShowMan___0 = this._this.manBtn;
                    this._btnShowMan___0.SetActive(true);
                    this._this.timeGold.SetActive(true);
                    this._current = new WaitForSecondsRealtime(0.12f);
                    if (!this._disposing)
                    {
                        this._PC = 6;
                    }
                    return true;
                case 6u:
                    this._current = new WaitForSecondsRealtime(0.083f);
                    if (!this._disposing)
                    {
                        this._PC = 7;
                    }
                    return true;
                case 7u:
                    this._current = new WaitForSecondsRealtime(0.2f);
                    if (!this._disposing)
                    {
                        this._PC = 8;
                    }
                    return true;
                case 8u:
                    if (PrefManager.getLevelPassed() >= RemoteConfig.Instance.fiveStarRateLevel && GameController.starCount >= 3 && !this._interstitialShowed___0)
                    {
                        // PRECISO DO DAP QUE TÁ DANDO ERRO
                        // AppRater.ShowRatingDialog();

                        // Application.OpenURL("https://davirvs.com.br/");

                    }
                    this._this.nextBtn.SetActive(true);
                    this._this.retryBtn.SetActive(true);
                    this._this.levelBtn.SetActive(true);
                    this._current = new WaitForSecondsRealtime(0.12f);
                    if (!this._disposing)
                    {
                        this._PC = 9;
                    }
                    return true;
                case 9u:
                    this._current = new WaitForSecondsRealtime(0.083f);
                    if (!this._disposing)
                    {
                        this._PC = 10;
                    }
                    return true;
                case 10u:
                    this._PC = -1;
                    return false;
                default:
                    return false;
            }
            if (this._i___1 >= GameController.starCount)
            {
                this._interstitialShowed___0 = this._this.showInterstitial();
                AdsInitializer.Instance.LoadRewardedAd("Win");
                this._this.videoBtn.SetActive(true);
                this._current = new WaitForSecondsRealtime(0.12f);
                if (!this._disposing)
                {
                    this._PC = 3;
                }
                return true;
            }
            SoundManager.Instance.playSound(SoundManager.Sound.star);
            this._this.stars[this._i___1].SetActive(true);
            this._current = new WaitForSecondsRealtime(0.2f);
            if (!this._disposing)
            {
                this._PC = 1;
            }
            return true;
        }

        public void Dispose()
        {
            this._disposing = true;
            this._PC = -1;
        }

        public void Reset()
        {
            throw new NotSupportedException();
        }
    }

    private sealed class _realShakeRewardBtn_c__Iterator1 : IEnumerator, IDisposable, IEnumerator<object>
    {
        internal DialogWin _this;

        internal object _current;

        internal bool _disposing;

        internal int _PC;

        object IEnumerator<object>.Current
        {
            get
            {
                return this._current;
            }
        }

        object IEnumerator.Current
        {
            get
            {
                return this._current;
            }
        }

        public _realShakeRewardBtn_c__Iterator1()
        {
        }

        public bool MoveNext()
        {
            uint num = (uint)this._PC;
            this._PC = -1;
            switch (num)
            {
                case 0u:
                    this._current = new WaitForSecondsRealtime(0.1f);
                    if (!this._disposing)
                    {
                        this._PC = 1;
                    }
                    return true;
                case 1u:
                    this._current = new WaitForSecondsRealtime(0.1f);
                    if (!this._disposing)
                    {
                        this._PC = 2;
                    }
                    return true;
                case 2u:
                    this._current = new WaitForSecondsRealtime(0.067f);
                    if (!this._disposing)
                    {
                        this._PC = 3;
                    }
                    return true;
                case 3u:
                    Timer.Register(2f, new Action(this._this.shakeRewardBtn), null, false, true, this._this);
                    this._PC = -1;
                    break;
            }
            return false;
        }

        public void Dispose()
        {
            this._disposing = true;
            this._PC = -1;
        }

        public void Reset()
        {
            throw new NotSupportedException();
        }
    }

    public Text title;

    public Text starCountTxt;

    public GameObject[] stars;

    public GameObject videoBtn;

    public GameObject timeGold;

    public GameObject manBtn;


    public GameObject retryBtn;

    public GameObject nextBtn;

    public GameObject levelBtn;

    public GameObject[] manEffects;

    public GameObject adIcon;

    public ButtonVideo btnVideo;


    public static int showTimes;

    private bool isVideoClicked;

    protected override void Awake()
    {
        base.Awake();
        AdsInitializer.Instance.onRewardVideoComplete = (Action<string>)Delegate.Combine(AdsInitializer.Instance.onRewardVideoComplete, new Action<string>(this.onRewardVideoComplete));
        SceneAnimCoverManager expr_31 = SceneAnimCoverManager.Instance;
        expr_31.actionBeforeLoadScene = (Action)Delegate.Combine(expr_31.actionBeforeLoadScene, new Action(this.Close));
    }

    private void OnDestroy()
    {
        AdsInitializer.Instance.onRewardVideoComplete = (Action<string>)Delegate.Remove(AdsInitializer.Instance.onRewardVideoComplete, new Action<string>(this.onRewardVideoComplete));
        SceneAnimCoverManager expr_2B = SceneAnimCoverManager.Instance;
        expr_2B.actionBeforeLoadScene = (Action)Delegate.Remove(expr_2B.actionBeforeLoadScene, new Action(this.Close));
    }

    protected override void Start()
    {
        base.Start();
        int num = this.manEffects.Length;
        if (!PrefManager.isPromoteManUnlocked())
        {
            num--;
        }
        int num2 = UnityEngine.Random.Range(0, num);
        for (int i = 0; i < this.manEffects.Length; i++)
        {
            this.manEffects[i].SetActive(i == num2);
        }
        DialogWin.showTimes++;
        base.StartCoroutine(this.init());
        SoundManager.Instance.playSound(SoundManager.Sound.win);

    }

    private IEnumerator init()
    {
        DialogWin._init_c__Iterator0 _init_c__Iterator = new DialogWin._init_c__Iterator0();
        _init_c__Iterator._this = this;
        return _init_c__Iterator;
    }

    private void shakeRewardBtn()
    {
        base.StartCoroutine(this.realShakeRewardBtn());
    }

    private IEnumerator realShakeRewardBtn()
    {
        DialogWin._realShakeRewardBtn_c__Iterator1 _realShakeRewardBtn_c__Iterator = new DialogWin._realShakeRewardBtn_c__Iterator1();
        _realShakeRewardBtn_c__Iterator._this = this;
        return _realShakeRewardBtn_c__Iterator;
    }

    public void onNext()
    {
        LevelManager.playNext();
    }

    public void onRetry()
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

    public void onVideoClick()
    {
        AdsInitializer.Instance.LoadRewardedAd("Win");
        this.videoBtn.SetActive(false);
    }

    private bool showInterstitial()
    {
        if (this.shouldShowInterstital())
        {
            DialogWin.showTimes = 0;
            AdsInitializer.Instance.LoadRewardedAd("Win");
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

        return false;
    }

    private void onRewardVideoComplete(string from)
    {
        if (from == "Win")
        {
            int num = UnityEngine.Random.Range(100, 200);
            Coins.updateCoin(num, "shopVideo");
        }
    }
}
