using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class PanelOpener : MonoBehaviour
{

    public GameObject panel;
    public GameObject closeButton;
    public GameObject crosshair;

    public void OpenPanel()
    {
        if (panel && closeButton != null)
        {
            panel.SetActive(true);
            closeButton.SetActive(true);
            crosshair.SetActive(false);
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
