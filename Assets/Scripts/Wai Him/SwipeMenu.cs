using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SwipeMenu : MonoBehaviour
{

    public GameObject scrollbar;
    float scroll_pos = 0;
    float[] pos;

    private string sceneName;

    public List<string> levelName = new List<string>();

    [SerializeField]
    public Button[] buttons;

    public GameObject buttonPrefab;
    
    // Start is called before the first frame update
    void Start()
    {
        initializeButtons();
        //GetComponent<LevelLoader>().LoadNextLevel(sceneName);
    }

    // Update is called once per frame
    void Update()
    {

        pos = new float[transform.childCount];
        float distance = 1f / (pos.Length - 1f);
        for (int i = 0; i < pos.Length; i++) 
        {
            pos[i] = distance * i;
        }
        if (Input.GetMouseButton(0)) 
        {
            scroll_pos = scrollbar.GetComponent<Scrollbar>().value;
        }
        else
        {
            for (int i = 0; i < pos.Length; i++)
            {
                if (scroll_pos < pos [i] + (distance / 2) && scroll_pos > pos [i] - (distance / 2)) 
                {
                    sceneName = levelName[i];
                    scrollbar.GetComponent<Scrollbar>().value = Mathf.Lerp(scrollbar.GetComponent<Scrollbar>().value, pos[i], 0.1f);
                }
            }
        }

        for (int i = 0; i < pos.Length; i++) {
            if (scroll_pos < pos[i] + (distance / 2) && scroll_pos > pos[i] - (distance / 2)) {
                transform.GetChild(i).localScale = Vector2.Lerp(transform.GetChild(i).localScale, new Vector2(1f, 1f), 0.1f);
                for (int j = 0; j < pos.Length; j++) {
                    if (j != i) {
                        transform.GetChild(j).localScale = Vector2.Lerp(transform.GetChild(j).localScale, new Vector2(0.5f, 0.5f), 0.1f);
                    }
                }
            }
        }
    }

    public void initializeButtons()
    {
        Text buttonName;

        for (int i = 0; i < levelName.Count; i++) 
        {
            buttonName = buttons[i].GetComponentInChildren<Text>();
            buttons[i].gameObject.SetActive(true);
            buttonName.text = levelName[i];
            

        }
    }


}
