using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManage : MonoBehaviour
{
   public PlayerControl tim;
    public TextMeshProUGUI TimerTest;
    public float Timer= 4;
    
    void Start()
    {
        //tim = GameObject.Find("GameOver").GetComponent<PlayerControl>();

    }
    void Update()
    {


        //    Timer -= Time.deltaTime;
        //    TimerTest.text = "Timer :" + Timer;


    }
    public void updateTimer()
    {
        
    }
    
}
    