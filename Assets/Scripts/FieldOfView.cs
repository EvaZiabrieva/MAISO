using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FieldOfView : MonoBehaviour
{
    [SerializeField] private Image fov;
    [SerializeField] private Color triggeredColor = Color.red;
    private Color startColor;
    private void Start()
    {
        startColor = fov.color;
    }
    public void SetRadius(float radius)
    {
        fov.transform.localScale = Vector3.one * radius;
    }

    public void SetTriggerded(bool isTriggered)
    {
        if (isTriggered)
            fov.color = triggeredColor;
        else
            fov.color = startColor;
    }
}
