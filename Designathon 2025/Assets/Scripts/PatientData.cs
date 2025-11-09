using UnityEngine;
using Firebase.Firestore;

[FirestoreData]
public struct PatientData
{
    [FirestoreProperty]
    public string Name {  get; set; }

    [FirestoreProperty]
    public string Description { get; set; }

}
