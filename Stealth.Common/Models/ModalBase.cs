using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rage;
using System.Windows.Forms;
using Stealth.Common.Extensions;

namespace Stealth.Common.Models
{
    [Obsolete]
    public class ModalBase : IModalBase
    {
        public bool Display { get; set; }
        public string HeaderText { get; set; } = "";
        public string ModalText { get; set; } = "";
        public bool FreezePlayer { get; set; }
        public string FontName { get; set; } = "Arial";
        public float FontSize { get; set; } = 14.0f;
        public List<string> Responses { get; set; } = new List<string>();
        public Color TextColor { get; set; } = Color.White;
        public bool ShowClosePrompt { get; set; } = true;

        Rectangle drawRect;
        Rectangle drawBorder;
        int headerX = 150;

        protected Nullable<int> mResponse = null;

        public virtual int Show()
        {
            Activate();

            return GetResponse();
        }

        protected void Activate()
        {
            drawRect = new Rectangle(Game.Resolution.Width / 4, Game.Resolution.Height / 7, 800, 400);
            drawBorder = new Rectangle(Game.Resolution.Width / 4 - 5, Game.Resolution.Height / 7 - 5, 800, 400);

            SizeF mHeaderSize = Rage.Graphics.MeasureText(HeaderText, FontName, FontSize);

            float sideWidths = (800 - mHeaderSize.Width) / 2;
            headerX = Convert.ToInt32(Math.Floor(sideWidths));

            Display = true;
            Game.RawFrameRender += DrawModal;
        }

        protected virtual int GetResponse()
        {
            if (FreezePlayer == true)
                Game.LocalPlayer.Character.IsPositionFrozen = true;

            WaitForKeyPress();

            while (mResponse == null)
            {
                GameFiber.Yield();
                if (!Display) { return -1; }
            }

            Hide();
            return mResponse.Value;
        }

        protected virtual void WaitForKeyPress()
        {
            GameFiber.StartNew(delegate
            {
                while (Display)
                {
                    GameFiber.Yield();
                    mResponse = ReadKey();

                    if (Game.IsKeyDown(Keys.D0))
                    {
                        mResponse = -1;
                    }
                }
            });
        }

        public void Hide()
        {
            if (FreezePlayer == true)
                Game.LocalPlayer.Character.IsPositionFrozen = false;

            Display = false;
        }

        protected Nullable<int> ReadKey()
        {
            Nullable<int> mKeyResponse = null;

            if (Game.IsKeyDown(Keys.D1) && Responses.Count >= 1)
            {
                mKeyResponse = 0;
            }

            if (Game.IsKeyDown(Keys.D2) && Responses.Count >= 2)
            {
                mKeyResponse = 1;
            }

            if (Game.IsKeyDown(Keys.D3) && Responses.Count >= 3)
            {
                mKeyResponse = 2;
            }

            if (Game.IsKeyDown(Keys.D4) && Responses.Count >= 4)
            {
                mKeyResponse = 3;
            }

            if (Game.IsKeyDown(Keys.D5) && Responses.Count >= 5)
            {
                mKeyResponse = 4;
            }

            return mKeyResponse;
        }

        protected void DrawModal(object sender, GraphicsEventArgs e)
        {
            if (Display)
            {
                StringFormat stringFormat = new StringFormat();
                stringFormat.Alignment = StringAlignment.Center;
                stringFormat.LineAlignment = StringAlignment.Center;

                //40, 80, 130

                //193, 234, 255
                e.Graphics.DrawRectangle(drawBorder, Color.FromArgb(90, 40, 80, 130));
                e.Graphics.DrawRectangle(drawRect, Color.FromArgb(100, 40, 80, 130));

                Font mFont = new Font(FontName, FontSize, GraphicsUnit.Point);

                e.Graphics.DrawText(HeaderText, FontName, 18.0f, new PointF(drawBorder.X + headerX, drawBorder.Y + 20), Color.White, drawBorder);

                int YIncreaser = 60;

                if (ModalText != "")
                {
                    List<string> textLines = ModalText.WrapText(700, FontName, FontSize);

                    foreach (string mText in textLines)
                    {
                        e.Graphics.DrawText(mText, FontName, FontSize, new PointF(drawBorder.X + 10, drawBorder.Y + YIncreaser), Color.White, drawBorder);
                        YIncreaser += 30;
                    }

                    YIncreaser += 30;
                }

                //int YIncreaser = 60;

                int i = 0;
                foreach (string x in Responses)
                {
                    e.Graphics.DrawText("[" + (i + 1).ToString() + "] - " + x, FontName, FontSize, new PointF(drawRect.X + 10, drawRect.Y + YIncreaser), Color.White, drawRect);
                    YIncreaser += 20;
                    i++;
                }

                if (ShowClosePrompt == true)
                {
                    YIncreaser += 20;
                    e.Graphics.DrawText("[0] - Close", FontName, FontSize, new PointF(drawRect.X + 10, drawRect.Y + YIncreaser), Color.White, drawRect);
                }
            }
            else
            {
                Game.RawFrameRender -= DrawModal;
            }
        }
    }
}
