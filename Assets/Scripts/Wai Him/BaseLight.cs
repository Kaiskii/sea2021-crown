using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class BaseLight : MonoBehaviour {
    // Start is called before the first frame update
    Light2D fireLight;
    float lightInt;
    [SerializeField]
    public float minInt;

    [SerializeField]
    public float maxInt;

    [SerializeField]
    public float flickerTime = 1.2f;
    public float flickerTimer = 0.0f;


    void Start() {
        fireLight = GetComponent<Light2D>();
        flickerTimer = flickerTime;
    }

    // Update is called once per frame
    void Update() {
        if (flickerTimer > 0.0f) {
           
            flickerTimer -= Time.deltaTime;
        }
        if (flickerTimer <= 0.0f) {
            lightInt = Random.Range(minInt, maxInt);
            fireLight.intensity = lightInt;
            flickerTimer = flickerTime;
        }
    }
}