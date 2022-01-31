using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGenerator : MonoBehaviour
{
    public GameObject enemyPrefab;
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

        Debug.Log(x_lim);
        Debug.Log(y_lim);

    }

    void Start()
    {
        SetScreenVertices();
    }

    void Update()
    {
        this.delta += Time.deltaTime;
        if (this.delta > this.span)
        {
            this.delta = 0;
            GameObject go = Instantiate(enemyPrefab);

            float px = Random.Range(-x_lim, x_lim);
            float py = Random.Range(-y_lim, y_lim);
            // Debug.Log(px);
            // Debug.Log(py);
            go.transform.position = new Vector3(px, py, 0);

        }
    }
}
