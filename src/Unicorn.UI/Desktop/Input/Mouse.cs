﻿using System;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows;

namespace Unicorn.UI.Desktop.Input
{
    public class Mouse
    {
        private const int ExtraMillisecondsBecauseOfBugInWindows = 13;
        private static Mouse instance = new Mouse();
        private readonly short doubleClickTime = GetDoubleClickTime();
        private DateTime lastClickTime = DateTime.Now;
        private Point lastClickLocation;

        private Mouse()
        {
        }

        public static Mouse Instance => instance;

        public virtual Point Location
        {
            get
            {
                var point = new System.Drawing.Point();
                GetCursorPos(ref point);
                return point.ConvertToWindowsPoint();
            }

            set
            {
                if (value.IsInvalid())
                {
                    throw new InvalidOperationException(string.Format("Trying to set location outside the screen. {0}", value));
                }

                SetCursorPos(value.ToDrawingPoint());
            }
        }

        public static void MouseLeftButtonUpAndDown()
        {
            LeftDown();
            LeftUp();
        }

        public static void LeftUp()
        {
            SendInput(Input.Mouse(new MouseInput(WindowsConstants.MouseEventFLeftUp, GetMessageExtraInfo())));
        }

        public static void LeftDown()
        {
            SendInput(Input.Mouse(new MouseInput(WindowsConstants.MouseEventFLeftDown, GetMessageExtraInfo())));
        }

        public virtual void RightClick()
        {
            SendInput(Input.Mouse(new MouseInput(WindowsConstants.MouseEventFRightDown, GetMessageExtraInfo())));
            SendInput(Input.Mouse(new MouseInput(WindowsConstants.MouseEventFRightUp, GetMessageExtraInfo())));
        }

        public virtual void ResetPosition()
        {
            Instance.Location = new Point(0, 0);
        }

        public virtual void Click()
        {
            Point clickLocation = this.Location;
            if (this.lastClickLocation.Equals(clickLocation))
            {
                int timeout = this.doubleClickTime - DateTime.Now.Subtract(this.lastClickTime).Milliseconds;
                if (timeout > 0)
                {
                    Thread.Sleep(timeout + ExtraMillisecondsBecauseOfBugInWindows);
                }
            }

            MouseLeftButtonUpAndDown();
            this.lastClickTime = DateTime.Now;
            this.lastClickLocation = this.Location;
        }

        public virtual void RightClick(Point point)
        {
            this.Location = point;
            RightClick();
        }

        public virtual void Click(Point point)
        {
            this.Location = point;
            Click();
        }

        public virtual void MoveOut()
        {
            this.Location = new Point(0, 0);
        }

        public virtual void DoubleClick(Point point)
        {
            this.Location = point;
            MouseLeftButtonUpAndDown();
            MouseLeftButtonUpAndDown();
        }

        [DllImport("user32", EntryPoint = "SendInput")]
        private static extern int SendInput(int numberOfInputs, ref Input input, int structSize);

        [DllImport("user32.dll")]
        private static extern IntPtr GetMessageExtraInfo();

        [DllImport("user32.dll")]
        private static extern bool GetCursorPos(ref System.Drawing.Point cursorInfo);

        [DllImport("user32.dll")]
        private static extern bool SetCursorPos(System.Drawing.Point cursorInfo);

        [DllImport("user32.dll")]
        private static extern short GetDoubleClickTime();

        private static void SendInput(Input input)
        {
            SendInput(1, ref input, Marshal.SizeOf(typeof(Input)));
        }
    }
}
