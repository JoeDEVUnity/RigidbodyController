using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class DialogueSwitch : MonoBehaviour
{
    private float textTimer;
    private TMP_Text tmp;

    public float maxTimer = 4f;
    private int startIndex;
    private bool enteredRoom01;

    // Grab ref to the player script in order to access shortcut for accessing Dialogue

    public List<string> textList = new List<string>();

    // Start is called before the first frame update
    void Start()
    {
        tmp = GetComponent<TMP_Text>();
    }

    // Update is called once per frame
    void Update()
    {
        tmp.text = textList[startIndex];
        SwitchText();
        textTimer += Time.deltaTime;
    }


    void SwitchText()
    {

        if(textTimer > maxTimer)
        {
            // Add one to nextIndex
            startIndex += 1;
            textTimer = 0f;
        } 

        // Switch indexes 
        switch (startIndex)
        {
            case 0:
                tmp.text = "Welcome to Metatronic.";
                break;
            case 1:
                tmp.text = "To navigate around, use the mouse.";
                tmp.color = Color.white;
                break;
            case 2:
                tmp.text = "You can as well shoot out a ray using the left mouse button.";
                break;
            case 3:
                tmp.text = "Use V to switch between pick-up, and ray expulsion";
                break;
            case 4:
                tmp.text = "Jump with SPACEBAR, and Sprint with SHIFT";
                break;
            case 5:
                tmp.text = "To access this menu again, press H";
                tmp.color = Color.yellow;
                break;
        }


    }


   
}
