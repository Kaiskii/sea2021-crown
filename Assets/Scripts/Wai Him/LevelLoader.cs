using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class LevelLoader : MonoBehaviour {


    public Animator transition;

    public float transitionTime = 2f;

    [SerializeField]
    private string Scene;

    /*public void LoadNextLevel() {
        StartCoroutine(LoadLevel(Scene));
    }*/

    public void LoadLevelGUI(SwipeMenu menu) {
        int count = 0;
        for (int i = 0; i < menu.transform.childCount; i++) {
            if (EventSystem.current.currentSelectedGameObject.name == menu.transform.GetChild(i).name) {
                count = i;
                break;
            }
        }

        StartCoroutine(LoadLevel(menu.levelName[count]));
    }

    public void ReloadLevel()
    {
      StartCoroutine(LoadLevel(SceneManager.GetActiveScene().name));
    }

    public void ReturnToMenu()
    {
      StartCoroutine(LoadLevel("TitleScreen"));
    }

    public IEnumerator LoadLevel(string levelIndex) {
        transition.SetTrigger("Start");

        yield return new WaitForSeconds(transitionTime);

        SceneManager.LoadScene(levelIndex);
    }

    public void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.CompareTag("Player") && !other.isTrigger) 
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }

}