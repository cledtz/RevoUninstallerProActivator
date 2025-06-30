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
        public static void dbg_error()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(" [Error]");
            Console.ForegroundColor = ConsoleColor.Gray;
        }

        public static void dbg_success()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(" [Success]");
            Console.ForegroundColor = ConsoleColor.Gray;
        }

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

            Console.Write("[+] Checking for administrator permissions..");

            if (!has_admin())
            {
                dbg_error();
                MessageBox.Show("Please run the activator with administrator permissions.", "Error");
                Environment.Exit(1);
            }
            else
            {
                dbg_success();
            }

            Console.Write("[+] Verifying Revo installation..");

            if (Directory.Exists(@"C:\ProgramData\VS Revo Group\Revo Uninstaller Pro"))
            {
                dbg_success();

                if (File.Exists("C:\\ProgramData\\VS Revo Group\\Revo Uninstaller Pro\\revouninstallerpro5.lic"))
                {
                    Console.Write("[+] Uninstalling old license..");
                    File.Delete("C:\\ProgramData\\VS Revo Group\\Revo Uninstaller Pro\\revouninstallerpro5.lic");
                    if (!File.Exists("C:\\ProgramData\\VS Revo Group\\Revo Uninstaller Pro\\revouninstallerpro5.lic"))
                    {
                        dbg_success();
                    }
                    else
                    {
                        dbg_error();
                        MessageBox.Show("Cleaning failed.", "Error");
                        Environment.Exit(1);
                    }
                }

                Console.Write("[+] Installing license..");

                extract_embedded_exe("RevoUninstallerActivator.Files.revouninstallerpro5.lic", "C:\\ProgramData\\VS Revo Group\\Revo Uninstaller Pro\\revouninstallerpro5.lic");

                if (File.Exists("C:\\ProgramData\\VS Revo Group\\Revo Uninstaller Pro\\revouninstallerpro5.lic"))
                {
                    dbg_success();
                    MessageBox.Show("Revo Uninstaller Pro has successfully been permanently activated.", "Success");
                    Environment.Exit(1);
                }
                else
                {
                    dbg_error();
                    MessageBox.Show("Activation failed.", "Error");
                    Environment.Exit(1);
                }
            }
            else
            {
                dbg_error();

                MessageBox.Show("RevoUninstaller is not installed. Please install it before executing the activation tool.", "Error");
                Environment.Exit(1);
            }
        }
    }
}
