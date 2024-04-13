using System.Threading.Tasks;
using Firebase;
using Firebase.Analytics;
using VillageDefender.Utils;

public class FirebaseInit : Singleton<FirebaseInit>
{
    public async Task Init()
    {
        var dependencyStatus = await FirebaseApp.CheckAndFixDependenciesAsync().ConfigureAwait(false);
        if (dependencyStatus == DependencyStatus.Available)
        {
            FirebaseAnalytics.SetAnalyticsCollectionEnabled(true);
        }
        else
        {
            UnityEngine.Debug.LogError(System.String.Format(
                "Could not resolve all Firebase dependencies: {0}", dependencyStatus));
        }
    }
}