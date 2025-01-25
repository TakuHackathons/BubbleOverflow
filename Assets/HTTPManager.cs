using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using MessagePack.Resolvers;
using MessagePack;

public class SampleMsgPack
{
    public string hello;
}

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
        SampleMsgPack sample = MessagePackSerializer.Deserialize<SampleMsgPack>(request.downloadHandler.data, ContractlessStandardResolver.Options);
        Debug.Log(sample.hello);
    }
}
