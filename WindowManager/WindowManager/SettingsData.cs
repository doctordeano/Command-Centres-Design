﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowManager
{
    internal class SettingsData
    {
        public Tv Tv { get; set; }
        public Grid Grid { get; set; }
        public Panels Panels { get; set; }
    }
    public class Tv
    {
        public double Height { get; set; }
        public double Width { get; set; }
        public double X_Position { get; set; }
        public double Y_Position { get; set; }
        public int PanelNum { get; set; }
    }

    public class Grid
    {
        public double[] RowHeights { get; set; }
        public double[] ColumnWidths { get; set; }
    }

    public class Panels
    {
        public Panel Panel1 { get; set; }
        public Panel Panel2 { get; set; }
        public Panel Panel3 { get; set; }
        public Panel Panel4 { get; set; }
        public Panel Panel5 { get; set; }
        public Panel Panel6 { get; set; }
        public Panel Panel7 { get; set; }
        public Panel Panel8 { get; set; }
        public Panel Panel9 { get; set; }

        public Panel[] GetPanelsArray()
        {
            return new[] { Panel1, Panel2, Panel3, Panel4, Panel5, Panel6, Panel7, Panel8, Panel9 };

        }
        
        public void ClosePanelByName(string panelName)
        {
            switch (panelName)
            {
                case "Panel1":
                    Panel1 = null;
                    break;
                case "Panel2":
                    Panel2 = null;
                    break;
                case "Panel3":
                    Panel3 = null;
                    break;
                case "Panel4":
                    Panel4 = null;
                    break;
                case "Panel5":
                    Panel5 = null;
                    break;
                case "Panel6":
                    Panel6 = null;
                    break;
                case "Panel7":
                    Panel7 = null;
                    break;
                case "Panel8":
                    Panel8 = null;
                    break;
                case "Panel9":
                    Panel9 = null;
                    break;
                default:
                    throw new ArgumentException("Invalid panel name", nameof(panelName));
            }
        }

    }
    public class Panel
    {
        public int PanelNum { get; set; }
        public string Uri { get; set; }
        public int RowSpan { get; set; }
        public int ColumnSpan { get; set; }
    }

}