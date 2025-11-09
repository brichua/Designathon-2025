using UnityEngine;
using UnityEngine.UI;

public class ChangeSprite : MonoBehaviour
{
    [SerializeField] GameObject character;
    [SerializeField] BodyPart part;
    [SerializeField] int spriteNum;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    Button button;
    void Start()
    {
        button = GetComponent<Button>();
        Character_Manager character_Manager = character.GetComponent<Character_Manager>();
        button.onClick.AddListener(() => character_Manager.shiftSprite(part, spriteNum));
        
    }

    // Update is called once per frame
    void Update()
    {

    }
}
