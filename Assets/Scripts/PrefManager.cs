using System;
using System.Collections.Generic;
using UnityEngine;

public class PrefManager
{
    public static void setBgMusiceEnable(bool on)
    {
        PlayerPrefs.SetInt("BgMusiceEnable", (!on) ? 0 : 1);
    }

    public static bool isBgMusiceEnable()
    {
        return PlayerPrefs.GetInt("BgMusiceEnable", 1) == 1;
    }

    public static void setSoundEnable(bool enable)
    {
        PlayerPrefs.SetInt("SoundEnable", (!enable) ? 0 : 1);
    }

    public static bool isSoundEnable()
    {
        return PlayerPrefs.GetInt("SoundEnable", 1) != 0;
    }

    public static void setLocalization(string lan)
    {
        PlayerPrefs.SetString("Localization_lan", lan);
    }

    public static string getLocalizetion()
    {
        return PlayerPrefs.GetString("Localization_lan", string.Empty);
    }

    public static void setCoin(int coin)
    {
        PlayerPrefs.SetInt("coin", coin);
    }

    public static int getCoin()
    {
        return PlayerPrefs.GetInt("coin", 0);
    }

    public static void setTimeGoldBeginTime()
    {
        PlayerPrefs.SetString("TimeGoldBeginTime", Util.GetCurrentTimeInDouble() + string.Empty);
    }

    public static double getTimeGoldBeginTime()
    {
        return double.Parse(PlayerPrefs.GetString("TimeGoldBeginTime", "0"));
    }

    // game level managment
    public static void setStar(int sectionIndex, int level, int star)
    {
        PlayerPrefs.SetInt(string.Concat(new object[]
        {
            "levelStar_",
            sectionIndex,
            "_",
            level
        }), star);
    }

    public static int getStar(int sectionIndex, int level)
    {
        return PlayerPrefs.GetInt(string.Concat(new object[]
        {
            "levelStar_",
            sectionIndex,
            "_",
            level
        }), 0);
    }

    public static void setUnlockLevel(int sectionIndex, int level)
    {
        PlayerPrefs.SetInt("unlockLevel_" + sectionIndex, level);
    }

    public static int getUnlockLevel(int sectionIndex)
    {
        return PlayerPrefs.GetInt("unlockLevel_" + sectionIndex, 0);
    }

    // level design
    public static void setHintUsed()
    {
        PlayerPrefs.SetInt("HintUsed", 1);
    }

    public static void setCollectStarShowCount(int count)
    {
        PlayerPrefs.SetInt("CollectStarShowCount_" + Util.GetCurrentTimeInDays(), count);
    }

    public static int getCollectStarShowCount()
    {
        return PlayerPrefs.GetInt("CollectStarShowCount_" + Util.GetCurrentTimeInDays(), 0);
    }

    public static void setPromoteManUnlocked()
    {
        PlayerPrefs.SetInt("PromoteManUnlocked", 1);
    }

    public static bool isPromoteManUnlocked()
    {
        return PlayerPrefs.GetInt("PromoteManUnlocked", 0) != 0;
    }

    public static void setLotteryUsed()
    {
        PlayerPrefs.SetInt("LotteryUsed", 1);
    }

    public static bool isLotteryUsed()
    {
        return PlayerPrefs.GetInt("LotteryUsed", 0) != 0;
    }

    public static void setLevelPassed(int passedLevels)
    {
        PlayerPrefs.SetInt("LevelPassed", passedLevels);
    }

    public static int getLevelPassed()
    {
        return PlayerPrefs.GetInt("LevelPassed", 0);
    }

    public static void setWinHandShowed()
    {
        PlayerPrefs.SetInt("WinHandShowed", 1);
    }

    public static bool isWinHandShowed()
    {
        return PlayerPrefs.GetInt("WinHandShowed", 0) != 0;
    }

    public static void setManPromoteShowCount(int count)
    {
        PlayerPrefs.SetInt("ManPromoteShowCount", count);
    }

    public static int getManPromoteShowCount()
    {
        return PlayerPrefs.GetInt("ManPromoteShowCount", 0);
    }
}
