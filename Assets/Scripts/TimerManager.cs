using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class TimerManager : MonoBehaviour
{
	private List<Timer> _timers = new List<Timer>();

	private List<Timer> _timersToAdd = new List<Timer>();

	private static Predicate<Timer> __f__am_cache0;

	private void Start()
	{
		UnityEngine.Object.DontDestroyOnLoad(base.gameObject);
	}

	public void RegisterTimer(Timer timer)
	{
		this._timersToAdd.Add(timer);
	}

	public void CancelAllTimers()
	{
		foreach (Timer current in this._timers)
		{
			current.Cancel();
		}
		this._timers = new List<Timer>();
		this._timersToAdd = new List<Timer>();
	}

	private void Update()
	{
		this.UpdateAllTimers();
	}

	private void UpdateAllTimers()
	{
		if (this._timersToAdd.Count > 0)
		{
			this._timers.AddRange(this._timersToAdd);
			this._timersToAdd.Clear();
		}
		foreach (Timer current in this._timers)
		{
			current.Update();
		}
		this._timers.RemoveAll((Timer t) => t.isDone);
	}

	private void OnDestroy()
	{
		this.CancelAllTimers();
	}
}
