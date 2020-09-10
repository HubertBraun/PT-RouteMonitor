namespace NetworkRouteMonitor
{
    partial class StartForm
    {
        /// <summary>
        /// Wymagana zmienna projektanta.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Wyczyść wszystkie używane zasoby.
        /// </summary>
        /// <param name="disposing">prawda, jeżeli zarządzane zasoby powinny zostać zlikwidowane; Fałsz w przeciwnym wypadku.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Kod generowany przez Projektanta formularzy systemu Windows

        /// <summary>
        /// Metoda wymagana do obsługi projektanta — nie należy modyfikować
        /// jej zawartości w edytorze kodu.
        /// </summary>
        private void InitializeComponent()
        {
            this.InterfacesList = new System.Windows.Forms.ListBox();
            this.LoadFileButton = new System.Windows.Forms.Button();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.SuspendLayout();
            // 
            // InterfacesList
            // 
            this.InterfacesList.FormattingEnabled = true;
            this.InterfacesList.ItemHeight = 16;
            this.InterfacesList.Location = new System.Drawing.Point(12, 41);
            this.InterfacesList.Name = "InterfacesList";
            this.InterfacesList.Size = new System.Drawing.Size(413, 388);
            this.InterfacesList.TabIndex = 2;
            this.InterfacesList.SelectedIndexChanged += new System.EventHandler(this.SelectInterface);
            // 
            // LoadFileButton
            // 
            this.LoadFileButton.Location = new System.Drawing.Point(431, 41);
            this.LoadFileButton.Name = "LoadFileButton";
            this.LoadFileButton.Size = new System.Drawing.Size(75, 23);
            this.LoadFileButton.TabIndex = 3;
            this.LoadFileButton.Text = "Load File";
            this.LoadFileButton.UseVisualStyleBackColor = true;
            this.LoadFileButton.Click += new System.EventHandler(this.LoadFileButton_Click);
            // 
            // StartForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.LoadFileButton);
            this.Controls.Add(this.InterfacesList);
            this.Name = "StartForm";
            this.Text = "StartForm";
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.ListBox InterfacesList;
        private System.Windows.Forms.Button LoadFileButton;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
    }
}

