using UnityEngine;

public class PlayerBalloon : MonoBehaviour
{
    public enum IconType {
        Dog,
        Cat,
        Bunny,
        Horse,
    }

    [SerializeField] public Animator animator;
    [SerializeField] public SpriteRenderer dogIcon;
    [SerializeField] public SpriteRenderer catIcon;
    [SerializeField] public SpriteRenderer bunnyIcon;
    [SerializeField] public SpriteRenderer horseIcon;


    public void Start()
    {
        animator.Play("player_balloon_idle");
    }

    public void ShowIcon(IconType type) {
        SpriteRenderer[] icons = { dogIcon, catIcon, bunnyIcon, horseIcon };
        foreach (var icon in icons) {
            icon.gameObject.SetActive(false);
        }
        switch (type) {
            case IconType.Dog:
                dogIcon.gameObject.SetActive(true);
                break;
            case IconType.Cat:
                catIcon.gameObject.SetActive(true);
                break;
            case IconType.Bunny:
                bunnyIcon.gameObject.SetActive(true);
                break;
            case IconType.Horse:
                horseIcon.gameObject.SetActive(true);
                break;
        }
    }
}
