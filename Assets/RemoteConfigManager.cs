using System;
using UnityEngine;
using Firebase.RemoteConfig;
using System.Threading.Tasks;
using Firebase.Extensions;

[Serializable]
public class ConfigData
{
    public string playerName;
    public float gameVersion;
    public int crrLevel;
    public bool over18;
}

public class RemoteConfigManager : MonoBehaviour
{
    public ConfigData configData;
    
    private void Awake()
    {
        //print("json: " + JsonUtility.ToJson(configData));
        CheckRemoteConfigValues();
    }

    public Task CheckRemoteConfigValues()
    {
        Debug.Log("Fetching remote config values...");
        Task fetchTask = FirebaseRemoteConfig.DefaultInstance.FetchAsync(TimeSpan.Zero);
        return fetchTask.ContinueWithOnMainThread(FetchComplete);
    }

    private void FetchComplete(Task fetchTask)
    {
        if (!fetchTask.IsCompleted)
        {
            Debug.LogError("Retrieval of remote config values hasn't finished!");
            return;
        }

        var remoteConfig = FirebaseRemoteConfig.DefaultInstance;
        var info = remoteConfig.Info;
        if (info.LastFetchStatus != LastFetchStatus.Success)
        {
            Debug.LogError($"{nameof(FetchComplete)} was not successful: {info.LastFetchStatus}");
            return;
        }

        remoteConfig.ActivateAsync()
            .ContinueWithOnMainThread(task =>
            {
                Debug.Log($"Remote config values fetched successfully! Last fetch time {info.FetchTime}");
                
                string configData = remoteConfig.GetValue("gameData").StringValue;
                this.configData = JsonUtility.FromJson<ConfigData>(configData);
                
                /*print("Amount of kvp in remote config: " + remoteConfig.AllValues.Count);
                foreach (var kvp in remoteConfig.AllValues)
                {
                    Debug.Log($"{kvp.Key} = {kvp.Value.StringValue}");
                }*/
            });
    }
}
