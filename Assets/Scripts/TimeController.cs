using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeController : MonoBehaviour
{
    [SerializeField] private float one_year; // 一年の秒数
    float delta = 0;
    float one_day;
    public int progress_date = 0;
    GameObject ProgressCircle;
    GameObject Grade;

    bool year2_flag = false;
    bool year3_flag = false;
    bool year4_flag = false;

    GameManager gameManager;

    void NextGrade(int r, int g, int b, int grade)
    {
        this.ProgressCircle.GetComponent<Image>().color = new Color(r/255.0f, g/255.0f, b/255.0f, 255/255.0f);
        this.Grade.GetComponent<Text>().color = new Color(r/255.0f, g/255.0f, b/255.0f, 255/255.0f);
        this.Grade.GetComponent<Text>().text = grade.ToString() + "年";
    }

    // Start is called before the first frame update
    void Start()
    {
        float one_day = one_year / 365.0f;
        this.ProgressCircle = GameObject.Find("ProgressCircle");
        this.Grade = GameObject.Find("Grade");
        NextGrade(68, 167, 196, 1);
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        this.delta += Time.deltaTime;
        if (this.delta > this.one_day)
        {
            this.delta = 0;
            this.progress_date += 1;
            this.ProgressCircle.GetComponent<Image>().fillAmount += 1.0f/365.0f;
        }

        // 1年経過
        if (365 <= this.progress_date & this.progress_date < 365*2){
            if (!year2_flag){
                NextGrade(100, 200, 20, 2);
                this.ProgressCircle.GetComponent<Image>().fillAmount = 0;
                year2_flag = true;
            }
        }

        // 2年経過
        if (365*2 <= this.progress_date & this.progress_date < 365*3){
            if (!year3_flag){
                NextGrade(255, 150, 0, 3);
                this.ProgressCircle.GetComponent<Image>().fillAmount = 0;
                year3_flag = true;
            }
        }

        // 3年経過
        if (365*3 <= this.progress_date & this.progress_date < 365*4){
            if (!year4_flag){
                NextGrade(192, 0, 0, 4);
                this.ProgressCircle.GetComponent<Image>().fillAmount = 0;
                year4_flag = true;
            }
        }

        // 4年が経過すればゲームクリア
        if (365*4 <= this.progress_date){
            // ゲームオーバーしてなければ
            if (!gameManager.GameOverFlag){
                    gameManager.GameClear();
                }
        }
    }

}
