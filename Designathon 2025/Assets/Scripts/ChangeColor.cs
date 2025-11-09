using UnityEngine;
using UnityEngine.UI;
public class ChangeColor : MonoBehaviour
{
    [SerializeField] GameObject character;
    [SerializeField] BodyPart part;
    [SerializeField] Color newColor;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    Button button;
    void Start()
    {
        button = GetComponent<Button>();
        Character_Manager character_Manager = character.GetComponent<Character_Manager>();
        button.onClick.AddListener(() => character_Manager.changeColor(part, newColor));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
