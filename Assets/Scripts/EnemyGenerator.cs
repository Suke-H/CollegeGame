using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyGenerator : MonoBehaviour
{
    public GameObject enemyPrefab_1;
    public GameObject enemyPrefab_2;
    int[] schedule_1 = {0, 365*4};
    int[] schedule_2 = {365*2, 365*3};
    [SerializeField] private float span_1;
    [SerializeField] private float span_2;

    float delta_1 = 0;
    float delta_2 = 0;

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

    void TimeSync()
    {
        TimeController timeCon = GameObject.Find("GameManager").GetComponent<TimeController>();
        progress_date = timeCon.progress_date;
    }

    void Start()
    {
        SetScreenVertices();
        TimeSync();
    }

    (float x, float y) RandomPosition(){
        float px = Random.Range(-x_lim, x_lim);
        float py = Random.Range(-y_lim, y_lim);

        return (x: px, y: py);
    }

    void Sporn(GameObject enemyPrefab, int[] schedule)
    {
        TimeSync(); // 時間同期

        // スケジュールが合っていたら敵生成
        if (schedule[0] <= this.progress_date & this.progress_date < schedule[1]){

            Debug.Log(schedule);
            Debug.Log(progress_date);

            GameObject enemy = Instantiate(enemyPrefab);
            var pos = RandomPosition();
            enemy.transform.position = new Vector3(pos.x, pos.y, 0);
        }
    }

    void Update()
    {
        
        this.delta_1 += Time.deltaTime;
        this.delta_2 += Time.deltaTime;

        if (this.delta_1 > this.span_1){
            Sporn(enemyPrefab_1, schedule_1);
            this.delta_1 = 0;
        }

        if (this.delta_2 > this.span_2){
            Sporn(enemyPrefab_2, schedule_2);
            this.delta_2 = 0;
        }
        
    }
}