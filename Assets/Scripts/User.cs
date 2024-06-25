using Firebase.Firestore;
[FirestoreData]
public struct User
{
    [FirestoreProperty]
    public float Score { get; set; }
    [FirestoreProperty]
    public float ByClick { get; set; }
    [FirestoreProperty]
    public float PerSec { get; set; }
}
