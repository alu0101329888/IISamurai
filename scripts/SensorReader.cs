using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SensorReader : MonoBehaviour
{
    Vector3 angular_velocity;
    Vector3 acceleration;
    Vector3 gravity;
    float altitude;
    float longitude;
    float latitude;
    float north;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Input.gyro.enabled = true;
        Input.compass.enabled = true;
        gravity = Input.gyro.gravity;
        angular_velocity = Input.gyro.rotationRate * Time.deltaTime;
        acceleration = Input.acceleration;
        
        if (Input.location.status != LocationServiceStatus.Running)
        {
            Input.location.Start();
        } 
        else
        {
            altitude = Input.location.lastData.altitude;
            longitude = Input.location.lastData.longitude;
            latitude = Input.location.lastData.latitude;
            north = Input.compass.trueHeading;
        }
        GameObject.Find("aceleracion").GetComponent<TextMeshProUGUI>().text = "Aceleración: " + acceleration;
        GameObject.Find("velocidad_angular").GetComponent<TextMeshProUGUI>().text = "Velocidad angular: " + angular_velocity;
        GameObject.Find("gravedad").GetComponent<TextMeshProUGUI>().text = "Gravedad: " + gravity;
        GameObject.Find("altitud").GetComponent<TextMeshProUGUI>().text = "Altitud: " + altitude;
        GameObject.Find("latitud").GetComponent<TextMeshProUGUI>().text = "Latitud: " + latitude;
        GameObject.Find("longitud").GetComponent<TextMeshProUGUI>().text = "Longitud: " + longitude;
        GameObject.Find("samurai").GetComponent<Transform>().rotation = Quaternion.Euler(0, -north, 0);
        Vector3 forwardDir = GameObject.Find("samurai").GetComponent<Transform>().forward;
        GameObject.Find("samurai").GetComponent<Transform>().Translate(forwardDir * -acceleration.y * Time.deltaTime, Space.Self);
    }
}
