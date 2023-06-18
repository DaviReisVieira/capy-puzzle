using System;
using UnityEngine;
using UnityEngine.UI;

public class DialogPause : Dialog
{
	public Text title;

	public Image sound;

	public Image music;

	public Sprite soundOn;

	public Sprite soundOff;

	public Sprite musicOn;

	public Sprite musicOff;

	protected override void Awake()
	{
		base.Awake();
		SceneAnimCoverManager expr_0B = SceneAnimCoverManager.Instance;
		expr_0B.actionBeforeLoadScene = (Action)Delegate.Combine(expr_0B.actionBeforeLoadScene, new Action(this.Close));
	}

	private void OnDestroy()
	{
		SceneAnimCoverManager expr_05 = SceneAnimCoverManager.Instance;
		expr_05.actionBeforeLoadScene = (Action)Delegate.Remove(expr_05.actionBeforeLoadScene, new Action(this.Close));
	}

	protected override void Start()
	{
		base.Start();
		this.title.text = LocalizationManager.Instance.getLocalizeStringFormat("LEVEL_dialog_title", LevelManager.getLevelName());
		this.sound.overrideSprite = ((!PrefManager.isSoundEnable()) ? this.soundOff : this.soundOn);
		this.music.overrideSprite = ((!PrefManager.isBgMusiceEnable()) ? this.musicOff : this.musicOn);
	}

	public void onResume()
	{
		this.Close();
	}

	public void onLevel()
	{
		this.hidingAnimation = null;
		SceneAnimCoverManager.Instance.loadScene("Level");
	}

	public void onHome()
	{
		this.hidingAnimation = null;
		SceneAnimCoverManager.Instance.loadScene("Home");
	}

	public void onSound()
	{
		PrefManager.setSoundEnable(!PrefManager.isSoundEnable());
		this.sound.overrideSprite = ((!PrefManager.isSoundEnable()) ? this.soundOff : this.soundOn);
	}

	public void onMusic()
	{
		PrefManager.setBgMusiceEnable(!PrefManager.isBgMusiceEnable());
		this.music.overrideSprite = ((!PrefManager.isBgMusiceEnable()) ? this.musicOff : this.musicOn);
		if (PrefManager.isBgMusiceEnable())
		{
			SoundManager.Instance.playBg();
		}
		else
		{
			SoundManager.Instance.stopBg();
		}
	}
}
