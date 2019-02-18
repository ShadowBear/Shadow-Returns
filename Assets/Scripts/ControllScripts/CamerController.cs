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
    
    RaycastHit[] hits;

    // Use this for initialization
    void Start () {
        player = GameObject.FindGameObjectWithTag("Player");
        follow = player;
        StartCoroutine(LateCameraPositionStart());
    }
	
	void FixedUpdate () {             
        if(hits != null) HideObjects(true);
        hits = Physics.SphereCastAll(transform.position, 0.5f, (cameraPoint.position - transform.position), Vector3.Distance(transform.position, cameraPoint.position));
        HideObjects(false);
    }

    private void LateUpdate()
    {
        if (Input.GetKey(KeyCode.Mouse1))
        {
            TurnCameraLook();
        }
        desiredPosition = follow.transform.position + offset;
        smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = smoothedPosition;
    }

    void TurnCameraLook()
    {
        Quaternion camTurnAngle = Quaternion.AngleAxis(Input.GetAxis("Mouse X") * rotationSpeed, Vector3.up);
        offset = camTurnAngle * offset;
        transform.LookAt(player.transform);
    }

    
    void HideObjects(bool state)
    {
        foreach (RaycastHit hit in hits)
        {
            Renderer r = null;
            if (hit.transform != null && !hit.collider.isTrigger && !hit.collider.CompareTag("Invisible")) r = hit.collider.GetComponent<Renderer>();
            if (r)
            {
                Color temp = r.material.color;
                temp.a = state ? 1f : 0.5f;
                r.material.color = Color.Lerp(r.material.color, temp, 0.2f);
                if (!state)
                {
                    r.material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.One);
                    r.material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
                    r.material.SetInt("_ZWrite", 0);
                    r.material.DisableKeyword("_ALPHATEST_ON");
                    r.material.DisableKeyword("_ALPHABLEND_ON");
                    r.material.EnableKeyword("_ALPHAPREMULTIPLY_ON");
                    r.material.renderQueue = 3000;
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
            }
        }
    }

    IEnumerator LateCameraPositionStart()
    {
        yield return new WaitForSeconds(1.5f);
        TurnCameraLook();
        yield return null;
    }
}

