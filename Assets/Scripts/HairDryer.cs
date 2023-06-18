using System;
using UnityEngine;

public class HairDryer : MonoBehaviour
{
	public GameObject effect;

	public GameObject particle;

	public GameObject trigger;

	private void Start()
	{
		ClickDismiss expr_05 = UnityEngine.Object.FindObjectOfType<ClickDismiss>();
		expr_05.onDismiss = (Action<GameObject>)Delegate.Combine(expr_05.onDismiss, new Action<GameObject>(this.onItemClick));
	}

	private void onItemClick(GameObject obj)
	{
		if (obj == this.trigger)
		{
			this.work();
		}
	}

	public void work()
	{
		this.effect.SetActive(true);
		this.particle.SetActive(true);
	}

	public void stop()
	{
		this.effect.SetActive(false);
		this.particle.SetActive(false);
	}
}
