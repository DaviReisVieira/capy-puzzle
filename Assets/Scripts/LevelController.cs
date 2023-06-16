using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class LevelController : BaseController
{
    private sealed class _initLevels_c__Iterator0 : IEnumerator, IDisposable, IEnumerator<object>
    {
        internal LevelManager.Section _section___0;

        internal int _levelCount___0;

        internal int _levelUnlocked___0;

        internal Transform _target___0;

        internal float _anchoredY___0;

        internal float _scrollerHeight___0;

        internal float _dest___0;

        internal LevelController _this;

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

        public _initLevels_c__Iterator0()
        {
        }

        public bool MoveNext()
        {
            uint num = (uint)this._PC;
            this._PC = -1;
            switch (num)
            {
                case 0u:
                    this._section___0 = LevelManager.getCurrentSection();
                    this._this.title.text = LocalizationManager.Instance.getLocalizeString(this._section___0.name);
                    this._this.stars.text = LevelManager.totalStarGeted + "/" + LevelManager.totalStars;
                    this._this.progressTxt.text = this._section___0.starGeted + "/" + this._section___0.totalStars;
                    this._this.progressImg.fillAmount = (float)PrefManager.getUnlockLevel(LevelManager.sectionIndex) * 1f / (float)this._section___0.sceneName.Count;
                    this._this.levelProgressTxt.text = PrefManager.getUnlockLevel(LevelManager.sectionIndex) + "/" + this._section___0.sceneName.Count;
                    this._levelCount___0 = this._section___0.sceneName.Count;
                    for (int i = 0; i < this._levelCount___0; i++)
                    {
                        // LevelItem levelItem = UnityEngine.Object.Instantiate<LevelItem>(this._this.levelItemPrefab);
                        // levelItem.levelIndex = i;
                        // levelItem.transform.SetParent(this._this.scrollView.content);
                        // levelItem.transform.localScale = Vector3.one;
                    }
                    this._current = 0;
                    if (!this._disposing)
                    {
                        this._PC = 1;
                    }
                    return true;
                case 1u:
                    this._levelUnlocked___0 = PrefManager.getUnlockLevel(LevelManager.sectionIndex);
                    if (this._levelUnlocked___0 >= this._section___0.sceneName.Count)
                    {
                        this._levelUnlocked___0 = this._section___0.sceneName.Count - 1;
                    }
                    this._target___0 = this._this.scrollView.content.GetChild(this._levelUnlocked___0);
                    this._anchoredY___0 = (this._target___0 as RectTransform).anchoredPosition.y;
                    this._scrollerHeight___0 = (this._this.scrollView.transform as RectTransform).rect.height;
                    this._dest___0 = this._anchoredY___0 * -1f - this._scrollerHeight___0 / 2f;
                    if (this._dest___0 < 0f)
                    {
                        this._dest___0 = 0f;
                    }
                    if (this._dest___0 > this._this.scrollView.content.sizeDelta.y - this._scrollerHeight___0)
                    {
                        this._dest___0 = this._this.scrollView.content.sizeDelta.y - this._scrollerHeight___0;
                    }
                    this._this.scrollView.content.anchoredPosition = Vector2.up * this._dest___0;
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

    // public LevelItem levelItemPrefab;

    public ScrollRect scrollView;

    public Text stars;

    public Text progressTxt;

    public Image progressImg;

    public Text title;

    public Text levelProgressTxt;

    private void Start()
    {
        base.StartCoroutine(this.initLevels());
    }

    private IEnumerator initLevels()
    {
        LevelController._initLevels_c__Iterator0 _initLevels_c__Iterator = new LevelController._initLevels_c__Iterator0();
        _initLevels_c__Iterator._this = this;
        return _initLevels_c__Iterator;
    }
}
