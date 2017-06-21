using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 *  This is dummy entity. It's purpose? To provide some sort of functioning machine to respond to you.
 *  Give it info, other scripts, etc.
 */

public class EntityScript : MonoBehaviour
{
    //  UNITY   ---------------------
    Camera mainCamera;

    //  6/20: DIALOGUE WINDOW  ---------------------
    

    //  ---------------------   UNITY

    // Use this for initialization
    void Start ()
    {
        mainCamera = Camera.main;
	}
	
	// Update is called once per frame
	void Update ()
    {
        #region On Click Detection
        if(Input.GetMouseButtonDown(0))
        {
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if(Physics.Raycast(ray, out hit))
            {
                OnClicked();
            }

        }
        #endregion

    }

    //  ---------------------   DEBUG

    private void OnClicked()
    {
        StartDialogue();
    }

    private void StartDialogue()
    {
        sDialogueWindowOptionParams parameters;
        parameters.option1_callback = Anger;
        parameters.option2_callback = Disgust;
        parameters.option3_callback = Awww;
        parameters.option4_callback = LastOption;
        parameters.info_text = "Choose an option.";
        DialogueWindowScript.StartNewDialogue(parameters);
    }

    private void Anger()
    {
        DialogueWindowScript.DEBUG_ChangeText("I am angry.");
    }

    private void Disgust()
    {
        DialogueWindowScript.DEBUG_ChangeText("I am DIGUSTED.");
    }

    private void Awww()
    {
        DialogueWindowScript.DEBUG_ChangeText("I am awww");
    }

    private void LastOption()
    {
        DialogueWindowScript.DEBUG_ChangeText("FLUSH THE YAYO");
    }


}
