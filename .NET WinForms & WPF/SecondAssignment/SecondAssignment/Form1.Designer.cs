//Programming using .NET advanced course
//Code Example : Animal Motel
//Haris Kljajic June 2012
namespace AnimalMotel
{
    partial class Form1

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
            this.nameTextbox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.ageTextbox = new System.Windows.Forms.TextBox();
            this.GenderListbox = new System.Windows.Forms.ListBox();
            this.label4 = new System.Windows.Forms.Label();
            this.CategoryListBox = new System.Windows.Forms.ListBox();
            this.AnimalListBox = new System.Windows.Forms.ListBox();
            this.label5 = new System.Windows.Forms.Label();
            this.SpecdjurListbox = new System.Windows.Forms.Label();
            this.AddButton = new System.Windows.Forms.Button();
            this.AddImpInfoTextbox = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.ShowImpInfoTextbox = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.AnimalRgstrListView = new System.Windows.Forms.ListView();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // nameTextbox
            // 
            this.nameTextbox.Font = new System.Drawing.Font("Modern No. 20", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.nameTextbox.ForeColor = System.Drawing.Color.DarkRed;
            this.nameTextbox.Location = new System.Drawing.Point(23, 57);
            this.nameTextbox.Name = "nameTextbox";
            this.nameTextbox.Size = new System.Drawing.Size(100, 21);
            this.nameTextbox.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Modern No. 20", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(20, 39);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(48, 18);
            this.label1.TabIndex = 1;
            this.label1.Text = "Name:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Modern No. 20", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.Black;
            this.label2.Location = new System.Drawing.Point(222, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(381, 34);
            this.label2.TabIndex = 2;
            this.label2.Text = "Apu\'s Animal Application";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Modern No. 20", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.Black;
            this.label3.Location = new System.Drawing.Point(20, 80);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(36, 18);
            this.label3.TabIndex = 3;
            this.label3.Text = "Age:";
            // 
            // ageTextbox
            // 
            this.ageTextbox.Font = new System.Drawing.Font("Modern No. 20", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ageTextbox.ForeColor = System.Drawing.Color.DarkRed;
            this.ageTextbox.Location = new System.Drawing.Point(23, 98);
            this.ageTextbox.Name = "ageTextbox";
            this.ageTextbox.Size = new System.Drawing.Size(45, 21);
            this.ageTextbox.TabIndex = 4;
            // 
            // GenderListbox
            // 
            this.GenderListbox.Font = new System.Drawing.Font("Modern No. 20", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.GenderListbox.ForeColor = System.Drawing.Color.DarkRed;
            this.GenderListbox.FormattingEnabled = true;
            this.GenderListbox.ItemHeight = 15;
            this.GenderListbox.Location = new System.Drawing.Point(420, 57);
            this.GenderListbox.Name = "GenderListbox";
            this.GenderListbox.Size = new System.Drawing.Size(88, 49);
            this.GenderListbox.TabIndex = 5;
            this.GenderListbox.SelectedIndexChanged += new System.EventHandler(this.GenderListbox_SelectedIndexChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Modern No. 20", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.Black;
            this.label4.Location = new System.Drawing.Point(417, 39);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(103, 18);
            this.label4.TabIndex = 6;
            this.label4.Text = "Choose gender:";
            // 
            // CategoryListBox
            // 
            this.CategoryListBox.Font = new System.Drawing.Font("Modern No. 20", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CategoryListBox.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.CategoryListBox.FormattingEnabled = true;
            this.CategoryListBox.ItemHeight = 15;
            this.CategoryListBox.Location = new System.Drawing.Point(155, 57);
            this.CategoryListBox.Name = "CategoryListBox";
            this.CategoryListBox.Size = new System.Drawing.Size(121, 109);
            this.CategoryListBox.TabIndex = 7;
            this.CategoryListBox.SelectedIndexChanged += new System.EventHandler(this.CategoryListBox_SelectedIndexChanged);
            // 
            // AnimalListBox
            // 
            this.AnimalListBox.Font = new System.Drawing.Font("Modern No. 20", 9.75F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.AnimalListBox.ForeColor = System.Drawing.Color.DarkRed;
            this.AnimalListBox.FormattingEnabled = true;
            this.AnimalListBox.ItemHeight = 15;
            this.AnimalListBox.Location = new System.Drawing.Point(291, 57);
            this.AnimalListBox.Name = "AnimalListBox";
            this.AnimalListBox.Size = new System.Drawing.Size(112, 79);
            this.AnimalListBox.TabIndex = 8;
            this.AnimalListBox.SelectedIndexChanged += new System.EventHandler(this.AnimalListBox_SelectedIndexChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Modern No. 20", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.Black;
            this.label5.Location = new System.Drawing.Point(152, 39);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(112, 18);
            this.label5.TabIndex = 9;
            this.label5.Text = "Choose category:";
            // 
            // SpecdjurListbox
            // 
            this.SpecdjurListbox.AutoSize = true;
            this.SpecdjurListbox.Font = new System.Drawing.Font("Modern No. 20", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SpecdjurListbox.ForeColor = System.Drawing.Color.Black;
            this.SpecdjurListbox.Location = new System.Drawing.Point(288, 39);
            this.SpecdjurListbox.Name = "SpecdjurListbox";
            this.SpecdjurListbox.Size = new System.Drawing.Size(106, 18);
            this.SpecdjurListbox.TabIndex = 10;
            this.SpecdjurListbox.Text = "Choose animal:";
            // 
            // AddButton
            // 
            this.AddButton.ForeColor = System.Drawing.Color.Black;
            this.AddButton.Location = new System.Drawing.Point(596, 182);
            this.AddButton.Name = "AddButton";
            this.AddButton.Size = new System.Drawing.Size(85, 30);
            this.AddButton.TabIndex = 11;
            this.AddButton.Text = "Add animal";
            this.AddButton.UseVisualStyleBackColor = true;
            this.AddButton.Click += new System.EventHandler(this.AddButton_Click);
            // 
            // AddImpInfoTextbox
            // 
            this.AddImpInfoTextbox.Font = new System.Drawing.Font("Modern No. 20", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.AddImpInfoTextbox.ForeColor = System.Drawing.Color.DarkRed;
            this.AddImpInfoTextbox.Location = new System.Drawing.Point(555, 44);
            this.AddImpInfoTextbox.Multiline = true;
            this.AddImpInfoTextbox.Name = "AddImpInfoTextbox";
            this.AddImpInfoTextbox.Size = new System.Drawing.Size(164, 132);
            this.AddImpInfoTextbox.TabIndex = 12;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Modern No. 20", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.Color.Black;
            this.label7.Location = new System.Drawing.Point(552, 26);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(110, 18);
            this.label7.TabIndex = 13;
            this.label7.Text = "Important info:";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Modern No. 20", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.ForeColor = System.Drawing.Color.Black;
            this.label12.Location = new System.Drawing.Point(531, 23);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(93, 18);
            this.label12.TabIndex = 20;
            this.label12.Text = "Special Data:";
            // 
            // ShowImpInfoTextbox
            // 
            this.ShowImpInfoTextbox.BackColor = System.Drawing.SystemColors.InactiveBorder;
            this.ShowImpInfoTextbox.Font = new System.Drawing.Font("Modern No. 20", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ShowImpInfoTextbox.ForeColor = System.Drawing.Color.Maroon;
            this.ShowImpInfoTextbox.Location = new System.Drawing.Point(534, 41);
            this.ShowImpInfoTextbox.Multiline = true;
            this.ShowImpInfoTextbox.Name = "ShowImpInfoTextbox";
            this.ShowImpInfoTextbox.Size = new System.Drawing.Size(164, 132);
            this.ShowImpInfoTextbox.TabIndex = 19;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.nameTextbox);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.ageTextbox);
            this.groupBox1.Controls.Add(this.GenderListbox);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.CategoryListBox);
            this.groupBox1.Controls.Add(this.AnimalListBox);
            this.groupBox1.Controls.Add(this.AddImpInfoTextbox);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.AddButton);
            this.groupBox1.Controls.Add(this.SpecdjurListbox);
            this.groupBox1.Font = new System.Drawing.Font("Modern No. 20", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.groupBox1.Location = new System.Drawing.Point(73, 46);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(740, 233);
            this.groupBox1.TabIndex = 21;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Add animal";
            // 
            // AnimalRgstrListView
            // 
            this.AnimalRgstrListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1});
            this.AnimalRgstrListView.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.AnimalRgstrListView.Location = new System.Drawing.Point(68, 41);
            this.AnimalRgstrListView.Name = "AnimalRgstrListView";
            this.AnimalRgstrListView.Size = new System.Drawing.Size(400, 131);
            this.AnimalRgstrListView.TabIndex = 21;
            this.AnimalRgstrListView.UseCompatibleStateImageBehavior = false;
            this.AnimalRgstrListView.SelectedIndexChanged += new System.EventHandler(this.AnimalRgstrListView_SelectedIndexChanged);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.AnimalRgstrListView);
            this.groupBox2.Controls.Add(this.label12);
            this.groupBox2.Controls.Add(this.ShowImpInfoTextbox);
            this.groupBox2.Font = new System.Drawing.Font("Modern No. 20", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.groupBox2.Location = new System.Drawing.Point(73, 285);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(740, 182);
            this.groupBox2.TabIndex = 22;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Animal Spec List";
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "ID                  Name                        Age                       Gender";
            this.columnHeader1.Width = 398;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LightBlue;
            this.ClientSize = new System.Drawing.Size(916, 614);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label2);
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.Name = "Form1";
            this.Text = "Apu\'s Application";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox nameTextbox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox ageTextbox;
        private System.Windows.Forms.ListBox GenderListbox;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ListBox CategoryListBox;
        private System.Windows.Forms.ListBox AnimalListBox;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label SpecdjurListbox;
        private System.Windows.Forms.Button AddButton;
        private System.Windows.Forms.TextBox AddImpInfoTextbox;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox ShowImpInfoTextbox;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.ListView AnimalRgstrListView;
        private System.Windows.Forms.ColumnHeader columnHeader1;
    }
}