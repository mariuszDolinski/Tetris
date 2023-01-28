namespace Tetris
{
    partial class NajlepszeWyniki
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
            this.label1 = new System.Windows.Forms.Label();
            this.tabela = new System.Windows.Forms.Label();
            this.punkty = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label1.Location = new System.Drawing.Point(61, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(150, 16);
            this.label1.TabIndex = 0;
            this.label1.Text = "NAJLEPSZE WYNIKI";
            // 
            // tabela
            // 
            this.tabela.Location = new System.Drawing.Point(23, 47);
            this.tabela.Name = "tabela";
            this.tabela.Size = new System.Drawing.Size(188, 140);
            this.tabela.TabIndex = 1;
            // 
            // punkty
            // 
            this.punkty.Location = new System.Drawing.Point(217, 47);
            this.punkty.Name = "punkty";
            this.punkty.Size = new System.Drawing.Size(56, 140);
            this.punkty.TabIndex = 2;
            // 
            // NajlepszeWyniki
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(292, 196);
            this.Controls.Add(this.punkty);
            this.Controls.Add(this.tabela);
            this.Controls.Add(this.label1);
            this.Name = "NajlepszeWyniki";
            this.Text = "NajlepszeWyniki";
            this.Load += new System.EventHandler(this.NajlepszeWyniki_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label tabela;
        private System.Windows.Forms.Label punkty;
    }
}