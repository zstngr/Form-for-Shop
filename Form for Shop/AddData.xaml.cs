using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Form_for_Shop
{
    /// <summary>
    /// Логика взаимодействия для AddData.xaml
    /// </summary>
    public partial class AddData : Window
    {
        public string MovieTitle;
        public string Director;
        public string MovieStartDate;
        public int Price; 

        public AddData()
        {
            InitializeComponent();
        }
        public void AddData_Load(object sender, EventArgs e)
        {
            MovieTitle = "MovieTitle";
            Director = "Director";
            MovieStartDate = "01:01:2000";
            Price = 0;

            TitleBox.Text = MovieTitle;
            DirectorBox.Text = Director;
            MovieStartDateBox.Text = MovieStartDate;
            PriceBox.Text = Price.ToString();
        }
        public void Add_Click(object sender, EventArgs e)
        {
            MovieTitle = TitleBox.Text;
            Director = DirectorBox.Text;
            MovieStartDate = MovieStartDateBox.Text;
            Price = Convert.ToInt32(PriceBox.Text);
            DialogResult = true;
        }
        public void Cancel_Click(object sender, EventArgs e)
        {

        }
    }
}
