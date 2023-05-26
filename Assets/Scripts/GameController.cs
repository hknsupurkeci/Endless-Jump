using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public List<Material> skyboxes = new List<Material>();

    float rotationSpeed = 1f;
    public static bool start = false;

    //public Material materyal;

    // Renk tonu aralýðý
    public float minRenkTonu = 0f;
    public float maxRenkTonu = 1f;

    // Renk tonu deðiþim hýzý
    public float renkTonuDegisimHizi = 1f;

    // Renk tonu arasýnda geçiþ yapmak için kullanýlan deðiþkenler
    private float h = 0f;
    private float s = 1f;
    private float v = 1f;

    //Canvas
    [SerializeField] private Button button;
    [SerializeField] private Text score;
    [SerializeField] private Text maxScore;
    [SerializeField] private Text maxScoreText;
    [SerializeField] private Text pressToStart;

    public static Button button2;
    public static Text maxScore2;
    public static Text maxScoreText2;
    public static Text pressToStart2;
    public static Color x;

    private void Awake()
    {
        score.enabled = false;

        button2 = button;
        maxScore2 = maxScore;
        maxScoreText2 = maxScoreText;
        pressToStart2 = pressToStart;
    }

    private void Update()
    {
        //#region Material Color
        //// Renk tonunu deðiþtirin.
        //h += renkTonuDegisimHizi * Time.deltaTime;
        //if (h > maxRenkTonu)
        //{
        //    h = minRenkTonu;
        //}
        //Color materyalRengi = Color.HSVToRGB(h, s, v);
        //// Materyalin rengini güncelleyin.
        //materyal.color = materyalRengi;
        //#endregion
    }
    // Declare a private variable to hold the time of the last update
    private float lastUpdateTime = 0.0f;
    void LateUpdate()
    {
        // Check if 10 seconds have passed since the last update
        if (Time.time - lastUpdateTime > 10.0f)
        {
            // Call the function to be executed every 10 seconds
            Skybox();

            // Update the last update time
            lastUpdateTime = Time.time;
        }
        RenderSettings.skybox.SetFloat("_Rotation", Time.time * rotationSpeed); // Skybox'ýn dönüþünü ayarla
    }

    private void Skybox()
    {
        RenderSettings.skybox = skyboxes[Random.Range(0, 3)];
    }

    public void GameStart()
    {
        if(AdManager.instance._bannerView != null)
        {
            AdManager.instance._bannerView.Hide();
        }
        //AdManager.bannerAd._bannerView.Hide();
        score.enabled = true;
        start = true;
        button.enabled = false;
        // Image bileþenini alýn
        Image buttonImage = button.GetComponent<Image>();
        
        // Image bileþeninin color özelliðini þeffaf hale getirin
        Color color = buttonImage.color;
        x = buttonImage.color;
        color.a = 0f;
        buttonImage.color = color;
        maxScore.enabled = false;
        maxScoreText.enabled = false;
        pressToStart.enabled = false;
    }
}
