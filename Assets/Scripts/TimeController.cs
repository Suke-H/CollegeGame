using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeController : MonoBehaviour
{
    [SerializeField] private float one_year; // 一年の秒数
    float delta = 0;
    float one_day;
    int progress_date = 0;
    GameObject ProgressCircle;

    // Start is called before the first frame update
    void Start()
    {
        float one_day = one_year / 365;
        this.ProgressCircle = GameObject.Find("ProgressCircle");
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
    }
}
