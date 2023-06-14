using System;
using UnityEngine;
using UnityEngine.UI;

public class DialogLotteryTripple : Dialog
{
    public Text coinTxt;
    public ButtonVideo videoBtn;

    [HideInInspector]
    public string videoFrom = "lotteryTripple";

    [HideInInspector]
    public int coins;

    public Action<int> onResult;

    protected override void Awake()
    {
        base.Awake();
        this.videoBtn.VideoFrom = this.videoFrom;
        AdsInitializer.Instance.onRewardVideoComplete = (Action<string>)Delegate.Combine(AdsInitializer.Instance.onRewardVideoComplete, new Action<string>(this.onRewardVideoComplete));
    }

    private void OnDestroy()
    {
        AdsInitializer.Instance.onRewardVideoComplete = (Action<string>)Delegate.Remove(AdsInitializer.Instance.onRewardVideoComplete, new Action<string>(this.onRewardVideoComplete));
    }

    public void setVideoType(string from)
    {
        this.videoFrom = from;
        this.videoBtn.VideoFrom = this.videoFrom;
    }

    protected override void Start()
    {
        base.Start();
        SoundManager.Instance.playSound(SoundManager.Sound.rewardx3);
    }

    public override void Show()
    {
        base.Show();
        this.coinTxt.text = this.coins + string.Empty;
    }

    private void onRewardVideoComplete(string from)
    {
        if (from == this.videoFrom)
        {
            this.Close();
            if (this.onResult != null)
            {
                this.onResult(this.coins * 3);
            }
        }
    }

    public override void OnBackPressed()
    {
        this.onClaim();
    }

    public void onClaim()
    {
        this.Close();
        if (this.onResult != null)
        {
            this.onResult(this.coins);
        }
    }
}
