using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayShooter : MonoBehaviour
{
    private Camera cam;

    private string hitPointText = "";

    void Start()
    {
      cam = GetComponent<Camera>();

      Cursor.lockState = CursorLockMode.Locked;
      Cursor.visible = false;
    }

    void OnGUI()
{
    int size = 12;
    float posX = cam.pixelWidth / 2 - size / 4;
    float posY = cam.pixelHeight / 2 - size / 2;

    GUIStyle asterisk = new GUIStyle();
    GUIStyle coordinates = new GUIStyle();

    asterisk.fontSize = 36;
    coordinates.fontSize = 16;

    GUI.Label(new Rect(posX, posY, size, size), "*", asterisk);
    GUI.Label(new Rect(20, 20, size, size), "Hit Point Coordinates: " + hitPointText, coordinates);
}

    void Update()
    {
      if (Input.GetMouseButtonDown(0))
      {
        Vector3 point = new Vector3(cam.pixelWidth / 2, cam.pixelHeight / 2, 0);
        Ray ray = cam.ScreenPointToRay(point);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            GameObject hitObject = hit.transform.gameObject;
            ReactiveTarget target = hitObject.GetComponent<ReactiveTarget>();
            if (target != null)
            {
                target.ReactToHit();
            }
            else
            {
                StartCoroutine(SphereIndicator(hit.point));
                hitPointText = hit.point.ToString();
            }
        }
      }  
    }

    private IEnumerator SphereIndicator(Vector3 pos)
    {
        GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        sphere.transform.position = pos;

        yield return new WaitForSeconds(1);

        Destroy(sphere);
    }
}
