using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Textarea : MonoBehaviour
{
    public static string stringToEdit = "Explode;";

    public void OnGUI()
    {
        stringToEdit = GUI.TextArea(new Rect(10, 10, 400, 300), stringToEdit, 100);
        
        
    }

    public void OnSub()
    {
        string[] commands = stringToEdit.Split(';');
        foreach (string command in commands)
        {
            Debug.Log(command);
        }
    }
    // Start is called before the first frame update
    public void Start()
    {
        // Make a multiline text area that modifies stringToEdit.
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
