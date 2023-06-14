using System;
using System.Collections.Generic;

public class DialogShop : Dialog
{
    protected override void Awake()
    {
        base.Awake();
        AdsInitializer.Instance.onRewardVideoComplete = (Action<string>)Delegate.Combine(AdsInitializer.Instance.onRewardVideoComplete, new Action<string>(this.onRewardVideoComplete));
    }

    private void OnDestroy()
    {
        AdsInitializer.Instance.onRewardVideoComplete = (Action<string>)Delegate.Remove(AdsInitializer.Instance.onRewardVideoComplete, new Action<string>(this.onRewardVideoComplete));
    }

    protected override void Start()
    {
        base.Start();
    }

    private void onRewardVideoComplete(string from)
    {
        if (from == "Shop")
        {
            Coins.updateCoin(400, "shopVideo");
        }
    }
}
