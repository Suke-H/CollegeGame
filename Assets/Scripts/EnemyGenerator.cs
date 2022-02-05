using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyGenerator : MonoBehaviour
{
    public GameObject enemyPrefab_1;
    int[] schedule_1 = {0, 365*3};
    [SerializeField] private float span_1;
    float delta_1 = 0;

    public GameObject enemyPrefab_2;
    int[] schedule_2 = {365*2, 365*3};
    [SerializeField] private float span_2;
    float delta_2 = 0;

    public GameObject enemyPrefab_3;
    int[] schedule_3 = {0, 365*3};
    [SerializeField] private float span_3;
    float delta_3 = 0;

    public GameObject enemyPrefab_4;
    int[] schedule_4 = {0, 365*3};
    [SerializeField] private float span_4;
    float delta_4 = 0;

    public GameObject enemyPrefab_5;
    int[] schedule_5 = {365*3, 365*4};
    [SerializeField] private float span_5;
    float delta_5 = 0;

    float x_lim;
    float y_lim;
    GameObject TimeController;
    int progress_date;

    //画面四隅の座標を設定
    private void SetScreenVertices()
    {
        // カメラの右上の座標を取ってきて、x,yの範囲を取得
        Vector2 TopRight = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));
        x_lim = TopRight.x;
        y_lim = TopRight.y;

        // 0.5引く
        x_lim = x_lim - 0.5f;
        y_lim = y_lim - 0.5f;
    }

    // 時間同期
    void TimeSync()
    {
        TimeController timeCon = GameObject.Find("GameManager").GetComponent<TimeController>();
        progress_date = timeCon.progress_date;
    }

    // 初期スポーン時間をランダムにずらす
    void RandomSeed(){
        
        this.delta_1 = Random.Range(0.0f, 1.0f);
        this.delta_2 = Random.Range(0.0f, 1.0f);
        this.delta_3 = Random.Range(0.0f, 1.0f);
        this.delta_4 = Random.Range(0.0f, 1.0f);
        this.delta_5 = Random.Range(0.0f, 1.0f);
    }

    // ランダムな位置にスポーン
    (float x, float y) RandomPosition(){
        float px = Random.Range(-x_lim, x_lim);
        float py = Random.Range(-y_lim, y_lim);

        return (x: px, y: py);
    }

    // スポーン
    void Spawn(GameObject enemyPrefab, int[] schedule)
    {
        TimeSync(); // 時間同期
        // スケジュールが合っていたらスポーン
        if (schedule[0] <= this.progress_date & this.progress_date < schedule[1]){
            GameObject enemy = Instantiate(enemyPrefab);
            var pos = RandomPosition();
            enemy.transform.position = new Vector3(pos.x, pos.y, 0);
        }
    }

    void Start()
    {
        SetScreenVertices();
        TimeSync();
        RandomSeed();
    }

    void Update()
    {
        // 時間経過
        this.delta_1 += Time.deltaTime;
        this.delta_2 += Time.deltaTime;
        this.delta_3 += Time.deltaTime;
        this.delta_4 += Time.deltaTime;
        this.delta_5 += Time.deltaTime;

        // スポーン
        if (this.delta_1 > this.span_1){
            Spawn(enemyPrefab_1, schedule_1);
            this.delta_1 = 0;
        }

        if (this.delta_2 > this.span_2){
            Spawn(enemyPrefab_2, schedule_2);
            this.delta_2 = 0;
        }

        if (this.delta_3 > this.span_3){
            Spawn(enemyPrefab_3, schedule_3);
            this.delta_3 = 0;
        }

        if (this.delta_4 > this.span_4){
            Spawn(enemyPrefab_4, schedule_4);
            this.delta_4 = 0;
        }

        if (this.delta_5 > this.span_5){
            Spawn(enemyPrefab_5, schedule_5);
            this.delta_5 = 0;
        }
        
    }
}