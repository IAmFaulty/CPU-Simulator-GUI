using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CpuSchedulingWinForms
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            string inputProcesses = Microsoft.VisualBasic.Interaction.InputBox(
        "Enter the number of processes: ", "Number of Processes", "", -1, -1);

    DialogResult algorithmChoice = MessageBox.Show(
        "Select Algorithm:\nYes - FCFS\nNo - SJF",
        "Choose Scheduling Algorithm",
        MessageBoxButtons.YesNoCancel);

    if (algorithmChoice == DialogResult.Yes)
    {
        Algorithms.fcfsAlgorithm(inputProcesses);
    }
    else if (algorithmChoice == DialogResult.No)
    {
        Algorithms.sjfAlgorithm(inputProcesses);
    }
    else
    {
        MessageBox.Show("No valid selection made.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
    }
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new CpuScheduler());
                                DialogResult deadlockChoice = MessageBox.Show(
                "Do you want to check for deadlocks?\nYes - Banker's Algorithm\nNo - Resource Allocation Graph",
                "Deadlock Detection", MessageBoxButtons.YesNoCancel);

            if (deadlockChoice == DialogResult.Yes)
            {
                DeadlockDetection.BankersAlgorithm();
            }
            else if (deadlockChoice == DialogResult.No)
            {
                DeadlockDetection.ResourceAllocationGraph();
            }

        }
        
    }
    
}
