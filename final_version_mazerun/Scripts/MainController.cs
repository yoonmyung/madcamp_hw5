using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainController : MonoBehaviour
{
    public Rigidbody rigidbody;
    public float rotationspeed = 300f;

    private float moveX;
    private float moveZ;
    private float speed = 7f;
    private float rotation = 0.0f;
    private float currentangle;
    private float runspeed = 1.0f;
    private float xvel;
    private float zvel;
    private Quaternion qTo;
    private bool release;
    private float destangle;
    private float localangle;

    public Joystick joystick;

    public AudioSource footstep;
    public AudioSource BGM;
    private bool playingfootstep;

    public GameObject menuUI;
    public GameObject itemsUI;
    public GameObject showmap;
    public GameObject paintgun;
    public GameObject spraybutton;
    public Button paintgunbutton;
    public Image paintgunimage;
    private bool menuactive;

    private float stamina = 5, maxStamina = 5;
    private bool isrunning;

    private Rect staminaRect;
    private Texture2D staminaTexture;

    // Use this for initialization
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        joystick = FindObjectOfType<Joystick>();
        footstep = GetComponent<AudioSource>();
        playingfootstep = false;
        menuactive = false;

        staminaRect = new Rect(Screen.width * 0.03f, Screen.height * 0.95f, Screen.width / 4, Screen.height / 50);
        staminaTexture = new Texture2D(1, 1);
        staminaTexture.SetPixel(0, 0, Color.red);
        staminaTexture.Apply();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        currentangle = rigidbody.transform.eulerAngles.y;
        xvel = joystick.Vertical * Mathf.Sin(currentangle * Mathf.Deg2Rad) + joystick.Horizontal * Mathf.Cos(currentangle * Mathf.Deg2Rad);
        zvel = -joystick.Horizontal * Mathf.Sin(currentangle * Mathf.Deg2Rad) + joystick.Vertical * Mathf.Cos(currentangle * Mathf.Deg2Rad);
        rigidbody.velocity = new Vector3(xvel * speed * runspeed, rigidbody.velocity.y, zvel*speed * runspeed);

        if((rigidbody.velocity.x > 0 || rigidbody.velocity.z > 0) && !playingfootstep)
        {
            footstep.Play();
            playingfootstep = true;
        }

        if(rigidbody.velocity.magnitude == 0)
        {
            footstep.Pause();
            playingfootstep = false;
        }

        localangle = Mathf.Atan2(joystick.Horizontal, joystick.Vertical) * Mathf.Rad2Deg;

        destangle = localangle + currentangle;

        /*if(joystick.Vertical < 0)
        {
            angle = angle+180;
        }*/
        

        //rigidbody.transform.rotation = qTo;

        //rigidbody.transform.Rotate(new Vector3(0.0f, angle, 0.0f) * Time.deltaTime * rotationspeed);

        qTo = Quaternion.Euler(0f, destangle, 0f);


        rigidbody.transform.rotation = Quaternion.RotateTowards(rigidbody.transform.rotation, qTo, rotationspeed * Mathf.Pow((Mathf.Abs(localangle) / 90), 2) * Time.deltaTime);
        
        if(!ParticleLauncher.haveammo)
        {
            spraybutton.SetActive(false);
            paintgun.SetActive(false);
        }

        if(isrunning)
        {
            stamina -= Time.deltaTime;
            if(stamina < 0)
            {
                stamina = 0;
                runspeed = 1f;
                isrunning = false;
            }
        }

        else if (stamina < maxStamina)
        {
            stamina += (Time.deltaTime/2);
        }



        /*moveZ = v * speedZ * runspeed * Time.deltaTime;

        if (-1f < rigidbody.transform.eulerAngles.y && rigidbody.transform.eulerAngles.y < 1f)
        {
            rigidbody.velocity = new Vector3(0, 0, moveZ);
        }

        else if (179f < rigidbody.transform.eulerAngles.y && rigidbody.transform.eulerAngles.y < 181f)
        {
            rigidbody.velocity = new Vector3(0, 0, -moveZ);
        }

        else if (89f < rigidbody.transform.eulerAngles.y && rigidbody.transform.eulerAngles.y < 91f)
        {
            rigidbody.velocity = new Vector3(moveZ, 0, 0);
        }

        else if (269f < rigidbody.transform.eulerAngles.y && rigidbody.transform.eulerAngles.y < 271f)
        {
            rigidbody.velocity = new Vector3(-moveZ, 0, 0);
        }*/
    }

    public void startrun()
    {
        runspeed = 1.5f;
        isrunning = true;
    }

    public void endrun()
    {
        runspeed = 1f;
        isrunning = false;
    }

    public void turn()
    {
        rotation = currentangle + 180.0f;
        qTo = Quaternion.Euler(0.0f, rotation, 0.0f);
        rigidbody.transform.rotation = qTo;
    }

    public void menubuttonclick()
    {
        if(!menuactive)
        {
            menuUI.SetActive(true);
            menuactive = true;
            Time.timeScale = 0;
            //BGM.Pause();
        }
        else
        {
            menuUI.SetActive(false);
            menuactive = false;
            Time.timeScale = 1;
            //BGM.Play();
        }
    }

    public void resumebuttononclick()
    {
        menuUI.SetActive(false);
        menuactive = false;
        Time.timeScale = 1;
        //BGM.Play();        
    }

    public void restartbuttononclick()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Time.timeScale = 1;
    }

    public void myitemsonclick()
    {
        menuUI.SetActive(false);
        menuactive = false;
        itemsUI.SetActive(true);
        //BGM.Play();   
    }

    public void exitbuttononclick()
    {
        itemsUI.SetActive(false);
        Time.timeScale = 1;
    }

    public void mapbuttononlclick()
    {
        itemsUI.SetActive(false);
        showmap.SetActive(true);
    }

    public void exitbuttonmaponclick()
    {
        showmap.SetActive(false);
        Time.timeScale = 1;
    }

    public void paintgunbuttononclick()
    {
        itemsUI.SetActive(false);
        paintgun.SetActive(true);
        spraybutton.SetActive(true);
        Time.timeScale = 1;
        ParticleLauncher.ammo += 100;
        ParticleLauncher.haveammo = true;
        paintgunimage.color = new Color(1f, 1f, 1f, 0.267f);
        paintgunbutton.interactable = false;
    }

    private void OnGUI()
    {
        float ratio = stamina / maxStamina;
        float rectWidth = ratio * Screen.width / 4;
        staminaRect.width = rectWidth;
        GUI.DrawTexture(staminaRect, staminaTexture);
    }
}