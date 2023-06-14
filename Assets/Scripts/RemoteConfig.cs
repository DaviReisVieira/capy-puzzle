using System;
using System.IO;
using UnityEngine;

[Serializable]
public class RemoteConfig
{
    private static RemoteConfig _Instance;

    public float interstitial_pause_interval = 60f;

    public int interstitial_level_interval = 3;

    public int interstitial_level_show = 10;

    public int banner_level_show = 5;

    public float interstitial_time_interval = 120f;

    public int hint_free_level = 10;

    public int hint_hand_level_max = 15;

    public string promote_man_id = "18";

    public int[] promote_man_level = new int[]
    {
        12,
        25,
        45
    };

    public int fiveStarRateLevel = 6;

    public int collectStarShowCount = 5;

    public int freePrize_status = 1;

    public int timeGold_total = 24000; // test: 24000

    public int timeGold_count = 12; // 12: 2 hours, 2880: 30seg

    public int[] interstitial_level_interval_new = new int[]
    {
        5,
        5,
        4,
        3,
        3,
        3,
        3
    };

    public static RemoteConfig Instance
    {
        get
        {
            if (RemoteConfig._Instance == null)
            {
                RemoteConfig.init();
            }
            return RemoteConfig._Instance;
        }
        set
        {
            RemoteConfig._Instance = value;
        }
    }

    private static void init()
    {
        if (File.Exists(PathHelper.CACHE_REMOTE_CONFIG_FILE))
        {
            string json = FileHelper.readAllText(PathHelper.CACHE_REMOTE_CONFIG_FILE);
            RemoteConfig._Instance = JsonUtility.FromJson<RemoteConfig>(json);
        }
        else
        {
            RemoteConfig._Instance = new RemoteConfig();
        }
    }

    public int getInterstitialLevelInterval()
    {
        if (this.interstitial_level_interval_new == null || this.interstitial_level_interval_new.Length == 0)
        {
            this.interstitial_level_interval_new = new int[]
            {
                5,
                5,
                4,
                3,
                3,
                3,
                3
            };
        }
        int num = this.getInterstitialLevelIntervalUseIndex();
        if (num >= this.interstitial_level_interval_new.Length)
        {
            num = this.interstitial_level_interval_new.Length - 1;
        }
        UnityEngine.Debug.LogError(string.Concat(new object[]
        {
            "interstitial_level_interval_new:",
            num,
            " value:",
            this.interstitial_level_interval_new[num]
        }));
        return this.interstitial_level_interval_new[num];
    }

    public void onUserEnter()
    {
        if (Util.GetCurrentTimeInDays() > this.getInterstitialLevelIntervalDay())
        {
            this.setInterstitialLevelIntervalUseIndex(this.getInterstitialLevelIntervalUseIndex() + 1);
        }
        this.setInterstitialLevelIntervalDay();
    }

    private void setInterstitialLevelIntervalDay()
    {
        PlayerPrefs.SetInt("InterstitialLevelInterval_day", Util.GetCurrentTimeInDays());
    }

    private int getInterstitialLevelIntervalDay()
    {
        return PlayerPrefs.GetInt("InterstitialLevelInterval_day", 0);
    }

    private void setInterstitialLevelIntervalUseIndex(int count)
    {
        PlayerPrefs.SetInt("InterstitialLevelInterval_useIndex", count);
    }

    private int getInterstitialLevelIntervalUseIndex()
    {
        return PlayerPrefs.GetInt("InterstitialLevelInterval_useIndex", -1);
    }
}
