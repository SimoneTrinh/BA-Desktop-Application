using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Markup;
using HtmlAgilityPack;
using Xceed.Wpf.Toolkit;

namespace BA_website_to_Application
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public class Information
        {
            public string Thongbao { get; set; }

            public string Link { get; set; }

            public string NgayDang { get; set; }
        }
        public MainWindow()
        {
            InitializeComponent();
        }
		public void Button_Click(object sender, RoutedEventArgs e)
		{
			DataGridMain.Items.Clear();
			HtmlWeb htmlWeb = new HtmlWeb
			{
				UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/87.0.4280.88 Safari/537.36"
			};
            //HtmlDocument _htmlDoc = htmlWeb.Load(_url);
            //foreach (HtmlNode item in (IEnumerable<HtmlNode>)(_htmlDoc.DocumentNode.SelectNodes("//div[contains(@style, 'margin:5px')]") ?? throw new Exception("Null!")))
            //{
            //	string thongbao = item.SelectSingleNode("div").InnerText;
            //	string link = item.SelectSingleNode("div/a").Attributes["href"].Value;
            //	string innerText = item.SelectSingleNode("div[2]").InnerText;
            //	string tbec = HttpUtility.HtmlDecode(thongbao);
            //	string linkec = HttpUtility.HtmlDecode(link);
            //	string ngaydangfilter = innerText.Remove(0, 11);
            //	string httplink = "http://online.hvnh.edu.vn" + linkec;
            //	Information data = new Information
            //	{
            //		Thongbao = tbec,
            //		Link = httplink,
            //		NgayDang = ngaydangfilter
            //	};
            //	DataGridMain.Items.Add(data);
            //	DataGridColumn columngrid = DataGridMain.Columns[1];
            //	DataGridMain.Items.SortDescriptions.Add(new SortDescription(columngrid.SortMemberPath, ListSortDirection.Descending));
            //}
            //string maxvalue = Regex.Split(_htmlDoc.DocumentNode.SelectSingleNode("//div[@style=' font-weight: bold; padding: 10px; ']").InnerText, "\\D+").Max();
            //string datapg = intupdown.Value + " trong tổng số " + maxvalue + " trang";
            //PageCount.Text = datapg;
            string _url = "http://online.hvnh.edu.vn/?page=" + intupdown.Value;
			try
			{
                HtmlDocument _htmldoc = htmlWeb.Load(_url);
                foreach(HtmlNode item in _htmldoc.DocumentNode.SelectNodes("//div[contains(@style, 'margin:5px')]"))
                {
                    string thongbao = item.SelectSingleNode("//div/a").InnerText;
                    string link = item.SelectSingleNode("div/a").Attributes["href"].Value;
                    string ngaydangText = item.SelectSingleNode("//i").InnerText;
                    string tbec = HttpUtility.HtmlDecode(thongbao);
                    string linkec = HttpUtility.HtmlDecode(link);
                    string ngaydangfilter = ngaydangText.Remove(0, 11);
                    string httplink = "http://online.hvnh.edu.vn" + linkec;
                    Information data = new Information
                    {
                        Thongbao = tbec,
                        Link = httplink,
                        NgayDang = ngaydangfilter
                    };
                    DataGridMain.Items.Add(data);
                    DataGridColumn columngrid = DataGridMain.Columns[1];
                    DataGridMain.Items.SortDescriptions.Add(new SortDescription(columngrid.SortMemberPath, ListSortDirection.Descending));
                }
                string maxvalue = Regex.Split(_htmldoc.DocumentNode.SelectSingleNode("//div[@style=' font-weight: bold; padding: 10px; ']").InnerText, "\\D+").Max();
                string datapg = intupdown.Value + " trong tổng số " + maxvalue + " trang";
                PageCount.Text = datapg;
            }
            catch (Exception ex)
			{
				System.Windows.MessageBox.Show(ex.Message);
			}
		}
		private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
		{
			Regex regex = new Regex("[^1-9]+");
			e.Handled = regex.IsMatch(e.Text);
		}

	}
}
