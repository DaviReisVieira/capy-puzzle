using System;
using UnityEngine;

public class RealTimeParticle : MonoBehaviour
{
	private float _timeAtLastFrame;

	private ParticleSystem _particleSystem;

	private float _deltaTime;

	public void Awake()
	{
		this._timeAtLastFrame = Time.realtimeSinceStartup;
		this._particleSystem = base.gameObject.GetComponent<ParticleSystem>();
	}

	public void Update()
	{
		this._deltaTime = Time.realtimeSinceStartup - this._timeAtLastFrame;
		this._timeAtLastFrame = Time.realtimeSinceStartup;
		if (Time.timeScale == 0f)
		{
			this._particleSystem.Simulate(this._deltaTime, false, false);
			this._particleSystem.Play();
		}
	}
}
