using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyController : MonoBehaviour
{

    GameObject player; //追いかける対象のTransform
    [SerializeField] private float bulletSpeed;     //弾の速度
    [SerializeField] private float limitSpeed;      //弾の制限速度
    private Rigidbody2D rb;                         //弾のRigidbody2D
    private Transform bulletTrans;                  //弾のTransform

    // GameObject GameResult;
    GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("ore");
        rb = GetComponent<Rigidbody2D>();
        bulletTrans = GetComponent<Transform>();

        // this.GameResult = GameObject.Find("GameResult");
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 vector3 = player.transform.position - bulletTrans.position;  //弾から追いかける対象への方向を計算
        rb.AddForce(vector3.normalized * bulletSpeed);                  //方向の長さを1に正規化、任意の力をAddForceで加える

        float speedXTemp = Mathf.Clamp(rb.velocity.x, -limitSpeed, limitSpeed);  //X方向の速度を制限
        float speedYTemp = Mathf.Clamp(rb.velocity.y, -limitSpeed, limitSpeed);  //Y方向の速度を制限
        rb.velocity = new Vector3(speedXTemp, speedYTemp);
    }

    // ゲームオーバー判定
    void OnTriggerEnter2D(Collider2D collision)
    {
        // クリアしてなければ
        if (!gameManager.GameClearFlag){

            // プレイヤーに衝突したらゲームオーバー
            if (collision.gameObject.name == "ore"){
                // this.GameResult.GetComponent<Text>().text = "GAME OVER...";
                gameManager.GameOver();
            }
        }
    }
}
