using System;
using System.IO;
using UnityEngine;

public class FileHelper
{
    public static T GetJsonDataFromResource<T>(string path)
    {
        TextAsset textAsset = Resources.Load<TextAsset>(path);
        if (textAsset == null)
        {
            return default(T);
        }
        return JsonUtility.FromJson<T>(textAsset.text);
    }

    public static T GetJsonDataFromCaches<T>(string path)
    {
        T result;
        try
        {
            if (File.Exists(path))
            {
                string json = File.ReadAllText(path);
                result = JsonUtility.FromJson<T>(json);
            }
            else
            {
                result = default(T);
            }
        }
        catch (Exception ex)
        {
            UnityEngine.Debug.LogError(string.Concat(new object[]
            {
                "GetDataFromCaches Exception:",
                path,
                "    ",
                ex
            }));
            result = default(T);
        }
        return result;
    }

    public static string readAllText(string filePath)
    {
        if (File.Exists(filePath))
        {
            return File.ReadAllText(filePath);
        }
        return null;
    }

    public static void WriteAllText(string filePath, string content)
    {
        try
        {
            string directoryName = Path.GetDirectoryName(filePath);
            if (!Directory.Exists(directoryName))
            {
                Directory.CreateDirectory(directoryName);
            }
            File.WriteAllText(filePath, content);
        }
        catch (Exception ex)
        {
            UnityEngine.Debug.LogError(string.Concat(new object[]
            {
                "FileWrite:",
                ex,
                "    ",
                filePath,
                "   ",
                content
            }));
        }
    }

    public static void DeleteRemoteConfigCacheFile()
    {
        if (File.Exists(PathHelper.CACHE_REMOTE_CONFIG_FILE))
        {
            File.Delete(PathHelper.CACHE_REMOTE_CONFIG_FILE);
        }
    }
}
