using UnityEngine;
using TMPro;

public class PlayerNameFollow : MonoBehaviour
{
    // インスペクターからプレイヤーオブジェクトを設定できるようにする
    [SerializeField] private GameObject playerObject;

    // プレイヤー名の表示位置オフセット
    [SerializeField] private Vector3 offset = new Vector3(0f, 1.5f, 0f);

    private void Start()
    {
        // プレイヤーオブジェクトが設定されていない場合はエラーログを出力
        if (playerObject == null)
        {
            Debug.LogError("プレイヤーオブジェクトが設定されていません！");
            return;
        }
    }

    private void LateUpdate()
    {
        // プレイヤーオブジェクトがない場合は処理しない
        if (playerObject == null)
            return;

        // プレイヤーの位置に合わせて名前の位置を更新
        // LateUpdateを使用することで、プレイヤーの移動後に名前の位置を更新
        transform.position = playerObject.transform.position + offset;

        // もしカメラに対して常に正面を向くようにしたい場合は以下のコードを使用
        // transform.rotation = Camera.main.transform.rotation;
    }
}