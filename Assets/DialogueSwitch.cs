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


        switch (startIndex)
        {
            case 0:
                tmp.text = "Hello. Welcome to Metatronic.";
                break;
            case 1: 
                tmp.text = "To move around, use the WASD keys.";
                break;
            case 2:
                maxTimer = 5f;
                tmp.text = "Press V to switch between modes. \n There's a 3 second cooldown timer";
                break;
            case 3:
                maxTimer = 6f;
                tmp.text = "Now, I want to see you put the crate into the little barrier I've set out.";
                break;
            case 4:
                maxTimer = 9f;
                tmp.text = "This will unlock the door.";
                break;
            case 5:
                tmp.text = "Great work.";
                maxTimer = 3f;
                break;
            case 6:
                tmp.text = "Head into the next room.";
                maxTimer = 6f;
                break;
            case 7:
                tmp.text = "Go ahead and try to leave. You'll be greeted with friends.";
                maxTimer = 7f;
                break;
            case 8:
                tmp.text = "You probably realized you can do all sorts of things by now. \n Try sprinting with the shift key.";
                break;
        }


    }


   
}
