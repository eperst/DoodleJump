
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    public Transform player;
    public Text scoreText;
    private float score = 0;
    
    public float S
    {
        get { return score; }
    }
    // Update is called once per frame
    void Update()
    {
        if(player.position.y * 4 > score )
        {
            score = player.position.y * 4;
            scoreText.text = score.ToString("0");
        }
    }
}
