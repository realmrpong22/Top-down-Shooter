using UnityEngine;

public class Crosshair : MonoBehaviour
{
    Camera mainCam;

    [SerializeField] GameObject readyCrosshair;
    [SerializeField] Transform cooldownCircle;

    PlayerShoot playerShoot;

    void Start()
    {
        mainCam = Camera.main;
        playerShoot = FindObjectOfType<PlayerShoot>();

        Cursor.visible = false;
    }

    void Update()
    {
        UpdatePosition();
        UpdateCooldownVisual();
    }

    void UpdatePosition()
    {
        Vector3 mousePosition = Input.mousePosition;
        Vector3 cursorPosition = mainCam.ScreenToWorldPoint(mousePosition);

        cursorPosition.z = 0;
        transform.position = cursorPosition;
    }

    void UpdateCooldownVisual()
    {
        float ratio = playerShoot.GetCooldownRatio();

        if (ratio > 0)
        {
            readyCrosshair.SetActive(false);
            cooldownCircle.gameObject.SetActive(true);

            cooldownCircle.localRotation =
                Quaternion.Euler(0, 0, 360 * ratio);
        }
        else
        {
            cooldownCircle.gameObject.SetActive(false);
            readyCrosshair.SetActive(true);
        }
    }
}