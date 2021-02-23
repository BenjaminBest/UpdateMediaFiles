using CommandLine;

namespace UpdateMediaFiles
{
    public class CommandLineOptions
    {
        [Option('a', "absolutefilename", Required = false, HelpText = "Filename with path.")]
        public string AbsoluteFilename { get; set; }

        [Option('f', "folder", Required = false, HelpText = "Folder with will be processed recursivly.")]
        public string Folder { get; set; }

        [Option('e', "ext", Required = false, HelpText = "Extention of files.")]
        public string Extension { get; set; }

        [Option('l', "list", Required = false, HelpText = "List all files with Extention.")]
        public string ListFileswithExtension { get; set; }
    }
}
