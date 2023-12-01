using TMPro;
using UnityEngine;

public class FPSCounter : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI text;

    [SerializeField] private Color badColor = Color.red;
    [SerializeField] private Color neutralColor = Color.yellow;
    [SerializeField] private Color goodColor = Color.cyan;

    [SerializeField] private float badValue;
    [SerializeField] private float neutralValue;

    [SerializeField] private float fps;

    private const float updateInterval = 0.5f;

    private float accum;
    private float timeLeft;
    private float frames;

    private void Update()
    {
        timeLeft -= Time.deltaTime;
        accum += Time.timeScale / Time.deltaTime;
        ++frames;

        if (timeLeft <= 0)
        {
            fps = accum / frames;

            if (fps < badValue)
            {
                text.color = badColor;
            }
            else if (fps < neutralValue)
            {
                text.color = neutralColor;
            }
            else
            {
                text.color = goodColor;
            }

            text.text = fps.ToString("f1");
            timeLeft = updateInterval;
            accum = 0;
            frames = 0;
        }
    }
}