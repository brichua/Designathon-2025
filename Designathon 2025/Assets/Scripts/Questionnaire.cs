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
    public Button submitButton; // 👈 new

    [Header("Colors")]
    public Color unselectedColor = Color.white;
    public Color selectedColor = Color.green;

    private int currentQuestionIndex = 0;
    private List<QuestionData> questions = new List<QuestionData>();
    private Dictionary<int, List<int>> selectedAnswers = new Dictionary<int, List<int>>();

    public PatientReport report = new PatientReport();

    private void Start()
    {
        InitializeQuestions();
        ShowQuestion(0);

        nextButton.onClick.AddListener(OnNext);
        backButton.onClick.AddListener(OnBack);
        submitButton.onClick.AddListener(OnSubmit);

        submitButton.gameObject.SetActive(false);
    }

    void InitializeQuestions()
    {
        questions = new List<QuestionData>()
        {
            new QuestionData("Do you feel pain anywhere?",
                new List<string>{ "Yes, my head hurts", "Yes, my arm hurts", "Yes, my legs hurt", "Yes, my stomach hurts", "Yes, something hurts", "No, I feel fine everywhere" },
                allowsMultiple:true,
                image:null),

            new QuestionData("Have you coughed or vomited recently?",
                new List<string>{ "Yes, I coughed", "Yes, I vomited", "No, I haven't" },
                allowsMultiple:true,
                image:null),

            new QuestionData("Does your skin feel itchy?",
                new List<string>{ "Yes, my skin feels itchy", "No, my skin feels fine" },
                allowsMultiple:false,
                image:null),

            new QuestionData("Do you feel hot, tired, dizzy, or uncomfy?",
                new List<string>{ "Yes, I feel hot", "Yes, I feel tired", "Yes, I feel dizzy", "Yes, I feel uncomfy", "No, I don't feel any of that" },
                allowsMultiple:true,
                image:null),

            new QuestionData("Is it hard to hear or see?",
                new List<string>{ "Yes, it's hard to hear", "Yes, it's hard to see" },
                allowsMultiple:true,
                image:null),

            new QuestionData("Do you have a runny or itchy nose or eyes?",
                new List<string>{ "Yes, I have a runny nose", "Yes, I have an itchy nose", "Yes, I have a runny eye", "Yes, I have an itchy eye", "No, I don't feel any of that" },
                allowsMultiple:true,
                image:null),

            new QuestionData("Does it hurt to breathe or walk?",
                new List<string>{ "Yes, it hurts to breathe", "Yes, it hurts to walk", "No, it doesn't hurt" },
                allowsMultiple:true,
                image:null)
        };
    }

    void ShowQuestion(int index)
    {
        currentQuestionIndex = index;
        var q = questions[index];
        question.text = q.text;

        if (questionImage != null)
        {
            questionImage.sprite = q.image;
            questionImage.gameObject.SetActive(q.image != null);
        }

        // Show only as many answers as exist
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
        submitButton.gameObject.SetActive(index == questions.Count - 1); // 👈 show only at the end
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
                        allowsMultiple:false,
                        image:null
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

    void OnSubmit()
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
    public Sprite image;

    public QuestionData(string text, List<string> answers, bool allowsMultiple, Sprite image)
    {
        this.text = text;
        this.answers = answers;
        this.allowsMultiple = allowsMultiple;
        this.image = image;
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
