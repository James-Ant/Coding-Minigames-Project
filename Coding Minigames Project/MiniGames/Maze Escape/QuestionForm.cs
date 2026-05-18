using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace Coding_Minigames_Project.MiniGames.Maze_Escape
{
    public partial class QuestionForm : Form
    {
        Label lblQuestion = new Label();
        TextBox txtAnswer = new TextBox();
        Button btnSubmit = new Button();

        List<(string question, string answer)> questions = new List<(string question, string answer)>();
        Random rnd = new Random();

        public bool isCorrect = false;

        string currentAnswer = "";

        public QuestionForm()
        {
            InitializeComponent();

            this.Text = "Question Door";
            this.Width = 500;
            this.Height = 250;
            this.StartPosition = FormStartPosition.CenterScreen;

            LoadQuestions();
            PickQuestion();
            SetupUI();
        }

        void LoadQuestions()
        {
            string path = "questions.txt";

            if (!File.Exists(path))
            {
                MessageBox.Show("questions.txt not found!");
                return;
            }

            var lines = File.ReadAllLines(path);

            foreach (var line in lines)
            {
                if (line.Contains("|"))
                {
                    var parts = line.Split('|');

                    if (parts.Length == 2)
                    {
                        questions.Add((parts[0], parts[1]));
                    }
                }
            }
        }

        void PickQuestion()
        {
            if (questions.Count == 0)
            {
                MessageBox.Show("No questions loaded!");
                return;
            }

            var q = questions[rnd.Next(questions.Count)];

            lblQuestion.Text = q.question;
            currentAnswer = q.answer.Trim().ToLower();
        }

        void SetupUI()
        {
            lblQuestion.Top = 20;
            lblQuestion.Left = 20;
            lblQuestion.Width = 440;

            txtAnswer.Top = 80;
            txtAnswer.Left = 20;
            txtAnswer.Width = 440;

            btnSubmit.Text = "Submit";
            btnSubmit.Top = 130;
            btnSubmit.Left = 20;
            btnSubmit.Width = 100;

            btnSubmit.Click += BtnSubmit_Click;

            this.Controls.Add(lblQuestion);
            this.Controls.Add(txtAnswer);
            this.Controls.Add(btnSubmit);
        }

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