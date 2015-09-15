/* 
 * C# Animated Notify Icon Component
 *
 * @package uk.co.codeblog.csharp.controls.AnimatedNotifyIcon
 * @author Oliver Green <green2go@gmail.com>
 * @copyright  Copyright (c) 2009-2012 CodeBlog (http://www.codeblog.co.uk)
 * @license Creative Commons Attribution-ShareAlike 3.0 Unported License. (http://creativecommons.org/licenses/by-sa/3.0/)
 * Version: 0.1 $Revision: 350 $
 * Date: $Date: 2012-01-04 23:27:28 +0000 (Wed, 04 Jan 2012) $
 * 
 */

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Text;
using System.Windows.Forms;

using System.Drawing;
using System.Timers;

namespace AnimatedNotifyIconNS
{
    public partial class AnimatedNotifyIcon : Component
    {
        /* Timer to control the animation */
        System.Timers.Timer _t = new System.Timers.Timer();

        /* Our 'filstrip' of icons to build the animation from */
        Icon[] _icons;

        /* Which index of the film strip are we currently on? */
        int _cIndex = 0;

        /* How many times have we looped the animation so far? */
        int _loopCount = 1;
        
        /* How many times should we loop the animation for? 0 = infinite */
        int _loopNo = 0;

        /* 
         * Properties
         * Most of which wrap the NotifyIcons properties on a basic level
         * (If you want events you'll have to reference the inner NotifyIcon 
         * object
         */


        ///<summary>The text that will be displaid when the mouse hovers over the icon</summary>

        [CategoryAttribute("Appearance"),
         DescriptionAttribute("The text that will be displaid when the mouse hovers over the icon")]

        public string Text
        {
            get { return _notifyIcon.Text; }
            set { _notifyIcon.Text = value; }
        }



        ///<summary>The icon to associate with the balloon ToolTip</summary>
        
        [CategoryAttribute("Appearance"),
         DescriptionAttribute("The icon to associate with the BalloonTip")]

        public ToolTipIcon BalloonTipIcon
        {
            get { return _notifyIcon.BalloonTipIcon; }
            set { _notifyIcon.BalloonTipIcon = value; }
        }



        ///<summary>The text to associate with the balloon ToolTip</summary>
        
        [CategoryAttribute("Appearance"),
         DescriptionAttribute("The text to associate with the balloon ToolTip")]

        public string BalloonTipText
        {
            get { return _notifyIcon.BalloonTipText; }
            set { _notifyIcon.BalloonTipText = value; }
        }


        ///<summary>The title to associate with the balloon ToolTip</summary>
        
        [CategoryAttribute("Appearance"),
         DescriptionAttribute("The title to associate with the balloon ToolTip")]
        
        public string BalloonTipTitle
        {
            get { return _notifyIcon.BalloonTipTitle; }
            set { _notifyIcon.BalloonTipTitle = value; }
        }


        ///<summary>The shortcut menu to show when the user right-clicks on the icon</summary>
        
        [CategoryAttribute("Behavior"),
         DescriptionAttribute("The shortcut menu to show when the user right-clicks on the icon")]
        
        public ContextMenuStrip ContextMenuStrip
        {
            get { return _notifyIcon.ContextMenuStrip; }
            set { _notifyIcon.ContextMenuStrip = value; }
        }


        ///<summary>Determines whether the control is visible or hidden</summary>
        
        [CategoryAttribute("Behavior"),
         DescriptionAttribute("Determines whether the control is visible or hidden")]
        
        public Boolean Visible
        {
            get { return _notifyIcon.Visible; }
            set { _notifyIcon.Visible = value; }
        }


        ///<summary>Gets the underlying NotifyIcon control</summary>
        
        [CategoryAttribute("Misc"),
         DescriptionAttribute("Gets the underlying NotifyIcon control")]
    
        public NotifyIcon NotifyIcon
        {
            get { return _notifyIcon; }
        }


        ///<summary>Sets the number of times for the animation to loop (0 = Infinite)</summary>
        
        [CategoryAttribute("Behavior"),
         DescriptionAttribute("Sets the number of times for the animation to loop (0 = Infinite)")]
        
        public int Loop
        {
            get { return _loopNo; }
            set { _loopNo = value; }
        }


        ///<summary>Sets the animation frame rate (FPS)</summary>
        
        [ CategoryAttribute("Behavior"),
          DescriptionAttribute("Sets the animation frame rate (FPS)")]
        
        public double FrameRate
        {
            get { return (1000 / _t.Interval); }
            set 
            { 
                int interval = (int) Math.Ceiling(Convert.ToDouble(1000 / value));

                if (interval > 0)
                {
                    _t.Interval = interval;
                }
                else
                {
                    _t.Interval = 100;
                }
            }
        }


        ///<summary>Returns whether the animation is running or not</summary>
        
        [CategoryAttribute("Misc"),
         DescriptionAttribute("Returns whether the animation is running or not")]

        public Boolean IsRunning
        {
            get { return _t.Enabled; }
        }

        public AnimatedNotifyIcon()
        {
            InitializeComponent();
            InitializeIcon();
        }

        public AnimatedNotifyIcon(IContainer container)
        {
            container.Add(this);
            InitializeComponent();
            InitializeIcon();
        }


        ///<summary>Sets up the tray icon</summary>

        protected void InitializeIcon()
        {
            _t.Interval = 700;
            _t.Elapsed += new ElapsedEventHandler(ChangeIcon);
            
        }


        ///<summary>Changes the icon on timer</summary>

        protected void ChangeIcon(object source, ElapsedEventArgs e)
        {
            if (_loopNo == 0 || _loopCount <= _loopNo)
            {                
                _cIndex++;
                if (_cIndex >= _icons.Length)
                {
                    _cIndex = 0;
                    _loopCount++;
                }
                _notifyIcon.Icon = _icons[_cIndex];
            }
            else
            {
                _loopCount = 1;
                this.Stop();
            }
        }


        ///<summary>Starts the animation</summary>

        public void Start()
        {
            _t.Enabled = true;
            this.Visible = true;
   //         _t.Start();
        }


        ///<summary>Stops the animation</summary>

        public void Stop()
        {
     //       _t.Stop();
            this.Visible = false;
            _t.Enabled = false;
        }


        ///<summary>Displays a balloon ToolTip in the taskbar for a specified period</summary>
        ///<param name="Timeout">Time period, in millisecods, the balloon should display for</param>

        public void ShowBalloonTip(int Timeout)
        {
            _notifyIcon.ShowBalloonTip(Timeout);
        }


        ///<summary>Displays a balloon ToolTip in the taskbar for a specified period</summary>
        ///<param name="Timeout">Time period, in millisecods, the balloon should display for</param>
        ///<param name="TipTitle">The title to display on the balloon tip</param>
        ///<param name="TipText">The text to display on the balloon tip</param>
        ///<param name="TipIcon">One of the System.Windows.Forms.ToolTipIcon values</param>

        public void ShowBalloonTip(int Timeout, string TipTitle, string TipText, ToolTipIcon TipIcon)
        {
            _notifyIcon.ShowBalloonTip(Timeout, TipTitle, TipText, TipIcon);
        }


        ///<summary>Sets the animation frames</summary>
        ///<param name="Icons">Array of icons to form animation</param>
        ///<param name="MoveToFirstFrame">Sets the NotifyIcons current icon to the first icon in the Array</param>

        public void SetAnimationFrames(Icon[] Icons, Boolean MoveToFirstFrame = true)
        {
            if (Icons.Length > 0)
            {
                _icons = Icons;

                if (MoveToFirstFrame)
                {
                    _notifyIcon.Icon = Icons[0];
                    _cIndex = 0;
                }


            }
        }

        

    }
}
