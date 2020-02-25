using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Linq;
using System.Data;

namespace Dzonson
{
    class Matrix
    {
        //Ta klasa jest odpowiedzialna za działania na macierzach tworzenie/drukowanie/zapisywanie/wczytywanie

        public int[,] Value { get; }
        public Matrix(int n) { Value = new int[n, 2]; }
        public Matrix(int[,] a) { Value = a; }

        public static Matrix DatatableToMatrix(DataTable datatable)
        {
            int n = datatable.Columns.Count;
           
            datatable.Rows[0][0] = "0";
            datatable.Rows[1][0] = "0";
            
            Matrix matrix = new Matrix(n-1);
            for (int i = 0; i < n-1; i++)
            {
                
                    matrix.Value[i, 0] = Convert.ToInt32(datatable.Rows[0][i+1]);
                matrix.Value[i, 1] = Convert.ToInt32(datatable.Rows[1][i+1]);

         

            }
            return matrix;

        }

        public static DataTable MatrixToDataTable(Matrix matrix)
        {
            DataTable datatable = new DataTable();
            datatable.Columns.Add("Zadania");
            
            int n = matrix.Value.GetLength(0);
            
            for (int i = 1; i <= n; i++) datatable.Columns.Add("" + i);
            DataRow datarow = datatable.NewRow();
            datarow[0] = "Maszyna pierwsza";
            for (int i = 0; i < n; i++) datarow[i+1] = matrix.Value[i, 0];
            datatable.Rows.Add(datarow);
            datarow = datatable.NewRow();
            datarow[0] = "Maszyna druga";
            for (int i = 0; i < n; i++) datarow[i+1] = matrix.Value[i, 1];
            
            datatable.Rows.Add(datarow);

            return datatable;
        }
      
        public static Matrix GenerateRandomMatrix(int n) //tworzenie losowej macierzy kwadratowej parametr to jej wielkość
        {
            Random rng = new Random(); 
            Matrix matrix = new Matrix(n);
            int rowLength = matrix.Value.GetLength(0);
            int colLength = matrix.Value.GetLength(1);

            for (int i = 0; i < rowLength; i++)
            {
                for (int j = 0; j < colLength; j++)
                {
                    matrix.Value[i, j] = rng.Next(1, 10);
                }

            }
            return matrix;

        }
       
        public static void PrintMatrix(Matrix matrix) // Wypisywanie macierzy w konsoli

        {
            int rowLength = matrix.Value.GetLength(0);
            int colLength = matrix.Value.GetLength(1);
            Console.Write(Environment.NewLine + Environment.NewLine);
            Console.Write(Environment.NewLine + Environment.NewLine);
            Console.Write(Environment.NewLine + Environment.NewLine);
            for (int i = 0; i < rowLength; i++)
            {
                for (int j = 0; j < colLength; j++)
                {
                    Console.Write(string.Format("{0} ", matrix.Value[i, j]));
                }
                Console.Write(Environment.NewLine + Environment.NewLine);
            }


        }
        public static void PrintToFile(Matrix matrix, string path) //Zapisywanie macierzy do pliku
        {
           
                int colLength = matrix.Value.GetLength(0);
            
        string mat = "";
            for (int i =0; i<colLength; i++)
            {

                    mat +=  matrix.Value[i, 0] + " " + matrix.Value[i, 1] + "\n";

            }
            using (TextWriter tw = new StreamWriter(path))
            {
                tw.Write(mat);
            }


        }
        public static T[,] JaggedToMultidimensional<T>(T[][] jaggedArray)
        {
            int rows = jaggedArray.Length;
            int cols = jaggedArray.Max(subArray => subArray.Length);
            T[,] array = new T[rows, cols];
            for (int i = 0; i < rows; i++)
            {
                cols = jaggedArray[i].Length;
                for (int j = 0; j < cols; j++)
                {
                    array[i, j] = jaggedArray[i][j];
                }
            }
            return array;
        }
        public static Matrix LoadFromFile(int n, string path) //Wykorzystuję tu bibliotekę LINQ, parametr to wielkość macierzy
        {
            Matrix matrix = new Matrix(n);
            try
            {
                var lines = File.ReadAllLines(path); //Zczytywanie całego pliku
                for (int i = 0; i < n; ++i)
                {
                    var data = lines[i].Split(' ').Select(c => Convert.ToInt32(c)).ToList();
                    for (int j = 0; j < 2; ++j) matrix.Value[i, j] = data[j];

                }
  

                return matrix;
            }
            catch
            {
                Console.WriteLine("Błędna ścieżka pliku lub błędny format macierzy");
                return matrix;
            }
        }
    }
}
