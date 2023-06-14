using System;
using System.IO;
using UnityEngine;

public sealed class PathHelper
{
    public static readonly string CACHE_ROOT_DIRECTORY = Application.persistentDataPath;

    public static string CACHE_DATA_DIRECTORY = Path.Combine(PathHelper.CACHE_ROOT_DIRECTORY, "data");

    public static string CACHE_REMOTE_CONFIG_FILE = Path.Combine(PathHelper.CACHE_DATA_DIRECTORY, "remote.data");

    public static string CACHE_BANNER_REMOTE_CONFIG_FILE = Path.Combine(PathHelper.CACHE_DATA_DIRECTORY, "banner_remote.data");

    public static string CACHE_SALE_REMOTE_CONFIG_FILE = Path.Combine(PathHelper.CACHE_DATA_DIRECTORY, "sale_remote.data");

    public static readonly string RES_ROOT = Path.Combine(Path.Combine(Environment.CurrentDirectory, "Assets"), "Resources");

    public static string getResPath(string path)
    {
        return Path.Combine(PathHelper.RES_ROOT, path);
    }
}
