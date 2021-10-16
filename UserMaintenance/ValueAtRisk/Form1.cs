﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ValueAtRisk.Entities;

namespace ValueAtRisk
{
    public partial class Form1 : Form
    {
        PortfolioEntities context = new PortfolioEntities();
        List<Tick> ticks;

        List<PortfolioItem> portfolioItems = new List<PortfolioItem>();

        public Form1()
        {
            InitializeComponent();
            ticks = context.Ticks.ToList();
            dataGridView1.DataSource = ticks;
            CreatePortfolio();
        }

        private void CreatePortfolio()
        {
            portfolioItems.Add(new PortfolioItem() {Index = "OTP", Volume= 10 });
            portfolioItems.Add(new PortfolioItem() { Index = "ZWACK", Volume = 10 });
            portfolioItems.Add(new PortfolioItem() { Index = "ELMU", Volume = 10 });

            dataGridView2.DataSource = portfolioItems;
        }

        private decimal GetPortfolioValue(DateTime date)
        {
            decimal value = 0;
            foreach (var item in portfolioItems)
            {
                var last = (from x in ticks
                            where item.Index == x.Index.Trim()
                            && date <= x.TradingDay
                            select x).First();

                value += (decimal)last.Price * item.Volume;
            }
            return value;
        }
    }
}
