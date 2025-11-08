using UnityEngine;

public class Character_Manager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] GameObject headContainer;
    [SerializeField] GameObject bodyContainer;
    [SerializeField] GameObject legsContainer;

    [SerializeField] BodyPart head;
    [SerializeField] BodyPart body;
    [SerializeField] BodyPart legs;


    SpriteRenderer headSprite;
    SpriteRenderer bodySprite;
    SpriteRenderer legsSprite;


    void Start()
    {
        headSprite = headContainer.GetComponent<SpriteRenderer>();
        bodySprite = bodyContainer.GetComponent<SpriteRenderer>();
        legsSprite = legsContainer.GetComponent<SpriteRenderer>();
        head.currentSprite = 0;
        body.currentSprite = 0;
        legs.currentSprite = 0;
    }

    // Update is called once per frame
    void Update()
    {
        headSprite.sprite = head.allSprites[head.currentSprite];
        bodySprite.sprite = body.allSprites[body.currentSprite];
        legsSprite.sprite = legs.allSprites[legs.currentSprite];
        if (Input.GetKeyDown(KeyCode.Space))
        {
            shiftSprite(head, true);
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
}
