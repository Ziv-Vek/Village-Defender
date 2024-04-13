using System.Collections;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using VillageDefender.Utils;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace VillageDefender.Systems
{
    public class SceneLoadingManager : Singleton<SceneLoadingManager>
    {
        [SerializeField] private LoadingScreen _loadingScreen;
        
        public async void LoadSceneAsync(int sceneIndex)
        {
            Debug.Log("Loading is called to load scene: " + sceneIndex);
            
            SceneManager.LoadSceneAsync(sceneIndex);
            /*var task = Task.Run( () => 
            {
                SceneManager.LoadSceneAsync(sceneIndex);
            });*/
        }
        
        public void LoadMainMenu()
        {
            StartCoroutine(LoadMainMenuAsync());
        }

        private IEnumerator LoadMainMenuAsync()
        {
            _loadingScreen.Show();
            
            
            yield return SceneManager.LoadSceneAsync(1);
            
            //yield return new WaitForSeconds(10f);
            
            _loadingScreen.Hide();
        }
    }
}