using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FlightSimulator2
{
    class DataBase
    {
        private string[][] data;

        public int numberOfRows
        {
            get;
        }

        public DataBase(string path)
        {
            StreamReader reader = new StreamReader(path);
            var rows = new List<string[]>();
            numberOfRows = 0;

            while (!reader.EndOfStream)
            {
                string[] row = reader.ReadLine().Split(',');
                rows.Add(row);
                numberOfRows++;
            }

            this.data = rows.ToArray();
        }

        public string getRowAtIndex(int index)
        {
            return String.Join(",", data[index]);
        }

        public float[] getColumnAtIndex(int index)
        {
            float[] column = new float[numberOfRows];
            for (int i = 0; i < numberOfRows; i++)
            {
                column[i] = float.Parse(data[i][index]);
            }
            return column;
        }

    }

    //class Program
    //{
    //    static void Main(string[] args)
    //    {
    //        string path = @"C:\Users\noa83\Downloads\reg_flight.csv";
    //        // this is the path on my computer. i dont know how to define a path that will be accurate on every computer
    //        DataBase dataBase = new DataBase(path);

    //        var client = new TcpClient("localhost", 5400);
    //        var stream = client.GetStream();

    //        int currentRowIndex = 0; // bind to "currentRow" to jump to another frame
    //        int lastRowIndex = dataBase.numberOfRows;
    //        int timeToSleep = 100; // bind to "timeToSleep" to change the speed

    //        while (currentRowIndex != lastRowIndex)
    //        {
    //            var row = Encoding.ASCII.GetBytes(dataBase.getRowAtIndex(currentRowIndex) + "\n");
    //            stream.Write(row, 0, row.Length);
    //            Console.Write(dataBase.getRowAtIndex(currentRowIndex));
    //            Thread.Sleep(timeToSleep);
    //            currentRowIndex++;
    //        }

    //        stream.Close();
    //        client.Close();
    //    }
    //}
}
