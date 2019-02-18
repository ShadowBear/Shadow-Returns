using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FLickerLights : MonoBehaviour {

    private Light lightSource;
    private float maxLightIntensitiy = 0.75f;
    public float waitTimeZip1 = 0.1f;
    public float waitTimeZip2 = 0.4f;
    public float waitTimeZip3 = 0.7f;
    public float lightTime = 3f;
    
    // Use this for initialization
	void Start () {
        lightSource = gameObject.GetComponent<Light>();
        StartCoroutine(Flicker());
    }

    void Update()
    {
        if(lightSource.intensity > 0)
        {
            GetComponent<SphereCollider>().enabled = true;
        }
        else
        {
            GetComponent<SphereCollider>().enabled = false;
        }
    }

    IEnumerator Flicker()
    {
        while (true)
        {
            //Flicker 4 times before light´s up
            lightSource.intensity = maxLightIntensitiy;
            yield return new WaitForSeconds(waitTimeZip1);
            lightSource.intensity = 0f;
            yield return new WaitForSeconds(waitTimeZip1);
            lightSource.intensity = maxLightIntensitiy;
            yield return new WaitForSeconds(waitTimeZip1);
            lightSource.intensity = 0f;;
            yield return new WaitForSeconds(waitTimeZip3);

            lightSource.intensity = maxLightIntensitiy;
            yield return new WaitForSeconds(waitTimeZip2);
            lightSource.intensity = 0f;
            yield return new WaitForSeconds(waitTimeZip3);

            //Lights up for x Seconds
            lightSource.intensity = maxLightIntensitiy;
            yield return new WaitForSeconds(lightTime);

            //Flicker 2 times before light´s out
            lightSource.intensity = 0f;
            yield return new WaitForSeconds(waitTimeZip1);
            lightSource.intensity = maxLightIntensitiy;
            yield return new WaitForSeconds(waitTimeZip1);
            lightSource.intensity = 0;
            yield return new WaitForSeconds(waitTimeZip1);
            lightSource.intensity = maxLightIntensitiy;
            yield return new WaitForSeconds(waitTimeZip3);

            lightSource.intensity = 0f;
            yield return new WaitForSeconds(waitTimeZip2);
            lightSource.intensity = maxLightIntensitiy;
            yield return new WaitForSeconds(waitTimeZip3);

            //Lights out for x Seconds
            lightSource.intensity = 0f;
            yield return new WaitForSeconds(lightTime);
        }
    }
}
