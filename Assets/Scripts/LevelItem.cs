using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class LevelItem : MonoBehaviour
{
    public Text txt;

    public GameObject lockObj;

    public Sprite starOff;

    public Sprite starOn;

    public Image shortCut;

    public Image[] starItems;

    [HideInInspector]
    public int levelIndex;

    private bool unlocked;

    private void Start()
    {
        this.txt.text = LocalizationManager.Instance.getLocalizeString("Level") + " " + (LevelManager.getLevelCountBeforeSection(LevelManager.sectionIndex) + this.levelIndex + 1);
        if (this.levelIndex > PrefManager.getUnlockLevel(LevelManager.sectionIndex))
        {
            this.lockObj.SetActive(true);
            this.unlocked = false;
        }
        else
        {
            this.unlocked = true;
        }
        base.GetComponent<Button>().onClick.AddListener(new UnityAction(this.onClick));
        int star = PrefManager.getStar(LevelManager.sectionIndex, this.levelIndex);
        for (int i = 0; i < 3; i++)
        {
            this.starItems[i].overrideSprite = ((i >= star) ? this.starOff : this.starOn);
        }
        LevelManager.Section currentSection = LevelManager.getCurrentSection();
        this.shortCut.overrideSprite = Resources.Load<Sprite>(string.Concat(new object[]
        {
            "LevelConfig/LevelImg/",
            currentSection.name,
            "/",
            currentSection.sceneName[this.levelIndex]
        }));
    }

    public void onClick()
    {
        if (this.unlocked)
        {
            LevelManager.levelIndex = this.levelIndex;
            LevelManager.playGame();
        }
        else
        {
            // DialogController.instance.ShowDialog(DialogType.levelUnlockTip, DialogShow.REPLACE_CURRENT, string.Empty);
        }
    }
}
