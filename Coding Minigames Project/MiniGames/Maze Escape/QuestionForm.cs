using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Coding_Minigames_Project.MiniGames.Maze_Escape
{
    public partial class QuestionForm : Form
    {
        Label lblQuestion;
        Label lblHint;
        TextBox txtAnswer;
        Button btnSubmit;

        List<(string question, string answer)> questions =
            new List<(string question, string answer)>();

        Random rnd = new Random();

        public bool isCorrect = false;

        string currentAnswer = "";

        public QuestionForm()
        {
            // FORM SETUP
            this.Text = "Question Door";
            this.Size = new Size(500, 300);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = Color.White;

            SetupUI();
            LoadQuestions();
            PickQuestion();
        }

        // ================= UI =================
        void SetupUI()
        {
            lblQuestion = new Label();
            lblHint = new Label();
            txtAnswer = new TextBox();
            btnSubmit = new Button();

            // QUESTION
            lblQuestion.Top = 20;
            lblQuestion.Left = 20;
            lblQuestion.Width = 440;
            lblQuestion.Height = 60;
            lblQuestion.Font = new Font("Segoe UI", 12, FontStyle.Bold);
            lblQuestion.ForeColor = Color.Black;

            // HINT (JUMBLED ANSWER)
            lblHint.Top = 80;
            lblHint.Left = 20;
            lblHint.Width = 440;
            lblHint.Height = 20;
            lblHint.Font = new Font("Segoe UI", 10, FontStyle.Italic);
            lblHint.ForeColor = Color.Gray;

            // ANSWER BOX
            txtAnswer.Top = 110;
            txtAnswer.Left = 20;
            txtAnswer.Width = 440;
            txtAnswer.Font = new Font("Segoe UI", 11);

            // BUTTON
            btnSubmit.Text = "Submit";
            btnSubmit.Top = 160;
            btnSubmit.Left = 20;
            btnSubmit.Width = 120;
            btnSubmit.Height = 35;
            btnSubmit.BackColor = Color.MediumSlateBlue;
            btnSubmit.ForeColor = Color.White;

            btnSubmit.Click += BtnSubmit_Click;

            Controls.Add(lblQuestion);
            Controls.Add(lblHint);
            Controls.Add(txtAnswer);
            Controls.Add(btnSubmit);
        }

        // ================= QUESTIONS =================
        void LoadQuestions()
        {
            questions.Add(("What keyword creates a class in C#?", "class"));
            questions.Add(("What keyword creates an object?", "new"));
            questions.Add(("What is the main entry method?", "main"));
            questions.Add(("What symbol ends a statement?", ";"));
            questions.Add(("What loop runs while condition is true?", "while"));

            questions.Add(("Loop that runs fixed times?", "for"));
            questions.Add(("Loop that runs at least once?", "do while"));
            questions.Add(("Keyword to exit loop?", "break"));
            questions.Add(("Keyword to skip iteration?", "continue"));
            questions.Add(("Decision keyword?", "if"));

            questions.Add(("Alternative to if?", "else"));
            questions.Add(("Equality operator?", "=="));
            questions.Add(("Not equal operator?", "!="));
            questions.Add(("Logical AND operator?", "&&"));
            questions.Add(("Integer type?", "int"));

            questions.Add(("Decimal type?", "float"));
            questions.Add(("Boolean type?", "bool"));
            questions.Add(("Text type?", "string"));
            questions.Add(("Character type?", "char"));
            questions.Add(("Used to return value?", "return"));

            questions.Add(("Access modifier public?", "public"));
            questions.Add(("Access modifier private?", "private"));
            questions.Add(("No return method keyword?", "void"));
            questions.Add(("Self reference keyword?", "this"));
            questions.Add(("Parent reference keyword?", "base"));
        }

        // ================= PICK QUESTION =================
        void PickQuestion()
        {
            if (questions.Count == 0)
            {
                lblQuestion.Text = "No questions available!";
                return;
            }

            var q = questions[rnd.Next(questions.Count)];

            lblQuestion.Text = q.question;

            currentAnswer = q.answer.ToLower();

            lblHint.Text = "Hint: " + Jumble(currentAnswer);
        }

        // ================= JUMBLE =================
        string Jumble(string word)
        {
            char[] chars = word.ToCharArray();

            for (int i = 0; i < chars.Length; i++)
            {
                int j = rnd.Next(chars.Length);

                char temp = chars[i];
                chars[i] = chars[j];
                chars[j] = temp;
            }

            return new string(chars);
        }

        // ================= SUBMIT =================
        void BtnSubmit_Click(object sender, EventArgs e)
        {
            string userAnswer = txtAnswer.Text.Trim().ToLower();

            if (userAnswer == currentAnswer)
            {
                isCorrect = true;
                MessageBox.Show("Correct!");
            }
            else
            {
                isCorrect = false;
                MessageBox.Show("Wrong answer!");
            }

            this.Close();
        }
    }
}