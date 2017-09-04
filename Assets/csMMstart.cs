using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class csMMstart: MonoBehaviour
{
    private bool clkd = false;
    private TextMesh tm;
    private float t;

	void Start ()
	{
        tm = GetComponent<TextMesh>();
	}

	void Update ()
	{
        if (clkd == true)
        {
            t += Time.deltaTime / 1f;
            transform.localScale = new Vector2(transform.localScale.x + .2f, transform.localScale.y + .2f);
            tm.color = new Color(tm.color.r, tm.color.g, tm.color.b, Mathf.Lerp(.588f, 0, t));
        }

	}

    void OnMouseDown()
    {
        GetComponent<AudioSource>().Play();
        PlayerData.previousscene = SceneManager.GetActiveScene().name;
        clkd = true;
        StartCoroutine(Playsound());
    }

    IEnumerator Playsound()
    {
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene("Scene_Upgrades");
    }
}