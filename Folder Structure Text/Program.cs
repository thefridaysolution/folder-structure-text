namespace FileStructureCreator
{
    class Program
    {
        static void Main(string[] args)
        {

            string rootDirectory = @"Your Folder Path";

            if (!Directory.Exists(rootDirectory))
            {
                Console.WriteLine("Invalid Folder path ");
                return;
            }

            string structure = GetDirectoryStructure(rootDirectory, 0);
            Console.WriteLine(rootDirectory);
            Console.WriteLine(structure);
        }

        static string GetDirectoryStructure(string directoryPath, int level)
        {
            string indent = string.Concat(Enumerable.Repeat("│   ", level));
            string spaces = new string(' ', 2);
            string structure = "";

            try
            {
                string[] directories = Directory.GetDirectories(directoryPath);
                string[] files = Directory.GetFiles(directoryPath);
                directories = directories.Where(x => !Path.GetFileName(x).StartsWith('.')).ToArray();

                for (int i = 0; i < directories.Length; i++)
                {
                    string directory = directories[i];
                    bool isLastDirectory = i == directories.Length - 1;
                    bool isFiles = Directory.GetFiles(directoryPath).Length > 0;
                    structure += $"{spaces}{indent}{(isLastDirectory && !isFiles ? "└─" : "├─")}{Path.GetFileName(directory)}\n";
                    structure += GetDirectoryStructure(directory, level + 1);
                }

                for (int i = 0; i < files.Length; i++)
                {
                    string file = files[i];
                    bool isLastDirectory = i == files.Length - 1;
                    structure += $"{spaces}{indent}{(isLastDirectory ? "└─" : "├─")}{Path.GetFileName(file)}\n";
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception: {ex.Message}");
            }

            return structure;
        }
    }
}