using BookManagementSystem.BusinessLogicLayer.Services;
using BookManagementSystem.DataAccessLayer.Configuration;
using BookManagementSystem.DataAccessLayer.Contexts;
using BookManagementSystem.DataAccessLayer.Repositories;
using BookManagementSystem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WinFormsApp
{
    public partial class Form1 : Form
    {
        private readonly BookDbContext _context;
        private readonly BookService _service;

        public Form1()
        {
            InitializeComponent();

            var connectionString = SqliteConnectionProvider.GetDefaultConnectionString();
            _context = BookDbContextFactory.Create(connectionString);
            _service = new BookService(new EfBookRepository(_context));

            Activated += Form1_Activated;
            comboBoxGenre.Items.AddRange(new[]
            {
                "Фантастика",
                "Роман",
                "Научпоп",
                "Деловая литература",
                "Пьеса",
                "Исторический роман",
                "Детектив",
                "Поэзия"
            });

            LoadBooks();
        }

        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            base.OnFormClosed(e);
            _context.Dispose();
        }

        private void LoadBooks()
        {
            listBoxBooks.Items.Clear();
            foreach (var book in _service.GetAllBooks())
            {
                listBoxBooks.Items.Add(book);
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            var title = textBoxTitle.Text;
            var author = textBoxAuthor.Text;
            var genre = comboBoxGenre.SelectedItem?.ToString();

            if (!int.TryParse(textBoxYear.Text, out var year))
            {
                MessageBox.Show("Год должен быть числом.");
                return;
            }

            if (string.IsNullOrWhiteSpace(title) ||
                string.IsNullOrWhiteSpace(author) ||
                string.IsNullOrWhiteSpace(genre))
            {
                MessageBox.Show("Все поля обязательны.");
                return;
            }

            _service.CreateBook(title, author, year, genre);
            LoadBooks();
            ClearFields();
            MessageBox.Show("Книга добавлена.");
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            var id = ReadSelectedBookId();
            if (id is null)
            {
                MessageBox.Show("Выберите книгу для удаления.");
                return;
            }

            if (_service.DeleteBook(id.Value))
            {
                LoadBooks();
                ClearFields();
                MessageBox.Show("Книга удалена.");
            }
            else
            {
                MessageBox.Show("Не удалось удалить книгу.");
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            var id = ReadSelectedBookId();
            if (id is null)
            {
                MessageBox.Show("Выберите книгу для редактирования.");
                return;
            }

            var book = _service.GetBookById(id.Value);
            if (book is null)
            {
                MessageBox.Show("Книга не найдена.");
                return;
            }

            textBoxId.Text = book.ID.ToString();
            textBoxTitle.Text = book.Title;
            textBoxAuthor.Text = book.Author;
            textBoxYear.Text = book.Year.ToString();
            comboBoxGenre.SelectedItem = book.Genre;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!int.TryParse(textBoxId.Text, out var id))
            {
                MessageBox.Show("Выберите книгу для сохранения.");
                return;
            }

            var title = textBoxTitle.Text;
            var author = textBoxAuthor.Text;
            var genre = comboBoxGenre.SelectedItem?.ToString();

            if (!int.TryParse(textBoxYear.Text, out var year))
            {
                MessageBox.Show("Год должен быть числом.");
                return;
            }

            if (string.IsNullOrWhiteSpace(title) ||
                string.IsNullOrWhiteSpace(author) ||
                string.IsNullOrWhiteSpace(genre))
            {
                MessageBox.Show("Все поля обязательны.");
                return;
            }

            if (_service.UpdateBook(id, title, author, year, genre))
            {
                LoadBooks();
                ClearFields();
                MessageBox.Show("Книга обновлена.");
            }
            else
            {
                MessageBox.Show("Не удалось обновить запись.");
            }
        }

        private void btnGroup_Click(object sender, EventArgs e)
        {
            var groups = _service.GroupBooksByGenre();
            if (!groups.Any())
            {
                MessageBox.Show("Книги отсутствуют.");
                return;
            }

            var result = new StringBuilder();
            foreach (var group in groups)
            {
                result.AppendLine($"Жанр: {group.Key}");
                foreach (var book in group.Value)
                {
                    result.AppendLine($"  {book}");
                }

                result.AppendLine();
            }

            MessageBox.Show(result.ToString(), "Группировка по жанрам");
        }

        private void btnFind_Click(object sender, EventArgs e)
        {
            var author = textBoxSearchAuthor.Text;
            if (string.IsNullOrWhiteSpace(author))
            {
                MessageBox.Show("Укажите автора.");
                return;
            }

            var books = _service.FindBooksByAuthor(author);
            ShowBooksDialog(books, "Поиск по автору");
        }

        private void btnFindTitle_Click(object sender, EventArgs e)
        {
            var title = textBoxSearchTitle.Text;
            if (string.IsNullOrWhiteSpace(title))
            {
                MessageBox.Show("Укажите часть названия.");
                return;
            }

            var books = _service.FindBooksByTitle(title);
            ShowBooksDialog(books, "Поиск по названию");
        }

        private void btnFindTitle_Click_1(object sender, EventArgs e)
        {
            btnFindTitle_Click(sender, e);
        }

        private void Form1_Activated(object? sender, EventArgs e)
        {
            LoadBooks();
        }

        private void ClearFields()
        {
            textBoxId.Clear();
            textBoxTitle.Clear();
            textBoxAuthor.Clear();
            textBoxYear.Clear();
            comboBoxGenre.SelectedIndex = -1;
            textBoxSearchAuthor.Clear();
            textBoxSearchTitle.Clear();
        }

        private int? ReadSelectedBookId()
        {
            if (listBoxBooks.SelectedItem is Book book)
            {
                return book.ID;
            }

            if (listBoxBooks.SelectedItem is string text)
            {
                var parts = text.Split(',');
                if (parts.Length > 0)
                {
                    var idPart = parts[0].Split(':').LastOrDefault();
                    if (int.TryParse(idPart?.Trim(), out var parsed))
                    {
                        return parsed;
                    }
                }
            }

            return null;
        }

        private void ShowBooksDialog(IEnumerable<Book> books, string caption)
        {
            var list = books.ToList();
            if (!list.Any())
            {
                MessageBox.Show("Книги не найдены.");
                return;
            }

            var result = string.Join(Environment.NewLine, list.Select(b => b.ToString()));
            MessageBox.Show(result, caption);
        }

        private void listBoxBooks_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void textBoxSearchTitle_TextChanged(object sender, EventArgs e)
        {
        }
    }
}
