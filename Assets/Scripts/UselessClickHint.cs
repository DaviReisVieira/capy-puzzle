using DG.Tweening;
using System;
using UnityEngine;

public class UselessClickHint : MonoBehaviour
{
	private const float ANIM_TIME = 0.5f;

	private bool showed;

	public void showHint()
	{
		if (this.showed)
		{
			return;
		}
		this.showed = true;
		base.transform.DOMoveY(base.transform.position.y - 2.2f, 0.5f, false);
		Timer.Register(2f, delegate
		{
			this.hideHint();
		}, null, false, false, this);
	}

	private void hideHint()
	{
		base.transform.DOMoveY(base.transform.position.y + 2.2f, 0.5f, false).OnComplete(delegate
		{
			this.showed = false;
		});
	}
}
