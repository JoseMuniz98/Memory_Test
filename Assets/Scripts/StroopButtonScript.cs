using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StroopButtonScript : MonoBehaviour
{
    public StroopScript Stroop;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void onStroopClick()
    {
        string thisColor = GetComponentInChildren<Button>().GetComponentInChildren<Text>().text;
        string targetColor = Stroop.targetColor.text;
        if(thisColor == targetColor)
        {
            Stroop.hits++;
            Debug.Log("Hit");
        }
        else
        {
            Debug.Log("Miss");
        }
        Stroop.clicks++;
        Stroop.round++;
        Debug.Log(Stroop.round);
        Stroop.shuffle();
    }
}
