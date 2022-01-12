using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{

    [SerializeField] private Transform playerTrans; //追いかける対象のTransform
    [SerializeField] private float bulletSpeed;     //弾の速度
    [SerializeField] private float limitSpeed;      //弾の制限速度
    private Rigidbody2D rb;                         //弾のRigidbody2D
    private Transform bulletTrans;                  //弾のTransform

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        bulletTrans = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 vector3 = playerTrans.position - bulletTrans.position;  //弾から追いかける対象への方向を計算
        rb.AddForce(vector3.normalized * bulletSpeed);                  //方向の長さを1に正規化、任意の力をAddForceで加える

        float speedXTemp = Mathf.Clamp(rb.velocity.x, -limitSpeed, limitSpeed);  //X方向の速度を制限
        float speedYTemp = Mathf.Clamp(rb.velocity.y, -limitSpeed, limitSpeed);  //Y方向の速度を制限
        rb.velocity = new Vector3(speedXTemp, speedYTemp);
    }
}
