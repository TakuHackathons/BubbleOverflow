using System;
using TMPro;
using UnityEngine;

public class RoomListCell : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI roomNameText;

    public Action OnJoinRoom;

    public void Init()
    {
        
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnJoinRoomButton()
    {
        if (OnJoinRoom != null)
        {
            OnJoinRoom();
        }
    }
}
