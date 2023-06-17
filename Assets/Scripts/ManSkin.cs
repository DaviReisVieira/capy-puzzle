using System;
using UnityEngine;

public class ManSkin : MonoBehaviour
{
    public string key;

    private void Awake()
    {
        MessageManager.instance.Subscribe(MessageManager.Event.MAN_CHANGE, new MessageManager.funsig(this.onManChange));
    }

    private void OnDestroy()
    {
        MessageManager.instance.Unsubscribe(MessageManager.Event.MAN_CHANGE, new MessageManager.funsig(this.onManChange));
    }

    private void Start()
    {
        this.onManChange();
    }

    private void onManChange()
    {
        // base.GetComponent<SpriteRenderer>().sprite = ManStyleManager.instance.getSkinSprite(this.key);
    }
}
