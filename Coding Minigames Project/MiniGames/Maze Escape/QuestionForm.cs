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
            // --- Basic Syntax & Keywords (1-10) ---
            questions.Add(("What keyword creates a class in C#?", "class"));
            questions.Add(("What keyword creates an object instance?", "new"));
            questions.Add(("What is the main entry method?", "main"));
            questions.Add(("What symbol ends a statement?", ";"));
            questions.Add(("What loop runs while condition is true?", "while"));
            questions.Add(("Loop that runs fixed times?", "for"));
            questions.Add(("Loop that runs at least once?", "do while"));
            questions.Add(("Keyword to exit a loop?", "break"));
            questions.Add(("Keyword to skip an iteration?", "continue"));
            questions.Add(("Decision control keyword?", "if"));

            // --- Operators & Conditionals (11-20) ---
            questions.Add(("Alternative path if conditional fails?", "else"));
            questions.Add(("Equality comparison operator?", "=="));
            questions.Add(("Not equal comparison operator?", "!="));
            questions.Add(("Logical AND conditional operator?", "&&"));
            questions.Add(("Logical OR conditional operator?", "||"));
            questions.Add(("Logical NOT inversion operator?", "!"));
            questions.Add(("Remainder operator symbol?", "%"));
            questions.Add(("Operator used to assign values?", "="));
            questions.Add(("Multi-way branch conditional keyword?", "switch"));
            questions.Add(("Individual option within a switch block?", "case"));

            // --- Data Types (21-30) ---
            questions.Add(("Integer structural type?", "int"));
            questions.Add(("Single-precision floating data type?", "float"));
            questions.Add(("Boolean logic structural type?", "bool"));
            questions.Add(("Text array data type?", "string"));
            questions.Add(("Single text character representation type?", "char"));
            questions.Add(("Double-precision decimal floating type?", "double"));
            questions.Add(("High precision monetary currency type?", "decimal"));
            questions.Add(("Data type that holds true or false?", "bool"));
            questions.Add(("Data type for integer 64-bit precision?", "long"));
            questions.Add(("The base class of all types in C#?", "object"));

            // --- OOP & Scope Concepts (31-40) ---
            questions.Add(("Used to return value from a block?", "return"));
            questions.Add(("Access modifier with globally open scope?", "public"));
            questions.Add(("Access modifier restricted to current class?", "private"));
            questions.Add(("No return method type modifier?", "void"));
            questions.Add(("Self reference context keyword?", "this"));
            questions.Add(("Parent reference structural context keyword?", "base"));
            questions.Add(("Access modifier open only to children classes?", "protected"));
            questions.Add(("Blueprint template used to instantiate objects?", "class"));
            questions.Add(("Method with the same name as the class?", "constructor"));
            questions.Add(("Keyword to inherit or extend a class?", ":"));

            // --- Intermediate Framework & Features (41-50) ---
            questions.Add(("Keyword to define fixed variables?", "const"));
            questions.Add(("Keyword to import code namespaces?", "using"));
            questions.Add(("Keyword to dynamically size local variables?", "var"));
            questions.Add(("Keyword to trigger garbage collection disposal?", "using"));
            questions.Add(("Keyword used to handle error exceptions?", "try"));
            questions.Add(("Keyword executing directly after try or catch?", "finally"));
            questions.Add(("Keyword to explicitly generate an exception?", "throw"));
            questions.Add(("Keyword to declare unique structural data types?", "struct"));
            questions.Add(("Keyword for read-only collection iteration?", "foreach"));
            questions.Add(("Keyword defining a purely virtual class template?", "interface"));
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