using UnityEngine;

/// <summary>
/// This class controls the cameras rotation, zoom and so on.
/// Zoom: Scroll-Wheel / E & Q
/// Move up: Arrow Up
/// Move Down: Arrow down
/// Move left: Arrow left
/// Move right: Arrow right
/// Hold left mouse button to move camera
/// </summary>
public class CameraScript : MonoBehaviour {

    [Tooltip("Defines the cameras maximum field of view.")]
    [Range(60, 150)]
	[SerializeField] private float maxFOV = 100f;

    [Tooltip("Defines the cameras minimum field of view.")]
    [Range(20, 59)]
	[SerializeField] private float minFOV = 40f;

    [Tooltip("Defines how fast the camera zoom in and out.")]
    [Range(5f, 50f)]
	[SerializeField] private float zoomSpeed = 40f;

	[SerializeField] private GameObject centerObject = null;

    private float scrollDir = 0f;
    private float cameraFOV = 60f;
    private float mouseXDir = 0f;

    [Range(1, 50)]
    [SerializeField] private float mouseSpeed = 25f;

	private Camera mainCam;

	// Use this for initialization
	void Start () {
		mainCam = Camera.main;

		mainCam.fieldOfView = maxFOV;
	}
	
	/// <summary>
    /// Camera movement.
    /// </summary>
	void Update () {
        scrollDir = Input.GetAxis("Mouse ScrollWheel");
        cameraFOV = mainCam.fieldOfView;

        // Zoom in / out
        if (scrollDir > 0f) {
            if (cameraFOV > minFOV) {
                cameraFOV -= Time.deltaTime * zoomSpeed;
				mainCam.fieldOfView = cameraFOV;
            }

        } else if (scrollDir < 0f) {
            if (cameraFOV < maxFOV) {
                cameraFOV += Time.deltaTime * zoomSpeed;
				mainCam.fieldOfView = cameraFOV;
            }
        }

        // Zoom with keyboard
        if (Input.GetKey(KeyCode.E)) {
            if (cameraFOV > minFOV) {
                cameraFOV -= Time.deltaTime * zoomSpeed;
				mainCam.fieldOfView = cameraFOV;

            } else {
				mainCam.fieldOfView = minFOV;
            }

        } else if (Input.GetKey(KeyCode.Q)) {
            if (cameraFOV < maxFOV) {
                cameraFOV += Time.deltaTime * zoomSpeed;
				mainCam.fieldOfView = cameraFOV;

            } else {
				mainCam.fieldOfView = maxFOV;
            }
        }

        // Orbit with keys
        if (Input.GetKey(KeyCode.UpArrow)) {
            transform.RotateAround(centerObject.transform.position, Vector3.right, mouseSpeed * Time.deltaTime);

        } else if (Input.GetKey(KeyCode.DownArrow)) {
            transform.RotateAround(centerObject.transform.position, Vector3.left, mouseSpeed * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.RightArrow)) {
            transform.RotateAround(centerObject.transform.position, Vector3.down, mouseSpeed * Time.deltaTime);

        } else if (Input.GetKey(KeyCode.LeftArrow)) {
            transform.RotateAround(centerObject.transform.position, Vector3.up, mouseSpeed * Time.deltaTime);
        }

        // Orbit with mouse
        if (Input.GetButton("Fire1")) {
			if (Input.GetAxis("Mouse X") != 0f) {
				mouseXDir = Input.GetAxis("Mouse X");
				if (mouseXDir > 0f) {
					transform.RotateAround(centerObject.transform.position, Vector3.down, mouseSpeed * Time.deltaTime);

				} else if (mouseXDir < 0f) {
					transform.RotateAround(centerObject.transform.position, Vector3.down, -mouseSpeed * Time.deltaTime);
				}
			}

			if (Input.GetAxis("Mouse Y") != 0f) {
				mouseXDir = Input.GetAxis("Mouse Y");
				if (mouseXDir > 0f) {
					transform.RotateAround(centerObject.transform.position, Vector3.left, mouseSpeed * Time.deltaTime);

				} else if (mouseXDir < 0f) {
					transform.RotateAround(centerObject.transform.position, Vector3.left, -mouseSpeed * Time.deltaTime);
				}
			}
		}

        // Quit application
        if (Input.GetKey(KeyCode.Escape)) {
            Application.Quit();
        }
    }
}
