using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProccesManagmentApp
{
    public partial class TaskManagmentApp : Form
    {
        private Process[] processes;
        public TaskManagmentApp()
        {
            InitializeComponent();
            GetAllRunningProcceses();
        }

        public void GetAllRunningProcceses()
        {
            processes = Process.GetProcesses();

            listView1.Items.Clear();
            foreach (Process p in processes)
            {
                ListViewItem newItem = new ListViewItem(p.ProcessName);
                newItem.SubItems.Add(new ListViewItem.ListViewSubItem() { Text = p.Id.ToString() });
                newItem.SubItems.Add(new ListViewItem.ListViewSubItem() { Text = DisplayMemory(p.PrivateMemorySize64) });
                newItem.SubItems.Add(new ListViewItem.ListViewSubItem() { Text = p.PrivateMemorySize64.ToString() });
                listView1.Items.Add(newItem);
            }
        }
        string DisplayMemory(long memory)
        {
            string[] suffixes = { " B", " KB", " MB", " GB", " TB", " PB" };
            for (int i = 0; i < suffixes.Length; i++)
            {
                long tmp = memory / (int)Math.Pow(1024, i + 1);
                if (tmp == 0)
                    return (memory / (int)Math.Pow(1024, i)) + suffixes[i];

            }
            return memory.ToString();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count > 0)
            {

                int index = 0;
                foreach (Process p in processes)
                {
                    if (p.Id == Int16.Parse(listView1.SelectedItems[0].SubItems[1].Text))
                    {
                        index = processes.ToList().IndexOf(p);
                        break;
                    }
                }
                processes[index].Kill();
                GetAllRunningProcceses();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            GetAllRunningProcceses();
        }
    }
}
