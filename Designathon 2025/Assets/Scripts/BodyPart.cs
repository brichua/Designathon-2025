using UnityEngine;

[CreateAssetMenu(fileName = "BodyPart", menuName = "Scriptable Objects/BodyPart")]
public class BodyPart : ScriptableObject
{
    public string bodyPart;
    public Sprite[] allSprites;
    public int currentSprite;
    public Color color;
}
