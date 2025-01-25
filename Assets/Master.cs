using UnityEngine;
using UnityEngine.InputSystem;

public class Master : MonoBehaviour
{


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player_.Init(Gamepad.current);
    }

    // Update is called once per frame
    void Update()
    {

    }


    [SerializeField] private Player player_;

}
