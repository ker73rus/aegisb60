using Firebase.Firestore;
[FirestoreData]
public struct Upgrade
{
    [FirestoreProperty]
    public string Name { get; set; }
    [FirestoreProperty]
    public float Cost { get; set; }
    [FirestoreProperty]
    public float Boost { get; set; }
    [FirestoreProperty]
    public int tier { get; set; }
    [FirestoreProperty]
    public int lvl { get; set; }
    [FirestoreProperty]
    public bool type { get; set; }
}
