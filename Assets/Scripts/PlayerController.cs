using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D rigid;
    [SerializeField] private float max_x_Speed = 3.0f;
    [SerializeField] private float max_y_Speed = 3.0f;
    // float moveForce = 90.0f;
    Vector3 rightVec;
    Vector3 upVec;
    float x_lim;
    float y_lim;

    //画面四隅の座標を設定
    private void SetScreenVertices()
    {
        
        // カメラの右上の座標を取ってきて、x,yの範囲を取得
        Vector2 TopRight = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));
        x_lim = TopRight.x;
        y_lim = TopRight.y;

    }

    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        rightVec = transform.right;
        upVec = transform.up;

        SetScreenVertices();
    }

    void Update()
    {

        int x_key = 0;
        int y_key = 0;

        // キー入力
        if (Input.GetKey(KeyCode.RightArrow)) x_key = 1; 
        if (Input.GetKey(KeyCode.LeftArrow)) x_key = -1;
        if (Input.GetKey(KeyCode.UpArrow)) y_key = 1;
        if (Input.GetKey(KeyCode.DownArrow)) y_key = -1;
        
        // maxSpeedを超えていない or 今の方向と入力方向が違えばAddForce
        float speedx = Mathf.Abs(rigid.velocity.x);
        float speedy = Mathf.Abs(rigid.velocity.y);
        int current_x_key = (int)Mathf.Sign(rigid.velocity.x);
        int current_y_key = (int)Mathf.Sign(rigid.velocity.y);

        if (0 != x_key){
            rigid.velocity = new Vector3(x_key * max_x_Speed, rigid.velocity.y, 0);
        }

        if (0 != y_key){
            rigid.velocity = new Vector3(rigid.velocity.x, y_key * max_y_Speed, 0);
        }

        // 枠に当たったら反射
        if (transform.position.x < -x_lim | x_lim < transform.position.x){
            rigid.velocity = new Vector3(-rigid.velocity.x, rigid.velocity.y, 0);
        }
        if (transform.position.y < -y_lim | y_lim < transform.position.y){
            rigid.velocity = new Vector3(rigid.velocity.x, -rigid.velocity.y, 0);
        }

    }

    

}
