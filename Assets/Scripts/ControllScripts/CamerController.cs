using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamerController : MonoBehaviour {

    public GameObject follow;
    public Vector3 offset;
    [Range(0.1f,1.0f)]
    public float smoothSpeed = .125f;
    private Vector3 desiredPosition;
    private Vector3 smoothedPosition;
    private GameObject player;

    public Transform cameraPoint;
    
    public bool rotateAroundPlayer = true;
    public float rotationSpeed = 5.0f;
    
    //private RaycastHit oldHit;
    RaycastHit[] hits;

    // Use this for initialization
    void Start () {
        player = GameObject.FindGameObjectWithTag("Player");
        follow = player;
        StartCoroutine(LateCameraPositionStart());
    }
	
	// Update is called once per frame
	void FixedUpdate () {

        if (Input.GetKey(KeyCode.Mouse1))
        {
            TurnCameraLook();
        }
        //Debug.DrawRay(this.transform.position, (cameraPoint.position - transform.position), Color.magenta);
        if(hits != null) HideObjects(true);        
        desiredPosition = follow.transform.position + offset;
        smoothedPosition = Vector3.Slerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = smoothedPosition;
        //hits = Physics.RaycastAll(transform.position, (cameraPoint.position - transform.position), Vector3.Distance(transform.position, cameraPoint.position));
        hits = Physics.SphereCastAll(transform.position, 0.5f, (cameraPoint.position - transform.position), Vector3.Distance(transform.position, cameraPoint.position));
        HideObjects(false);
        //XRay();
    }

    void TurnCameraLook()
    {
        Quaternion camTurnAngle = Quaternion.AngleAxis(Input.GetAxis("Mouse X") * rotationSpeed, Vector3.up);
        offset = camTurnAngle * offset;
        transform.LookAt(player.transform);
        //player.GetComponent<PlayerHealth>().RotateHealthbar();
    }

    /**************** OLD ***********************/
    //void HideObjects(bool state)
    //{
    //    foreach (RaycastHit hit in hits)
    //    {
    //        Renderer r = null;
    //        if (hit.transform != null && !hit.collider.isTrigger && !hit.collider.CompareTag("Invisible")) r = hit.collider.GetComponent<Renderer>();
    //        if (r)
    //        {
    //            //r.enabled = state;
    //            Color temp = r.material.color;
    //            temp.a = state ? 1f : 0.5f;
    //            r.material.color = temp;
    //            if (!state)
    //            {
    //                shaderName = r.material.shader.name;
    //                r.material.shader = Shader.Find("Transparent/Diffuse");
    //            }
    //            else r.material.shader = Shader.Find(shaderName);
    //            //else r.material.shader = Shader.Find("Standart");
    //            //else r.material.shader = Shader.Find("Toon/Lit");
    //        }
    //    }
    //}


    void HideObjects(bool state)
    {
        foreach (RaycastHit hit in hits)
        {
            Renderer r = null;
            if (hit.transform != null && !hit.collider.isTrigger && !hit.collider.CompareTag("Invisible")) r = hit.collider.GetComponent<Renderer>();
            if (r)
            {
                //r.enabled = state;
                Color temp = r.material.color;
                temp.a = state ? 1f : 0.5f;
                r.material.color = Color.Lerp(r.material.color, temp, 0.2f);
                //print(r.material.shader);
                //r.material.color = temp;
                if (!state)
                {
                    //shaderName = r.material.shader.name;
                    //renderList.Add(shaderName);
                    //r.material.EnableKeyword("_ALPHABLEND_ON");
                    //r.material.shader = Shader.Find("Transparent/Diffuse");
                    r.material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.One);
                    r.material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
                    r.material.SetInt("_ZWrite", 0);
                    r.material.DisableKeyword("_ALPHATEST_ON");
                    r.material.DisableKeyword("_ALPHABLEND_ON");
                    r.material.EnableKeyword("_ALPHAPREMULTIPLY_ON");
                    r.material.renderQueue = 3000;
                    
                    //r.material.shader = Shader.Find("Standard/Transparent");
                }
                else
                {
                    r.material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.One);
                    r.material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.Zero);
                    r.material.SetInt("_ZWrite", 1);
                    r.material.DisableKeyword("_ALPHATEST_ON");
                    r.material.DisableKeyword("_ALPHABLEND_ON");
                    r.material.DisableKeyword("_ALPHAPREMULTIPLY_ON");
                    r.material.renderQueue = -1;
                }
                //else r.material.DisableKeyword("_ALPHABLEND_ON");
                //else r.material.shader = Shader.Find("Standard");
                //else r.material.shader = Shader.Find();
                //else r.material.shader = Shader.Find("Toon/Lit");
            }
        }
    }

    IEnumerator LateCameraPositionStart()
    {
        yield return new WaitForSeconds(1.5f);
        TurnCameraLook();
        yield return null;
    }


    //private void XRay()
    //{

    //    float characterDistance = Vector3.Distance(transform.position, GameObject.FindGameObjectWithTag("Player").transform.position);
    //    Vector3 fwd = transform.TransformDirection(Vector3.forward);

    //    RaycastHit hit;
    //    if (Physics.Raycast(transform.position, fwd, out hit, characterDistance))
    //    {
    //        if (oldHit.transform)
    //        {

    //            // Add transparence
    //            Color colorA = oldHit.transform.gameObject.GetComponent<Renderer>().material.color;
    //            colorA.a = 1f;
    //            oldHit.transform.gameObject.GetComponent<Renderer>().material.SetColor("_Color", colorA);
    //        }

    //        // Add transparence
    //        Color colorB = hit.transform.gameObject.GetComponent<Renderer>().material.color;
    //        colorB.a = 0.5f;
    //        hit.transform.gameObject.GetComponent<Renderer>().material.SetColor("_Color", colorB);

    //        // Save hit
    //        oldHit = hit;
    //    }
    //}
}

