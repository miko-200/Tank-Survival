using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameOver : MonoBehaviour
{
   public TextMeshProUGUI timerFinal;
   public TextMeshProUGUI time;

   public void GameOverTimer()
   {
      timerFinal.text = time.text;
   }
}
