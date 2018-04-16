using Rage;
using Stealth.Common.Extensions;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Stealth.Common.Models
{
    [Obsolete]
    public class ModalWindow : ModalBase, IModalBase
    {
        public ModalWindow(string pHeaderText, string pModalText, bool pFreezePlayer) : this(pHeaderText, pModalText, new List<string>(), pFreezePlayer) { }

        public ModalWindow(string pHeaderText, string pModalText, List<string> pResponses, bool pFreezePlayer) : this(pHeaderText, pModalText, new List<string>(), Color.White, pFreezePlayer, "Arial", 14.0f) { }

        public ModalWindow(string pHeaderText, string pModalText, List<string> pResponses, Color pTextColor, bool pFreezePlayer, string pFontName, float pFontSize)
        {
            HeaderText = pHeaderText;
            ModalText = pModalText;
            Responses = pResponses;
            TextColor = pTextColor;
            FreezePlayer = pFreezePlayer;
            FontName = pFontName;
            FontSize = pFontSize;
        }
    }
}