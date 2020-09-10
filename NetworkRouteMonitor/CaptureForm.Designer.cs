using System.Drawing;

namespace NetworkRouteMonitor
{
    partial class CaptureForm
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.StartButton = new System.Windows.Forms.Button();
            this.Frames = new System.Windows.Forms.ListBox();
            this.StopButton = new System.Windows.Forms.Button();
            this.Clear = new System.Windows.Forms.Button();
            this.MapButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // StartButton
            // 
            this.StartButton.Location = new System.Drawing.Point(12, 12);
            this.StartButton.Name = "StartButton";
            this.StartButton.Size = new System.Drawing.Size(75, 23);
            this.StartButton.TabIndex = 0;
            this.StartButton.Text = "Start";
            this.StartButton.UseVisualStyleBackColor = true;
            this.StartButton.Click += new System.EventHandler(this.StartCapturing);
            // 
            // Frames
            // 
            this.Frames.Font = new System.Drawing.Font("Courier New", 8.25F);
            this.Frames.FormattingEnabled = true;
            this.Frames.ItemHeight = 17;
            this.Frames.Location = new System.Drawing.Point(13, 42);
            this.Frames.Name = "Frames";
            this.Frames.Size = new System.Drawing.Size(737, 446);
            this.Frames.TabIndex = 1;
            this.Frames.SelectedIndexChanged += new System.EventHandler(this.Frames_SelectedIndexChanged);
            // 
            // StopButton
            // 
            this.StopButton.Location = new System.Drawing.Point(93, 13);
            this.StopButton.Name = "StopButton";
            this.StopButton.Size = new System.Drawing.Size(75, 23);
            this.StopButton.TabIndex = 2;
            this.StopButton.Text = "Stop";
            this.StopButton.UseVisualStyleBackColor = true;
            this.StopButton.Click += new System.EventHandler(this.StopCapturing);
            // 
            // Clear
            // 
            this.Clear.Location = new System.Drawing.Point(174, 12);
            this.Clear.Name = "Clear";
            this.Clear.Size = new System.Drawing.Size(75, 23);
            this.Clear.TabIndex = 3;
            this.Clear.Text = "Clear";
            this.Clear.UseVisualStyleBackColor = true;
            this.Clear.Click += new System.EventHandler(this.Clear_Click);
            // 
            // MapButton
            // 
            this.MapButton.Location = new System.Drawing.Point(255, 12);
            this.MapButton.Name = "MapButton";
            this.MapButton.Size = new System.Drawing.Size(75, 23);
            this.MapButton.TabIndex = 4;
            this.MapButton.Text = "Map";
            this.MapButton.UseVisualStyleBackColor = true;
            this.MapButton.Click += new System.EventHandler(this.MapButton_Click);
            // 
            // CaptureForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(762, 504);
            this.Controls.Add(this.MapButton);
            this.Controls.Add(this.Clear);
            this.Controls.Add(this.StopButton);
            this.Controls.Add(this.Frames);
            this.Controls.Add(this.StartButton);
            this.Name = "CaptureForm";
            this.Text = "CaptureForm";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.StopCapturing);
            this.Resize += new System.EventHandler(this.ResizeForm);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button StartButton;
        private System.Windows.Forms.ListBox Frames;
        private System.Windows.Forms.Button StopButton;
        private System.Windows.Forms.Button Clear;
        private System.Windows.Forms.Button MapButton;
    }
}