using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelCloser : MonoBehaviour
{
    
    public GameObject panel;
    public GameObject closeButton;
    public GameObject crosshair;
    
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            ClosePanel();
        }
        
    }
    
    public void ClosePanel()
    {
        if (panel && closeButton != null)
        {
            panel.SetActive(false);
            closeButton.SetActive(false);
            crosshair.SetActive(true);
        }
    }

}
