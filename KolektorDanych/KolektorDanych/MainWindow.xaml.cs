using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
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
using System.Management;
using System.Management.Instrumentation;

using System.Security.Policy;
using System.Xml;

namespace KolektorDanych
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// 
    
    public partial class MainWindow : Window
    {
        public List<dane> daneList = new List<dane>();
        string tekst = "Test";
        public MainWindow()
        {
            InitializeComponent();
       
            //textBox.Text = Environment.UserName;
        }

        private void butPobierz_Click(object sender, RoutedEventArgs e)
        {
  
            string plik = @"C:\Users\softkj\Documents\Visual Studio 2015\Projects\kolektorPobierz\kolektorPobierz\bin\Debug\kolektorPobierz";
            Process.Start(plik);
            // MessageBox.Show("Powrót");

        }

        private void butNumer_Click(object sender, RoutedEventArgs e)
        {
            ManagementObjectSearcher theSearcher = new ManagementObjectSearcher("SELECT * FROM Win32_DiskDrive WHERE InterfaceType='USB'");
            foreach (ManagementObject currentObject in theSearcher.Get())
            {
                
                ManagementObject theSerialNumberObjectQuery = new ManagementObject("Win32_PhysicalMedia.Tag='" + currentObject["DeviceID"] + "'");
                MessageBox.Show(theSerialNumberObjectQuery["SerialNumber"].ToString());
            }
            MessageBox.Show("Koniec");
        }

        

        private void butTest_Click(object sender, RoutedEventArgs e)
        {
            naz nn = new naz();
            //MessageBox.Show(nn.imie+" "+nn.nazwisko+" "+nn.wiek);
            MessageBox.Show(nn.razem());
        }

        private void butSetup_Click(object sender, RoutedEventArgs e)
        {
            XmlDocument xmlDoc = new XmlDocument();
            //xmlDoc.Load(new StringReader(@"setup.xml"));
            xmlDoc.Load(@"setup.xml");
            XmlNode node = xmlDoc.SelectSingleNode("setup/standardTransmisji");
            node.InnerText = "ILUO";
            //DO something to replace the innnertext of the node with a new string
            // It is here I could use some help.

            xmlDoc.Save(@"setup.xml");
        }

        public string kodPen()
        {
            string klucz = "";
            var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789abcdefghijklmnopqrstuvwxyz0123456789";
            var stringChars = new char[30];
            var random = new Random();
            for (int i = 0; i < stringChars.Length; i++)
            {
                stringChars[i] = chars[random.Next(chars.Length)];
            }
            var finalString = new String(stringChars);
            klucz = finalString.Substring(0, 10) + numerSeryjny().Substring(1, 1) + finalString.Substring(11);


            return klucz;
        }

        private void butLos_Click(object sender, RoutedEventArgs e)
        {
            var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789abcdefghijklmnopqrstuvwxyz0123456789";
            var stringChars = new char[30];
            var random = new Random();
            string ser = "";
            

            for (int i = 0; i < stringChars.Length; i++)
            {
                stringChars[i] = chars[random.Next(chars.Length)];
            }

            var finalString = new String(stringChars);
            MessageBox.Show(finalString);
            MessageBox.Show(numerSeryjny());
            string klucz = finalString.Substring(0,10)+numerSeryjny().Substring(1,1)+finalString.Substring(11);
            MessageBox.Show(klucz);
            if (numerSeryjny().Substring(1,1) == klucz.Substring(10, 1))
            {
                MessageBox.Show("Ok.");
            }
            else
            {
                MessageBox.Show(numerSeryjny().Substring(1, 1)+"  "+ klucz.Substring(10, 1));
            }

        }

        public string numerSeryjny()
        {
            string numer = "";
            ManagementObjectSearcher theSearcher = new ManagementObjectSearcher("SELECT * FROM Win32_DiskDrive WHERE InterfaceType='USB'");
            foreach (ManagementObject currentObject in theSearcher.Get())
            {

                ManagementObject theSerialNumberObjectQuery = new ManagementObject("Win32_PhysicalMedia.Tag='" + currentObject["DeviceID"] + "'");
                //MessageBox.Show(theSerialNumberObjectQuery["SerialNumber"].ToString());
                numer = theSerialNumberObjectQuery["SerialNumber"].ToString();
            }
            
            //MessageBox.Show(numer);
            return numer;

        }

        private void butBox_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void ProgramBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            XmlDocument xmlDoc;
            XmlNode node;
            string wybranyProgram = ProgramBox.SelectedItem.ToString().Substring(38);
            string plik = @"setup.xml";
            xmlDoc = new XmlDocument();
            xmlDoc.Load(plik);
            node = xmlDoc.SelectSingleNode("setup/standardTransmisji");
            node.InnerText = wybranyProgram;
            xmlDoc.Save(plik);
            /*switch (wybranyProgram)
            {
                case "ILUO":
                    xmlDoc = new XmlDocument();
                    xmlDoc.Load(plik);
                    node = xmlDoc.SelectSingleNode("setup/standardTransmisji");
                    node.InnerText = "ILUO";
                    xmlDoc.Save(plik);
                    break;
                case "WAPRO Mag":
                    xmlDoc = new XmlDocument();
                    xmlDoc.Load(plik);
                    node = xmlDoc.SelectSingleNode("setup/standardTransmisji");
                    node.InnerText = "WAPRO Mag";
                    xmlDoc.Save(plik);
                    break;
                case "COMARCH":
                    xmlDoc = new XmlDocument();
                    xmlDoc.Load(plik);
                    node = xmlDoc.SelectSingleNode("setup/standardTransmisji");
                    node.InnerText = "COMARCH";
                    xmlDoc.Save(plik);
                    break;
            }
            */
            MessageBox.Show(wybranyProgram);
        }

       

        private void butInstal_Click(object sender, RoutedEventArgs e)
        {
            //string numerSeryjny = numerSeryjny().;
            string dysk = tbDysk.Text;
            string fileName = dysk + @":\kj_kolektor\";
            string katalog = dysk + @":\kj_kolektor\";
            //string dane = @"C:\Users\softkj\Documents\Visual Studio 2015\Projects\KolektorDanych\KolektorDanych\bin\Debug\dane.txt";
            //string KolektorDanych = @"C:\Users\softkj\Documents\Visual Studio 2015\Projects\KolektorDanych\KolektorDanych\bin\Debug\KolektorDanych.exe";
            //string setup = @"C:\Users\softkj\Documents\Visual Studio 2015\Projects\KolektorDanych\KolektorDanych\bin\Debug\setup.xml";
            string dane = "dane.txt";
            string KolektorDanych = "KolektorDanych.exe";
            string setup = "setup.xml";
            string dane_new = katalog + "dane.txt";
            string KolektorDanych_new = katalog + "KolektorDanych.exe";
            string setup_new = katalog + "setup.xml";
            string numSer = numerSeryjny();
            MessageBox.Show(numSer);
            MessageBox.Show(kodPen());
            // Ładowanie klucza
            XmlDocument xmlDoc;
            XmlNode node;
            string klucz = kodPen();
            string plik = @"setup.xml";
            xmlDoc = new XmlDocument();
            xmlDoc.Load(plik);
            node = xmlDoc.SelectSingleNode("setup/klucz");
            node.InnerText = klucz;
            xmlDoc.Save(plik);
            //Koniec ładowania klucza
            //if (!Directory.Exists(System.IO.Path.GetDirectoryName(fileName)))
            //{
            //    Directory.CreateDirectory(System.IO.Path.GetDirectoryName(fileName));
            //}
            //string fileToCopy = "filelocation\\file_name.txt";
            //String server = Environment.UserName;
            //string newLocation = "C:\\Users\\" + server + "\\Pictures\\Tenders\\file_name.txt";
            //string folderLocation = "C:\\Users\\" + server + "\\Pictures\\Tenders\\";


            bool exists = System.IO.Directory.Exists(katalog);

            if (!exists)
            {
                System.IO.Directory.CreateDirectory(katalog);
            }

            if (System.IO.File.Exists(KolektorDanych))
                {
                    System.IO.File.Copy(dane, dane_new, true);
                    System.IO.File.Copy(KolektorDanych, KolektorDanych_new, true);
                    System.IO.File.Copy(setup,setup_new, true);
                    MessageBox.Show("Kopiowanie Ok.");

                }
                else
                {
                    MessageBox.Show("Plików nie odnaleziono");

                }
        }
    }
}
