using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class LightFlicker : MonoBehaviour {
    // Start is called before the first frame update
    Light2D fireLight;
    LightFlickerDataSO lightData;

    float flickerTimer = 0.0f;
    float lightInt;

    [SerializeField] int lightDataID = 0;

    void Start() {
        fireLight = GetComponent<Light2D>();

        lightData = ResourceIndex.GetAsset<LightFlickerDataSO>(lightDataID);
        if(!lightData) return;

        flickerTimer = lightData.flickerTime;
    }

    // Update is called once per frame
    void Update() {
        if(!lightData) return;
    
        if (flickerTimer > 0.0f) {
           
            flickerTimer -= Time.deltaTime;
        }
        if (flickerTimer <= 0.0f) {
            lightInt = Random.Range(lightData.minInt, lightData.maxInt);
            fireLight.intensity = lightInt;
            flickerTimer = lightData.flickerTime;
        }
    }
}