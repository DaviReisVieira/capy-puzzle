using System;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class TimeGold : MonoBehaviour
{
    private const int oneCircleTime = 10;

    private int maxTime;

    private float oneCircleAddCoin;

    public Text time;

    public Text value;

    public Text claim;

    public Image progress;

    public GameObject shine;

    // public GameObject particalOne;

    // public GameObject particalFull;

    public GameObject particalAnchor;

    private double timeUsed;

    private double timeStart;

    private int coinAdd;

    private GameObject fullParitcal;

    private int canvasLayer;

    private static Action<int> __f__am_cache0;

    private void Start()
    {
        this.maxTime = 86400 / RemoteConfig.Instance.timeGold_count;
        this.oneCircleAddCoin = (float)RemoteConfig.Instance.timeGold_total * 1f / (float)RemoteConfig.Instance.timeGold_count / (float)(this.maxTime / 10);
        this.timeStart = PrefManager.getTimeGoldBeginTime();
        this.canvasLayer = this.getCanvasLayers();
        this.setValue();
    }

    private void Update()
    {
        this.setValue();
    }

    public void onClaim()
    {
        if (this.coinAdd < (int)this.oneCircleAddCoin)
        {
            return;
        }
        if (this.fullParitcal != null)
        {
            UnityEngine.Object.Destroy(this.fullParitcal);
            this.fullParitcal = null;
        }
        PrefManager.setTimeGoldBeginTime();
        this.timeStart = PrefManager.getTimeGoldBeginTime();
        if (this.coinAdd >= RemoteConfig.Instance.timeGold_total / RemoteConfig.Instance.timeGold_count / 2)
        {
            DialogLotteryTripple dialogLotteryTripple = (DialogLotteryTripple)DialogController.instance.GetDialog(DialogType.lotteryTripple, string.Empty);
            dialogLotteryTripple.coins = this.coinAdd;
            dialogLotteryTripple.setVideoType("timeGoldTripple");
            DialogLotteryTripple expr_97 = dialogLotteryTripple;
            expr_97.onResult = (Action<int>)Delegate.Combine(expr_97.onResult, new Action<int>(delegate (int x)
            {
                Coins.updateCoin(x, "timeGoldTripple");
            }));
            DialogController.instance.ShowDialog(dialogLotteryTripple, DialogShow.OVER_CURRENT);
        }
        else
        {
            Coins.updateCoin(this.coinAdd, "timeGold");
            this.coinAdd = 0;
        }
    }

    private void setValue()
    {
        this.timeUsed = Util.GetCurrentTimeInDouble() - this.timeStart;
        if (this.timeUsed <= 0.0)
        {
            this.timeUsed = 0.0;
        }
        if (this.timeUsed >= (double)this.maxTime)
        {
            this.timeUsed = (double)this.maxTime;
        }
        this.setTimeLeftValue();
        this.setCoinValue();
        this.claim.gameObject.SetActive(this.coinAdd >= (int)this.oneCircleAddCoin);
        this.shine.SetActive(this.timeUsed == (double)this.maxTime);
        this.progress.fillAmount = (float)(this.timeUsed % 10.0) / 10f;
    }

    private void setTimeLeftValue()
    {
        if (this.timeUsed == (double)this.maxTime)
        {
            this.time.text = LocalizationManager.Instance.getLocalizeString("full");
        }
        else
        {
            int num = (this.maxTime - (int)this.timeUsed) / 3600;
            int num2 = (this.maxTime - (int)this.timeUsed) % 3600 / 60;
            int num3 = (this.maxTime - (int)this.timeUsed) % 3600 % 60;
            this.time.text = string.Concat(new string[]
            {
                (num >= 10) ? (num + string.Empty) : ("0" + num),
                ":",
                (num2 >= 10) ? (num2 + string.Empty) : ("0" + num2),
                ":",
                (num3 >= 10) ? (num3 + string.Empty) : ("0" + num3)
            });
        }
    }

    private void setCoinValue()
    {
        if (this.coinAdd != 0 && this.coinAdd != (int)Mathf.Ceil((float)((int)this.timeUsed / 10) * this.oneCircleAddCoin) && this.timeUsed != (double)this.maxTime)
        {
            // GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.particalOne);
            // gameObject.transform.position = this.particalAnchor.transform.position;
            // gameObject.GetComponent<Renderer>().sortingOrder += this.canvasLayer;
        }
        else if (this.timeUsed == (double)this.maxTime && this.fullParitcal == null)
        {
            // this.fullParitcal = UnityEngine.Object.Instantiate<GameObject>(this.particalFull);
            // this.fullParitcal.transform.position = this.particalAnchor.transform.position;
            // this.fullParitcal.GetComponent<Renderer>().sortingOrder += this.canvasLayer;
        }
        this.coinAdd = (int)Mathf.Ceil((float)((int)this.timeUsed / 10) * this.oneCircleAddCoin);
        if (this.coinAdd >= 1000)
        {
            this.value.text = string.Concat(new object[]
            {
                this.coinAdd / 1000,
                ".",
                this.coinAdd % 1000 / 100,
                "k"
            });
        }
        else
        {
            this.value.text = this.coinAdd + string.Empty;
        }
    }

    private int getCanvasLayers()
    {
        Transform parent = base.transform.parent;
        while (parent != null)
        {
            if (parent.GetComponent<Canvas>() != null)
            {
                return parent.GetComponent<Canvas>().sortingOrder;
            }
            parent = parent.parent;
        }
        return 0;
    }
}
