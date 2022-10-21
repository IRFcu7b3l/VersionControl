﻿using cu7b3l_week5.Entities;
using cu7b3l_week5.MnbServiceReference;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using System.Xml;

namespace cu7b3l_week5
{
    public partial class Form1 : Form
    {
        BindingList<RateData> Rates=new BindingList<RateData>();
        public Form1()
        {
            InitializeComponent();            
            RefreshData();
        }
        string GetExchangeRates() {
            var mnbService = new MNBArfolyamServiceSoapClient();

            var request = new GetExchangeRatesRequestBody()
            {
                currencyNames = comboBox1.SelectedItem.ToString(),
                startDate=dateTimePicker1.ToString(),
                endDate=dateTimePicker2.ToString()
            };
            var response = mnbService.GetExchangeRates(request);
            var result = response.GetExchangeRatesResult;
            return result;
        }

        void XMLProcessing() {
            var xml= new XmlDocument();
            xml.LoadXml(GetExchangeRates());

            foreach  (XmlElement element in xml.DocumentElement)
            {
                var rate = new RateData();
                Rates.Add(rate);

                rate.Date = DateTime.Parse(element.GetAttribute("date"));

                var childElement = (XmlElement)element.ChildNodes[0];
                rate.Currency = childElement.GetAttribute("curr");

                var unit = decimal.Parse(childElement.GetAttribute("unit"));
                var value = decimal.Parse(childElement.InnerText);
                if (unit != 0)
                    rate.Value = value / unit;
                {

                }
            };
        }
        void InitializeData() {
            var series = chartRateData.Series[0];
            series.ChartType = SeriesChartType.Line;
            series.XValueMember = "Date";
            series.YValueMembers = "Value";
            series.BorderWidth = 2;

            var legend = chartRateData.Legends[0];
            legend.Enabled = false;

            var chartArea = chartRateData.ChartAreas[0];
            chartArea.AxisX.MajorGrid.Enabled = false;
            chartArea.AxisY.MajorGrid.Enabled = false;
            chartArea.AxisY.IsStartedFromZero = false;
        }
        void RefreshData() {
            Rates.Clear();
            GetExchangeRates();
            dataGridView1.DataSource = Rates;
            XMLProcessing();
            InitializeData();
            chartRateData.DataSource = Rates;

        }

        private void dateTimePicker2_ValueChanged(object sender, EventArgs e)
        {
            RefreshData();
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            RefreshData();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            RefreshData();
        }
    }
    
    
}
