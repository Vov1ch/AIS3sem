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
            dataGridViewBooks = new DataGridView();
            label1 = new Label();
            label2 = new Label();
            textBoxTitle = new TextBox();
            label3 = new Label();
            textBoxAuthor = new TextBox();
            label4 = new Label();
            textBoxYear = new TextBox();
            label5 = new Label();
            labelGenresValue = new Label();
            btnEditGenres = new Button();
            btnAdd = new Button();
            btnDelete = new Button();
            btnSave = new Button();
            btnGroup = new Button();
            btnRefresh = new Button();
            label6 = new Label();
            textBoxSearchAuthor = new TextBox();
            listBoxAuthorSuggestions = new ListBox();
            label7 = new Label();
            textBoxId = new TextBox();
            textBoxSearchTitle = new TextBox();
            label8 = new Label();
            listBoxTitleSuggestions = new ListBox();
            ((System.ComponentModel.ISupportInitialize)dataGridViewBooks).BeginInit();
            SuspendLayout();
            // 
            // dataGridViewBooks
            // 
            dataGridViewBooks.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            dataGridViewBooks.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewBooks.Location = new Point(11, 43);
            dataGridViewBooks.Margin = new Padding(3, 4, 3, 4);
            dataGridViewBooks.MultiSelect = false;
            dataGridViewBooks.Name = "dataGridViewBooks";
            dataGridViewBooks.ReadOnly = true;
            dataGridViewBooks.RowHeadersVisible = false;
            dataGridViewBooks.RowHeadersWidth = 51;
            dataGridViewBooks.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridViewBooks.Size = new Size(864, 304);
            dataGridViewBooks.TabIndex = 0;
            dataGridViewBooks.SelectionChanged += dataGridViewBooks_SelectionChanged;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Microsoft Sans Serif", 10F, FontStyle.Bold, GraphicsUnit.Point, 204);
            label1.Location = new Point(11, 11);
            label1.Name = "label1";
            label1.Size = new Size(128, 20);
            label1.TabIndex = 1;
            label1.Text = "Список книг:";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(11, 369);
            label2.Name = "label2";
            label2.Size = new Size(80, 20);
            label2.TabIndex = 2;
            label2.Text = "Название:";
            // 
            // textBoxTitle
            // 
            textBoxTitle.Location = new Point(94, 365);
            textBoxTitle.Margin = new Padding(3, 4, 3, 4);
            textBoxTitle.Name = "textBoxTitle";
            textBoxTitle.Size = new Size(201, 27);
            textBoxTitle.TabIndex = 3;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(310, 369);
            label3.Name = "label3";
            label3.Size = new Size(54, 20);
            label3.TabIndex = 4;
            label3.Text = "Автор:";
            // 
            // textBoxAuthor
            // 
            textBoxAuthor.Location = new Point(365, 365);
            textBoxAuthor.Margin = new Padding(3, 4, 3, 4);
            textBoxAuthor.Name = "textBoxAuthor";
            textBoxAuthor.Size = new Size(201, 27);
            textBoxAuthor.TabIndex = 5;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(581, 369);
            label4.Name = "label4";
            label4.Size = new Size(36, 20);
            label4.TabIndex = 6;
            label4.Text = "Год:";
            // 
            // textBoxYear
            // 
            textBoxYear.Location = new Point(619, 365);
            textBoxYear.Margin = new Padding(3, 4, 3, 4);
            textBoxYear.Name = "textBoxYear";
            textBoxYear.Size = new Size(100, 27);
            textBoxYear.TabIndex = 7;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(11, 416);
            label5.Name = "label5";
            label5.Size = new Size(177, 20);
            label5.TabIndex = 8;
            label5.Text = "Жанры:";
            // 
            // labelGenresValue
            // 
            labelGenresValue.BorderStyle = BorderStyle.FixedSingle;
            labelGenresValue.Location = new Point(190, 412);
            labelGenresValue.Margin = new Padding(3, 4, 3, 4);
            labelGenresValue.Name = "labelGenresValue";
            labelGenresValue.Size = new Size(310, 60);
            labelGenresValue.TabIndex = 9;
            labelGenresValue.Text = "Жанры не выбраны";
            labelGenresValue.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // btnEditGenres
            // 
            btnEditGenres.Location = new Point(515, 406);
            btnEditGenres.Margin = new Padding(3, 4, 3, 4);
            btnEditGenres.Name = "btnEditGenres";
            btnEditGenres.Size = new Size(126, 41);
            btnEditGenres.TabIndex = 10;
            btnEditGenres.Text = "Выбрать жанры";
            btnEditGenres.UseVisualStyleBackColor = true;
            btnEditGenres.Click += btnEditGenres_Click;
            // 
            // btnAdd
            // 
            btnAdd.Location = new Point(11, 463);
            btnAdd.Margin = new Padding(3, 4, 3, 4);
            btnAdd.Name = "btnAdd";
            btnAdd.Size = new Size(120, 44);
            btnAdd.TabIndex = 11;
            btnAdd.Text = "Добавить";
            btnAdd.UseVisualStyleBackColor = true;
            btnAdd.Click += btnAdd_Click;
            // 
            // btnDelete
            // 
            btnDelete.Location = new Point(147, 463);
            btnDelete.Margin = new Padding(3, 4, 3, 4);
            btnDelete.Name = "btnDelete";
            btnDelete.Size = new Size(120, 44);
            btnDelete.TabIndex = 12;
            btnDelete.Text = "Удалить";
            btnDelete.UseVisualStyleBackColor = true;
            btnDelete.Click += btnDelete_Click;
            // 
            // btnSave
            // 
            btnSave.Location = new Point(283, 463);
            btnSave.Margin = new Padding(3, 4, 3, 4);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(120, 44);
            btnSave.TabIndex = 13;
            btnSave.Text = "Сохранить";
            btnSave.UseVisualStyleBackColor = true;
            btnSave.Click += btnSave_Click;
            // 
            // btnGroup
            // 
            btnGroup.Location = new Point(421, 463);
            btnGroup.Margin = new Padding(3, 4, 3, 4);
            btnGroup.Name = "btnGroup";
            btnGroup.Size = new Size(200, 44);
            btnGroup.TabIndex = 14;
            btnGroup.Text = "Сортировка по жанрам";
            btnGroup.UseVisualStyleBackColor = true;
            btnGroup.Click += btnGroup_Click;
            // 
            // btnRefresh
            // 
            btnRefresh.Location = new Point(635, 463);
            btnRefresh.Margin = new Padding(3, 4, 3, 4);
            btnRefresh.Name = "btnRefresh";
            btnRefresh.Size = new Size(140, 44);
            btnRefresh.TabIndex = 15;
            btnRefresh.Text = "Обновить";
            btnRefresh.UseVisualStyleBackColor = true;
            btnRefresh.Click += btnRefresh_Click;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(11, 537);
            label6.Name = "label6";
            label6.Size = new Size(128, 20);
            label6.TabIndex = 16;
            label6.Text = "Поиск по автору:";
            // 
            // textBoxSearchAuthor
            // 
            textBoxSearchAuthor.Location = new Point(154, 533);
            textBoxSearchAuthor.Margin = new Padding(3, 4, 3, 4);
            textBoxSearchAuthor.Name = "textBoxSearchAuthor";
            textBoxSearchAuthor.Size = new Size(201, 27);
            textBoxSearchAuthor.TabIndex = 17;
            textBoxSearchAuthor.TextChanged += textBoxSearchAuthor_TextChanged;
            // 
            // listBoxAuthorSuggestions
            // 
            listBoxAuthorSuggestions.FormattingEnabled = true;
            listBoxAuthorSuggestions.Location = new Point(154, 572);
            listBoxAuthorSuggestions.Margin = new Padding(3, 4, 3, 4);
            listBoxAuthorSuggestions.Name = "listBoxAuthorSuggestions";
            listBoxAuthorSuggestions.Size = new Size(201, 104);
            listBoxAuthorSuggestions.TabIndex = 18;
            listBoxAuthorSuggestions.Visible = false;
            listBoxAuthorSuggestions.SelectedIndexChanged += listBoxAuthorSuggestions_SelectedIndexChanged;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(735, 369);
            label7.Name = "label7";
            label7.Size = new Size(27, 20);
            label7.TabIndex = 19;
            label7.Text = "ID:";
            // 
            // textBoxId
            // 
            textBoxId.Enabled = false;
            textBoxId.Location = new Point(769, 365);
            textBoxId.Margin = new Padding(3, 4, 3, 4);
            textBoxId.Name = "textBoxId";
            textBoxId.Size = new Size(50, 27);
            textBoxId.TabIndex = 20;
            // 
            // textBoxSearchTitle
            // 
            textBoxSearchTitle.Location = new Point(541, 540);
            textBoxSearchTitle.Margin = new Padding(3, 4, 3, 4);
            textBoxSearchTitle.Name = "textBoxSearchTitle";
            textBoxSearchTitle.Size = new Size(114, 27);
            textBoxSearchTitle.TabIndex = 21;
            textBoxSearchTitle.TextChanged += textBoxSearchTitle_TextChanged;
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new Point(383, 540);
            label8.Name = "label8";
            label8.Size = new Size(151, 20);
            label8.TabIndex = 22;
            label8.Text = "Поиск по названию:";
            // 
            // listBoxTitleSuggestions
            // 
            listBoxTitleSuggestions.FormattingEnabled = true;
            listBoxTitleSuggestions.Location = new Point(455, 448);
            listBoxTitleSuggestions.Name = "listBoxTitleSuggestions";
            listBoxTitleSuggestions.Size = new Size(265, 84);
            listBoxTitleSuggestions.TabIndex = 24;
            listBoxTitleSuggestions.Visible = false;
            listBoxTitleSuggestions.SelectedIndexChanged += listBoxTitleSuggestions_SelectedIndexChanged;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(900, 707);
            Controls.Add(listBoxTitleSuggestions);
            Controls.Add(label8);
            Controls.Add(textBoxSearchTitle);
            Controls.Add(textBoxId);
            Controls.Add(label7);
            Controls.Add(listBoxAuthorSuggestions);
            Controls.Add(textBoxSearchAuthor);
            Controls.Add(label6);
            Controls.Add(btnRefresh);
            Controls.Add(btnGroup);
            Controls.Add(btnSave);
            Controls.Add(btnDelete);
            Controls.Add(btnAdd);
            Controls.Add(btnEditGenres);
            Controls.Add(labelGenresValue);
            Controls.Add(label5);
            Controls.Add(textBoxYear);
            Controls.Add(label4);
            Controls.Add(textBoxAuthor);
            Controls.Add(label3);
            Controls.Add(textBoxTitle);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(dataGridViewBooks);
            Margin = new Padding(3, 4, 3, 4);
            Name = "Form1";
            Text = "Управление книгами";
            ((System.ComponentModel.ISupportInitialize)dataGridViewBooks).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridViewBooks;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBoxTitle;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBoxAuthor;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textBoxYear;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label labelGenresValue;
        private System.Windows.Forms.Button btnEditGenres;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnGroup;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox textBoxSearchAuthor;
        private System.Windows.Forms.ListBox listBoxAuthorSuggestions;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox textBoxId;
        private System.Windows.Forms.TextBox textBoxSearchTitle;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ListBox listBoxTitleSuggestions;
    }
}
