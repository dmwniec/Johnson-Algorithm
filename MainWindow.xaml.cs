using Microsoft.Win32;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Dzonson
{
    /// <summary>
    /// Logika interakcji dla klasy MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            
            InitializeComponent();
            Count_Textbox.Text = "0";
        }
   
        private void Load(object sender, RoutedEventArgs e)
        {
            int count = Convert.ToInt32(Count_Textbox.Text);
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
            dlg.FileName = "Document";
            dlg.DefaultExt = ".txt";
            dlg.Filter = "Text documents (.txt)|*.txt";



            Nullable<bool> result = dlg.ShowDialog();
            string filename = "";

            if (result == true)
            {

                filename = dlg.FileName;
            }
            Matrix matrix = Matrix.LoadFromFile(count, filename);
            DataTable datatable = Matrix.MatrixToDataTable(matrix);
            MainGrid.ItemsSource = datatable.DefaultView;
        }

        private void Create(object sender, RoutedEventArgs e)
        {
            int n = Convert.ToInt32(Count_Textbox.Text);
            Matrix matrix = new Matrix(n);
            DataTable datatable = Matrix.MatrixToDataTable(matrix);
            MainGrid.ItemsSource = datatable.DefaultView;
        }

        private void Save(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();

            saveFileDialog1.Filter = "txt files (*.txt)|*.txt";
            saveFileDialog1.FilterIndex = 2;
            saveFileDialog1.RestoreDirectory = true;
            saveFileDialog1.ShowDialog();
            if (saveFileDialog1.FileName != "")
            {
                DataTable datatable = ((DataView)MainGrid.ItemsSource).ToTable();
                Matrix matrix = Matrix.DatatableToMatrix(datatable);
                Matrix.PrintToFile(matrix, saveFileDialog1.FileName);
            }
        }

        private void Generate(object sender, RoutedEventArgs e)
        {
            int n = Convert.ToInt32(Count_Textbox.Text);
            Matrix matrix = Matrix.GenerateRandomMatrix(n);
            DataTable datatable = Matrix.MatrixToDataTable(matrix);
            MainGrid.ItemsSource = datatable.DefaultView;
        }

        private void Solute(object sender, RoutedEventArgs e)
        {
            DataTable datatable = ((DataView)MainGrid.ItemsSource).ToTable();
            Matrix matrix = Matrix.DatatableToMatrix(datatable);
            int[] JobSequence = Solution.Johnson(matrix);
            string Jobs = "Kolejność =";
            foreach (int index in JobSequence)
            {
                Jobs += (index+1) + "-";
            }
            int length = Jobs.Length;
            Jobs = Jobs.Remove(length - 1);
            
            int Time = Solution.TimeCalc(matrix, JobSequence);
            JobsLabel.Content = Jobs +  "\n"+" Czas = " + Time;
             
        }
    }
}
