using Rage;
using System;
using System.Collections.Generic;
using System.Drawing;

namespace Stealth.Common.Models
{
    [Obsolete]
    public class SpeechModal : ModalBase, IModalBase
    {
        private int mSpeechIndex = 0;
        public List<string> SpeechLines = new List<string>();
        private string mDisplayName = "";

        public SpeechModal(string pDisplayName, List<string> pSpeechLines, bool pFreezePlayer) : this(pDisplayName, pSpeechLines, Color.White, pFreezePlayer, "Arial", 14.0f) { }

        public SpeechModal(string pDisplayName, List<string> pSpeechLines, Color pTextColor, bool pFreezePlayer, string pFontName, float pFontSize)
        {
            SpeechLines = pSpeechLines;
            HeaderText = "Conversation";
            ModalText = FormatSpeech(SpeechLines[0]);
            mDisplayName = pDisplayName;
            FreezePlayer = pFreezePlayer;
            FontName = pFontName;
            FontSize = pFontSize;
            
            if (SpeechLines.Count > 1)
            {
                Responses = new List<string> { "Next" };
            }
            else
            {
                Responses.Clear();
            }

            TextColor = pTextColor;
        }

        private string FormatSpeech(string pSpeechText)
        {
            return string.Format("{0}: {1}", mDisplayName, pSpeechText);
        }

        public override int Show()
        {
            if (SpeechLines.Count < 1)
            {
                Hide();
                return -1;
            }

            Activate();

            int mResponse = 0;

            while (mResponse > -1)
            {
                GameFiber.Yield();
                mResponse = GetResponse();

                if (!Display) { return -1; }
            }

            return -1;
        }

        protected override int GetResponse()
        {
            if (FreezePlayer == true)
                Game.LocalPlayer.Character.IsPositionFrozen = true;

            WaitForKeyPress();

            while (mResponse == null || mResponse > -1)
            {
                GameFiber.Yield();
                if (!Display) { return -1; }

                if (mResponse.HasValue && mResponse > -1)
                {
                    if (mResponse == 0 && Responses.Count > 0)
                    {
                        //Next
                        mSpeechIndex += 1;

                        if (mSpeechIndex > (SpeechLines.Count - 1))
                        {
                            mSpeechIndex = 0;
                        }

                        if (mSpeechIndex == (SpeechLines.Count - 1))
                        {
                            ModalText = FormatSpeech(SpeechLines[mSpeechIndex]);
                            Responses = new List<string> { "Start Over" };
                        }
                        else if (mSpeechIndex < (SpeechLines.Count - 1))
                        {
                            ModalText = FormatSpeech(SpeechLines[mSpeechIndex]);
                            Responses = new List<string> { "Next" };
                        }
                    }

                    mResponse = null;
                }
            }

            Hide();
            return mResponse.Value;
        }
    }
}
