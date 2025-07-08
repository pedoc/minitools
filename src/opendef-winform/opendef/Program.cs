using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;

namespace opendef
{
    internal static class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            var arguments = ParseArguments(args);
            if (arguments.Count <= 0)
            {
                Console.WriteLine(@"[Optional] Use the -d parameter to specify the directory");
                Console.WriteLine(@"[Required] Use -e <extension> to specify the file type, example -e sln");
                return;
            }

            var directory = GetOptionValue(arguments, "d", Environment.CurrentDirectory);
            var extension = GetOptionValue(arguments, "e", "");
            bool.TryParse(GetOptionValue(arguments, "r", "false"), out var recursion);

            if (string.IsNullOrEmpty(extension))
            {
                Console.WriteLine(@"No extension specified. Use -e <extension> to specify the file type.");
                return;
            }

            if (extension.StartsWith("."))
            {
                extension = extension.Substring(1);
            }

            var files = Directory.GetFiles(directory, "*." + extension,
                recursion ? SearchOption.AllDirectories : SearchOption.TopDirectoryOnly);
            if (files.Length == 0)
            {
                Console.WriteLine($@"No matching files were found in {directory}");
                return;
            }
            else if (files.Length == 1)
            {
                Console.WriteLine($@"One matching file was found: {files[0]}");
                Process.Start(files[0]);
            }
            else
            {
                Console.WriteLine($@"{files.Length} matching files were found:");
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                var form = new MainForm();
                form.AddFiles(files);
                Application.Run(form);
                if (form.DialogResult == DialogResult.OK)
                {
                    var selectedFile = form.GetSelectedFile();
                    if (!string.IsNullOrEmpty(selectedFile))
                    {
                        Console.WriteLine($@"Opening file: {selectedFile}");
                        Process.Start(selectedFile);
                    }
                    else
                    {
                        Console.WriteLine(@"No file selected.");
                    }
                }
                else
                {
                    Console.WriteLine(@"Operation cancelled by user.");
                }
            }
        }

        static Dictionary<string, string> ParseArguments(string[] args)
        {
            var options = new Dictionary<string, string>();
            string currentOption = null;

            for (int i = 0; i < args.Length; i++)
            {
                var arg = args[i];

                if (arg.StartsWith("-"))
                {
                    currentOption = arg.TrimStart('-');
                    options[currentOption] = null;
                }
                else if (currentOption != null)
                {
                    options[currentOption] = arg;
                    currentOption = null;
                }
            }

            return options;
        }

        static string GetOptionValue(this Dictionary<string, string> options, string optionName,
            string defaultValue = null)
        {
            if (options.TryGetValue(optionName, out var value))
            {
                return value;
            }

            return defaultValue;
        }
    }
}