using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class FilterButtonHandler : MonoBehaviour {

    private static string lastScene = "";

	public void ChangeFilter(string name)
    {
        if (lastScene != "")
        {
            Debug.Log("removeScene");
            SceneManager.UnloadSceneAsync(lastScene);
        }
        HomePage.Instance.RemoveDefaultImage();
        SceneManager.LoadScene(name, LoadSceneMode.Additive);
        lastScene = name;
    }
    
}
