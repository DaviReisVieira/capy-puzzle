using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class FailToTry : MonoBehaviour
{
	private sealed class _countDown_c__Iterator0 : IEnumerator, IDisposable, IEnumerator<object>
	{
		internal FailToTry _this;

		internal object _current;

		internal bool _disposing;

		internal int _PC;

		object IEnumerator<object>.Current
		{
			get
			{
				return this._current;
			}
		}

		object IEnumerator.Current
		{
			get
			{
				return this._current;
			}
		}

		public _countDown_c__Iterator0()
		{
		}

		public bool MoveNext()
		{
			uint num = (uint)this._PC;
			this._PC = -1;
			switch (num)
			{
			case 0u:
				this._this.txt.text = "3";
				this._current = new WaitForSeconds(1f);
				if (!this._disposing)
				{
					this._PC = 1;
				}
				return true;
			case 1u:
				this._this.txt.text = "2";
				this._current = new WaitForSeconds(1f);
				if (!this._disposing)
				{
					this._PC = 2;
				}
				return true;
			case 2u:
				this._this.txt.text = "1";
				this._current = new WaitForSeconds(1f);
				if (!this._disposing)
				{
					this._PC = 3;
				}
				return true;
			case 3u:
				UnityEngine.Object.FindObjectOfType<GameController>().onReset();
				this._PC = -1;
				break;
			}
			return false;
		}

		public void Dispose()
		{
			this._disposing = true;
			this._PC = -1;
		}

		public void Reset()
		{
			throw new NotSupportedException();
		}
	}

	public Text txt;

	private void Start()
	{
		base.StartCoroutine(this.countDown());
	}

	private IEnumerator countDown()
	{
		FailToTry._countDown_c__Iterator0 _countDown_c__Iterator = new FailToTry._countDown_c__Iterator0();
		_countDown_c__Iterator._this = this;
		return _countDown_c__Iterator;
	}
}
