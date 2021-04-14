using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.CodeDom.Compiler;
using System.Diagnostics;

using Microsoft.CSharp;

namespace CSCompiler
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            CSharpCodeProvider codeProvider = new CSharpCodeProvider(new Dictionary<string, string>() { { "CompilerVersion", textBoxVersion.Text } });

            CompilerParameters parameters = new CompilerParameters(new string[] { "mscorlib.dll", "System.Core.dll" }, textBoxFilename.Text, true);
            
            //generate execute file
            parameters.GenerateExecutable = true;

            CompilerResults results = codeProvider.CompileAssemblyFromSource(parameters, textBoxCode.Text);

            if (results.Errors.HasErrors)
            {
                textBoxResult.ForeColor = Color.Red;
                foreach (CompilerError error in results.Errors.Cast<CompilerError>())
                {
                    textBoxResult.Text += $"line {error.Line}: {error.ErrorText}";
                }
            }
            else {
                textBoxResult.ForeColor = Color.Green;
                textBoxResult.Text = "----Build completed----";
                Process.Start($"{Application.StartupPath}/{textBoxFilename.Text}");
            }
        }
    }
}
