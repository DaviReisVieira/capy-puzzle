using System;
using UnityEngine;

public class Balloon : MonoBehaviour
{
	private void OnCollisionEnter2D(Collision2D coll)
	{
		if (coll.gameObject.CompareTag("thorn"))
		{
			SoundManager.Instance.playSound(SoundManager.Sound.ballon);
			UnityEngine.Object.Destroy(base.gameObject);
		}
	}
}
