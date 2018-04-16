using Stealth.Common.Enums;
using Stealth.Common.Functions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Stealth.Common.Extensions
{
    public static class KeyExtensions
    {
        public static bool IsKeyPressed(this Keys key, EKeyboardCheckMode checkMode = EKeyboardCheckMode.Check_Only_Ctrl_Shift_Alt)
        {
            return GameFuncs.IsKeyCombinationPressed(key, Keys.None, checkMode);
        }

        public static bool IsKeyPressedWithModKey(this Keys key, Keys modKey, EKeyboardCheckMode checkMode = EKeyboardCheckMode.Check_Only_Ctrl_Shift_Alt)
        {
            return GameFuncs.IsKeyCombinationPressed(key, modKey, checkMode);
        }
    }
}
