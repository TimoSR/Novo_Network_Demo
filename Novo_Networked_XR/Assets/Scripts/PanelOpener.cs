using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Serialization;

public class PanelOpener : MonoBehaviour
{

    //Panel 
    public GameObject panel;
    public GameObject closeButton;
    public GameObject crosshair;
    public TMP_Text textArea;
    private StringReader strReader;
    private string _objectName;
    private char _firstCharacter;


    //Data link
    private readonly string getURL = "https://nnedigitaldesignstorage.blob.core.windows.net/candidatetasks/Metadata.csv?sp=r&st=2021-03-15T09:12:39Z&se=2024-11-05T17:12:39Z&spr=https&sv=2020-02-10&sr=b&sig=oyj3Qyg4W42%2BO0d7YqmjxmKk0k%2BLVmE243ixdLaq3gk%3D";
    
    //Panel Activation

    private void Start()
    {
        _objectName = gameObject.name;
        _firstCharacter = _objectName[0];
    }

    public void OpenPanel()
    {
        if (panel && closeButton != null)
        {
            panel.SetActive(true);
            closeButton.SetActive(true);
            crosshair.SetActive(false);
        }
    }

    //CSV Data Request
    
    IEnumerator GetRequest(string url, Action<UnityWebRequest> callback)
    {
        using (UnityWebRequest request = UnityWebRequest.Get(url))
        {
            // Send the request and wait for a response
            yield return request.SendWebRequest();
            callback(request);
        }
    }
    
    public void GetPosts()
    {
        StartCoroutine(GetRequest(getURL, (UnityWebRequest req) =>
        {
            if (req.isNetworkError || req.isHttpError)
            {
                Debug.Log($"{req.error}: {req.downloadHandler.text}");
            } else
            {
                
                strReader= new StringReader(req.downloadHandler.text);

                var text = strReader.ReadToEnd().Split('\n');
                
                //Debug.Log(text.Length);
                
                //sDebug.Log("Object name: " + _objectName + "First Char: " + _firstCharacter);

                for (int i = 1; i < text.Length-1; i++)
                {
                    Debug.Log(i);
                    
                    if (text[i][2] == _firstCharacter)
                    {
                        textArea.text = text[i];
                    }
                }

                //string[] cols = text.Split(';');

                //textArea.text = text[1][2].ToString();
            }
        }));
    }

}
