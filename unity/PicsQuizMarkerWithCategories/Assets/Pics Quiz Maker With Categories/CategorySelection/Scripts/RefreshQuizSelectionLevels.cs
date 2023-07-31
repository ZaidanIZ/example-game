using UnityEngine;
using UnityEngine.UI;

public class RefreshQuizSelectionLevels : MonoBehaviour
{

    public static string selectedCategory = "";
    public static int levelSelected = -1;
    public GameObject levelImagePrefab;
    Word_Database Words_Database_Selected;
    int solvedWords;
    int totalWords;
    public void selectedThisGameObjectName ()
    {
        selectedCategory = gameObject.name;
        PlayerPrefs.SetString("SelectedCategory", selectedCategory);
    }
    void Start ()
    {

        if (GameObject.Find("Main Camera").transform.position.x == -11 && PlayerPrefs.HasKey("SelectedCategory") && gameObject.name == "Quiz_Selection_Controller")
        {
            refreshQuizLevels();
        }
        
    }
    public void refreshQuizLevels()
    {
        selectedCategory = PlayerPrefs.GetString("SelectedCategory");
        foreach (Transform transformObj in GameObject.Find("QUIZ_SELECTION").transform.Find("Canvas").transform.Find("Mask").transform.Find("ScrollRect").transform.Find("Levels_Container").transform)
        {
            DestroyImmediate(transformObj.gameObject);
        }

        Words_Database_Selected = GameObject.Find("DATABASES").transform.Find(selectedCategory).GetComponent<Word_Database>();

        GameObject.Find("QUIZ_SELECTION").transform.Find("Canvas").transform.Find("TopBox").transform.Find("Title").GetComponent<Text>().text = Words_Database_Selected.nameOfCategory[PlayerPrefs.GetInt("numberLanguae")];

        for (int i = 0; i < Words_Database_Selected.words_List.Length;)
        {
            for (int j = 0; j < 4; j++)
            {
                if (i < Words_Database_Selected.words_List.Length)
                {
                    GameObject go = Instantiate(GameObject.Find("Quiz_Selection_Controller").GetComponent<RefreshQuizSelectionLevels>().levelImagePrefab, new Vector3(-11, 0, 0), Quaternion.identity) as GameObject;
                    go.transform.SetParent(GameObject.Find("QUIZ_SELECTION").transform.Find("Canvas").transform.Find("Mask").transform.Find("ScrollRect").transform.Find("Levels_Container").transform);
                    go.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
                    go.transform.Find("Image").GetComponent<Image>().sprite = Words_Database_Selected.image[i];
                    if (Words_Database_Selected.solvedWords[i])
                    {
                        go.transform.Find("SolvedQuiz").gameObject.SetActive(true);
                        go.GetComponent<Image>().color = Color.green;
                        solvedWords++;
                    }
                    totalWords++;
                    go.name = i.ToString();
                    i++;
                }
            }
        }

        GameObject.Find("Main Camera").transform.position = new Vector3(-11, 0, -10);

        GameObject.Find("QUIZ_SELECTION").transform.Find("Canvas").transform.Find("TopBox").transform.Find("SolvedWords").transform.Find("TotalSolvedWords").GetComponent<Text>().text = solvedWords + "/" + totalWords;
        GameObject.Find("QUIZ_SELECTION").transform.Find("Canvas").transform.Find("TopBox").transform.Find("TotalPorcent").transform.Find("TotalPorcent").GetComponent<Text>().text = ((solvedWords * 100) / totalWords).ToString() + "%";

    }

    public void setLevelSelected()
    {
          levelSelected = int.Parse(gameObject.name);
          Application.LoadLevel(Application.loadedLevelName);
          GameObject.Find("Main Camera").GetComponent<Animation>().Play("startScene");
          GameObject.Find("Main Camera").transform.position = new Vector3(0, 0, -10);
     
    }
}
