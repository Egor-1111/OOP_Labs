using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;
using OOP_Lab2.JSONSystem;
namespace OOP_Lab2
{
 
    public  class Settings
    {
        public int FontSize { get; set; }
        public string Theme { get; set; }
        [DllImport("kernel32.dll", SetLastError = true)]
        static extern IntPtr GetStdHandle(int nStdHandle);

        [DllImport("kernel32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        static extern bool SetCurrentConsoleFontEx(IntPtr consoleOutput, bool maximumWindow, ref CONSOLE_FONT_INFO_EX consoleCurrentFontEx);

        private const int STD_OUTPUT_HANDLE = -11;
        private const int TMPF_TRUETYPE = 4;
        private const int LF_FACESIZE = 32;

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
        public struct CONSOLE_FONT_INFO_EX
        {
            public uint cbSize;
            public uint nFont;
            public COORD dwFontSize;
            public int FontFamily;
            public int FontWeight;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = LF_FACESIZE)]
            public string FaceName;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct COORD
        {
            public short X;
            public short Y;
        }

        public void SetConsoleFont(/*string fontName*/ short fontSize)
        {
            string settingsPath = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
            @"Packages\Microsoft.WindowsTerminal_8wekyb3d8bbwe\LocalState\settings.json"
        );

            // Чтение и изменение JSON

            TerminalSettings settings;
            if (File.Exists(settingsPath))
            {
                string json = File.ReadAllText(settingsPath);
                settings = JsonSerializer.Deserialize<TerminalSettings>(json);
            }
            else
            {
                settings = new TerminalSettings();
            }

            // Обновляем размер шрифта
            settings.Profiles.Defaults.Font.Size = fontSize;
            var options = new JsonSerializerOptions
            {
                WriteIndented = true,
                DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
            };
            string updatedJson = JsonSerializer.Serialize(settings, options);
            File.WriteAllText(settingsPath, updatedJson);


            // Запускаем Terminal с новыми настройками
            //Process.Start("OOP_Lab2.exe");
            
        }
        public void SetConsoleTheme(string theme)
        {
            string settingsPath = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
            @"Packages\Microsoft.WindowsTerminal_8wekyb3d8bbwe\LocalState\settings.json"
        );
            TerminalSettings settings;
            if (File.Exists(settingsPath))
            {
                string json = File.ReadAllText(settingsPath);
                settings = JsonSerializer.Deserialize<TerminalSettings>(json);
            }
            else
            {
                settings = new TerminalSettings();
            }

            // Обновляем размер шрифта
            settings.Profiles.Defaults.Schema = theme;
            var options = new JsonSerializerOptions
            {
                WriteIndented = true,
                DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
            };
            string updatedJson = JsonSerializer.Serialize(settings, options);
            File.WriteAllText(settingsPath, updatedJson);


        }
    }
}
