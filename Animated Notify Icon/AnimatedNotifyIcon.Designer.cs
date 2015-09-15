/* 
 * Demo Form
 *
 * @package Animated_Notify_Icon_Demo_Host
 * @author Oliver Green <green2go@gmail.com>
 * @copyright  Copyright (c) 2009-2012 CodeBlog (http://www.codeblog.co.uk)
 * @license Creative Commons Attribution-ShareAlike 3.0 Unported License. (http://creativecommons.org/licenses/by-sa/3.0/)
 * Version: 0.1 $Revision: 350 $
 * Date: $Date: 2012-01-04 23:27:28 +0000 (Wed, 04 Jan 2012) $
 * 
 */

using System.Drawing;

namespace AnimatedNotifyIconNS
{
    [ToolboxBitmapAttribute(typeof(AnimatedNotifyIcon),
    "AnimatedNotifyIcon.ico")]
    partial class AnimatedNotifyIcon
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this._notifyIcon = new System.Windows.Forms.NotifyIcon(this.components);
            // 
            // trayIcon
            // 
            this._notifyIcon.BalloonTipIcon = System.Windows.Forms.ToolTipIcon.Info;
            this._notifyIcon.Text = "trayIcon";
            this._notifyIcon.Visible = true;

        }

        #endregion

        private System.Windows.Forms.NotifyIcon _notifyIcon;


    }
}
