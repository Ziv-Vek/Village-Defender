using System.Collections;
using Cysharp.Threading.Tasks;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace VillageDefender
{
    [DefaultExecutionOrder(-2)]
    [InitializeOnLoad]
    public static class Preloader
    {
        static Preloader() => EditorApplication.playModeStateChanged += Initialize;

        static async void Initialize(PlayModeStateChange state)
        {
            Debug.Log("Preloading Initialization Scene!");
            var index = SceneManager.GetActiveScene().buildIndex;
            if (state == PlayModeStateChange.ExitingEditMode)
            {
                EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo();
            }

            if (state == PlayModeStateChange.EnteredPlayMode)
            {
                var loadSceneOperation = SceneManager.LoadSceneAsync(0);
                await loadSceneOperation;
                
                if (loadSceneOperation.isDone)
                {
                    Debug.Log("Preloader finished loading initialization scene. Proceeding to the next scene.");
                    SceneManager.LoadScene(index);
                }
                else
                {
                    Debug.LogError("Preloader failed to load initialization scene.");
                }
            }
        }
    }
}