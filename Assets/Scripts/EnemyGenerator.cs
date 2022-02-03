using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGenerator : MonoBehaviour
{
    public GameObject enemyPrefab_1;
    public GameObject enemyPrefab_2;
    [SerializeField] private float span;
    float delta = 0;
    float x_lim;
    float y_lim;

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

        // Debug.Log(x_lim);
        // Debug.Log(y_lim);
    }

    void Start()
    {
        SetScreenVertices();
    }

    (float x, float y) RandomPosition(){

        float px = Random.Range(-x_lim, x_lim);
        float py = Random.Range(-y_lim, y_lim);

        return (x: px, y: py);
    }

    void Update()
    {
        this.delta += Time.deltaTime;

        // 一定のスパンで生成
        if (this.delta > this.span)
        {
            // 時間を戻す
            this.delta = 0;

            // プレハブ初期化
            GameObject go_1 = Instantiate(enemyPrefab_1);
            GameObject go_2 = Instantiate(enemyPrefab_2);

            // 生成位置
            var pos_1 = RandomPosition();
            var pos_2 = RandomPosition();

            // 生成
            go_1.transform.position = new Vector3(pos_1.x, pos_1.y, 0);
            go_2.transform.position = new Vector3(pos_2.x, pos_2.y, 0);

        }
    }
}