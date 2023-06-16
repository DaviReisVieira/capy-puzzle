using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Timer
{
	private float _duration_k__BackingField;

	private bool _isLooped_k__BackingField;

	private bool _isCompleted_k__BackingField;

	private bool _usesRealTime_k__BackingField;

	private static TimerManager _manager;

	private readonly Action _onComplete;

	private readonly Action<float> _onUpdate;

	private float _startTime;

	private float _lastUpdateTime;

	private float? _timeElapsedBeforeCancel;

	private float? _timeElapsedBeforePause;

	private readonly MonoBehaviour _autoDestroyOwner;

	private readonly bool _hasAutoDestroyOwner;

	public float duration
	{
		get;
		private set;
	}

	public bool isLooped
	{
		get;
		set;
	}

	public bool isCompleted
	{
		get;
		private set;
	}

	public bool usesRealTime
	{
		get;
		private set;
	}

	public bool isPaused
	{
		get
		{
			return this._timeElapsedBeforePause.HasValue;
		}
	}

	public bool isCancelled
	{
		get
		{
			return this._timeElapsedBeforeCancel.HasValue;
		}
	}

	public bool isDone
	{
		get
		{
			return this.isCompleted || this.isCancelled || this.isOwnerDestroyed;
		}
	}

	private bool isOwnerDestroyed
	{
		get
		{
			return this._hasAutoDestroyOwner && this._autoDestroyOwner == null;
		}
	}

	private Timer(float duration, Action onComplete, Action<float> onUpdate, bool isLooped, bool usesRealTime, MonoBehaviour autoDestroyOwner)
	{
		this.duration = duration;
		this._onComplete = onComplete;
		this._onUpdate = onUpdate;
		this.isLooped = isLooped;
		this.usesRealTime = usesRealTime;
		this._autoDestroyOwner = autoDestroyOwner;
		this._hasAutoDestroyOwner = (autoDestroyOwner != null);
		this._startTime = this.GetWorldTime();
		this._lastUpdateTime = this._startTime;
	}

	public static Timer Register(float duration, Action onComplete, Action<float> onUpdate = null, bool isLooped = false, bool useRealTime = false, MonoBehaviour autoDestroyOwner = null)
	{
		if (Timer._manager == null)
		{
			TimerManager timerManager = UnityEngine.Object.FindObjectOfType<TimerManager>();
			if (timerManager != null)
			{
				Timer._manager = timerManager;
			}
			else
			{
				GameObject gameObject = new GameObject
				{
					name = "TimerManager"
				};
				Timer._manager = gameObject.AddComponent<TimerManager>();
			}
		}
		Timer timer = new Timer(duration, onComplete, onUpdate, isLooped, useRealTime, autoDestroyOwner);
		Timer._manager.RegisterTimer(timer);
		return timer;
	}

	public static void Cancel(Timer timer)
	{
		if (timer != null)
		{
			timer.Cancel();
		}
	}

	public static void Pause(Timer timer)
	{
		if (timer != null)
		{
			timer.Pause();
		}
	}

	public static void Resume(Timer timer)
	{
		if (timer != null)
		{
			timer.Resume();
		}
	}

	public static void CancelAllRegisteredTimers()
	{
		if (Timer._manager != null)
		{
			Timer._manager.CancelAllTimers();
		}
	}

	public void Cancel()
	{
		if (this.isDone)
		{
			return;
		}
		this._timeElapsedBeforeCancel = new float?(this.GetTimeElapsed());
		this._timeElapsedBeforePause = null;
	}

	public void Pause()
	{
		if (this.isPaused || this.isDone)
		{
			return;
		}
		this._timeElapsedBeforePause = new float?(this.GetTimeElapsed());
	}

	public void Resume()
	{
		if (!this.isPaused || this.isDone)
		{
			return;
		}
		this._timeElapsedBeforePause = null;
	}

	public float GetTimeElapsed()
	{
		if (this.isCompleted || this.GetWorldTime() >= this.GetFireTime())
		{
			return this.duration;
		}
		float? timeElapsedBeforeCancel = this._timeElapsedBeforeCancel;
		float arg_6E_0;
		if (timeElapsedBeforeCancel.HasValue)
		{
			arg_6E_0 = timeElapsedBeforeCancel.Value;
		}
		else
		{
			float? timeElapsedBeforePause = this._timeElapsedBeforePause;
			arg_6E_0 = ((!timeElapsedBeforePause.HasValue) ? (this.GetWorldTime() - this._startTime) : timeElapsedBeforePause.Value);
		}
		return arg_6E_0;
	}

	public float GetTimeRemaining()
	{
		return this.duration - this.GetTimeElapsed();
	}

	public float GetRatioComplete()
	{
		return this.GetTimeElapsed() / this.duration;
	}

	public float GetRatioRemaining()
	{
		return this.GetTimeRemaining() / this.duration;
	}

	private float GetWorldTime()
	{
		return (!this.usesRealTime) ? Time.time : Time.realtimeSinceStartup;
	}

	private float GetFireTime()
	{
		return this._startTime + this.duration;
	}

	private float GetTimeDelta()
	{
		return this.GetWorldTime() - this._lastUpdateTime;
	}

	public void Update()
	{
		if (this.isDone)
		{
			return;
		}
		if (this.isPaused)
		{
			this._startTime += this.GetTimeDelta();
			this._lastUpdateTime = this.GetWorldTime();
			return;
		}
		this._lastUpdateTime = this.GetWorldTime();
		if (this._onUpdate != null)
		{
			this._onUpdate(this.GetTimeElapsed());
		}
		if (this.GetWorldTime() >= this.GetFireTime())
		{
			if (this._onComplete != null)
			{
				this._onComplete();
			}
			if (this.isLooped)
			{
				this._startTime = this.GetWorldTime();
			}
			else
			{
				this.isCompleted = true;
			}
		}
	}
}
