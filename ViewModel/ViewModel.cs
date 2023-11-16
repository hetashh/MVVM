using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Collections.ObjectModel;
using WpfApp1.Model;
using System.Windows.Data;
using System.Windows;
using static System.Reflection.Metadata.BlobBuilder;
using System;

namespace WpfApp1.ViewModel
{
    internal class ApplicationViewModel : INotifyPropertyChanged
    {
        private Book selectedBook;
        private User selectedUser;
        private Loan selectedLoan;
        public ObservableCollection<Book> books { get; set; }
        public ObservableCollection<User> users { get; set; }
        public ObservableCollection<Loan> loans { get; set; }
        private RelayCommand issueBook;
        public RelayCommand IssueBook
        {
            get
            {
                return issueBook ??
                  (issueBook = new RelayCommand(obj =>
                  {
                      if (selectedUser != null && selectedBook != null && selectedBook.Count > 0)
                      {
                          Loan newLoan = new Loan(selectedUser, selectedBook);
                          loans.Add(newLoan);

                          selectedBook.Count--;

                          CollectionViewSource.GetDefaultView(books).Refresh();
                      }
                      else
                      {
                          MessageBox.Show("Выберите пользователя и доступную книгу для выдачи.");
                      }
                  }));
            }
        }
        private RelayCommand returnBook;
        public RelayCommand ReturnBook
        {
            get
            {
                return returnBook ??
                  (returnBook = new RelayCommand(obj =>
                  {
                      {
                          if (selectedLoan != null)
                          {
                              selectedLoan.Book.Count++;

                              loans.Remove(selectedLoan);

                              CollectionViewSource.GetDefaultView(books).Refresh();
                          }
                          else
                          {
                              MessageBox.Show("Выберите книгу для возврата.");
                          }
                      }
                  }));
            }
        }
        //private string name1;
        //public string Name1 { 
        //    get { return name1; } 
        //    set { name1 = value; OnPropertyChanged(Name1); }
        //}
        //private string family1;
        //public string Family1
        //{
        //    get { return family1; }
        //    set { family1 = value; OnPropertyChanged(Family1); }
        //}
        

        public Book SelectedBook
        {
            get { return selectedBook; }
            set
            {
                selectedBook = value;
                OnPropertyChanged("SelectedBook");
            }
        }

        public User SelectedUser
        {
            get { return selectedUser; }
            set
            {
                selectedUser = value;
                OnPropertyChanged("SelectedUser");
            }
        }

        public Loan SelectedLoan
        {
            get { return selectedLoan; }
            set
            {
                selectedLoan = value;
                OnPropertyChanged("SelectedLoan");
            }
        }
        public ApplicationViewModel()
        {
            users = new() {
                new User(1, "Карим", "Агано"),
                new User(2, "Иван", "Костенко"),
                new User(3, "Ксения", "Иванова"),
                new User(4, "Валерия", "Романова"),
                new User(5, "Настя", "Красных"),
                new User(6, "Кристина", "Беру"),
                new User(7, "Юля", "Кравец"),
            };
            books = new() { 
                new Book("1984", "Джордж Оруэлл", DateTime.Now, 4),
                new Book("Мартин Иден", "Джек Лондон", DateTime.Now, 2),
                new Book("Мастер и Маргарита", "Михаил Булгкаов", DateTime.Now, 8),
                new Book("Отцы и дети", "Иван Тургенев", DateTime.Now, 22),
                new Book("Мастер и Маргарита", "Михаил Булгаков", DateTime.Now, 5),
                new Book("Маленький принц", "Антуан де Сент-Экзюпери", DateTime.Now, 1),
                new Book("Преступление и наказание", "Федор Достоевский", DateTime.Now, 4),
                new Book("Маленький принц", "Антуан де Сент-Экзюпери", DateTime.Now, 3),
                new Book("Собачье сердце", "Михаил Булгаков", DateTime.Now, 6),
        };
            loans = new() {
                new Loan (users[0], books[0])
            };
        }
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}