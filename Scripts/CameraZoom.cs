using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraZoom : MonoBehaviour {

    public Slider slider;
    private float offset = -1.38f;
    private Camera camera;

    private void Start()
    {
        camera = GetComponent<Camera>();
    }

    void Update () {
        //Debug.Log(slider.value);
        camera.transform.position = new Vector3(0f, 0f, offset + slider.value);

	}
}
