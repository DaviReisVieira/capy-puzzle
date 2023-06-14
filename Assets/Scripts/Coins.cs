using DG.Tweening;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Coins : MonoBehaviour
{
    public Text coinTxt;

    public GameObject icon;

    public GameObject anchor;


    private int coinCount;

    private int layer;

    private void Awake()
    {
        MessageManager.instance.Subscribe(MessageManager.Event.COIN_UPDATE, new MessageManager.funsig(this.onCoinUpdate));
    }

    private void OnDestroy()
    {
        MessageManager.instance.Unsubscribe(MessageManager.Event.COIN_UPDATE, new MessageManager.funsig(this.onCoinUpdate));
    }

    private void Start()
    {
        this.coinCount = PrefManager.getCoin();
        this.coinTxt.text = this.coinCount + string.Empty;
        this.layer = this.getCanvasLayers();
    }

    public static void updateCoin(int updatedCoin, string from)
    {
        PrefManager.setCoin(PrefManager.getCoin() + updatedCoin);
        if (updatedCoin > 0)
        {
            Dictionary<string, object> dictionary = new Dictionary<string, object>();
            dictionary.Add("From", from);

        }
        else
        {
            Dictionary<string, object> dictionary2 = new Dictionary<string, object>();
            dictionary2.Add("From", from);

        }
        MessageManager.instance.Publish(MessageManager.Event.COIN_UPDATE);
    }

    private void onCoinUpdate()
    {
        if (PrefManager.getCoin() > this.coinCount)
        {
            SoundManager.Instance.playSound(SoundManager.Sound.coins);

            this.icon.transform.DOScale(Vector3.one * 1.8f, 0.1f).OnComplete(delegate
            {
                this.icon.transform.DOScale(Vector3.one, 0.05f);
            });
        }
        this.showTextEffect(PrefManager.getCoin());
    }

    private void showTextEffect(int end)
    {
        DOTween.To(() => this.coinCount, delegate (int x)
        {
            this.coinCount = x;
            this.coinTxt.text = this.coinCount + string.Empty;
        }, end, 0.3f);
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
