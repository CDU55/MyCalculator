
namespace MyCalculator.UI
{
    partial class MainMenu
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
            this.InteractiveModeBtn = new System.Windows.Forms.Button();
            this.AutomaticModeBtn = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // InteractiveModeBtn
            // 
            this.InteractiveModeBtn.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.InteractiveModeBtn.Location = new System.Drawing.Point(76, 187);
            this.InteractiveModeBtn.Name = "InteractiveModeBtn";
            this.InteractiveModeBtn.Size = new System.Drawing.Size(246, 89);
            this.InteractiveModeBtn.TabIndex = 0;
            this.InteractiveModeBtn.Text = "Interactive Mode";
            this.InteractiveModeBtn.UseVisualStyleBackColor = true;
            this.InteractiveModeBtn.Click += new System.EventHandler(this.InteractiveMode_Click);
            // 
            // AutomaticModeBtn
            // 
            this.AutomaticModeBtn.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.AutomaticModeBtn.Location = new System.Drawing.Point(451, 187);
            this.AutomaticModeBtn.Name = "AutomaticModeBtn";
            this.AutomaticModeBtn.Size = new System.Drawing.Size(246, 89);
            this.AutomaticModeBtn.TabIndex = 1;
            this.AutomaticModeBtn.Text = "Automatic Mode";
            this.AutomaticModeBtn.UseVisualStyleBackColor = true;
            this.AutomaticModeBtn.Click += new System.EventHandler(this.AutomaticMode_Click);
            // 
            // MainMenu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 800);
            this.Controls.Add(this.AutomaticModeBtn);
            this.Controls.Add(this.InteractiveModeBtn);
            this.Name = "MainMenu";
            this.Text = "MyCalculator";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button InteractiveModeBtn;
        private System.Windows.Forms.Button AutomaticModeBtn;
    }
}

