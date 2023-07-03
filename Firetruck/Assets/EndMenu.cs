using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class EndMenu : MonoBehaviour
{




    public TextMeshProUGUI[] HighscoresText,HighscoresNames;
    public TextMeshProUGUI Score;
    public float highscore;
    public float score;
    [SerializeField] int amoutOfHighscores;
    string[] highscorestring;
    string[] highscorenamestring;
    //playerStats.score;

    bool firstload = true;
    [SerializeField] GameObject HighScorePanel,HappySong,SadSong;
    [SerializeField] TMP_InputField namefield;
    int nameSpotIndex;

    float resetTime=3;


    private void Update()
    {
        if(resetTime<0)
        {
            for (int i = 0; i < amoutOfHighscores; i++)
            {
               
               
                    PlayerPrefs.SetFloat(highscorestring[i], 0);
                    PlayerPrefs.SetString(highscorenamestring[i], "");

             

            }

            Debug.Log("reset");
            resetTime = 99;
        }


        if(Input.GetKey(KeyCode.R))
        {
            resetTime -= Time.deltaTime;
        }
        else
        {
            resetTime = 3;
        }
    }


    void Start()
    {
       
         
      
            highscorenamestring = new string[amoutOfHighscores];
            highscorestring = new string[amoutOfHighscores];
      

        for(int i=0;i<amoutOfHighscores;i++)
        {
            highscorenamestring[i] = "highscorename" + i;
            highscorestring[i] = "highscore" + i;

        }



        score = PlayerPrefs.GetFloat("score");


        Score.SetText(score.ToString());

        CalculateScores();

     

    }
        public void CalculateScores()
        {
            for (int i = 0; i < amoutOfHighscores; i++)
            {
              


                if (score > PlayerPrefs.GetFloat(highscorestring[i]))
                {

              
                   if (firstload)
                    {
                       firstload = false;
                        HighScorePanel.SetActive(true);
                    HappySong.SetActive(true);
                    SadSong.SetActive(false);
                    }
                for (int a = amoutOfHighscores -1 ; a > i ; a--)
                    {

                        PlayerPrefs.SetFloat(highscorestring[a], PlayerPrefs.GetFloat(highscorestring[a - 1]));

                        PlayerPrefs.SetString(highscorenamestring[a], PlayerPrefs.GetString(highscorenamestring[a - 1]));

                    }

                    nameSpotIndex = i;
                    PlayerPrefs.SetFloat(highscorestring[i], score);
                    i = amoutOfHighscores;

                }
            }
        ShowScores();


    }
    void ShowScores()
    {
        for (int i = 0; i < HighscoresText.Length; i++)
        {
            HighscoresText[i].SetText(PlayerPrefs.GetFloat(highscorestring[i]).ToString());

        }



        for (int i = 0; i < HighscoresText.Length; i++)
        {
            if(PlayerPrefs.GetString(highscorenamestring[i]) != "")
            {
                HighscoresNames[i].SetText(PlayerPrefs.GetString(highscorenamestring[i]));
            }
            else
            {
                HighscoresNames[i].SetText("---");
            }
           

        }
    }



        public void SetName()
        {
          if(namefield.text.Length == 3)
            {
            PlayerPrefs.SetString(highscorenamestring[nameSpotIndex], namefield.text);
            ShowScores();
            HighScorePanel.SetActive(false);
            }
        }

        public void Restart()
        {

            SceneManager.LoadScene("SampleScene");
        }

        public void LoadMenu()
        {
            Time.timeScale = 1f;

            SceneManager.LoadScene("MainMenu");
        }







    }
