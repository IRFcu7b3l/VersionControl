using irfzhgyak05.Entities;
using irfzhgyak05.MnbServiceReference;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using System.Xml;

namespace irfzhgyak05
{
    public partial class Form1 : Form
    {
        BindingList<RateData> Rates = new BindingList<RateData>();
        BindingList<string> Currencies=new BindingList<string>();
        public Form1()
        {
            InitializeComponent();
            GetExchangeRates();         
            xmlProcessing();
            dataGridView1.DataSource = Rates.ToList();
            ShowData();
            
            
            var mnbService = new MNBArfolyamServiceSoapClient();
            var request= new GetCurrenciesRequestBody();
            var response = mnbService.GetCurrencies(request);
            var result = response.GetCurrenciesResult;

            var xml = new XmlDocument();

            xml.LoadXml(result);

            foreach (XmlElement element in xml.DocumentElement.ChildNodes[0])
            {
                var childElement = (XmlElement)element;
                if (childElement==null)
                {
                    continue;
                }
                string c = childElement.InnerText;
                Currencies.Add(c);

                
            }
            comboBox1.DataSource = Currencies;
            RefreshData();


        }
        public string GetExchangeRates() {
                var mnbService = new MNBArfolyamServiceSoapClient();
            
                
                var request = new GetExchangeRatesRequestBody()
                {
                    currencyNames = comboBox1.SelectedItem.ToString(),
                    startDate = dateTimePicker1.Value.ToString(),
                    endDate = dateTimePicker2.Value.ToString()
                };
                
                var response = mnbService.GetExchangeRates(request);

                var result = response.GetExchangeRatesResult;
                            
                return result;

        }
        void xmlProcessing() {
            var xml = new XmlDocument();
            xml.LoadXml(GetExchangeRates());

            foreach (XmlElement element in xml.DocumentElement)
            {
                var rate = new RateData();
                Rates.Add(rate);
                rate.Date = DateTime.Parse(element.GetAttribute("date"));
                var childElement = (XmlElement)element.ChildNodes[0];
                if (childElement == null) 
                {
                    continue;
                }
                rate.Currency = childElement.GetAttribute("curr");
                var unit = decimal.Parse(childElement.GetAttribute("unit"));
                var value = decimal.Parse(childElement.InnerText);
                if (unit != 0) 
                    rate.Value = value / unit;
                if (childElement == null) continue;
            }
        
        }
        void ShowData() {
            chart1.DataSource = Rates;
            var elsőElem = chart1.Series[0];
            elsőElem.ChartType = SeriesChartType.Line;
            elsőElem.XValueMember = "Date";
            elsőElem.YValueMembers = "Value";
            elsőElem.BorderWidth = 2;

            var legend = chart1.Legends[0];
            legend.Enabled = false;

            var chartArea = chart1.ChartAreas[0];
            chartArea.AxisX.MajorGrid.Enabled = false;
            chartArea.AxisY.MajorGrid.Enabled = false;
            chartArea.AxisY.IsStartedFromZero = false;

        }
        void RefreshData() {
            Rates.Clear();
            ShowData();
            GetExchangeRates();
            xmlProcessing();            
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            RefreshData();
        }

        private void dateTimePicker2_ValueChanged(object sender, EventArgs e)
        {
            RefreshData();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            RefreshData();
        }      

    }
   
}
   
    
