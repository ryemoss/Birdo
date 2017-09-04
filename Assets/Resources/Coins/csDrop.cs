using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class csDrop : MonoBehaviour
{
    public GameObject coinpf;
    public AudioClip clip2;
    private GameObject coin;

    private int clicks;
    private bool collided;
    private ParticleSystem psys;
    private bool fade = false;
    private bool fadeonce = false;
    private SpriteRenderer color;
    private float t = 0;

    void Start() 
	{
        clicks = 2;
        psys = GetComponent<ParticleSystem>();
        color = gameObject.GetComponent<SpriteRenderer>();
        t = 0;
    }

	private void Update()
	{
        if (fade == true)
        {
            t += Time.deltaTime ;
            color.color = new Color(color.color.r, color.color.g, color.color.b, Mathf.Lerp(1, 0, t/2f));
        }
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "ground")
        {
            collided = true;
            if (fadeonce == false)
            {
                fadeonce = true;
                StartCoroutine(Fade());
            }
            Destroy(gameObject, 5f);
        }
    }

    void OnMouseDown()
    {
        if (collided == true && clicks > 0)
        {
            clicks--;
            GetComponent<Rigidbody2D>().AddForce(Vector2.up * 100f);
            if (clicks > 0)
                GetComponent<AudioSource>().Play();
        }
        if (clicks == 0)
        {
            clicks--;
            fade = false;
            Opentreasure();
        }
    }

    void Opentreasure()
    {
        psys.Play();
        GetComponent<AudioSource>().clip = clip2;
        GetComponent<AudioSource>().Play();
        coin = Instantiate(coinpf, new Vector2(transform.position.x, transform.position.y + .1f), Quaternion.identity);
        coin.GetComponent<Rigidbody2D>().AddForce(Vector2.up * 100f);
        PlayerData.roundcoins += coin.GetComponent<csCoin>().value;
        Destroy(color, .1f);
        GetComponent<CircleCollider2D>().enabled = false;
        Destroy(GetComponent<Rigidbody2D>(), .1f);
        Destroy(gameObject, 2f);
    }

    // fade object sprite after Xseconds over time t
    private IEnumerator Fade()
    {
        yield return new WaitForSeconds(3f);
        if (clicks > 0)
            fade = true;
    }
}