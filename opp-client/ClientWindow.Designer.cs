namespace opp_client
{
    partial class ClientWindow
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.debugConsole = new System.Windows.Forms.ListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.JoinRedButton = new System.Windows.Forms.Button();
            this.JoinBlueButton = new System.Windows.Forms.Button();
            this.GameLoop = new System.Windows.Forms.Timer(this.components);
            this.FieldPictureBox = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.FieldPictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // debugConsole
            // 
            this.debugConsole.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.debugConsole.FormattingEnabled = true;
            this.debugConsole.ItemHeight = 20;
            this.debugConsole.Location = new System.Drawing.Point(12, 374);
            this.debugConsole.Name = "debugConsole";
            this.debugConsole.Size = new System.Drawing.Size(776, 64);
            this.debugConsole.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 351);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(114, 20);
            this.label1.TabIndex = 1;
            this.label1.Text = "Debug Console:";
            // 
            // JoinRedButton
            // 
            this.JoinRedButton.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.JoinRedButton.Location = new System.Drawing.Point(507, 339);
            this.JoinRedButton.Name = "JoinRedButton";
            this.JoinRedButton.Size = new System.Drawing.Size(135, 29);
            this.JoinRedButton.TabIndex = 2;
            this.JoinRedButton.TabStop = false;
            this.JoinRedButton.Text = "Join Red Team";
            this.JoinRedButton.UseVisualStyleBackColor = true;
            this.JoinRedButton.Click += new System.EventHandler(this.JoinRedButton_Click);
            // 
            // JoinBlueButton
            // 
            this.JoinBlueButton.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.JoinBlueButton.Location = new System.Drawing.Point(648, 339);
            this.JoinBlueButton.Name = "JoinBlueButton";
            this.JoinBlueButton.Size = new System.Drawing.Size(140, 29);
            this.JoinBlueButton.TabIndex = 3;
            this.JoinBlueButton.TabStop = false;
            this.JoinBlueButton.Text = "Join Blue Team";
            this.JoinBlueButton.UseVisualStyleBackColor = true;
            this.JoinBlueButton.Click += new System.EventHandler(this.JoinBlueButton_Click);
            // 
            // GameLoop
            // 
            this.GameLoop.Enabled = true;
            this.GameLoop.Interval = 20;
            this.GameLoop.Tick += new System.EventHandler(this.GameLoop_Tick);
            // 
            // FieldPictureBox
            // 
            this.FieldPictureBox.BackColor = System.Drawing.Color.DarkOliveGreen;
            this.FieldPictureBox.Location = new System.Drawing.Point(0, 0);
            this.FieldPictureBox.Name = "FieldPictureBox";
            this.FieldPictureBox.Size = new System.Drawing.Size(799, 333);
            this.FieldPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.FieldPictureBox.TabIndex = 4;
            this.FieldPictureBox.TabStop = false;
            // 
            // ClientWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.JoinBlueButton);
            this.Controls.Add(this.JoinRedButton);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.debugConsole);
            this.Controls.Add(this.FieldPictureBox);
            this.KeyPreview = true;
            this.Name = "ClientWindow";
            this.Text = "ClientWindow";
            this.Load += new System.EventHandler(this.ClientWindow_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ClientWindow_KeyDown);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.ClientWindow_KeyUp);
            this.Resize += new System.EventHandler(this.ClientWindow_Resize);
            ((System.ComponentModel.ISupportInitialize)(this.FieldPictureBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox debugConsole;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button JoinRedButton;
        private System.Windows.Forms.Button JoinBlueButton;
        private System.Windows.Forms.Timer GameLoop;
        private System.Windows.Forms.PictureBox FieldPictureBox;
    }
}
