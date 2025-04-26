using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RevoUninstallerActivator
{
    internal class Program
    {
        static bool has_admin()
        {
            using (WindowsIdentity identity = WindowsIdentity.GetCurrent())
            {
                WindowsPrincipal principal = new WindowsPrincipal(identity);
                return principal.IsInRole(WindowsBuiltInRole.Administrator);
            }
        }
        public static void extract_embedded_exe(string resource_name, string output_path)
        {
            var assembly = Assembly.GetExecutingAssembly();

            using (var stream = assembly.GetManifestResourceStream(resource_name))
            {
                if (stream == null)
                {
                    MessageBox.Show("Resource not found!", "Error");
                    Environment.Exit(1);
                }

                using (var file_stream = new FileStream(output_path, FileMode.Create, FileAccess.Write))
                {
                    stream.CopyTo(file_stream);
                }
            }
        }

        static void Main(string[] args)
        {
            Console.Title = "RevoUninstaller Pro Activator - Open source at: https://github.com/cledtz/RevoUninstallerProActivator";

            if (!has_admin())
            {
                Console.WriteLine("Permissions: Error");
                MessageBox.Show("Please run the activator with administrator permissions.", "Error");
                Environment.Exit(1);
            }
            else
            {
                Console.WriteLine("Permissions: OK");
            }

            if (Directory.Exists(@"C:\ProgramData\VS Revo Group\Revo Uninstaller Pro"))
            {
                Console.WriteLine("Enviroment: OK");

                if (File.Exists("C:\\ProgramData\\VS Revo Group\\Revo Uninstaller Pro\\revouninstallerpro5.lic"))
                {
                    File.Delete("C:\\ProgramData\\VS Revo Group\\Revo Uninstaller Pro\\revouninstallerpro5.lic");
                    if (!File.Exists("C:\\ProgramData\\VS Revo Group\\Revo Uninstaller Pro\\revouninstallerpro5.lic"))
                    {
                        Console.WriteLine("Clean: OK");
                    }
                    else
                    {
                        Console.WriteLine("Clean: Error");
                        MessageBox.Show("Cleaning failed.", "Error");
                        Environment.Exit(1);
                    }
                }

                extract_embedded_exe("RevoUninstallerActivator.Files.revouninstallerpro5.lic", "C:\\ProgramData\\VS Revo Group\\Revo Uninstaller Pro\\revouninstallerpro5.lic");

                if (File.Exists("C:\\ProgramData\\VS Revo Group\\Revo Uninstaller Pro\\revouninstallerpro5.lic"))
                {
                    Console.WriteLine("Activation: OK");
                    MessageBox.Show("Activation complete.", "Success");
                    Environment.Exit(1);
                }
                else
                {
                    Console.WriteLine("Activation: Error");
                    MessageBox.Show("Activation failed.", "Error");
                    Environment.Exit(1);
                }
            }
            else
            {
                Console.WriteLine("Enviroment: Error");

                MessageBox.Show("RevoUninstaller is not installed. Please install it before executing the activation tool.", "Error");
                Environment.Exit(1);
            }
        }
    }
}
