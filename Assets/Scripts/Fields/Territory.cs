using UnityEngine;
using System;

public class Territory : MonoBehaviour
{
    [SerializeField] SpriteRenderer spriteRenderer;
    public PlayerNumberName PlayerNumberName { private set; get; }
    public Action<PlayerNumberName, GameObject> OnHitTerritory { set; private get; } = null;

    public void SetPlayerNumberName(PlayerNumberName pnn) {
        this.PlayerNumberName = pnn;
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
            OnHitTerritory(this.PlayerNumberName, collision.gameObject);
        }
    }
}
