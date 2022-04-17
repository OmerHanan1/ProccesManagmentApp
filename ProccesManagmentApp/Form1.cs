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
        /// <summary>
        /// List of procceses
        /// </summary>
        private Process[] processes;
        public TaskManagmentApp()
        {
            InitializeComponent();
            GetAllRunningProcceses();
        }

        /// <summary>
        /// Getting all running procceses from diagnostic library
        /// </summary>
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

        /// <summary>
        /// Function for displaying the memory usage of each procces in a readable way
        /// </summary>
        /// <param name="memory"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Kill task button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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

        /// <summary>
        /// Refresh the procceses list
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button2_Click(object sender, EventArgs e)
        {
            GetAllRunningProcceses();
        }
    }
}
