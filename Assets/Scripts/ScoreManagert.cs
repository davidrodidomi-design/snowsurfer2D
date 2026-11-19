using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
  [SerializeField] TextMeshProUGUI scoreText; // Reference to the TextMeshProUGUI component for displaying the score
  public void UpdateScore(int score)
  {
    scoreText.text =  score.ToString("00000"); // Update the score text with the current score
  }  
    
}
