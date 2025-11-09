using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Questionnaire : MonoBehaviour
{
    [Header("UI References")]
    public TextMeshProUGUI question;
    public List<TextMeshProUGUI> answerTexts;
    public List<Image> checkBoxes;
    public List<GameObject> checkBoxObjects;
    public List<GameObject> answerObjects;
    public Image questionImage;

    [Header("Buttons")]
    public Button nextButton;
    public Button backButton;
    public Button submitButton;

    [Header("Colors")]
    public Color unselectedColor = Color.white;
    public Color selectedColor = Color.green;

    [Header("Character Display Layers")]
    public Image template;
    public Image headImage;
    public Image eyesImage;
    public Image hairImage;
    public Image bodyImage;

    [Header("Character Data Source")]
    public Character_Manager characterManager;

    private int currentQuestionIndex = 0;
    private List<QuestionData> questions = new List<QuestionData>();
    private Dictionary<int, List<int>> selectedAnswers = new Dictionary<int, List<int>>();

    private PatientReport report = new PatientReport();

    private void Start()
    {
        InitializeQuestions();
        ShowQuestion(0);

        nextButton.onClick.AddListener(OnNext);
        backButton.onClick.AddListener(OnBack);
        submitButton.onClick.AddListener(OnSubmit);

        submitButton.gameObject.SetActive(false);
    }

    void UpdateCharacterIcon(QuestionData q)
    {
        if (characterManager == null) return;

        var headPart = characterManager.GetPart("Head");
        var eyesPart = characterManager.GetPart("Eyes");
        var hairPart = characterManager.GetPart("Hair");
        var bodyPart = characterManager.GetPart("Body");
        Debug.Log(bodyPart);

        if (q.headSprite != null)
        {
            headImage.sprite = q.headSprite;
            headImage.color = headPart != null ? headPart.color : Color.white;
        }

        if (q.eyesSprite != null)
        {
            eyesImage.sprite = q.eyesSprite;
            eyesImage.color = eyesPart != null ? eyesPart.color : Color.white;
        }

        if (q.bodySprite != null)
        {
            bodyImage.sprite = q.bodySprite;
            bodyImage.color = bodyPart != null ? bodyPart.color : Color.white;
        }

        if (q.hairSprites != null && q.hairSprites.Count > 0 && hairPart != null)
        {
            int index = Mathf.Clamp(hairPart.currentSprite, 0, q.hairSprites.Count - 1);
            hairImage.sprite = q.hairSprites[index];
            hairImage.color = hairPart.color;
        }

        if(q.template != null){
            template.sprite = q.template;
        }
    }

    void InitializeQuestions()
    {
        questions = new List<QuestionData>()
        {
            new QuestionData("Do you feel pain anywhere?",
                new List<string>{ "Yes, my head hurts", "Yes, my arm hurts", "Yes, my legs hurt", "Yes, my stomach hurts", "Yes, something hurts", "No, I feel fine everywhere" },
                allowsMultiple:true,
                head: Resources.Load<Sprite>("dizzy face"),
                body: Resources.Load<Sprite>("dizzy hoodie"),
                eyes: Resources.Load<Sprite>("transparent"),
                hairVariants: new List<Sprite>
                {
                    Resources.Load<Sprite>("dizzy hair 7"),
                    Resources.Load<Sprite>("dizzy hair 6"),
                    Resources.Load<Sprite>("dizzy hair 5"),
                    Resources.Load<Sprite>("dizzy hair 4"),
                    Resources.Load<Sprite>("dizzy hair 3"),
                    Resources.Load<Sprite>("dizzy hair 2"),
                    Resources.Load<Sprite>("dizzy hair 1"),
                },
                template : Resources.Load<Sprite>("dizzy")),

            new QuestionData("Have you coughed or vomited recently?",
                new List<string>{ "Yes, I coughed", "Yes, I vomited", "No, I haven't" },
                allowsMultiple:true,
                head: Resources.Load<Sprite>("nausea face"),
                body: Resources.Load<Sprite>("nausea hoodie"),
                eyes: Resources.Load<Sprite>("transparent"),
                hairVariants: new List<Sprite>
                {
                    Resources.Load<Sprite>("nausea hair 7"),
                    Resources.Load<Sprite>("nausea hair 6"),
                    Resources.Load<Sprite>("nausea hair 5"),
                    Resources.Load<Sprite>("nausea hair 4"),
                    Resources.Load<Sprite>("nausea hair 3"),
                    Resources.Load<Sprite>("nausea hair 2"),
                    Resources.Load<Sprite>("nausea hair 1"),
                },
                template : Resources.Load<Sprite>("nausea")),

            new QuestionData("Does your skin feel itchy?",
                new List<string>{ "Yes, my skin feels itchy", "No, my skin feels fine" },
                allowsMultiple:false,
                head: Resources.Load<Sprite>("rash face"),
                body: Resources.Load<Sprite>("rash hoodie"),
                eyes: Resources.Load<Sprite>("rash eye"),
                hairVariants: new List<Sprite>
                {
                    Resources.Load<Sprite>("rash hair 7"),
                    Resources.Load<Sprite>("rash hair 6"),
                    Resources.Load<Sprite>("rash hair 5"),
                    Resources.Load<Sprite>("rash hair 4"),
                    Resources.Load<Sprite>("rash hair 3"),
                    Resources.Load<Sprite>("rash hair 2"),
                    Resources.Load<Sprite>("rash hair 1"),
                },
                template : Resources.Load<Sprite>("rash")),

            new QuestionData("Do you feel hot, tired, dizzy, or uncomfy?",
                new List<string>{ "Yes, I feel hot", "Yes, I feel tired", "Yes, I feel dizzy", "Yes, I feel uncomfy", "No, I don't feel any of that" },
                allowsMultiple:true,
                head: Resources.Load<Sprite>("dizzy face"),
                body: Resources.Load<Sprite>("dizzy hoodie"),
                eyes: Resources.Load<Sprite>("dizzy eye"),
                hairVariants: new List<Sprite>
                {
                    Resources.Load<Sprite>("dizzy hair 7"),
                    Resources.Load<Sprite>("dizzy hair 6"),
                    Resources.Load<Sprite>("dizzy hair 5"),
                    Resources.Load<Sprite>("dizzy hair 4"),
                    Resources.Load<Sprite>("dizzy hair 3"),
                    Resources.Load<Sprite>("dizzy hair 2"),
                    Resources.Load<Sprite>("dizzy hair 1"),
                },
                template : Resources.Load<Sprite>("dizzy")),

            new QuestionData("Is it hard to hear or see?",
                new List<string>{ "Yes, it's hard to hear", "Yes, it's hard to see" },
                allowsMultiple:true,
                head: Resources.Load<Sprite>("hearing face"),
                body: Resources.Load<Sprite>("hearing hoodie"),
                eyes: Resources.Load<Sprite>("hearing eye"),
                hairVariants: new List<Sprite>
                {
                    Resources.Load<Sprite>("hearing hair 7"),
                    Resources.Load<Sprite>("hearing hair 6"),
                    Resources.Load<Sprite>("hearing hair 5"),
                    Resources.Load<Sprite>("hearing hair 4"),
                    Resources.Load<Sprite>("hearing hair 3"),
                    Resources.Load<Sprite>("hearing hair 2"),
                    Resources.Load<Sprite>("hearing hair 1"),
                },
                template : Resources.Load<Sprite>("hearing")),

            new QuestionData("Do you have a runny or itchy nose or eyes?",
                new List<string>{ "Yes, I have a runny nose", "Yes, I have an itchy nose", "Yes, I have a runny eye", "Yes, I have an itchy eye", "No, I don't feel any of that" },
                allowsMultiple:true,
                head: Resources.Load<Sprite>("runny nose face"),
                body: Resources.Load<Sprite>("runny nose hoodie"),
                eyes: Resources.Load<Sprite>("runny nose eye"),
                hairVariants: new List<Sprite>
                {
                    Resources.Load<Sprite>("runny nose hair 7"),
                    Resources.Load<Sprite>("runny nose hair 6"),
                    Resources.Load<Sprite>("runny nose hair 5"),
                    Resources.Load<Sprite>("runny nose hair 4"),
                    Resources.Load<Sprite>("runny nose hair 3"),
                    Resources.Load<Sprite>("runny nose hair 2"),
                    Resources.Load<Sprite>("runny nose hair 1"),
                },
                template : Resources.Load<Sprite>("runny nose")),

            new QuestionData("Does it hurt to breathe or walk?",
                new List<string>{ "Yes, it hurts to breathe", "Yes, it hurts to walk", "No, it doesn't hurt" },
                allowsMultiple:true,
                head: Resources.Load<Sprite>("nausea face"),
                body: Resources.Load<Sprite>("nausea hoodie"),
                eyes: Resources.Load<Sprite>("transparent"),
                hairVariants: new List<Sprite>
                {
                    Resources.Load<Sprite>("nausea hair 7"),
                    Resources.Load<Sprite>("nausea hair 6"),
                    Resources.Load<Sprite>("nausea hair 5"),
                    Resources.Load<Sprite>("nausea hair 4"),
                    Resources.Load<Sprite>("nausea hair 3"),
                    Resources.Load<Sprite>("nausea hair 2"),
                    Resources.Load<Sprite>("nausea hair 1"),
                },
                template : Resources.Load<Sprite>("nausea"))
        };
    }

    void ShowQuestion(int index)
    {
        currentQuestionIndex = index;
        var q = questions[index];
        question.text = q.text;

        for (int i = 0; i < answerObjects.Count; i++)
        {
            if (i < q.answers.Count)
            {
                answerObjects[i].SetActive(true);
                checkBoxObjects[i].SetActive(true);
                answerTexts[i].text = q.answers[i];
                UpdateCheckBoxColor(i);
            }
            else
            {
                answerObjects[i].SetActive(false);
                checkBoxObjects[i].SetActive(false);
            }
        }

        backButton.interactable = index > 0;
        nextButton.interactable = HasAnsweredCurrent() && index < questions.Count - 1;
        submitButton.interactable = (index == questions.Count - 1);
        UpdateCharacterIcon(questions[index]);
    }

    public void OnAnswerClicked(int answerIndex)
    {
        var q = questions[currentQuestionIndex];
        if (!selectedAnswers.ContainsKey(currentQuestionIndex))
            selectedAnswers[currentQuestionIndex] = new List<int>();

        var list = selectedAnswers[currentQuestionIndex];

        if (q.allowsMultiple)
        {
            if (list.Contains(answerIndex))
                list.Remove(answerIndex);
            else
                list.Add(answerIndex);
        }
        else
        {
            list.Clear();
            list.Add(answerIndex);
        }

        for (int i = 0; i < q.answers.Count; i++)
            UpdateCheckBoxColor(i);

        nextButton.interactable = HasAnsweredCurrent() && currentQuestionIndex < questions.Count - 1;
        submitButton.interactable = HasAnsweredCurrent() && currentQuestionIndex == questions.Count - 1;
    }

    void UpdateCheckBoxColor(int answerIndex)
    {
        bool selected = selectedAnswers.ContainsKey(currentQuestionIndex)
            && selectedAnswers[currentQuestionIndex].Contains(answerIndex);

        checkBoxes[answerIndex].color = selected ? selectedColor : unselectedColor;
    }

    bool HasAnsweredCurrent()
    {
        return selectedAnswers.ContainsKey(currentQuestionIndex)
            && selectedAnswers[currentQuestionIndex].Count > 0;
    }

    void OnNext()
    {
        if (!HasAnsweredCurrent()) return;

        if (currentQuestionIndex == 0)
        {
            var selectedPainIndices = selectedAnswers[0];
            if (selectedPainIndices.Count > 0 && !questions.Exists(q => q.text.Contains("pain in")))
            {
                List<QuestionData> followUps = new List<QuestionData>();
                foreach (int i in selectedPainIndices)
                {
                    string bodyPart = questions[0].answers[i]
                        .Replace("Yes, my ", "")
                        .Replace(" hurts", "");

                    followUps.Add(new QuestionData(
                        $"How does the pain in your {bodyPart} feel on a scale of 1–5?",
                        new List<string> { "1", "2", "3", "4", "5" },
                        allowsMultiple:false
                    ));
                }

                questions.InsertRange(1, followUps);
            }
        }

        if (currentQuestionIndex < questions.Count - 1)
            ShowQuestion(currentQuestionIndex + 1);
    }

    void OnBack()
    {
        if (currentQuestionIndex > 0)
            ShowQuestion(currentQuestionIndex - 1);
    }

    public void OnSubmit()
    {
        report = new PatientReport();

        foreach (var pair in selectedAnswers)
        {
            var q = questions[pair.Key];
            var indices = pair.Value;

            if (q.text.Contains("pain anywhere"))
            {
                report.hasPain = indices.Count > 0 && !indices.Contains(5);
                if (report.hasPain)
                {
                    foreach (int i in indices)
                    {
                        if (q.answers[i].Contains("head")) report.painHead = true;
                        if (q.answers[i].Contains("arm")) report.painArm = true;
                        if (q.answers[i].Contains("legs")) report.painLegs = true;
                        if (q.answers[i].Contains("stomach")) report.painStomach = true;
                        if (q.answers[i].Contains("something")) report.painOther = true;
                    }
                }
            }
            else if (q.text.Contains("coughed"))
            {
                foreach (int i in indices)
                {
                    if (q.answers[i].Contains("coughed")) report.hasCoughed = true;
                    if (q.answers[i].Contains("vomited")) report.hasVomited = true;
                }
            }
            else if (q.text.Contains("skin"))
            {
                report.skinItchy = indices.Contains(0);
            }
            else if (q.text.Contains("hot, tired, dizzy"))
            {
                foreach (int i in indices)
                {
                    if (q.answers[i].Contains("hot")) report.feelHot = true;
                    if (q.answers[i].Contains("tired")) report.feelTired = true;
                    if (q.answers[i].Contains("dizzy")) report.feelDizzy = true;
                    if (q.answers[i].Contains("uncomfy")) report.feelUncomfortable = true;
                }
            }
            else if (q.text.Contains("hear or see"))
            {
                foreach (int i in indices)
                {
                    if (q.answers[i].Contains("hear")) report.hardToHear = true;
                    if (q.answers[i].Contains("see")) report.hardToSee = true;
                }
            }
            else if (q.text.Contains("nose or eyes"))
            {
                foreach (int i in indices)
                {
                    if (q.answers[i].Contains("runny nose")) report.runnyNose = true;
                    if (q.answers[i].Contains("itchy nose")) report.itchyNose = true;
                    if (q.answers[i].Contains("runny eye")) report.runnyEyes = true;
                    if (q.answers[i].Contains("itchy eye")) report.itchyEyes = true;
                }
            }
            else if (q.text.Contains("breathe or walk"))
            {
                foreach (int i in indices)
                {
                    if (q.answers[i].Contains("breathe")) report.hurtBreathe = true;
                    if (q.answers[i].Contains("walk")) report.hurtWalk = true;
                }
            }
            else if (q.text.Contains("pain in"))
            {
                string bodyPart = q.text.Replace("How does the pain in your ", "").Replace(" feel on a scale of 1–5?", "");
                int severity = int.Parse(q.answers[indices[0]]);
                report.painSeverity[bodyPart] = severity;
            }
        }

        Debug.Log("✅ Patient report submitted!");
        Debug.Log(JsonUtility.ToJson(report, true));
    }
}

[System.Serializable]
public class QuestionData
{
    public string text;
    public List<string> answers;
    public bool allowsMultiple;

    [Header("Per-question character art")]
    public Sprite template;
    public Sprite headSprite;
    public Sprite eyesSprite;
    public Sprite bodySprite;
    public List<Sprite> hairSprites;

    public QuestionData(string text, List<string> answers, bool allowsMultiple,
                        Sprite head = null, Sprite eyes = null, Sprite body = null, List<Sprite> hairVariants = null, Sprite template = null)
    {
        this.text = text;
        this.answers = answers;
        this.allowsMultiple = allowsMultiple;
        this.headSprite = head;
        this.eyesSprite = eyes;
        this.bodySprite = body;
        this.hairSprites = hairVariants ?? new List<Sprite>();
        this.template = template;
    }
}


[System.Serializable]
public class PatientReport
{
    public bool hasPain;
    public bool painHead, painArm, painLegs, painStomach, painOther;
    public Dictionary<string, int> painSeverity = new Dictionary<string, int>();

    public bool hasCoughed, hasVomited;
    public bool skinItchy;
    public bool feelHot, feelTired, feelDizzy, feelUncomfortable;
    public bool hardToHear, hardToSee;
    public bool runnyNose, itchyNose, runnyEyes, itchyEyes;
    public bool hurtBreathe, hurtWalk;
}
