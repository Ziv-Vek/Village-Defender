using UnityEditor;
using UnityEngine;

namespace VillageDefender
{
    public class Developer : MonoBehaviour
    {
        [MenuItem("Developer/Delete All PlayerPrefs")]
        public static void DeleteAllPlayerPrefs()
        {
            PlayerPrefs.DeleteAll();
            Debug.Log("All PlayerPrefs have been deleted!");
        }
    }
}
