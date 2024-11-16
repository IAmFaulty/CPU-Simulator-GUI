public static class DeadlockDetection
{
    public static void BankersAlgorithm()
    {
        int processes = Convert.ToInt32(
            Microsoft.VisualBasic.Interaction.InputBox("Enter number of processes: ", "Processes", "", -1, -1));
        int resources = Convert.ToInt32(
            Microsoft.VisualBasic.Interaction.InputBox("Enter number of resource types: ", "Resources", "", -1, -1));

        int[,] allocation = new int[processes, resources];
        int[,] maximum = new int[processes, resources];
        int[] available = new int[resources];

        // Input Allocation Matrix
        for (int i = 0; i < processes; i++)
        {
            for (int j = 0; j < resources; j++)
            {
                allocation[i, j] = Convert.ToInt32(
                    Microsoft.VisualBasic.Interaction.InputBox(
                        $"Enter allocation for process {i + 1}, resource {j + 1}: ",
                        "Allocation Matrix", "", -1, -1));
            }
        }

        // Input Maximum Matrix
        for (int i = 0; i < processes; i++)
        {
            for (int j = 0; j < resources; j++)
            {
                maximum[i, j] = Convert.ToInt32(
                    Microsoft.VisualBasic.Interaction.InputBox(
                        $"Enter maximum need for process {i + 1}, resource {j + 1}: ",
                        "Maximum Matrix", "", -1, -1));
            }
        }

        // Input Available Resources
        for (int j = 0; j < resources; j++)
        {
            available[j] = Convert.ToInt32(
                Microsoft.VisualBasic.Interaction.InputBox(
                    $"Enter available units for resource {j + 1}: ",
                    "Available Resources", "", -1, -1));
        }

        // Calculate Need Matrix
        int[,] need = new int[processes, resources];
        for (int i = 0; i < processes; i++)
        {
            for (int j = 0; j < resources; j++)
            {
                need[i, j] = maximum[i, j] - allocation[i, j];
            }
        }

        // Safety Algorithm
        bool[] finished = new bool[processes];
        int[] safeSequence = new int[processes];
        int[] work = (int[])available.Clone();

        int count = 0;
        while (count < processes)
        {
            bool found = false;

            for (int i = 0; i < processes; i++)
            {
                if (!finished[i])
                {
                    int j;
                    for (j = 0; j < resources; j++)
                    {
                        if (need[i, j] > work[j])
                            break;
                    }

                    if (j == resources)
                    {
                        for (int k = 0; k < resources; k++)
                            work[k] += allocation[i, k];

                        safeSequence[count++] = i;
                        finished[i] = true;
                        found = true;
                    }
                }
            }

            if (!found)
            {
                MessageBox.Show("System is in a deadlock state.", "Deadlock Detected", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

        MessageBox.Show($"System is in a safe state.\nSafe Sequence: {string.Join(" -> ", safeSequence)}",
            "Safe State", MessageBoxButtons.OK, MessageBoxIcon.Information);
    }
private static bool DetectCycle(int process, bool[] visited, bool[] recursionStack, int processes, int[,] request)
{
    visited[process] = true;
    recursionStack[process] = true;

    for (int i = 0; i < processes; i++)
    {
        if (request[process, i] > 0)  // Dependency exists
        {
            if (!visited[i] && DetectCycle(i, visited, recursionStack, processes, request))
                return true;
            else if (recursionStack[i])
                return true;
        }
    }

    recursionStack[process] = false;
    return false;
}

public static void RecoverFromDeadlock()
{
    MessageBox.Show("Recovery strategy initiated. Terminating one or more processes to resolve deadlock.", 
                    "Deadlock Recovery", MessageBoxButtons.OK, MessageBoxIcon.Warning);

}

}
