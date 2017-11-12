using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class csAchievements : MonoBehaviour
{
    public GameObject window, button, descripgo, valuego, nodesgo, nodehighlight, crystals;

    private TextMesh descrip, value;
    private List<Achievement> achs;
    private List<GameObject> nodes;
    private bool clicked, anim;
    private float t = 0;
    private Vector3 pos;

	void Start() 
	{
        descrip = descripgo.GetComponent<TextMesh>();
        value = valuego.GetComponent<TextMesh>();
        achs = new List<Achievement>();
        nodes = new List<GameObject>();

        crystals.GetComponent<TextMesh>().text = "x" + PlayerData.crystalqty.ToString();

        foreach (KeyValuePair<int, Achievement> ach in csAchieveDatabase.achieveDB)
            achs.Add(ach.Value);
        foreach (Transform child in nodesgo.transform)
            nodes.Add(child.gameObject);

        pos = button.transform.position;
        ClearText();
	}

	void Update()
	{
        if (anim == true)
            ButtonAnim();
    }

    private void ClearText()
    {
        descrip.text = "";
        value.text = "";
    }

    public void clickAchievementNode(GameObject go)
    {
        int index = nodes.IndexOf(go);

        descrip.text = csAchieveDatabase.achieveDB[index].description;
        value.text = csAchieveDatabase.achieveDB[index].reward.ToString();
        nodehighlight.transform.position = new Vector3(go.transform.position.x, go.transform.position.y, -.3f);
    }

    public bool click_achievementsbutton()
    {
        if (anim == false)
        {
            window.SetActive(!window.activeInHierarchy);
            anim = true;
            return true;
        }
        else
            return false;
    }

    private void ButtonAnim()
    {
        t += Time.deltaTime;

        if (clicked == false)
        {
            button.transform.rotation = Quaternion.Euler(0, 0, Mathf.Lerp(0, 90, t / .2f));
            button.transform.position = Vector3.Lerp(pos, pos + Vector3.left, t / .2f);
            button.transform.localScale = Vector2.Lerp(Vector2.one, Vector2.one * 2, t / .2f);
        }
        else if (clicked == true)
        {
            button.transform.rotation = Quaternion.Euler(0, 0, Mathf.Lerp(90, 0, t / .2f));
            button.transform.position = Vector3.Lerp(pos + Vector3.left, pos, t / .2f);
            button.transform.localScale = Vector2.Lerp(Vector2.one * 2, Vector2.one, t / .2f);
        }

        if (t > .2f)
        {
            clicked = !clicked;
            t = 0;
            anim = false;
        }
    }

}