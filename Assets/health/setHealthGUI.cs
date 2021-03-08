using UnityEngine;
using System.Collections;

public class setHealthGUI : MonoBehaviour
{
    Material M;
    public float hlt = 1;
    float newhlt = 1;

    private Renderer healthMaterial;
    // Use this for initialization
    void Start()
    {
        healthMaterial = GetComponent<Renderer>();
        ShowGUI(hlt);
    }
    void Update()
    {
        if (newhlt != hlt)
        {
            hlt = Mathf.Lerp(hlt, newhlt, Time.deltaTime * 10);
            ShowGUI(hlt);
        }

    }

    public void setGUI(float value)
    {
        newhlt = value;

    }
    public void ShowGUI(float value)
    {
        if (healthMaterial)
        {
            M = healthMaterial.material;
            M.SetFloat("_Value", value);
        }
    }
}
