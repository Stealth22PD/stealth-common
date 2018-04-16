using Rage;
using Stealth.Common.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Stealth.Common.Functions
{
    public static class GameFuncs
    {
        public static uint DisplayNotification(string title, string subtitle, string text)
        {
            if (!subtitle.StartsWith("~"))
                subtitle = "~b~" + subtitle;

            return Game.DisplayNotification("web_lossantospolicedept", "web_lossantospolicedept", title, subtitle, text);
        }

        public static bool IsKeyCombinationPressed(Keys key, Keys modKey = Keys.None, EKeyboardCheckMode checkMode = EKeyboardCheckMode.Check_Only_Ctrl_Shift_Alt)
        {
            if (modKey == Keys.None)
            {
                if (Game.IsKeyDown(key))
                {
                    KeyboardState kbState = Game.GetKeyboardState();
                    
                    if (checkMode == EKeyboardCheckMode.Check_Only_Ctrl_Shift_Alt)
                    {
                        // Check if any of the 'regular' mod keys are pressed
                        return (!kbState.IsControlDown && !kbState.IsShiftDown && !kbState.IsAltDown);
                    }
                    else
                    {
                        // Check if PressedKeys contains any elements that are not equal to the currently pressed key
                        return kbState.PressedKeys.Any(x => x != key);
                    }
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return (Game.IsKeyDown(key) && Game.IsKeyDownRightNow(modKey));
            }
        }
    }
}
