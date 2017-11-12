using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class csSettings : MonoBehaviour
{
    public GameObject bg, confirmwindow;
    public GameObject clear, button;
    private Vector3 pos;
    private RaycastHit2D hit;
    private bool anim = false, insettings;
    private float t = 0;

    private bool clicked = false;

	void Start() 
	{
        pos = button.transform.position;
    }

    void FixedUpdate()
    {

    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

            if (hit.collider != null && hit.collider.name == "settingsbutton" && anim == false)
            {
                bg.SetActive(!bg.activeInHierarchy);
                anim = true;
            }
            else if (hit.collider != null && hit.collider.name == "cleardata")
                confirmwindow.SetActive(true);
            else if (hit.collider != null && hit.collider.name == "yes")
            {
                Serializer.Delete("savedata");
                Serializer.Delete("leveldata");
                PlayerPrefs.DeleteAll();
                Debug.Log("data cleared");
                PlayerData.clearquit = true;
                Application.Quit();
            }
            else if (hit.collider != null && hit.collider.name == "no")
                confirmwindow.SetActive(false);
        }
        if (anim == true)
            ButtonAnim();
    }   

    private void ButtonAnim()
    {
        t += Time.deltaTime;

        if (clicked == false)
        {
            button.transform.rotation = Quaternion.Euler(0, 0, Mathf.Lerp(0, 90, t / .2f));
            button.transform.position = Vector3.Lerp(pos, pos + Vector3.left, t /.2f);
            button.transform.localScale = Vector2.Lerp(Vector2.one, Vector2.one*2, t/.2f);
        }
        else if (clicked == true)
        {
            button.transform.rotation = Quaternion.Euler(0, 0, Mathf.Lerp(90, 0, t / .2f));
            button.transform.position = Vector3.Lerp(pos + Vector3.left, pos, t /.2f);
            button.transform.localScale = Vector2.Lerp(Vector2.one * 2, Vector2.one, t/.2f);
        }

        if (t > .2f)
        {
            clicked = !clicked;
            t = 0;
            anim = false;
        }
    }
}