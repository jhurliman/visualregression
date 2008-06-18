using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using ZedGraph;

namespace VisualRegression
{
    public class FormFileImport : Form
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// List of lists that make up a table of imported data values
        /// </summary>
        public ArrayList arrayRows;

        /// <summary>
        /// Reference to the main window for modifying the DataGridView
        /// </summary>
        public FormVisualRegression FormMain = null;

        /// <summary>
        /// Name of the file being imported
        /// </summary>
        private string fileName = "";

        public string FileName
        {
            set { fileName = value; }
        }

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
            this.txtFileDisplay = new System.Windows.Forms.TextBox();
            this.cboPredictor = new System.Windows.Forms.ComboBox();
            this.cboResponse = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.cmdCancel = new System.Windows.Forms.Button();
            this.cmdOk = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // txtFileDisplay
            // 
            this.txtFileDisplay.BackColor = System.Drawing.SystemColors.Window;
            this.txtFileDisplay.Font = new System.Drawing.Font("Lucida Console", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtFileDisplay.Location = new System.Drawing.Point(12, 12);
            this.txtFileDisplay.Multiline = true;
            this.txtFileDisplay.Name = "txtFileDisplay";
            this.txtFileDisplay.ReadOnly = true;
            this.txtFileDisplay.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtFileDisplay.Size = new System.Drawing.Size(568, 284);
            this.txtFileDisplay.TabIndex = 0;
            this.txtFileDisplay.WordWrap = false;
            // 
            // cboPredictor
            // 
            this.cboPredictor.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboPredictor.FormattingEnabled = true;
            this.cboPredictor.Location = new System.Drawing.Point(291, 307);
            this.cboPredictor.Name = "cboPredictor";
            this.cboPredictor.Size = new System.Drawing.Size(75, 21);
            this.cboPredictor.TabIndex = 2;
            // 
            // cboResponse
            // 
            this.cboResponse.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboResponse.FormattingEnabled = true;
            this.cboResponse.Location = new System.Drawing.Point(114, 307);
            this.cboResponse.Name = "cboResponse";
            this.cboResponse.Size = new System.Drawing.Size(75, 21);
            this.cboResponse.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(195, 310);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(90, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Predictor Column:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 310);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(96, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Response Column:";
            // 
            // cmdCancel
            // 
            this.cmdCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cmdCancel.Location = new System.Drawing.Point(497, 339);
            this.cmdCancel.Name = "cmdCancel";
            this.cmdCancel.Size = new System.Drawing.Size(83, 27);
            this.cmdCancel.TabIndex = 4;
            this.cmdCancel.Text = "Cancel";
            this.cmdCancel.UseVisualStyleBackColor = true;
            this.cmdCancel.Click += new System.EventHandler(this.cmdCancel_Click);
            // 
            // cmdOk
            // 
            this.cmdOk.Location = new System.Drawing.Point(408, 339);
            this.cmdOk.Name = "cmdOk";
            this.cmdOk.Size = new System.Drawing.Size(83, 27);
            this.cmdOk.TabIndex = 3;
            this.cmdOk.Text = "Ok";
            this.cmdOk.UseVisualStyleBackColor = true;
            this.cmdOk.Click += new System.EventHandler(this.cmdOk_Click);
            // 
            // FormFileImport
            // 
            this.AcceptButton = this.cmdOk;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cmdCancel;
            this.ClientSize = new System.Drawing.Size(592, 378);
            this.Controls.Add(this.cmdOk);
            this.Controls.Add(this.cmdCancel);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cboResponse);
            this.Controls.Add(this.cboPredictor);
            this.Controls.Add(this.txtFileDisplay);
            this.Name = "FormFileImport";
            this.Text = "Import File";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public TextBox txtFileDisplay;
        public ComboBox cboPredictor;
        public ComboBox cboResponse;

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button cmdCancel;
        private System.Windows.Forms.Button cmdOk;

        /// <summary>
        /// Constructor
        /// </summary>
        public FormFileImport()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Clear the main for DataGridView and add the user selected columns.
        /// </summary>
        private void cmdOk_Click(object sender, EventArgs e)
        {
            FormMain.menuNew_Click(this, null);

            int responseColumn = Convert.ToInt32(cboResponse.Text);
            int predictorColumn = Convert.ToInt32(cboPredictor.Text);

            FormMain.dataGrid.Rows.Add(arrayRows.Count);

            int j = 0;

            // Get this window out of the way so the user can watch the action
            this.Hide();
            FormMain.Refresh();

            for (int i = 0; i < arrayRows.Count; i++)
            {
                ArrayList arrayColumns = (ArrayList)arrayRows[i];
                if (Math.Max(responseColumn, predictorColumn) <= arrayColumns.Count)
                {
                    FormMain.dataGrid[0, j].Value = arrayColumns[responseColumn - 1];
                    FormMain.dataGrid[1, j].Value = arrayColumns[predictorColumn - 1];

                    j++;
                }
            }

            FormMain.FileName = fileName;
            FormMain.Modified = false;
            FormMain.menuSave.Enabled = false;

            this.Close();
        }

        /// <summary>
        /// Close this form without modifications.
        /// </summary>
        private void cmdCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
