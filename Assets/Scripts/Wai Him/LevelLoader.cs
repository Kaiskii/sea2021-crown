using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    public Animator transition;

    public float transitionTime = 1f;

    [SerializeField]
    private string Scene;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.N)) 
        {
            LoadNextLevel();
        }
    }
    public void LoadNextLevel() 
    {
        StartCoroutine(LoadLevel(Scene));
    }

    IEnumerator LoadLevel(string levelIndex)
    {
        transition.SetTrigger("Start");

        yield return new WaitForSeconds(transitionTime);

        SceneManager.LoadScene(levelIndex);

    }

}
