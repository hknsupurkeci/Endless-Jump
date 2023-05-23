using UnityEngine;

public class CameraController : MonoBehaviour
{
    //Camera takip
    public Transform target;
    public Vector3 offset;

    // Kameran�n bak�� a��s� de�eri
    public float cameraFOV = 100.0f;
    // Ekran boyutunu ve ��z�n�rl���n� belirleyecek de�i�kenler
    private float screenWidth;
    private float screenHeight;

    void Start()
    {
        offset = transform.position;

        // Ekran boyutunu ve ��z�n�rl���n� al
        screenWidth = Screen.width;
        screenHeight = Screen.height;

        // Kameran�n bak�� a��s�n� ayarla
        float aspectRatio = screenWidth / screenHeight;
        Camera.main.fieldOfView = cameraFOV / aspectRatio;
    }

    void Update()
    {
        // E�er ekran boyutu de�i�tiyse, kameran�n bak�� a��s�n� yeniden hesapla
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
