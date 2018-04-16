using System;
using System.Collections.Generic;
using System.Drawing;

namespace Stealth.Common.Models
{

    [Obsolete]
    interface IModalBase
    {
        bool Display { get; set; }
        string HeaderText { get; set; }
        string ModalText { get; set; }
        bool FreezePlayer { get; set; }
        string FontName { get; set; }
        float FontSize { get; set; }
        List<string> Responses { get; set; }
        Color TextColor { get; set; }
        bool ShowClosePrompt { get; set; }

        int Show();
        void Hide();
    }
}