using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Coding_Minigames_Project.MiniGames.Maze_Escape
{
    //This is the questions that pop ups when the player walks in the question tile in the maze minigame
    public class MazeQuestionForm : Form
    {
        Label lblQuestion, lblHint;
        TextBox txtAnswer;
        string currentAnswer = "";
        Random rnd = new Random();

        public bool IsCorrect { get; private set; }

        static readonly List<(string q, string a)> Questions = new List<(string, string)>
        {
            ("What keyword creates a class in C#?",                       "class"),
            ("What keyword creates an object instance?",                  "new"),
            ("What is the main entry method?",                            "main"),
            ("What symbol ends a statement?",                             ";"),
            ("What loop runs while condition is true?",                   "while"),
            ("Loop that runs fixed times?",                               "for"),
            ("Loop that runs at least once?",                             "do while"),
            ("Keyword to exit a loop?",                                   "break"),
            ("Keyword to skip an iteration?",                             "continue"),
            ("Decision control keyword?",                                 "if"),

            ("Alternative path if conditional fails?",                    "else"),
            ("Equality comparison operator?",                             "=="),
            ("Not equal comparison operator?",                            "!="),
            ("Logical AND conditional operator?",                         "&&"),
            ("Logical OR conditional operator?",                          "||"),
            ("Logical NOT inversion operator?",                           "!"),
            ("Remainder operator symbol?",                                "%"),
            ("Operator used to assign values?",                           "="),
            ("Multi-way branch conditional keyword?",                     "switch"),
            ("Individual option within a switch block?",                  "case"),

            ("Integer structural type?",                                  "int"),
            ("Single-precision floating data type?",                      "float"),
            ("Boolean logic structural type?",                            "bool"),
            ("Text array data type?",                                     "string"),
            ("Single text character representation type?",                "char"),
            ("Double-precision decimal floating type?",                   "double"),
            ("High precision monetary currency type?",                    "decimal"),
            ("Data type for integer 64-bit precision?",                   "long"),
            ("The base class of all types in C#?",                        "object"),

            ("Used to return value from a block?",                        "return"),
            ("Access modifier with globally open scope?",                 "public"),
            ("Access modifier restricted to current class?",              "private"),
            ("No return method type modifier?",                           "void"),
            ("Self reference context keyword?",                           "this"),
            ("Parent reference structural context keyword?",              "base"),
            ("Access modifier open only to children classes?",            "protected"),
            ("Method with the same name as the class?",                   "constructor"),
            ("Keyword to inherit or extend a class?",                     ":"),

            ("Keyword to define fixed variables?",                        "const"),
            ("Keyword to import code namespaces?",                        "using"),
            ("Keyword to dynamically size local variables?",              "var"),
            ("Keyword used to handle error exceptions?",                  "try"),
            ("Keyword executing directly after try or catch?",            "finally"),
            ("Keyword to explicitly generate an exception?",              "throw"),
            ("Keyword to declare unique structural data types?",          "struct"),
            ("Keyword for read-only collection iteration?",              "foreach"),
            ("Keyword defining a purely virtual class template?",         "interface"),
        };

        public MazeQuestionForm()
        {
            Text = "Question Door";
            Size = new Size(500, 300);
            StartPosition = FormStartPosition.CenterScreen;
            BackColor = Color.White;

            BuildUI();
            PickQuestion();
        }

        void BuildUI()
        {
            lblQuestion = new Label
            {
                Top = 20, Left = 20, Width = 440, Height = 60,
                Font = new Font("Segoe UI", 12, FontStyle.Bold),
                ForeColor = Color.Black
            };

            lblHint = new Label
            {
                Top = 80, Left = 20, Width = 440, Height = 20,
                Font = new Font("Segoe UI", 10, FontStyle.Italic),
                ForeColor = Color.Gray
            };

            txtAnswer = new TextBox
            {
                Top = 110, Left = 20, Width = 440,
                Font = new Font("Segoe UI", 11)
            };

            var btnSubmit = new Button
            {
                Text = "Submit",
                Top = 160, Left = 20, Width = 120, Height = 35,
                BackColor = Color.MediumSlateBlue,
                ForeColor = Color.White
            };
            btnSubmit.Click += (s, e) =>
            {
                IsCorrect = txtAnswer.Text.Trim().Equals(currentAnswer, StringComparison.OrdinalIgnoreCase);
                MessageBox.Show(IsCorrect ? "Correct!" : "Wrong answer!");
                Close();
            };

            Controls.AddRange(new Control[] { lblQuestion, lblHint, txtAnswer, btnSubmit });
        }

        void PickQuestion()
        {
            if (Questions.Count == 0) { lblQuestion.Text = "No questions!"; return; }

            var (q, a) = Questions[rnd.Next(Questions.Count)];
            lblQuestion.Text = q;
            currentAnswer = a.ToLower();
            lblHint.Text = "Hint: " + Jumble(currentAnswer);
        }

        string Jumble(string word)
        {
            var chars = word.ToCharArray();
            for (int i = chars.Length - 1; i > 0; i--)
            {
                int j = rnd.Next(i + 1);
                char tmp = chars[i]; chars[i] = chars[j]; chars[j] = tmp;
            }
            return new string(chars);
        }
    }
}
