using UnityEngine;
using System;

public class Territory : MonoBehaviour
{
    [SerializeField] SpriteRenderer spriteRenderer;
    private PlayerNumberName playerNumberName;
    public Action<PlayerNumberName, GameObject> OnHitTerritory { set; private get; } = null;

    public void SetPlayerNumberName(PlayerNumberName pnn) {
        this.playerNumberName = pnn;
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeSprite(Sprite sprite)
    {
        spriteRenderer.sprite = sprite;
    }

    void OnCollisionEnter(Collision collision)
    {
        if (OnHitTerritory != null)
        {
            OnHitTerritory(this.playerNumberName, collision.gameObject);
        }
    }
}
