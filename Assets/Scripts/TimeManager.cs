using System.Text;
using TMPro;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    public float seconds = 0;
    public int minutes = 0;
    private TMP_Text textUI;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        textUI = transform.GetComponent<TMP_Text>();
        seconds = DataManager.instance.gameData.saved_seconds;
        minutes = DataManager.instance.gameData.saved_minutes;
    }

    // Update is called once per frame
    void Update()
    {
        seconds += Time.deltaTime;
        DataManager.instance.gameData.saved_seconds += Time.deltaTime;

        if (seconds > 60f)
        {
            minutes += 1;
            seconds = 0f;
        }
        if (DataManager.instance.gameData.saved_seconds > 60f)
        {
            DataManager.instance.gameData.saved_minutes += 1;
            DataManager.instance.gameData.saved_seconds = 0f;
        }
        updateText();
    }
    
    public void updateText()
    {
        StringBuilder new_string = new StringBuilder("time dreamt: ", capacity: 15);
        if(minutes >= 1)
        {
            new_string.Append(minutes);
            new_string.Append(":");
            if(seconds < 10)
            {
                new_string.Append("0");
            }
            new_string.Append(Mathf.Round(seconds));
        }
        else
        {
            new_string.Append(Mathf.Round(seconds * 100f) / 100f);
        }
        textUI.text = new_string.ToString();
        new_string = null;
    }
}
