namespace FindImportantFeatures
{
    partial class Form1
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
            btnUsePFI = new Button();
            SuspendLayout();
            // 
            // btnUsePFI
            // 
            btnUsePFI.Location = new Point(582, 37);
            btnUsePFI.Name = "btnUsePFI";
            btnUsePFI.Size = new Size(152, 42);
            btnUsePFI.TabIndex = 0;
            btnUsePFI.Text = "關鍵影響因素";
            btnUsePFI.UseVisualStyleBackColor = true;
            btnUsePFI.Click += btnUsePFI_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(9F, 19F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(btnUsePFI);
            Name = "Form1";
            Text = "Form1";
            ResumeLayout(false);
        }

        #endregion

        private Button btnUsePFI;
    }
}
