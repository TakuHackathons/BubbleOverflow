using Firebase.Database;
using Firebase.Database.Query;
using Google.Apis.Auth.OAuth2;
using Google.Cloud.Firestore;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using System;
using System.IO;
using dotenv.net;
using dotenv.net.Utilities;

public class TestRecord
{
    public string Id { get; set; }
    public int IntValue { get; set; }
    public string StringValue { get; set; }
}

public class FirebaseDatabaseManager : SingletonBehaviour<FirebaseDatabaseManager>
{
    private FirebaseClient firebaseClient;

    private void Awake()
    {
        DotEnv.Load(options: new DotEnvOptions(envFilePaths: new[] { Path.Combine(Application.streamingAssetsPath, ".env") }));
    }

    async void Start()
    {
        String firebaseDatabaseUrl = EnvReader.GetStringValue("FIREBASE_REALTIME_DATABASE_URL");
        firebaseClient = new FirebaseClient(firebaseDatabaseUrl, new FirebaseOptions
        {
            AuthTokenAsyncFactory = () => LoginAsync()
        });
        // var dino = await firebaseClient.Child("dinosaurs").PostAsync(new TestRecord());
        /*
FirestoreDb db = FirestoreDb.Create("bubbleoverflow-ea9b3");
CollectionReference collection = db.Collection("users");
DocumentReference document = await collection.AddAsync(new { Name = new { First = "Ada", Last = "Lovelace" }, Born = 1815 });
DocumentSnapshot snapshot = await document.GetSnapshotAsync();
Dictionary<string, object> data = snapshot.ToDictionary();
foreach (var kv in data)
{
    Debug.Log(kv.Key);
    Debug.Log(kv.Value.ToString());
}
        */
    }

    private async Task<string> LoginAsync()
    {
        GoogleCredential credential = GoogleCredential.FromFile(Path.Combine(Application.streamingAssetsPath, "firebase-service-account.json")).CreateScoped(new string[] {
            "https://www.googleapis.com/auth/firebase.database"
        });
        ITokenAccess c = credential as ITokenAccess;
        string accessToken = await c.GetAccessTokenForRequestAsync();
        return accessToken;
    }


    void Update()
    {
        
    }
}
