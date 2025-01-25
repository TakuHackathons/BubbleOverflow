using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using System.Collections.Generic;
using MessagePack.Resolvers;
using MessagePack;

public class HTTPManager : SingletonBehaviour<HTTPManager>
{
    [SerializeField] private string rootUrl;

    void Start()
    {
        SendRequest();
    }

    void Update()
    {
        
    }

    public void SendRequest()
    {
        StartCoroutine(SendRequestRoutine());
    }

    private IEnumerator SendRequestRoutine()
    {
        UnityWebRequest request = UnityWebRequest.Get($"{rootUrl}/msgpack");
        yield return request.SendWebRequest();

        Dictionary<string, string> sample = MessagePackSerializer.Deserialize<Dictionary<string, string>>(request.downloadHandler.data, ContractlessStandardResolver.Options);
        foreach(var kv in sample)
        {
            Debug.Log(kv.Key);
            Debug.Log(kv.Value);
        }
    }
}
