using Rage;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Stealth.Common.Models
{
    [Obsolete]
    public class QuestionWindow : ModalBase, IModalBase
    {
        public List<QAItem> Questions { get; set; } = new List<QAItem>();

        public QuestionWindow(string pHeaderText, bool pFreezePlayer) : this(pHeaderText, new List<QAItem>(), pFreezePlayer) { }

        public QuestionWindow(string pHeaderText, List<QAItem> pQuestions, bool pFreezePlayer) : this(pHeaderText, pQuestions, Color.White, pFreezePlayer, "Arial", 14.0f) { }

        public QuestionWindow(string pHeaderText, List<QAItem> pQuestions, Color pTextColor, bool pFreezePlayer, string pFontName, float pFontSize)
        {
            HeaderText = pHeaderText;
            ModalText = "";
            TextColor = pTextColor;
            FreezePlayer = pFreezePlayer;
            FontName = pFontName;
            FontSize = pFontSize;

            Responses = new List<string>();
            Questions = new List<QAItem>();

            foreach (QAItem q in pQuestions)
            {
                Responses.Add(q.Question);
                Questions.Add(q);
                Game.LogVerboseDebug("Adding " + q.Question);
                Game.LogVerboseDebug("Answer " + q.Answer);
            }
        }

        protected override void WaitForKeyPress()
        {
            GameFiber.StartNew(delegate
            {
                while (Display)
                {
                    GameFiber.Yield();

                    int? mTempResponse = ReadKey();

                    if (Game.IsKeyDown(Keys.D0))
                    {
                        mResponse = -1;
                    }

                    if (mTempResponse != null && mTempResponse.HasValue && mTempResponse.Value >= 0)
                    {
                        int idx = mTempResponse.Value;

                        if (idx >= 0 && (idx < Responses.Count && idx < Questions.Count))
                        {
                            GameFiber.StartNew(delegate
                            {
                                string mAnswer = string.Format("~y~Subject: ~w~{0}", Questions[idx].Answer);
                                Game.DisplaySubtitle(mAnswer, 5000);
                            });
                        }
                    }
                }
            });
        }

        public class QAItem
        {
            public string Question { get; set; } = "";
            public string Answer { get; set; } = "";

            public QAItem(string pQuestion) : this(pQuestion, "") { }

            public QAItem(string pQuestion, string pAnswer)
            {
                Question = pQuestion;
                Answer = pAnswer;
            }
        }
    }
}
