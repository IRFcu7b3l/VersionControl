﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Excel = Microsoft.Office.Interop.Excel;
using System.Reflection;
using System.Data.Entity.Migrations.Model;

namespace _04_cu7b3l
{
    public partial class Form1 : Form
    {
        List<Flat> flats;
        RealEstateEntities context=new RealEstateEntities();
        Excel.Application xlApp;
        Excel.Workbook xlWB;
        Excel.Worksheet xlSheet;
        public Form1()
        {
            InitializeComponent();
            LoadData();
            try
            {
                xlApp = new Excel.Application();
                xlWB = new Excel.Workbook();
                xlSheet = new Excel.Worksheet();
                xlApp.Visible = true;
                xlApp.UserControl = true;

            }
            catch (Exception ex)
            {

                string errMsg=string.Format("Error: {0}\nLine: {1}", ex.Message, ex.Source);
                MessageBox.Show(errMsg, "Error");
                xlWB.Close();
                xlApp.Quit();
                xlWB = null;
                xlApp = null;
            }
        }
        void LoadData() {
            flats = context.Flats.ToList();
        }
        
    }
}
