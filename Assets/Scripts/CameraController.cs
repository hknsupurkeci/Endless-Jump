using UnityEngine;

public class CameraController : MonoBehaviour
{
    //Camera takip
    public Transform target;
    public Vector3 offset;

    // Kameranýn bakýþ açýsý deðeri
    public float cameraFOV = 100.0f;
    // Ekran boyutunu ve çözünürlüðünü belirleyecek deðiþkenler
    private float screenWidth;
    private float screenHeight;

    void Start()
    {
        offset = transform.position;

        // Ekran boyutunu ve çözünürlüðünü al
        screenWidth = Screen.width;
        screenHeight = Screen.height;

        // Kameranýn bakýþ açýsýný ayarla
        float aspectRatio = screenWidth / screenHeight;
        Camera.main.fieldOfView = cameraFOV / aspectRatio;
    }

    void Update()
    {
        // Eðer ekran boyutu deðiþtiyse, kameranýn bakýþ açýsýný yeniden hesapla
        if (screenWidth != Screen.width || screenHeight != Screen.height)
        {
            screenWidth = Screen.width;
            screenHeight = Screen.height;

            float aspectRatio = screenWidth / screenHeight;
            Camera.main.fieldOfView = cameraFOV / aspectRatio;
        }
    }

    private void LateUpdate()
    {
        Vector3 desiredPosition = target.position + offset;
        transform.position = new Vector3(0, desiredPosition.y, desiredPosition.z);
    }
}
