using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class CategoryControlOther : MonoBehaviour {


    GameObject cameraObj;
    public static int totalSolvedWords;
    int totalWords;
    void Start ()
    {
        totalSolvedWords = 0;
        cameraObj = GameObject.Find("Main Camera").gameObject;

        //Total solved words
        GameObject databaseGO = GameObject.Find("DATABASES");
        
        foreach (Transform child in databaseGO.transform)
        {
            foreach(bool solvedWord in child.GetComponent<Word_Database>().solvedWords)
            {
                if (solvedWord)
                {
                    totalSolvedWords++;
                }
                totalWords++;
            }
            
        }

        GameObject.Find("CATEGORY_SELECTION").transform.Find("Canvas").transform.Find("TopBox").transform.Find("SolvedWords").transform.Find("TotalSolvedWords").GetComponent<Text>().text = totalSolvedWords + "/" + totalWords;
        GameObject.Find("CATEGORY_SELECTION").transform.Find("Canvas").transform.Find("TopBox").transform.Find("TotalPorcent").transform.Find("TotalPorcent").GetComponent<Text>().text = ((totalSolvedWords * 100) / totalWords).ToString()+"%";

    }

	// Update is called once per frame
	void Update ()
    {

        if (Input.GetMouseButtonUp(0))
        {

            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10)), Vector2.zero);

            if (hit.collider != null)
            {
                GameObject objectHit = hit.collider.gameObject;
                if (objectHit.name == "GoCategorySelection")
                {
                    cameraObj.transform.position = new Vector3(-22, 0, -10);
                }
                else if (objectHit.name == "GoToMenu")
                {
                    cameraObj.transform.position = new Vector3(-32, 0, -10);
                }
            }
        }

    }
}
