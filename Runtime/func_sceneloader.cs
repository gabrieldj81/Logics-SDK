//Created by: Cake Logic
using UnityEngine;
using UnityEngine.SceneManagement;

namespace CakeLogicSDK {
[AddComponentMenu("Cake Logic/Logic's SDK/func_sceneloader")]
public class func_sceneloader : MonoBehaviour
{
    private func_manager manager;
    public void LoadScene(string scene)
    {
      if(manager == null)
      {
        manager = func_manager.Instance;
      }
      manager.LoadingScene();
      SceneManager.LoadScene(scene);
    }
}
}