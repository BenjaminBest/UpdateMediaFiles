using CommandLine;
using System;
using System.IO;

namespace UpdateMediaFiles
{
    class Program
    {
        static void Main(string[] args)
        {
            //Get options
            Parser.Default.ParseArguments<CommandLineOptions>(args).WithParsed(RunOptions);

            //The end
            Console.WriteLine("Press any key ..");
            Console.ReadKey();            
        }

        static void RunOptions(CommandLineOptions opts)
        {
            if (!string.IsNullOrEmpty(opts.AbsoluteFilename))
                UpdateFile(opts.AbsoluteFilename);

            if(!string.IsNullOrEmpty(opts.Folder) && !string.IsNullOrEmpty(opts.Extension))
                UpdateFolder(opts.Folder, opts.Extension);

            if (!string.IsNullOrEmpty(opts.Folder) && !string.IsNullOrEmpty(opts.ListFileswithExtension))
                ListFilesWithExtension(opts.Folder, opts.ListFileswithExtension);
        }

        static void UpdateFolder(string folder, string extension)
        {
            foreach (string file in Directory.EnumerateFiles(folder, $"*.{extension}", SearchOption.AllDirectories))
            {
                UpdateFile(file);
            }
        }

        static void ListFilesWithExtension(string folder, string extension)
        {
            Console.WriteLine($"List of files in '{folder}' with extension '{extension}'");
            foreach (string file in Directory.EnumerateFiles(folder, $"*.{extension}", SearchOption.AllDirectories))
            {
                Console.WriteLine($"{file}");
            }
        }

        static void UpdateFile(string absoluteFilename)
        {
            //TODO: M4v
            var filename = Path.GetFileName(absoluteFilename).Replace(Path.GetExtension(absoluteFilename), "");

            if (!File.Exists(absoluteFilename))
            {
                Console.WriteLine($"File '{absoluteFilename}' does not exist");
                return;
            }

            //https://github.com/mono/taglib-sharp
            var tfile = TagLib.File.Create(absoluteFilename);
            string title = tfile.Tag.Title;

            Console.WriteLine("Title of video: '{0}' is '{1}'", filename, title);

            // change title in the file
            tfile.Tag.Title = filename;
            tfile.Save();

            Console.WriteLine("Title of video changed to '{0}'", tfile.Tag.Title);
        }

    }
}
