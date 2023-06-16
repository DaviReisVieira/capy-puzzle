using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class LevelManager
{
    [Serializable]
    public class Section
    {
        public int unlockStarCount;

        public string name;

        public List<int> sceneName;

        public int totalStars;

        public int starGeted;
    }

    private static List<LevelManager.Section> levelData;

    public static int sectionIndex;

    public static int levelIndex;

    public static int totalStars;

    public static int totalStarGeted;

    private static string lastSceneName;

    public static int sameLevelCount;

    private static MessageManager.funsig<int> __f__mg_cache0;

    static LevelManager()
    {
        LevelManager.levelData = new List<LevelManager.Section>();
        MessageManager arg_2D_0 = MessageManager.instance;
        MessageManager.Event arg_2D_1 = MessageManager.Event.STARS_UPDATE;
        if (LevelManager.__f__mg_cache0 == null)
        {
            LevelManager.__f__mg_cache0 = new MessageManager.funsig<int>(LevelManager.onStarUpdate);
        }
        arg_2D_0.Subscribe(arg_2D_1, LevelManager.__f__mg_cache0);
        string text = Resources.Load<TextAsset>("LevelConfig/sectionConfig").text;
        string[] array = text.Split(new char[]
        {
            '\r',
            '\n'
        }, StringSplitOptions.RemoveEmptyEntries);
        int num = 0;
        string[] array2 = array;
        for (int i = 0; i < array2.Length; i++)
        {
            string json = array2[i];
            LevelManager.Section section = JsonUtility.FromJson<LevelManager.Section>(json);
            int count = section.sceneName.Count;
            int num2 = 0;
            for (int j = 0; j < count; j++)
            {
                num2 += PrefManager.getStar(num, j);
            }
            section.starGeted = num2;
            section.totalStars = count * 3;
            LevelManager.levelData.Add(section);
            LevelManager.totalStarGeted += num2;
            LevelManager.totalStars += section.totalStars;
            num++;
        }
    }

    private static void onStarUpdate(int index)
    {
        LevelManager.Section section = LevelManager.levelData[index];
        int count = section.sceneName.Count;
        int num = 0;
        for (int i = 0; i < count; i++)
        {
            num += PrefManager.getStar(index, i);
        }
        LevelManager.totalStarGeted += num - section.starGeted;
        section.starGeted = num;
    }

    public static int getLevelCountBeforeSection(int sectionIndex)
    {
        int num = 0;
        for (int i = 0; i < sectionIndex; i++)
        {
            num += LevelManager.levelData[i].sceneName.Count;
        }
        return num;
    }

    public static string getLevelName()
    {
        return LevelManager.getLevelCountBeforeSection(LevelManager.sectionIndex) + LevelManager.levelIndex + 1 + string.Empty;
    }

    public static string getAnalyticLevelName()
    {
        string text = LevelManager.levelData[LevelManager.sectionIndex].name;
        text = text.Replace(" ", "_");
        return text + "-" + (LevelManager.levelIndex + 1);
    }

    public static LevelManager.Section getCurrentSection()
    {
        return LevelManager.levelData[LevelManager.sectionIndex];
    }

    public static LevelManager.Section getSection(int index)
    {
        if (index >= LevelManager.levelData.Count)
        {
            index = 0;
        }
        return LevelManager.levelData[index];
    }

    public static int getSectionCount()
    {
        return LevelManager.levelData.Count;
    }

    public static int getContinueSectionIndex()
    {
        LevelManager.sectionIndex = 0;
        for (int i = LevelManager.levelData.Count - 1; i >= 0; i--)
        {
            if (LevelManager.levelData[i].unlockStarCount <= LevelManager.totalStarGeted)
            {
                int unlockLevel = PrefManager.getUnlockLevel(i);
                if (unlockLevel != 0)
                {
                    LevelManager.sectionIndex = i;
                    break;
                }
                if (i != 0 && PrefManager.getUnlockLevel(i - 1) >= LevelManager.levelData[i - 1].sceneName.Count)
                {
                    LevelManager.sectionIndex = i;
                    break;
                }
            }
        }
        return LevelManager.sectionIndex;
    }

    public static void continueGamePlay()
    {
        LevelManager.sectionIndex = LevelManager.getContinueSectionIndex();
        LevelManager.levelIndex = PrefManager.getUnlockLevel(LevelManager.sectionIndex);
        if (LevelManager.levelIndex >= LevelManager.levelData[LevelManager.sectionIndex].sceneName.Count)
        {
            LevelManager.levelIndex = LevelManager.levelData[LevelManager.sectionIndex].sceneName.Count - 1;
        }
        LevelManager.playGame();
    }

    public static void playGame()
    {
        if (LevelManager.levelIndex >= LevelManager.levelData[LevelManager.sectionIndex].sceneName.Count)
        {
            LevelManager.levelIndex = LevelManager.levelData[LevelManager.sectionIndex].sceneName.Count - 1;
        }
        string text = LevelManager.levelData[LevelManager.sectionIndex].name + "_" + LevelManager.levelData[LevelManager.sectionIndex].sceneName[LevelManager.levelIndex];
        if (text == LevelManager.lastSceneName)
        {
            LevelManager.sameLevelCount++;
        }
        else
        {
            LevelManager.sameLevelCount = 0;
            LevelManager.lastSceneName = text;
        }
        SceneAnimCoverManager.Instance.loadScene(text);
    }

    public static void playNext()
    {
        LevelManager.levelIndex++;
        if (LevelManager.levelIndex >= LevelManager.levelData[LevelManager.sectionIndex].sceneName.Count)
        {
            if (LevelManager.sectionIndex >= LevelManager.levelData.Count - 1)
            {
                // SceneAnimCoverManager.Instance.loadScene("Section");
            }
            else
            {
                LevelManager.sectionIndex++;
                LevelManager.levelIndex = 0;
                if (LevelManager.levelData[LevelManager.sectionIndex].unlockStarCount > LevelManager.totalStarGeted)
                {
                    // SectionController.showStarUnlockTip = true;
                    // SceneAnimCoverManager.Instance.loadScene("Section");
                }
                else
                {
                    LevelManager.playGame();
                }
            }
        }
        else
        {
            LevelManager.playGame();
        }
    }
}
