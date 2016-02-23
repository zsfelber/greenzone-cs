using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace GreenZoneFxBuildTasks
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void testCsParserButton_Click(object sender, EventArgs e)
        {
            GreenZoneParser.ParseTesterForm gzform1 = new GreenZoneParser.ParseTesterForm();
            gzform1.ShowDialog();
        }

        private void testCsCompilerButton_Click(object sender, EventArgs e)
        {
            GreenZoneParser.CompilerTesterForm gzform1 = new GreenZoneParser.CompilerTesterForm();
            gzform1.ShowDialog();
        }

        private void generateFxBase_Click(object sender, EventArgs e)
        {
            GreenZoneFxBaseGenerator.GreenZoneFxBaseGenerator.Generate();
        }

        private void genReflButton_Click(object sender, EventArgs e)
        {
            GreenZoneFxReflectGenerator.GreenZoneFxReflectGenerator.Generate();
        }

        private void debugButton_Click(object sender, EventArgs e)
        {
            Type tpA = typeof(Aaa<>);
            Type tpB = typeof(Aaa<>.B<>);
            Type tpC = typeof(Aaa<>.B<>.C<>);
            Console.WriteLine("A:" + tpA + " name:" + tpA.Name + " " + GenericArgsInfo(tpA));
            Console.WriteLine("B:" + tpB + " name:" + tpB.Name + " " + GenericArgsInfo(tpB));
            Console.WriteLine("C:" + tpC + " name:" + tpC.Name + " " + GenericArgsInfo(tpC));

            Aaa<int> a = new Aaa<int>();
            Aaa<int>.B<double> b = new Aaa<int>.B<double>();
            Aaa<int>.B<double>.C<string> c = new Aaa<int>.B<double>.C<string>();

            Console.WriteLine("a:" + a.GetType() + " name:" + a.GetType().Name + " " + GenericArgsInfo(a.GetType()));
            Console.WriteLine("b:" + b.GetType() + " name:" + b.GetType().Name + " " + GenericArgsInfo(b.GetType()));
            Console.WriteLine("c:" + c.GetType() + " name:" + c.GetType().Name + " " + GenericArgsInfo(c.GetType()));
        }

        string GenericArgsInfo(Type type)
        {
            string result = "GenericArgsCnt:" + GenericArgsCnt(type) + " all:" + type.GetGenericArguments().Length;
            return result;
        }

        int GenericArgsCnt(Type type)
        {
            int r = 0;
            foreach (Type ga in type.GetGenericArguments())
            {
                if (ga.DeclaringType == type)
                {
                    r++;
                }
            }
            return r;
        }
    }
}


// TODO c# test - remove

public class Aaa<a>
{
    public class B<b>
    {
        public class C<c>
        {
        }
    }
}
