using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*
 *  The purpose of this script is to provide a means to display information and options, and then respond to those options accordingly.
 */

//  Let's imagine the window as a tool, or husk, for other services to use.
//  1- the Buttons should not be added, but created by this Window
//      .1- attach ScriptableObjects with an OPTION_# to each Button
//  2- whatever creates this must supply the callbacks
//  3- instead of making it a static singleton reference (as you are doing now), have the service using it
//      operate it based on options given; this means whatever made this Window will have this kind of object reference.
//      Does this also mean I can have multiple windows up at once, each responding to separate options and information?
//  4- think about how these windows will be created in gamespace. Something is going to need to create and place them
//  5- WOW LOOK AT THIS PARAMETER YOU CAN DO FOR LIKE, INFINITE OPTIONS. SHOULD THAT BE ALLOWED?? :::   params OnButtonCallback[] options

public class DialogueWindowScript : MonoBehaviour
{
    //  DEBUG     ---------------------
    public static DialogueWindowScript instance = null;

    //  DELEGATES   ---------------------
    public delegate void OnButtonCallback();

    //  DIALOGUE LOGIC Assets   ---------------------
    OnButtonCallback m_callback1;
    OnButtonCallback m_callback2;
    OnButtonCallback m_callback3;
    OnButtonCallback m_callback4;

    //  UI Assets   ---------------------
    public List<Button> m_options;
    public Text m_windowText;

    //  ---------------------   UNITY Methods

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
    
    void Start ()
    {
		
	}
	
	void Update ()
    {
		
	}

    //  ---------------------   LOGIC Methods

    /// <summary>
    /// When creating this script, this method NEEDS to be called first.
    /// </summary>
    /// <param name="option1"></param>
    /// <param name="option2"></param>
    /// <param name="option3"></param>
    /// <param name="option4"></param>
    private void InitializeWindow(sDialogueWindowOptionParams parameters)
    {
        m_callback1 = parameters.option1_callback;
        m_callback2 = parameters.option2_callback;
        m_callback3 = parameters.option3_callback;
        m_callback4 = parameters.option4_callback;

        ChangeText(parameters.info_text);
    }

    //  ---------------------   MODIFIER Methods
    public void ChangeText(string new_text)
    {
        m_windowText.text = new_text;
    }

    //  ---------------------   BUTTON Methods

    /// <summary>
    /// The event other buttons will call when pressed. 
    /// </summary>
    /// <param name="option_num"></param>
    public void BUTTON_OnPress(int option_num)
    {
        switch (option_num)
        {
            case 0:
                {
                    m_callback1();
                }
                break;
            case 1:
                {
                    m_callback2();
                }
                break;
            case 2:
                {
                    m_callback3();
                }
                break;
            case 3:
                {
                    m_callback4();
                }
                break;
        }
    }

    //  If this is not a singleton, destroy this function -- the InitializeWindow should be getting called!
    public static void StartNewDialogue(sDialogueWindowOptionParams parameters)
    {
        DialogueWindowScript.instance.InitializeWindow(parameters);
    }
    //  If this is not a singleton, destroy this function
    public static void DEBUG_ChangeText(string new_text)
    {
        instance.ChangeText(new_text);   
    }

}


public struct sDialogueWindowOptionParams
{
    public DialogueWindowScript.OnButtonCallback option1_callback;
    public DialogueWindowScript.OnButtonCallback option2_callback;
    public DialogueWindowScript.OnButtonCallback option3_callback;
    public DialogueWindowScript.OnButtonCallback option4_callback;

    public string info_text;
}
