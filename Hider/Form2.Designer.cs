using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
namespace Hider
{
    partial class Hider
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
            this.GroupBoxLog = new System.Windows.Forms.GroupBox();
            this.RichTextBoxLog = new System.Windows.Forms.RichTextBox();
            this.ProgessBarPercent = new System.Windows.Forms.ProgressBar();
            this.groupBoxInputFile = new System.Windows.Forms.GroupBox();
            this.ButtonBrowseInput = new System.Windows.Forms.Button();
            this.TextBoxInput = new System.Windows.Forms.TextBox();
            this.GroupBoxData = new System.Windows.Forms.GroupBox();
            this.ButtonSave = new System.Windows.Forms.Button();
            this.radioButton3 = new System.Windows.Forms.RadioButton();
            this.radioButton2 = new System.Windows.Forms.RadioButton();
            this.RichTextBoxData = new System.Windows.Forms.RichTextBox();
            this.ButtonBrowseData = new System.Windows.Forms.Button();
            this.ButtonHide = new System.Windows.Forms.Button();
            this.ButtonExtract = new System.Windows.Forms.Button();
            this.LabelPercent = new System.Windows.Forms.Label();
            this.ComboBoxHighLevel = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.GroupBoxKey = new System.Windows.Forms.GroupBox();
            this.RichTextBoxKey = new System.Windows.Forms.TextBox();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.GroupBoxLog.SuspendLayout();
            this.groupBoxInputFile.SuspendLayout();
            this.GroupBoxData.SuspendLayout();
            this.GroupBoxKey.SuspendLayout();
            this.SuspendLayout();
            // 
            // GroupBoxLog
            // 
            this.GroupBoxLog.Controls.Add(this.RichTextBoxLog);
            this.GroupBoxLog.Location = new System.Drawing.Point(12, 353);
            this.GroupBoxLog.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.GroupBoxLog.Name = "GroupBoxLog";
            this.GroupBoxLog.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.GroupBoxLog.Size = new System.Drawing.Size(685, 214);
            this.GroupBoxLog.TabIndex = 0;
            this.GroupBoxLog.TabStop = false;
            this.GroupBoxLog.Text = "Log";
            // 
            // RichTextBoxLog
            // 
            this.RichTextBoxLog.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.RichTextBoxLog.Location = new System.Drawing.Point(6, 21);
            this.RichTextBoxLog.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.RichTextBoxLog.Name = "RichTextBoxLog";
            this.RichTextBoxLog.ReadOnly = true;
            this.RichTextBoxLog.Size = new System.Drawing.Size(673, 189);
            this.RichTextBoxLog.TabIndex = 0;
            this.RichTextBoxLog.Text = "";
            // 
            // ProgessBarPercent
            // 
            this.ProgessBarPercent.Location = new System.Drawing.Point(18, 334);
            this.ProgessBarPercent.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.ProgessBarPercent.Name = "ProgessBarPercent";
            this.ProgessBarPercent.Size = new System.Drawing.Size(679, 18);
            this.ProgessBarPercent.Step = 1;
            this.ProgessBarPercent.TabIndex = 1;
            // 
            // groupBoxInputFile
            // 
            this.groupBoxInputFile.Controls.Add(this.ButtonBrowseInput);
            this.groupBoxInputFile.Controls.Add(this.TextBoxInput);
            this.groupBoxInputFile.Location = new System.Drawing.Point(12, 10);
            this.groupBoxInputFile.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBoxInputFile.Name = "groupBoxInputFile";
            this.groupBoxInputFile.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBoxInputFile.Size = new System.Drawing.Size(685, 66);
            this.groupBoxInputFile.TabIndex = 2;
            this.groupBoxInputFile.TabStop = false;
            this.groupBoxInputFile.Text = "Input file";
            // 
            // ButtonBrowseInput
            // 
            this.ButtonBrowseInput.Location = new System.Drawing.Point(585, 21);
            this.ButtonBrowseInput.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.ButtonBrowseInput.Name = "ButtonBrowseInput";
            this.ButtonBrowseInput.Size = new System.Drawing.Size(94, 23);
            this.ButtonBrowseInput.TabIndex = 1;
            this.ButtonBrowseInput.Text = "Browse";
            this.ButtonBrowseInput.UseVisualStyleBackColor = true;
            this.ButtonBrowseInput.Click += new System.EventHandler(this.ButtonBrowseInput_Click);
            // 
            // TextBoxInput
            // 
            this.TextBoxInput.Location = new System.Drawing.Point(6, 21);
            this.TextBoxInput.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.TextBoxInput.Name = "TextBoxInput";
            this.TextBoxInput.Size = new System.Drawing.Size(573, 22);
            this.TextBoxInput.TabIndex = 0;
            this.TextBoxInput.Text = "Select file ";
            // 
            // GroupBoxData
            // 
            this.GroupBoxData.Controls.Add(this.ButtonSave);
            this.GroupBoxData.Controls.Add(this.radioButton3);
            this.GroupBoxData.Controls.Add(this.radioButton2);
            this.GroupBoxData.Controls.Add(this.RichTextBoxData);
            this.GroupBoxData.Controls.Add(this.ButtonBrowseData);
            this.GroupBoxData.Location = new System.Drawing.Point(12, 81);
            this.GroupBoxData.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.GroupBoxData.Name = "GroupBoxData";
            this.GroupBoxData.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.GroupBoxData.Size = new System.Drawing.Size(685, 156);
            this.GroupBoxData.TabIndex = 2;
            this.GroupBoxData.TabStop = false;
            this.GroupBoxData.Text = "Data";
            // 
            // ButtonSave
            // 
            this.ButtonSave.Location = new System.Drawing.Point(585, 57);
            this.ButtonSave.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.ButtonSave.Name = "ButtonSave";
            this.ButtonSave.Size = new System.Drawing.Size(94, 23);
            this.ButtonSave.TabIndex = 5;
            this.ButtonSave.Text = "Save";
            this.ButtonSave.UseVisualStyleBackColor = true;
            this.ButtonSave.Click += new System.EventHandler(this.ButtonSave_Click);
            // 
            // radioButton3
            // 
            this.radioButton3.AutoSize = true;
            this.radioButton3.Location = new System.Drawing.Point(585, 122);
            this.radioButton3.Name = "radioButton3";
            this.radioButton3.Size = new System.Drawing.Size(66, 20);
            this.radioButton3.TabIndex = 4;
            this.radioButton3.TabStop = true;
            this.radioButton3.Text = "UTF-8";
            this.radioButton3.UseVisualStyleBackColor = true;
            this.radioButton3.Visible = false;
            // 
            // radioButton2
            // 
            this.radioButton2.AutoSize = true;
            this.radioButton2.Location = new System.Drawing.Point(585, 96);
            this.radioButton2.Name = "radioButton2";
            this.radioButton2.Size = new System.Drawing.Size(57, 20);
            this.radioButton2.TabIndex = 4;
            this.radioButton2.TabStop = true;
            this.radioButton2.Text = "DNA";
            this.radioButton2.UseVisualStyleBackColor = true;
            this.radioButton2.Visible = false;
            // 
            // RichTextBoxData
            // 
            this.RichTextBoxData.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.RichTextBoxData.Location = new System.Drawing.Point(6, 20);
            this.RichTextBoxData.Name = "RichTextBoxData";
            this.RichTextBoxData.Size = new System.Drawing.Size(573, 131);
            this.RichTextBoxData.TabIndex = 2;
            this.RichTextBoxData.Text = "";
            // 
            // ButtonBrowseData
            // 
            this.ButtonBrowseData.Location = new System.Drawing.Point(585, 21);
            this.ButtonBrowseData.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.ButtonBrowseData.Name = "ButtonBrowseData";
            this.ButtonBrowseData.Size = new System.Drawing.Size(94, 23);
            this.ButtonBrowseData.TabIndex = 1;
            this.ButtonBrowseData.Text = "Browse";
            this.ButtonBrowseData.UseVisualStyleBackColor = true;
            this.ButtonBrowseData.Click += new System.EventHandler(this.ButtonBrowseData_Click);
            // 
            // ButtonHide
            // 
            this.ButtonHide.Location = new System.Drawing.Point(459, 297);
            this.ButtonHide.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.ButtonHide.Name = "ButtonHide";
            this.ButtonHide.Size = new System.Drawing.Size(108, 26);
            this.ButtonHide.TabIndex = 3;
            this.ButtonHide.Text = "Hide";
            this.ButtonHide.UseVisualStyleBackColor = true;
            this.ButtonHide.Click += new System.EventHandler(this.ButtonHide_Click_1);
            // 
            // ButtonExtract
            // 
            this.ButtonExtract.Location = new System.Drawing.Point(459, 268);
            this.ButtonExtract.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.ButtonExtract.Name = "ButtonExtract";
            this.ButtonExtract.Size = new System.Drawing.Size(108, 26);
            this.ButtonExtract.TabIndex = 3;
            this.ButtonExtract.Text = "Extract";
            this.ButtonExtract.UseVisualStyleBackColor = true;
            this.ButtonExtract.Click += new System.EventHandler(this.ButtonExtract_Click);
            // 
            // LabelPercent
            // 
            this.LabelPercent.AutoSize = true;
            this.LabelPercent.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.LabelPercent.Location = new System.Drawing.Point(662, 334);
            this.LabelPercent.Name = "LabelPercent";
            this.LabelPercent.Size = new System.Drawing.Size(29, 20);
            this.LabelPercent.TabIndex = 4;
            this.LabelPercent.Text = "0%";
            this.LabelPercent.Visible = false;
            // 
            // ComboBoxHighLevel
            // 
            this.ComboBoxHighLevel.DisplayMember = "1, 2, 3, 4";
            this.ComboBoxHighLevel.FormattingEnabled = true;
            this.ComboBoxHighLevel.Items.AddRange(new object[] {
            "1",
            "2",
            "3",
            "4"});
            this.ComboBoxHighLevel.Location = new System.Drawing.Point(575, 297);
            this.ComboBoxHighLevel.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.ComboBoxHighLevel.Name = "ComboBoxHighLevel";
            this.ComboBoxHighLevel.Size = new System.Drawing.Size(124, 24);
            this.ComboBoxHighLevel.TabIndex = 5;
            this.ComboBoxHighLevel.Text = "1";
            this.ComboBoxHighLevel.ValueMember = "1, 2, 3, 4";
            this.ComboBoxHighLevel.SelectedIndexChanged += new System.EventHandler(this.ComboBoxHighLevel_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.label2.Location = new System.Drawing.Point(573, 273);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(124, 20);
            this.label2.TabIndex = 4;
            this.label2.Text = "High level (in bit)";
            // 
            // GroupBoxKey
            // 
            this.GroupBoxKey.Controls.Add(this.RichTextBoxKey);
            this.GroupBoxKey.Location = new System.Drawing.Point(18, 250);
            this.GroupBoxKey.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.GroupBoxKey.Name = "GroupBoxKey";
            this.GroupBoxKey.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.GroupBoxKey.Size = new System.Drawing.Size(435, 72);
            this.GroupBoxKey.TabIndex = 6;
            this.GroupBoxKey.TabStop = false;
            this.GroupBoxKey.Text = "Key";
            // 
            // RichTextBoxKey
            // 
            this.RichTextBoxKey.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.RichTextBoxKey.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RichTextBoxKey.Location = new System.Drawing.Point(11, 25);
            this.RichTextBoxKey.Name = "RichTextBoxKey";
            this.RichTextBoxKey.PasswordChar = '*';
            this.RichTextBoxKey.Size = new System.Drawing.Size(418, 21);
            this.RichTextBoxKey.TabIndex = 1;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(61, 4);
            // 
            // Hider
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(712, 572);
            this.Controls.Add(this.GroupBoxKey);
            this.Controls.Add(this.ComboBoxHighLevel);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.LabelPercent);
            this.Controls.Add(this.ButtonExtract);
            this.Controls.Add(this.ButtonHide);
            this.Controls.Add(this.GroupBoxData);
            this.Controls.Add(this.groupBoxInputFile);
            this.Controls.Add(this.ProgessBarPercent);
            this.Controls.Add(this.GroupBoxLog);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.MaximizeBox = false;
            this.Name = "Hider";
            this.Text = "Hider";
            this.GroupBoxLog.ResumeLayout(false);
            this.groupBoxInputFile.ResumeLayout(false);
            this.groupBoxInputFile.PerformLayout();
            this.GroupBoxData.ResumeLayout(false);
            this.GroupBoxData.PerformLayout();
            this.GroupBoxKey.ResumeLayout(false);
            this.GroupBoxKey.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private GroupBox GroupBoxLog;
        private ProgressBar ProgessBarPercent;
        private GroupBox groupBoxInputFile;
        private Button ButtonBrowseInput;
        private TextBox TextBoxInput;
        private GroupBox GroupBoxData;
        private RichTextBox RichTextBoxData;
        private Button ButtonBrowseData;
        private Button ButtonHide;
        private Button ButtonExtract;
        private Label LabelPercent;
        private ComboBox ComboBoxHighLevel;
        private Label label2;
        private RichTextBox RichTextBoxLog;
        private GroupBox GroupBoxKey;
        private RadioButton radioButton3;
        private RadioButton radioButton2;
        private Button ButtonSave;
        private TextBox RichTextBoxKey;
        private ContextMenuStrip contextMenuStrip1;
    }
}