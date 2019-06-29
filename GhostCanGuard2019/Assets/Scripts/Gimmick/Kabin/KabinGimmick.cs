using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Rigidbody))]
public class KabinGimmick : GimmickBase
{
    private Vector3 throwPos = Vector3.zero;   // 花瓶が飛んでいく場所
    [SerializeField] private float power = 400f; // 花瓶を押す力
    [SerializeField] private GameObject player = null;
    [SerializeField] private float speed = 0.01f;

    [SerializeField] private GameObject kabinBreaking = null;    // 割れた花瓶のprefab
    private Vector3 kabinSpawn = Vector3.zero;                 // 割れた花瓶の出現場所

    private bool kabinSetPos = false;       // 指定の場所に花瓶があるかどうか

    private Rigidbody rb = null;

    protected override void Start()
    {
        base.Start();
        GimmickEventSetUp(EventTriggerType.PointerDown, GimmickEventOpen);

        rb = GetComponent<Rigidbody>();
    }
    private void Update()
    {
        // 初期値のままだったらreturn
        if (kabinSpawn == Vector3.zero) return;

        KabinBreakingStart();
    }
    private void KabinGimmickSetup()
    {
        Vector3 playerPosition = player.transform.position;
        playerPosition += player.transform.forward;

        KabinToPlayer( playerPosition );
    }
    private void KabinGimmickAction()
    {
        KabinGimmickSetup();
        
        if (!kabinSetPos) return;
        if (!Input.GetMouseButtonDown(0)) return;

        Vector3 mousePos = Input.mousePosition;
        mousePos.z = 10f;
        throwPos = Camera.main.ScreenToWorldPoint(mousePos);
        Debug.Log(throwPos);
        throwPos.y = 1.0f;

        // 力を加える方向をきめる
        Vector3 direction = (throwPos - this.transform.position).normalized;
        Debug.Log(direction);
        Debug.Log("thorw!");
        rb.AddForce(direction * power);
        throwPos = Vector3.zero;

        GimmickManager.Instance.ClearGimmick();
        GimmickUIClose();
        ClearGimmickEvent();
    }
    // ButtonのonClickで呼ぶ関数
    public void ClickUIStart()
    {
        GimmickManager.Instance.SetGimmickAction( () => KabinGimmickAction() );
        GimmickUIsOnOff(false);
    }
    private void KabinToPlayer(Vector3 pPos)
    {
        while(transform.position != pPos)
        {
            transform.position = Vector3.MoveTowards(transform.position, pPos, speed);
        }
        kabinSetPos = true;
    }
    /// <summary>
    /// 花瓶が他のオブジェクトに衝突時の地点取得
    /// </summary>
    private void OnCollisionEnter(Collision collision)
    {
        // 花瓶のギミックが発動してなかったらreturn
        if (kabinSetPos == false) return;
        // playerにあたったならreturnする
        if (collision.gameObject == player) return;

        foreach (ContactPoint contact in collision.contacts)
        {
            // 飛ばした花瓶が接触した地点を取得
            kabinSpawn = contact.point;
        }
    }
    private void KabinBreakingStart()
    {
        Destroy(this.gameObject);
        kabinSpawn.y = 0.2f;
        Instantiate(kabinBreaking, kabinSpawn, Quaternion.identity);      
    }
}
