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
    Collider2D co2D;

    // GameObject GameResult;
    GameManager gameManager;

    float delta = 0;
    float vanishTime = 8.0f; // スポーンしてから消滅するまでの時間
    float invincibleTime = 1.0f; // スポーン時の無敵時間

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("ore");
        rb = GetComponent<Rigidbody2D>();
        bulletTrans = GetComponent<Transform>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();

        // 最初の無敵時間だけ透ける判定に
        this.co2D = GetComponent<Collider2D>();
        this.co2D.isTrigger = true;
    }

    // Update is called once per frame
    void Update()
    {

        // 時間経過
        this.delta += Time.deltaTime;

        // 時間経過で無敵解除
        if (this.delta > this.invincibleTime){
            this.co2D.isTrigger = false;
        }

        // 時間経過で消滅
        if (this.delta > this.vanishTime){
            Destroy(gameObject);
        }

        Vector3 vector3 = player.transform.position - bulletTrans.position;  //弾から追いかける対象への方向を計算
        rb.AddForce(vector3.normalized * bulletSpeed);                  //方向の長さを1に正規化、任意の力をAddForceで加える

        float speedXTemp = Mathf.Clamp(rb.velocity.x, -limitSpeed, limitSpeed);  //X方向の速度を制限
        float speedYTemp = Mathf.Clamp(rb.velocity.y, -limitSpeed, limitSpeed);  //Y方向の速度を制限
        rb.velocity = new Vector3(speedXTemp, speedYTemp);
    }

    // ゲームオーバー判定
    void OnCollisionEnter2D(Collision2D collision)
    {
        // if (this.delta > this.invincibleTime){ // 無敵時間を過ぎていれば
            if (!gameManager.GameClearFlag){ // クリアしてなければ
                if (collision.gameObject.name == "ore"){ // プレイヤーに衝突したら
                    gameManager.GameOver(); // ゲームオーバー
                }
            }
        
    }
}
