using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class csPlayerGun : MonoBehaviour
{
    public GameObject AmmoBar;
    private float ammo = 0;
    public GameObject effectgo;
    private GameObject effect;
    public GameObject staffgo;
    public GameObject orbgo;
    private LineRenderer beam;
    public GameObject fireratebar;
    private Slider frbar;

    public GameObject bulletpf;
    private GameObject bullet;
    private bool reloading = false;
    private float reloadspd;
    private float reloadtime;
    private float wepenergy;
    private string bullettype;
    private Staff playerstaff;
    private bool canshoot;
    private bool startshot = false;
    private Vector2 sps; //shot position start
    private Vector2 spe; //shot position end
    private Vector2 spd; //shot direction vector
    private Vector2 bps, bpe, bpd; //beam positions
    private float sdis; //click pos distance
    private float firerate;
    private float frtimer;

    private float t;
    private ParticleSystem psys;
    private ParticleSystem.MainModule pmod;

    void Start()
    {
        playerstaff = PlayerData.staff;
        bulletpf = PlayerData.bullet;
        beam = GetComponent<LineRenderer>();

        staffgo.GetComponent<SpriteRenderer>().sprite = playerstaff.sprite;
        frbar = fireratebar.GetComponent<Slider>();

        reloadspd = PlayerData.reloadspd;
        wepenergy = playerstaff.energy;
        firerate = PlayerData.firerate;
        bullettype = playerstaff.type;

        reloadtime = 4 / reloadspd;
        psys = GameObject.Find("reloadparticles").GetComponent<ParticleSystem>();
        pmod = psys.main;
        pmod.startLifetime = reloadtime / 2f;
        pmod.startSpeed = -psys.shape.radius / pmod.startLifetimeMultiplier;
        beam.sortingOrder = 2;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && reloading == false && startshot == false)
        {
            sps = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            if (/*Mathf.Abs(Vector2.Distance(sps, orbgo.transform.position)) < 3.2f &&*/ Camera.main.ScreenToWorldPoint(Input.mousePosition).y > -8f)
            {
                sps = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                bps = new Vector3(orbgo.transform.position.x, orbgo.transform.position.y, -1);
                beam.SetPosition(0, bps);
                startshot = true;
                beam.SetPosition(1, bps);
                beam.enabled = true;
            }
        }
        if (startshot == true && Input.GetMouseButton(0) && ((Input.touchCount == 1) || Application.platform == RuntimePlatform.WindowsEditor))
        {
            spe = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            bpe = bps-(spe-sps);
            sdis = Vector2.Distance(bps, bpe) * 3;
            if (sdis < 12f)
                bpe = bps + (bpe - bps).normalized * sdis;
            else
                bpe = bps + (bpe - bps).normalized * 12f;
            beam.SetPosition(1, bpe);
        }
        if (startshot == true && ((Input.touchCount > 1 && Input.GetTouch(1).phase == TouchPhase.Began) || Input.GetKeyDown(KeyCode.F)) && canshoot == true)
        {
            spd = bpe - bps;
            spd.Normalize();
            fireratebar.SetActive(true);
            frbar.value = 0;
            frtimer = 0;
            Firebullet();
            GetComponent<AudioSource>().Play();
            Decreaseammo();
            ShotEffect();
        }
        if ((startshot == true && Input.GetMouseButtonUp(0)) || reloading == true)
        {
            beam.enabled = false;
            startshot = false;
        }
        if (reloading == true)
            Reloading();

        if (canshoot == false)
        {
            FirerateHandler();
        }
    }

    void Firebullet()
    {
        canshoot = false;
        if (bullettype == "normal") BulletNorm();
        else if(bullettype == "sniper") BulletSniper();
        else if (bullettype == "shotgun") StartCoroutine("BulletShotgun");
        else if (bullettype == "laser") BulletLaser();
    }

    void Decreaseammo()
    {
        AmmoBar.GetComponent<Slider>().value -= wepenergy;
        ammo = AmmoBar.GetComponent<Slider>().value;

        if (ammo <= 0)
        {
            reloading = true;
            t = 0;
            GameObject.Find("reloadparticles").GetComponent<ParticleSystem>().Play();
        }
    }

    void Reloading()
    {
        t += reloadspd / 4f * Time.deltaTime;
        AmmoBar.GetComponent<Slider>().value = Mathf.Lerp(0, 1, t);

        if (AmmoBar.GetComponent<Slider>().value == 1)
        {
            reloading = false;
            CancelInvoke();
        }
        else if (AmmoBar.GetComponent<Slider>().value >= (reloadtime - pmod.startLifetimeMultiplier) / reloadtime)
        {
            GameObject.Find("reloadparticles").GetComponent<ParticleSystem>().Stop();
        }
    }

    void ShotEffect()
    {
        for (int x = 0; x < 5; x++)
        {
            effect = Instantiate(effectgo) as GameObject;
            effect.GetComponent<csEffectB>().shotdir = spd;
            effect.transform.position = new Vector3(staffgo.transform.position.x, staffgo.transform.position.y + .6f, -.1f);
            effect.transform.parent = staffgo.transform;
        }
    }

    void FirerateHandler()
    {
        frtimer += Time.deltaTime;
        frbar.value = Mathf.Lerp(0, 1, frtimer*firerate);

        if (frbar.value >= 1f)
        {
            frtimer = 0;
            canshoot = true;
            fireratebar.SetActive(false);
        }
    }

    void BulletNorm()
    {
        bullet = Instantiate(bulletpf);
        bullet.transform.position = new Vector3(transform.position.x, transform.position.y, -1f);
        bullet.GetComponent<Rigidbody2D>().AddForce(spd * 10f);

        PlayerData.roundshots++;

        Destroy(bullet, 5f);
    }

    void BulletSniper()
    {
        bullet = Instantiate(bulletpf);
        bullet.transform.position = new Vector3(transform.position.x, transform.position.y, -1f);
        bullet.GetComponent<Rigidbody2D>().AddForce(spd * 12f);

        PlayerData.roundshots++;

        Destroy(bullet, 5f);
    }

    IEnumerator BulletShotgun()
    {
        for (int x = 0; x < 3; x++)
        {
            bullet = Instantiate(bulletpf);
            bullet.transform.position = new Vector3(transform.position.x, transform.position.y, -1f);
            Vector2 randir = new Vector2(spd.x * Random.Range(.9f, 1.1f), spd.y * (Random.Range(.9f, 1.1f))).normalized;
            bullet.GetComponent<Rigidbody2D>().AddForce(randir*10f);

            PlayerData.roundshots++;

            Destroy(bullet, 5f);
            yield return new WaitForSeconds(.01f);
        }
    }

    void BulletLaser()
    {
        for (int x = 0; x < 5; x++)
        {
            bullet = Instantiate(bulletpf);
            bullet.transform.position = new Vector3(transform.position.x, transform.position.y, -1f);
            Vector2 randir = new Vector2(spd.x * Random.Range(.9f, 1.1f), spd.y * (Random.Range(.9f, 1.1f))).normalized;
            bullet.GetComponent<Rigidbody2D>().AddForce(randir * 10f);

            PlayerData.roundshots++;

            Destroy(bullet, 5f);
        }
    }


}