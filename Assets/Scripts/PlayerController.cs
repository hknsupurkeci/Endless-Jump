using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static AdManager;

public class PlayerController : MonoBehaviour
{
    public AudioClip mySound; // Ses dosyanýzý tanýmlayýn
    private AudioSource audioSource; // Ses kaynaðýnýzý tanýmlayýn

    public float maxAngle = 45f;
    public float jumpForce = 1f;
    Rigidbody rb;
    private Animator animator;

    public static bool death = false;
    //animasyonu bir defa oynatmak için(count)
    int count = 0;

    public static int score = 0;
    [SerializeField] private Text scoreText;
    [SerializeField] private Text maxScoreText;
    [SerializeField] private GameObject touchScreenAnimPref;

    int adCount = 0;

    [SerializeField] private Transform bitisPosition;
    public float lerpSpeed = 2f;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        maxScoreText.text = PlayerPrefs.GetInt("maxScore").ToString();
    }

    private void Update()
    {
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began && !death)
        {
            Jump();
            Sound();
            Vector3 touchPosition = new Vector3(Input.GetTouch(0).position.x, Input.GetTouch(0).position.y, 10f);
            Vector3 worldPosition = Camera.main.ScreenToWorldPoint(touchPosition);
            ScreenTouchAnimation(worldPosition);
        }

        if (transform.position.y < -2f && over)
        {
            over = false;
            Invoke("ReloadScene", 0.5f);
        }

        if (death)
            Lerp();
    }

    private void Sound()
    {
        audioSource.PlayOneShot(mySound);
    }
    private void ScreenTouchAnimation(Vector3 position)
    {
        Vector3 newPos = new Vector3(position.x, position.y, 0);
        Instantiate(touchScreenAnimPref, newPos, Quaternion.identity);
    }

    private void Jump()
    {
        Vector3 touchPos = Input.GetTouch(0).position;
        touchPos.z = Camera.main.transform.position.z;

        Vector3 jumpDirection = touchPos - Camera.main.WorldToScreenPoint(transform.position);
        jumpDirection.y = Mathf.Abs(jumpDirection.y);
        jumpDirection = jumpDirection.normalized * jumpForce;

        float angle = Vector3.Angle(jumpDirection, Vector3.up);
        if (angle > maxAngle)
        {
            //Debug.Log("("+jumpDirection.x + ") - (" + transform.position.x+")");
            if(jumpDirection.x < transform.position.x)
                jumpDirection = Quaternion.AngleAxis(-maxAngle, Vector3.forward) * Vector3.left * jumpForce;
            else
                jumpDirection = Quaternion.AngleAxis(maxAngle, Vector3.forward) * Vector3.right * jumpForce;
        }

        rb.AddForce(jumpDirection, ForceMode.Impulse);
    }

    private void OnTriggerEnter(Collider other)
    {
        if((other.gameObject.tag.Equals("Enemy") || other.gameObject.tag.Equals("BlackHole")) && count == 0)
        {
            GameOver();
        }
        else if (other.gameObject.tag.Equals("Score") && count == 0)
        {
            score++;
            other.gameObject.GetComponent<BoxCollider>().enabled = false;
            scoreText.text = score.ToString();
        }
    }
    void ReloadScene()
    {
        //death false ile oyun yeniden baþlayabiliyor.
        if (AdManager.instance._bannerView != null)
        {
            AdManager.instance._bannerView.Show();
            if (adCount % 2 == 0)
            {
                AdManager.instance.ShowAd();
            }
            adCount++;
        }
        death = false;
        #region UI
        GameController.button2.enabled = true;
        // Image bileþenini alýn
        Image buttonImage = GameController.button2.GetComponent<Image>();
        // Image bileþeninin color özelliðini þeffaf hale getirin
        Color color = buttonImage.color;
        color.a = GameController.x.a;
        buttonImage.color = color;

        GameController.maxScore2.enabled = true;
        GameController.maxScoreText2.enabled = true;
        GameController.pressToStart2.enabled = true;

        scoreText.enabled = false;
        scoreText.text = "0";
        #endregion

        score = 0;
    }
    //over burada yere düþme tamamlandýmý kontrol ediyor.
    bool over = false;
    void GameOver()
    {
        over = true;
        int maxScore = PlayerPrefs.GetInt("maxScore");
        if (score > maxScore)
        {
            PlayerPrefs.SetInt("maxScore", score);
            maxScoreText.text = score.ToString();
        }
        count++;
        rb.useGravity = false;
        rb.isKinematic = true;
        death = true;
        animator.SetTrigger("Death");

        //DarkHole._asto.SetActive(false);

        foreach (Transform child in DarkHole._asto.transform)
        {
            Destroy(child.gameObject);
        }
        GameController.start = false;
        DarkHole._DarkHole.transform.position = new Vector3(0,-7.77f,0);
        //karakterin düþmeye baþlama süresi
        Invoke("PlayerValues", 0.5f);
    }

    void PlayerValues()
    {
        #region Player Values
        count = 0;
        rb.useGravity = true;
        rb.isKinematic = false;
        //death = false;
        animator.SetTrigger("Spawn");
        #endregion
    }

    void Lerp()
    {
        Vector3 position = Vector3.Lerp(transform.position, bitisPosition.position, lerpSpeed * Time.deltaTime);
        transform.position = position;
    }
}


