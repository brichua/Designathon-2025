using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;
using Firebase;
using Firebase.Analytics;
using Firebase.Firestore;


public class Character_Manager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] List<GameObject> bodyContainers = new List<GameObject>();

    [SerializeField]
    BodyPart[] bodyParts;

    void Start()
    {
        //var patientData = new PatientData
        //{
        //    Name = "Test",
        //    Description = "Please work,"
        //};
        //var firestore = FirebaseFirestore.DefaultInstance;
        //firestore.Document("Users/NewUser").SetAsync(patientData);

        foreach(BodyPart part in bodyParts)
        {
            part.currentSprite = 0;
            part.color = Color.white;
            
            GameObject container = this.transform.Find(part.name).gameObject;
            bodyContainers.Add(container);
        }
 
    }

    // Update is called once per frame
    void Update()
    {
        for(int i = 0; i<bodyContainers.Count; i++)
        {
            SpriteRenderer partRenderer = bodyContainers[i].GetComponent<SpriteRenderer>();
            partRenderer.sprite = bodyParts[i].allSprites[bodyParts[i].currentSprite];
            partRenderer.color = bodyParts[i].color;
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            shiftSprite(bodyParts[0], true);
        }
    }

    public void shiftSprite(BodyPart part, bool toRight)
    {
        if (toRight)
        {
            part.currentSprite++;
        }
        else
        {
            part.currentSprite--;
        }
        if(part.currentSprite < 0) 
        { 
            part.currentSprite = part.allSprites.Length - 1;
        }else if(part.currentSprite >= part.allSprites.Length) 
        {
            part.currentSprite = 0;
        }

    }

    public void changeColor(BodyPart part, Color color)
    {
        part.color = color;
    }
}
