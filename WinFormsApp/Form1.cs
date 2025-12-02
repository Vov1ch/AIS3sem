using BookManagementSystem.Domain.Entities;
using BookManagementSystem.PresentationLayer;
using BookManagementSystem.Shared;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace WinFormsApp
{
    /// <summary>
    /// Реализация View для работы со списком книг.
    /// </summary>
    public partial class Form1 : Form, IBookView
    {
        private BookController _controller = null!;
        private readonly List<string> _currentGenres = new();
        private readonly OutsideClickMessageFilter _outsideClickFilter;
        private readonly ComboBox? _genreFilterComboBox;

        private bool _suppressSelectionChange;
        private bool _suppressFilterEvents;
        private bool _suppressGenreFilterUpdates;

        private const string AllGenresOption = "Все жанры";

        public Form1()
        {
            InitializeComponent();
            _outsideClickFilter = new OutsideClickMessageFilter(this);
            Application.AddMessageFilter(_outsideClickFilter);
            _genreFilterComboBox = comboBoxGenreFilter;
            ConfigureGrid();
            UpdateGenresDisplay();
        }

        public void SetController(BookController controller)
        {
            _controller = controller;
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            _controller?.Initialize();
        }

        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            base.OnFormClosed(e);
            Application.RemoveMessageFilter(_outsideClickFilter);
        }

        /// <summary>
        /// Фильтр сообщений, скрывающий подсказки при клике вне контролов.
        /// </summary>
        private sealed class OutsideClickMessageFilter : IMessageFilter
        {
            private readonly Form1 _owner;

            public OutsideClickMessageFilter(Form1 owner)
            {
                _owner = owner;
            }

            public bool PreFilterMessage(ref Message m)
            {
                const int WM_LBUTTONDOWN = 0x0201;
                const int WM_RBUTTONDOWN = 0x0204;
                const int WM_MBUTTONDOWN = 0x0207;

                if (m.Msg == WM_LBUTTONDOWN || m.Msg == WM_RBUTTONDOWN || m.Msg == WM_MBUTTONDOWN)
                {
                    var clickedControl = Control.FromHandle(m.HWnd);

                    if (_owner.listBoxTitleSuggestions.Visible &&
                        !IsWithinAllowedControls(clickedControl, _owner.listBoxTitleSuggestions, _owner.textBoxSearchTitle))
                    {
                        _owner.HideTitleSuggestions();
                    }

                    if (_owner.listBoxAuthorSuggestions.Visible &&
                        !IsWithinAllowedControls(clickedControl, _owner.listBoxAuthorSuggestions, _owner.textBoxSearchAuthor))
                    {
                        _owner.HideAuthorSuggestions();
                    }
                }

                return false;
            }

            private static bool IsWithinAllowedControls(Control? control, params Control?[] roots)
            {
                if (control is null)
                {
                    return false;
                }

                foreach (var root in roots)
                {
                    if (IsDescendant(control, root))
                    {
                        return true;
                    }
                }

                return false;
            }

            private static bool IsDescendant(Control? candidate, Control? root)
            {
                while (candidate is not null)
                {
                    if (candidate == root)
                    {
                        return true;
                    }

                    candidate = candidate.Parent;
                }

                return false;
            }
        }

        /// <summary>
        /// Настройка таблицы для отображения книг.
        /// </summary>
        private void ConfigureGrid()
        {
            dataGridViewBooks.AutoGenerateColumns = false;
            dataGridViewBooks.AllowUserToAddRows = false;
            dataGridViewBooks.AllowUserToDeleteRows = false;
            dataGridViewBooks.MultiSelect = false;
            dataGridViewBooks.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridViewBooks.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            dataGridViewBooks.Columns.Clear();
            dataGridViewBooks.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = nameof(Book.ID),
                HeaderText = "ID",
                FillWeight = 15,
                ReadOnly = true
            });
            dataGridViewBooks.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = nameof(Book.Title),
                HeaderText = "Название",
                FillWeight = 35,
                ReadOnly = true
            });
            dataGridViewBooks.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = nameof(Book.Author),
                HeaderText = "Автор",
                FillWeight = 30,
                ReadOnly = true
            });
            dataGridViewBooks.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = nameof(Book.Year),
                HeaderText = "Год",
                FillWeight = 10,
                ReadOnly = true
            });
            dataGridViewBooks.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = nameof(Book.GenresDisplay),
                HeaderText = "Жанры",
                FillWeight = 30,
                ReadOnly = true
            });
        }

        #region IBookView implementation

        public bool TryGetBookInput(out BookInput input)
        {
            input = default!;
            var title = textBoxTitle.Text.Trim();
            var author = textBoxAuthor.Text.Trim();

            if (!int.TryParse(textBoxYear.Text, out var year))
            {
                ShowError("Год указан некорректно.");
                return false;
            }

            if (string.IsNullOrWhiteSpace(title) ||
                string.IsNullOrWhiteSpace(author) ||
                !_currentGenres.Any())
            {
                ShowError("Заполните название, автора и хотя бы один жанр.");
                return false;
            }

            input = new BookInput(title, author, year, _currentGenres.ToList());
            return true;
        }

        public bool TryGetSelectedBookId(out int id)
        {
            if (dataGridViewBooks.CurrentRow?.DataBoundItem is Book book)
            {
                id = book.ID;
                return true;
            }

            if (int.TryParse(textBoxId.Text, out id))
            {
                return true;
            }

            id = default;
            return false;
        }

        public BookFilters GetFilters()
        {
            return new BookFilters(
                textBoxSearchAuthor.Text,
                textBoxSearchTitle.Text,
                GetSelectedGenreFilter());
        }

        public void ShowBooks(IReadOnlyCollection<Book> books)
        {
            _suppressSelectionChange = true;
            dataGridViewBooks.DataSource = null;
            dataGridViewBooks.DataSource = books.ToList();
            dataGridViewBooks.ClearSelection();
            _suppressSelectionChange = false;
        }

        public void ShowBookDetails(Book? book)
        {
            _suppressFilterEvents = true;
            _suppressSelectionChange = true;

            if (book is null)
            {
                textBoxId.Clear();
                textBoxTitle.Clear();
                textBoxAuthor.Clear();
                textBoxYear.Clear();
                SetCurrentGenres(Array.Empty<string>());
            }
            else
            {
                textBoxId.Text = book.ID.ToString();
                textBoxTitle.Text = book.Title;
                textBoxAuthor.Text = book.Author;
                textBoxYear.Text = book.Year.ToString();
                var genres = book.Genres
                    .Select(g => g.Name)
                    .Where(name => !string.IsNullOrWhiteSpace(name))
                    .ToList();
                SetCurrentGenres(genres);
            }

            _suppressSelectionChange = false;
            _suppressFilterEvents = false;
        }

        public void ShowGenres(IReadOnlyCollection<string> genres)
        {
            if (_genreFilterComboBox is null)
            {
                return;
            }

            var selected = GetSelectedGenreFilter();
            _suppressGenreFilterUpdates = true;
            _genreFilterComboBox.Items.Clear();
            _genreFilterComboBox.Items.Add(AllGenresOption);
            foreach (var genre in genres)
            {
                _genreFilterComboBox.Items.Add(genre);
            }

            if (!string.IsNullOrWhiteSpace(selected) && _genreFilterComboBox.Items.Contains(selected))
            {
                _genreFilterComboBox.SelectedItem = selected;
            }
            else
            {
                _genreFilterComboBox.SelectedIndex = 0;
            }

            _suppressGenreFilterUpdates = false;
        }

        public void ShowInfo(string message)
        {
            MessageBox.Show(message, "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        public void ShowError(string message)
        {
            MessageBox.Show(message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        public void ClearInput()
        {
            _suppressFilterEvents = true;
            textBoxId.Clear();
            textBoxTitle.Clear();
            textBoxAuthor.Clear();
            textBoxYear.Clear();
            textBoxSearchAuthor.Clear();
            textBoxSearchTitle.Clear();
            SetCurrentGenres(Array.Empty<string>());
            if (_genreFilterComboBox is not null && _genreFilterComboBox.Items.Count > 0)
            {
                _suppressGenreFilterUpdates = true;
                _genreFilterComboBox.SelectedIndex = 0;
                _suppressGenreFilterUpdates = false;
            }
            HideAuthorSuggestions();
            HideTitleSuggestions();
            dataGridViewBooks.ClearSelection();
            _suppressFilterEvents = false;
        }

        public IReadOnlyCollection<string> RequestGenresSelection(IReadOnlyCollection<string> availableGenres)
        {
            using var dialog = new GenreDialog(_currentGenres, availableGenres);
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                return dialog.Genres
                    .Select(name => name.Trim())
                    .Where(name => !string.IsNullOrWhiteSpace(name))
                    .Distinct(StringComparer.CurrentCultureIgnoreCase)
                    .ToList();
            }

            return _currentGenres.ToList();
        }

        public void SetCurrentGenres(IReadOnlyCollection<string> genres)
        {
            _currentGenres.Clear();
            _currentGenres.AddRange(genres);
            UpdateGenresDisplay();
        }

        public void ShowAuthorSuggestions(IReadOnlyCollection<string> authors)
        {
            if (!authors.Any())
            {
                HideAuthorSuggestions();
                return;
            }

            listBoxAuthorSuggestions.BeginUpdate();
            listBoxAuthorSuggestions.Items.Clear();
            listBoxAuthorSuggestions.Items.AddRange(authors.Cast<object>().ToArray());
            listBoxAuthorSuggestions.EndUpdate();
            listBoxAuthorSuggestions.Visible = true;
        }

        public void HideAuthorSuggestions()
        {
            listBoxAuthorSuggestions.Visible = false;
            listBoxAuthorSuggestions.Items.Clear();
        }

        public void ShowTitleSuggestions(IReadOnlyCollection<string> titles)
        {
            if (!titles.Any())
            {
                HideTitleSuggestions();
                return;
            }

            listBoxTitleSuggestions.BeginUpdate();
            listBoxTitleSuggestions.Items.Clear();
            listBoxTitleSuggestions.Items.AddRange(titles.Cast<object>().ToArray());
            listBoxTitleSuggestions.EndUpdate();
            listBoxTitleSuggestions.Visible = true;
        }

        public void HideTitleSuggestions()
        {
            listBoxTitleSuggestions.Visible = false;
            listBoxTitleSuggestions.Items.Clear();
        }

        #endregion

        /// <summary>
        /// Отрисовывает текущий список жанров.
        /// </summary>
        private void UpdateGenresDisplay()
        {
            labelGenresValue.Text = _currentGenres.Any()
                ? string.Join(", ", _currentGenres)
                : "Жанры не выбраны";
        }

        private string? GetSelectedGenreFilter()
        {
            if (_genreFilterComboBox is null ||
                _genreFilterComboBox.SelectedItem is not string selected ||
                selected == AllGenresOption)
            {
                return null;
            }

            return selected;
        }

        private void btnAdd_Click(object sender, EventArgs e) =>
            _controller.OnAdd();

        private void btnDelete_Click(object sender, EventArgs e) =>
            _controller.OnDelete();

        private void btnSave_Click(object sender, EventArgs e) =>
            _controller.OnUpdate();

        private void btnGroup_Click(object sender, EventArgs e) =>
            _controller.OnGroup();

        private void btnRefresh_Click(object sender, EventArgs e) =>
            _controller.OnRefresh();

        private void btnEditGenres_Click(object sender, EventArgs e) =>
            _controller.OnEditGenres();

        private void comboBoxGenreFilter_SelectedIndexChanged(object? sender, EventArgs e)
        {
            if (_suppressGenreFilterUpdates)
            {
                return;
            }

            _controller.OnFiltersChanged();
        }

        private void dataGridViewBooks_SelectionChanged(object sender, EventArgs e)
        {
            if (_suppressSelectionChange)
            {
                return;
            }

            var book = dataGridViewBooks.CurrentRow?.DataBoundItem as Book;
            _controller.OnBookSelected(book);
        }

        private void textBoxSearchAuthor_TextChanged(object sender, EventArgs e)
        {
            if (_suppressFilterEvents)
            {
                return;
            }

            _controller.OnFiltersChanged();
        }

        private void listBoxAuthorSuggestions_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBoxAuthorSuggestions.SelectedItem is not string author)
            {
                return;
            }

            _suppressFilterEvents = true;
            textBoxSearchAuthor.Text = author;
            textBoxSearchAuthor.SelectionStart = author.Length;
            _suppressFilterEvents = false;
            HideAuthorSuggestions();
            _controller.OnFiltersChanged();
        }

        private void textBoxSearchTitle_TextChanged(object sender, EventArgs e)
        {
            if (_suppressFilterEvents)
            {
                return;
            }

            _controller.OnFiltersChanged();
        }

        private void listBoxTitleSuggestions_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBoxTitleSuggestions.SelectedItem is not string title)
            {
                return;
            }

            _suppressFilterEvents = true;
            textBoxSearchTitle.Text = title;
            textBoxSearchTitle.SelectionStart = title.Length;
            _suppressFilterEvents = false;
            HideTitleSuggestions();
            _controller.OnFiltersChanged();
        }
    }
}
