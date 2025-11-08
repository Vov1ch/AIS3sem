namespace WinFormsApp
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
            listBoxBooks = new ListBox();
            label1 = new Label();
            label2 = new Label();
            textBoxTitle = new TextBox();
            label3 = new Label();
            textBoxAuthor = new TextBox();
            label4 = new Label();
            textBoxYear = new TextBox();
            label5 = new Label();
            comboBoxGenre = new ComboBox();
            btnAdd = new Button();
            btnDelete = new Button();
            btnUpdate = new Button();
            btnSave = new Button();
            btnGroup = new Button();
            btnFind = new Button();
            label6 = new Label();
            textBoxSearchAuthor = new TextBox();
            label7 = new Label();
            textBoxId = new TextBox();
            textBoxSearchTitle = new TextBox();
            label8 = new Label();
            btnFindTitle = new Button();
            SuspendLayout();
            // 
            // listBoxBooks
            // 
            listBoxBooks.FormattingEnabled = true;
            listBoxBooks.ItemHeight = 15;
            listBoxBooks.Location = new Point(10, 32);
            listBoxBooks.Name = "listBoxBooks";
            listBoxBooks.Size = new Size(680, 229);
            listBoxBooks.TabIndex = 0;
            listBoxBooks.SelectedIndexChanged += listBoxBooks_SelectedIndexChanged;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Microsoft Sans Serif", 10F, FontStyle.Bold, GraphicsUnit.Point, 204);
            label1.Location = new Point(10, 8);
            label1.Name = "label1";
            label1.Size = new Size(103, 17);
            label1.TabIndex = 1;
            label1.Text = "Список книг:";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(10, 277);
            label2.Name = "label2";
            label2.Size = new Size(62, 15);
            label2.TabIndex = 2;
            label2.Text = "Название:";
            // 
            // textBoxTitle
            // 
            textBoxTitle.Location = new Point(82, 274);
            textBoxTitle.Name = "textBoxTitle";
            textBoxTitle.Size = new Size(176, 23);
            textBoxTitle.TabIndex = 3;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(271, 277);
            label3.Name = "label3";
            label3.Size = new Size(43, 15);
            label3.TabIndex = 4;
            label3.Text = "Автор:";
            // 
            // textBoxAuthor
            // 
            textBoxAuthor.Location = new Point(319, 274);
            textBoxAuthor.Name = "textBoxAuthor";
            textBoxAuthor.Size = new Size(176, 23);
            textBoxAuthor.TabIndex = 5;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(508, 277);
            label4.Name = "label4";
            label4.Size = new Size(29, 15);
            label4.TabIndex = 6;
            label4.Text = "Год:";
            // 
            // textBoxYear
            // 
            textBoxYear.Location = new Point(542, 274);
            textBoxYear.Name = "textBoxYear";
            textBoxYear.Size = new Size(88, 23);
            textBoxYear.TabIndex = 7;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(10, 309);
            label5.Name = "label5";
            label5.Size = new Size(41, 15);
            label5.TabIndex = 8;
            label5.Text = "Жанр:";
            // 
            // comboBoxGenre
            // 
            comboBoxGenre.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBoxGenre.FormattingEnabled = true;
            comboBoxGenre.Location = new Point(82, 307);
            comboBoxGenre.Name = "comboBoxGenre";
            comboBoxGenre.Size = new Size(176, 23);
            comboBoxGenre.TabIndex = 9;
            // 
            // btnAdd
            // 
            btnAdd.Location = new Point(13, 347);
            btnAdd.Name = "btnAdd";
            btnAdd.Size = new Size(105, 33);
            btnAdd.TabIndex = 10;
            btnAdd.Text = "Добавить";
            btnAdd.UseVisualStyleBackColor = true;
            btnAdd.Click += btnAdd_Click;
            // 
            // btnDelete
            // 
            btnDelete.Location = new Point(131, 347);
            btnDelete.Name = "btnDelete";
            btnDelete.Size = new Size(105, 33);
            btnDelete.TabIndex = 11;
            btnDelete.Text = "Удалить";
            btnDelete.UseVisualStyleBackColor = true;
            btnDelete.Click += btnDelete_Click;
            // 
            // btnUpdate
            // 
            btnUpdate.Location = new Point(249, 347);
            btnUpdate.Name = "btnUpdate";
            btnUpdate.Size = new Size(105, 33);
            btnUpdate.TabIndex = 12;
            btnUpdate.Text = "Редактировать";
            btnUpdate.UseVisualStyleBackColor = true;
            btnUpdate.Click += btnUpdate_Click;
            // 
            // btnSave
            // 
            btnSave.Location = new Point(368, 347);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(105, 33);
            btnSave.TabIndex = 13;
            btnSave.Text = "Сохранить";
            btnSave.UseVisualStyleBackColor = true;
            btnSave.Click += btnSave_Click;
            // 
            // btnGroup
            // 
            btnGroup.Location = new Point(486, 347);
            btnGroup.Name = "btnGroup";
            btnGroup.Size = new Size(204, 33);
            btnGroup.TabIndex = 14;
            btnGroup.Text = "Группировка по жанрам";
            btnGroup.UseVisualStyleBackColor = true;
            btnGroup.Click += btnGroup_Click;
            // 
            // btnFind
            // 
            btnFind.Location = new Point(564, 394);
            btnFind.Name = "btnFind";
            btnFind.Size = new Size(125, 33);
            btnFind.TabIndex = 15;
            btnFind.Text = "Найти по автору";
            btnFind.UseVisualStyleBackColor = true;
            btnFind.Click += btnFind_Click;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(10, 403);
            label6.Name = "label6";
            label6.Size = new Size(102, 15);
            label6.TabIndex = 16;
            label6.Text = "Поиск по автору:";
            // 
            // textBoxSearchAuthor
            // 
            textBoxSearchAuthor.Location = new Point(135, 400);
            textBoxSearchAuthor.Name = "textBoxSearchAuthor";
            textBoxSearchAuthor.Size = new Size(176, 23);
            textBoxSearchAuthor.TabIndex = 17;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(643, 277);
            label7.Name = "label7";
            label7.Size = new Size(21, 15);
            label7.TabIndex = 18;
            label7.Text = "ID:";
            // 
            // textBoxId
            // 
            textBoxId.Enabled = false;
            textBoxId.Location = new Point(668, 274);
            textBoxId.Name = "textBoxId";
            textBoxId.Size = new Size(22, 23);
            textBoxId.TabIndex = 19;
            // 
            // textBoxSearchTitle
            // 
            textBoxSearchTitle.Location = new Point(398, 312);
            textBoxSearchTitle.Name = "textBoxSearchTitle";
            textBoxSearchTitle.Size = new Size(100, 23);
            textBoxSearchTitle.TabIndex = 20;
            textBoxSearchTitle.TextChanged += textBoxSearchTitle_TextChanged;
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new Point(273, 315);
            label8.Name = "label8";
            label8.Size = new Size(119, 15);
            label8.TabIndex = 21;
            label8.Text = "Поиск по названию:";
            // 
            // btnFindTitle
            // 
            btnFindTitle.Location = new Point(522, 312);
            btnFindTitle.Name = "btnFindTitle";
            btnFindTitle.Size = new Size(108, 23);
            btnFindTitle.TabIndex = 22;
            btnFindTitle.Text = "Найти";
            btnFindTitle.UseVisualStyleBackColor = true;
            btnFindTitle.Click += btnFindTitle_Click_1;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(700, 438);
            Controls.Add(btnFindTitle);
            Controls.Add(label8);
            Controls.Add(textBoxSearchTitle);
            Controls.Add(textBoxId);
            Controls.Add(label7);
            Controls.Add(textBoxSearchAuthor);
            Controls.Add(label6);
            Controls.Add(btnFind);
            Controls.Add(btnGroup);
            Controls.Add(btnSave);
            Controls.Add(btnUpdate);
            Controls.Add(btnDelete);
            Controls.Add(btnAdd);
            Controls.Add(comboBoxGenre);
            Controls.Add(label5);
            Controls.Add(textBoxYear);
            Controls.Add(label4);
            Controls.Add(textBoxAuthor);
            Controls.Add(label3);
            Controls.Add(textBoxTitle);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(listBoxBooks);
            Name = "Form1";
            Text = "Управление книгами";
            ResumeLayout(false);
            PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox listBoxBooks;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBoxTitle;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBoxAuthor;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textBoxYear;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox comboBoxGenre;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnUpdate;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnGroup;
        private System.Windows.Forms.Button btnFind;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox textBoxSearchAuthor;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox textBoxId;
        private TextBox textBoxSearchTitle;
        private Label label8;
        private Button btnFindTitle;
    }
}