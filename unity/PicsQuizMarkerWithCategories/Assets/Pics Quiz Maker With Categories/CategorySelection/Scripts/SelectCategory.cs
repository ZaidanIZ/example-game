using UnityEngine;
using UnityEngine.UI;

public class SelectCategory : MonoBehaviour {

    [SerializeField] GameObject categoryPrefab;
    int totalSolvedLevels;

    // Use this for initialization
    void Start ()
    {

        GameObject databaseGO = GameObject.Find("DATABASES");
        foreach (Transform child in databaseGO.transform)
        {
            GameObject go = Instantiate(categoryPrefab, Vector3.zero, Quaternion.identity) as GameObject;
            //change the text of "Solved questions" for the translation
            go.transform.Find("QuestionsSolved").GetComponent<Text>().text = child.GetComponent<Word_Database>().uiTextsLang[PlayerPrefs.GetInt("numberLanguae"), 17];
            go.transform.SetParent(GameObject.Find("CATEGORY_SELECTION").transform.Find("Canvas").transform.Find("Mask").transform.Find("ScrollRect").transform.Find("Levels_Container").transform);
            go.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
            go.transform.Find("NameLevel").GetComponent<Text>().text = child.GetComponent<Word_Database>().nameOfCategory[PlayerPrefs.GetInt("numberLanguae")];
            
            for (int i = 0; i < child.GetComponent<Word_Database>().solvedWords.Length; i++)
            {
                if (child.GetComponent<Word_Database>().solvedWords[i])
                {
                    totalSolvedLevels++;
                }
            }

         
            go.transform.Find("NumberSolved").GetComponent<Text>().text = totalSolvedLevels + "/"+child.GetComponent<Word_Database>().words_List.Length;
            float porcent = (totalSolvedLevels * 100) / child.GetComponent<Word_Database>().words_List.Length;
            go.transform.Find("PorcentBar").transform.Find("Porcent").GetComponent<Text>().text = porcent + "%";
            go.transform.Find("PorcentBar").transform.Find("WhiteBar").GetComponent<RectTransform>().localPosition = new Vector3(563 * (porcent/100), 0, 0);
            go.name = child.name;
            if (child.GetComponent<Word_Database>().howManyQuizSolvedNeedToUnlock > CategoryControlOther.totalSolvedWords)
            {
                go.GetComponent<Image>().color = new Vector4 (0, 0.5f, 0.65f, 1);
                go.transform.Find("PorcentBar").transform.Find("Porcent").GetComponent<Text>().text = child.GetComponent<Word_Database>().uiTextsLang[PlayerPrefs.GetInt("numberLanguae"), 18] + child.GetComponent<Word_Database>().howManyQuizSolvedNeedToUnlock+ child.GetComponent<Word_Database>().uiTextsLang[PlayerPrefs.GetInt("numberLanguae"), 19];
                Destroy(go.GetComponent<Button>());
                go.transform.Find("Locked").gameObject.SetActive(true);
                Destroy(go.transform.Find("PorcentBar").GetComponent<Mask>());
                Destroy(go.transform.Find("PorcentBar").GetComponent<Image>());
                Destroy(go.transform.Find("PorcentBar").transform.Find("WhiteBar").GetComponent<Image>());

            }

            totalSolvedLevels = 0;
        }
	}

}
