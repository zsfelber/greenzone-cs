using GreenZoneFxEngine.Trading;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using GreenZoneFxEngine.Types;
using System.IO;
using System.Collections;
using System.Collections.Generic;

namespace UnitTests
{
    
    
    /// <summary>
    ///This is a test class for Mt4FileUtilTest and is intended
    ///to contain all Mt4FileUtilTest Unit Tests
    ///</summary>
    [TestClass()]
    public class Mt4FileUtilTest : TradingConst
    {


        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Additional test attributes
        // 
        //You can use the following additional attributes as you write your tests:
        //
        //Use ClassInitialize to run code before running the first test in the class
        //[ClassInitialize()]
        //public static void MyClassInitialize(TestContext testContext)
        //{
        //}
        //
        //Use ClassCleanup to run code after all tests in a class have run
        //[ClassCleanup()]
        //public static void MyClassCleanup()
        //{
        //}
        //
        //Use TestInitialize to run code before running each test
        //[TestInitialize()]
        //public void MyTestInitialize()
        //{
        //}
        //
        //Use TestCleanup to run code after each test has run
        //[TestCleanup()]
        //public void MyTestCleanup()
        //{
        //}
        //
        #endregion


        [TestMethod()]
        public void CsvRWTest()
        {
            ServerEnvironmentRuntime env = new ServerEnvironmentRuntime("");
            env.SetImportDir("F:\\workspaces\\general_web\\ForexRobots\\windows_dll\\GreenZoneFxEAnalyzer\\UnitTests\\bin\\Debug", this);
            ServerChartGroupRuntime chartGroupRuntime = new ServerChartGroupRuntime(env);
            ServerChartRuntime chartRuntime = new ServerChartRuntime(chartGroupRuntime, true);
            UserRuntime userRuntime = new UserRuntime(chartRuntime, null);

            CsvFileR fileR = (CsvFileR)userRuntime.FileOpen("Big.csv", FILE_CSV | FILE_READ);
            Assert.IsTrue(fileR.FileMode == FileMode.Open, "fileR.FileMode == FileMode.Open");
            Assert.IsTrue(fileR.FileAccess == FileAccess.Read, "fileR.FileAccess == FileAccess.Read");
            Assert.IsTrue(fileR.Stream.CanSeek, "fileR.Stream.CanSeek");

            CsvFileW fileW = (CsvFileW)userRuntime.FileOpen("Big.csv.out", FILE_CSV | FILE_WRITE);
            Assert.IsTrue(fileW.FileMode == FileMode.Create, "fileW.FileMode == FileMode.Create");
            Assert.IsTrue(fileW.FileAccess == FileAccess.Write, "fileW.FileAccess == FileAccess.Write");
            Assert.IsTrue(fileW.Stream.CanSeek, "fileW.Stream.CanSeek");

            do
            {
                do
                {
                    string col = fileR.ReadString();
                    if (fileR.IsLineEnding() && !fileR.IsEnding())
                    {
                        fileW.WriteStringCsv(col+"\r\n");
                        Assert.IsTrue(fileR.Tell() == fileW.Tell(), "Position mismatch   R:" + fileR.Tell() + " of " + fileR.Size() + "   W:" + fileW.Tell());
                    }
                    else
                    {
                        fileW.WriteStringCsv(col);
                        Assert.IsTrue(fileR.Tell() == fileW.Tell(), "Position mismatch   R:" + fileR.Tell() + " of " + fileR.Size() + "   W:" + fileW.Tell());
                    }
                } while (!fileR.IsLineEnding());
            } while (!fileR.IsEnding());

            Assert.IsTrue(fileR.Size() == fileW.Size(), "Size mismatch   R:" + fileR.Size() + " W:" + fileW.Size());

            fileR.Close();
            fileW.Close();


            
            GeneralFile fileR1 = userRuntime.FileOpen("Big.csv", FILE_CSV | FILE_READ);
            GeneralFile fileR2 = userRuntime.FileOpen("Big.csv.out", FILE_CSV | FILE_READ);

            do
            {
                string col1 = fileR1.ReadString();
                string col2 = fileR2.ReadString();

                Assert.AreEqual(col1, col2, "Content mismatch  col1 != col2   R1:" + fileR1.Tell() + " R2:" + fileR2.Tell());
                Assert.IsTrue(fileR1.Tell() == fileR2.Tell(), "Position mismatch   R1:" + fileR1.Tell() + " R2:" + fileR2.Tell());
                Assert.IsTrue(fileR1.IsLineEnding() == fileR2.IsLineEnding(), "LineEnding mismatch   fileR1.IsLineEnding():" + fileR1.IsLineEnding() + " != fileR2.IsLineEnding():" + fileR2.IsLineEnding());
                Assert.IsTrue(fileR1.IsEnding() == fileR2.IsEnding(), "EOF mismatch   fileR1.IsEnding():" + fileR1.IsEnding() + " != fileR2.IsEnding():" + fileR2.IsEnding());
            } while (!fileR1.IsEnding());

            fileR1.Close();
            fileR2.Close();



            
            fileR = (CsvFileR) userRuntime.FileOpen("Big.csv", FILE_CSV | FILE_READ);
            fileW = (CsvFileW) userRuntime.FileOpen("Big.csv.out", FILE_CSV | FILE_WRITE);
            do
            {
                object[] row = fileR.ReadArrayCsv(false);
                if (fileR.IsEnding())
                {
                    foreach (var v in row)
                    {
                        fileW.WriteStringCsv((string) v);
                    }
                }
                else
                {
                    fileW.Write(row);
                }
                Assert.IsTrue(fileR.Tell() == fileW.Tell(), "Position mismatch   R:" + fileR.Tell() + " of " + fileR.Size() + "   W:" + fileW.Tell());
            } while (!fileR.IsEnding());

            Assert.IsTrue(fileR.Size() == fileW.Size(), "Size mismatch   R:" + fileR.Size() + " W:" + fileW.Size());

            fileR.Close();
            fileW.Close();


            
            fileR1 = userRuntime.FileOpen("Big.csv", FILE_CSV | FILE_READ);
            fileR2 = userRuntime.FileOpen("Big.csv.out", FILE_CSV | FILE_READ);

            do
            {
                string col1 = fileR1.ReadString();
                string col2 = fileR2.ReadString();

                Assert.AreEqual(col1, col2, "Content mismatch  col1 != col2   R1:" + fileR1.Tell() + " R2:" + fileR2.Tell());
                Assert.IsTrue(fileR1.Tell() == fileR2.Tell(), "Position mismatch   R1:" + fileR1.Tell() + " R2:" + fileR2.Tell());
                Assert.IsTrue(fileR1.IsLineEnding() == fileR2.IsLineEnding(), "LineEnding mismatch   fileR1.IsLineEnding():" + fileR1.IsLineEnding() + " != fileR2.IsLineEnding():" + fileR2.IsLineEnding());
                Assert.IsTrue(fileR1.IsEnding() == fileR2.IsEnding(), "EOF mismatch   fileR1.IsEnding():" + fileR1.IsEnding() + " != fileR2.IsEnding():" + fileR2.IsEnding());
            } while (!fileR1.IsEnding());

            Assert.IsTrue(fileR1.Size() == fileR2.Size(), "Size mismatch   R1:" + fileR1.Size() + " R2:" + fileR2.Size());

            fileR1.Close();
            fileR2.Close();
        }

        [TestMethod()]
        public void CsvSeekAppendTest()
        {

            double[] arr =  {21.0112,234.987123,9823.120101,978128.12112,98312.012012,19789.2376085676521576,
                             867362.081278,12893089.12879,12897.01897127,2378.8017781,123.79182,1.0178978912,
                             9.1278987612,112.122112789,1212.0112122389,9.12121212,63.01789762,832.121278912,
                             12.23788923786,1786123.898,231786.0111212,726.1278978,12378.0789862,1278.112234,
                             1212.1212,763.12121212,1.12100001,23786.12786898,0.127812501,12987.111111,9.001,
                             112.001189,9.0176178,0.00000000876,98.0178601,89987.001011221,125.001100101,1.1};
            long[] seekindexes = new long[2 * arr.Length];
            int[] modindexes = { 2, 5, 9, 12, 13, 16, 19, 22, 27, 35 };


            ServerEnvironmentRuntime env = new ServerEnvironmentRuntime("");
            env.SetImportDir("F:\\workspaces\\general_web\\ForexRobots\\windows_dll\\GreenZoneFxEAnalyzer\\UnitTests\\bin\\Debug", this);
            ServerChartGroupRuntime chartGroupRuntime = new ServerChartGroupRuntime(env);
            ServerChartRuntime chartRuntime = new ServerChartRuntime(chartGroupRuntime, true);
            UserRuntime userRuntime = new UserRuntime(chartRuntime, null);

            userRuntime.FileDelete("Append.csv.out");

            CsvFileW csvFileW = (CsvFileW)userRuntime.FileOpen("Append.csv.out", FILE_CSV | FILE_READ | FILE_WRITE);
            Assert.IsTrue(csvFileW.FileMode == FileMode.OpenOrCreate, "fileW.FileMode == FileMode.OpenOrCreate");
            Assert.IsTrue(csvFileW.FileAccess == FileAccess.ReadWrite, "fileW.FileAccess == FileAccess.ReadWrite");
            Assert.IsTrue(csvFileW.Stream.CanSeek, "fileW.Stream.CanSeek");

            for (int i = 0; i < arr.Length; i++)
            {
                seekindexes[i] = csvFileW.Tell() + 1;
                if (i % 10 == 9)
                {
                    csvFileW.WriteStringCsv(arr[i] + "\r\n");
                }
                else
                {
                    csvFileW.WriteNumberCsv(arr[i]);
                }
            }
            for (int i = 0; i < modindexes.Length; i++)
            {
                var mind = modindexes[i];
                // bin:
                //csvFileW.Seek(mind * sizeof(double) + 2 * mind / 10, SeekOrigin.Begin);
                csvFileW.Seek(seekindexes[mind], SeekOrigin.Begin);
                double readNum = csvFileW.ReadNumber();
                double originalNum = arr[mind];

                Assert.IsTrue(UserRuntime.CompareDouble(readNum, originalNum), "Content mismatch  readNum != originalNum   readNum:" + readNum + " originalNum:" + originalNum + "  i:" + i + "  mind:" + mind + "  seekindexes[mind]:" + seekindexes[mind]);

                csvFileW.Seek(seekindexes[mind], SeekOrigin.Begin);
                if (((int)originalNum) % 10 == 9)
                {
                    csvFileW.WriteNumberCsv(originalNum - 1);
                }
                else
                {
                    csvFileW.WriteNumberCsv(originalNum + 1);
                }
            }

            for (int i = 0; i < modindexes.Length; i++)
            {
                var mind = modindexes[i];
                csvFileW.Seek(seekindexes[mind], SeekOrigin.Begin);
                double readNum = csvFileW.ReadNumber();
                double originalNum = arr[mind];

                if (((int)originalNum) % 10 == 9)
                {
                    Assert.IsTrue(UserRuntime.CompareDouble(readNum, originalNum - 1), "Content mismatch  readNum != originalNum - 1   readNum:" + readNum + "  (originalNum - 1):" + (originalNum - 1) + "  i:" + i + "  mind:" + mind + "  seekindexes[mind]:" + seekindexes[mind]);
                }
                else
                {
                    Assert.IsTrue(UserRuntime.CompareDouble(readNum, originalNum + 1), "Content mismatch  readNum != originalNum + 1   readNum:" + readNum + "  (originalNum + 1):" + (originalNum + 1) + "  i:" + i + "  mind:" + mind + "  seekindexes[mind]:" + seekindexes[mind]);
                }
            }

            csvFileW.Close();



            csvFileW = (CsvFileW)userRuntime.FileOpen("Append.csv.out", FILE_CSV | FILE_READ | FILE_WRITE);
            Assert.IsTrue(csvFileW.FileMode == FileMode.OpenOrCreate, "fileW.FileMode == FileMode.OpenOrCreate");
            Assert.IsTrue(csvFileW.FileAccess == FileAccess.ReadWrite, "fileW.FileAccess == FileAccess.ReadWrite");
            Assert.IsTrue(csvFileW.Stream.CanSeek, "fileW.Stream.CanSeek");

            csvFileW.Seek(seekindexes[36], SeekOrigin.Begin);
            for (int i = 0; i < arr.Length; i++)
            {
                seekindexes[36 + i] = csvFileW.Tell();
                if ((36 + i) % 10 == 9)
                {
                    csvFileW.WriteStringCsv(arr[i] + "\r\n");
                }
                else
                {
                    csvFileW.WriteNumberCsv(arr[i]);
                }
            }

            SortedSet<int> smodindexes = new SortedSet<int>();
            foreach (var mind in modindexes)
            {
                smodindexes.Add(mind);
            }

            csvFileW.Seek(0, SeekOrigin.Begin);

            for (int i = 0; i < 36 + arr.Length; i++)
            {
                double readNum = csvFileW.ReadNumber();
                double originalNum;
                if (i < 36)
                {
                    originalNum = arr[i];
                }
                else
                {
                    originalNum = arr[i - 36];
                }

                if (smodindexes.Contains(i))
                {
                    if (((int)originalNum) % 10 == 9)
                    {
                        Assert.IsTrue(UserRuntime.CompareDouble(readNum, originalNum - 1), "Content mismatch  readNum != originalNum - 1   readNum:" + readNum + "  (originalNum - 1):" + (originalNum - 1) + "  i:" + i + "  seekindexes[i]:" + seekindexes[i]);
                    }
                    else
                    {
                        Assert.IsTrue(UserRuntime.CompareDouble(readNum, originalNum + 1), "Content mismatch  readNum != originalNum + 1   readNum:" + readNum + "  (originalNum + 1):" + (originalNum + 1) + "  i:" + i + "  seekindexes[i]:" + seekindexes[i]);
                    }
                }
                else
                {
                    Assert.IsTrue(UserRuntime.CompareDouble(readNum, originalNum), "Content mismatch  readNum != originalNum   readNum:" + readNum + "  originalNum:" + originalNum + "  i:" + i + "  seekindexes[i]:" + seekindexes[i]);
                }
            }

            csvFileW.Close();


            CsvFileR csvFileR = (CsvFileR)userRuntime.FileOpen("Append.csv.out", FILE_CSV | FILE_READ);

            for (int i = 0; i < 36 + arr.Length; i++)
            {
                double readNum = csvFileR.ReadNumber();
                double originalNum;
                if (i < 36)
                {
                    originalNum = arr[i];
                }
                else
                {
                    originalNum = arr[i - 36];
                }

                if (smodindexes.Contains(i))
                {
                    if (((int)originalNum) % 10 == 9)
                    {
                        Assert.IsTrue(UserRuntime.CompareDouble(readNum, originalNum - 1), "Content mismatch  readNum != originalNum - 1   readNum:" + readNum + "  (originalNum - 1):" + (originalNum - 1) + "  i:" + i + "  seekindexes[i]:" + seekindexes[i]);
                    }
                    else
                    {
                        Assert.IsTrue(UserRuntime.CompareDouble(readNum, originalNum + 1), "Content mismatch  readNum != originalNum + 1   readNum:" + readNum + "  (originalNum + 1):" + (originalNum + 1) + "  i:" + i + "  seekindexes[i]:" + seekindexes[i]);
                    }
                }
                else
                {
                    Assert.IsTrue(UserRuntime.CompareDouble(readNum, originalNum), "Content mismatch  readNum != originalNum   readNum:" + readNum + "  originalNum:" + originalNum + "  i:" + i + "  seekindexes[i]:" + seekindexes[i]);
                }
            }

            csvFileR.Close();
        }
















        [TestMethod()]
        public void BinaryRWTest1()
        {
            ServerEnvironmentRuntime env = new ServerEnvironmentRuntime("");
            env.SetImportDir("F:\\workspaces\\general_web\\ForexRobots\\windows_dll\\GreenZoneFxEAnalyzer\\UnitTests\\bin\\Debug", this);
            ServerChartGroupRuntime chartGroupRuntime = new ServerChartGroupRuntime(env);
            ServerChartRuntime chartRuntime = new ServerChartRuntime(chartGroupRuntime, true);
            UserRuntime userRuntime = new UserRuntime(chartRuntime, null);

            CsvFileR fileR = (CsvFileR)userRuntime.FileOpen("Big.csv", FILE_CSV | FILE_READ);

            BinaryFileW fileW = (BinaryFileW)userRuntime.FileOpen("Big.bin.out", FILE_BIN | FILE_WRITE);
            Assert.IsTrue(fileW.FileMode == FileMode.Create, "fileW.FileMode == FileMode.Create");
            Assert.IsTrue(fileW.FileAccess == FileAccess.Write, "fileW.FileAccess == FileAccess.Write");
            Assert.IsTrue(fileW.Stream.CanSeek, "fileW.Stream.CanSeek");

            do
            {
                string col = fileR.ReadString();
                try
                {
                    double d = Convert.ToDouble(col);
                    fileW.WriteDouble(d);
                }
                catch (FormatException)
                {
                    fileW.WriteString(col, 100);
                }
                Assert.IsTrue(fileW.Stream.Position == fileW.Tell(), "fileW position mismatch   fileW.Stream.Position:" + fileW.Stream.Position + " != fileW.Tell():" + fileW.Tell());
            } while (!fileR.IsEnding());

            fileR.Close();
            fileW.Close();



            fileR = (CsvFileR)userRuntime.FileOpen("Big.csv", FILE_CSV | FILE_READ);
            BinaryFileR fileR2 = (BinaryFileR)userRuntime.FileOpen("Big.bin.out", FILE_BIN | FILE_READ);

            do
            {
                string col = fileR.ReadString();
                object col1, col2;
                try
                {
                    col1 = Convert.ToDouble(col);
                    col2 = fileR2.ReadDouble();
                    Assert.IsTrue(UserRuntime.CompareDouble((double)col1, (double)col2), "Content mismatch  " + col1 + " != " + col2 + "   R:" + fileR.Tell() + " R2:" + fileR2.Tell());
                }
                catch (FormatException)
                {
                    col1 = col;
                    col2 = fileR2.ReadString(100);
                    Assert.AreEqual(col1, col2, "Content mismatch  col1 != col2   R:" + fileR.Tell() + " R2:" + fileR2.Tell());
                }

                Assert.IsTrue(fileR.IsEnding() == fileR2.IsEnding(), "EOF mismatch   fileR.IsEnding():" + fileR.IsEnding() + " != fileR2.IsEnding():" + fileR2.IsEnding() + "  col1:" + col1 + "  col2:" + col2 + "   R:" + fileR.Tell() + " R2:" + fileR2.Tell());
            } while (!fileR.IsEnding());

            fileR.Close();
            fileR2.Close();



        }

        [TestMethod()]
        public void BinaryRWTest2()
        {
            ServerEnvironmentRuntime env = new ServerEnvironmentRuntime("");
            env.SetImportDir("F:\\workspaces\\general_web\\ForexRobots\\windows_dll\\GreenZoneFxEAnalyzer\\UnitTests\\bin\\Debug", this);
            ServerChartGroupRuntime chartGroupRuntime = new ServerChartGroupRuntime(env);
            ServerChartRuntime chartRuntime = new ServerChartRuntime(chartGroupRuntime, true);
            UserRuntime userRuntime = new UserRuntime(chartRuntime, null);

            CsvFileR fileR = (CsvFileR)userRuntime.FileOpen("Big.csv", FILE_CSV | FILE_READ);
            // TODO it is generating a CSV !
            BinaryFileW fileW = (BinaryFileW)userRuntime.FileOpen("Big.bin.out", FILE_BIN | FILE_WRITE);
            do
            {
                object[] row = fileR.ReadArrayCsv();
                fileW.WriteArray(row);

                Assert.IsTrue(fileW.Stream.Position == fileW.Tell(), "fileW position mismatch   fileW.Stream.Position:" + fileW.Stream.Position + " != fileW.Tell():" + fileW.Tell());
            } while (!fileR.IsEnding());

            fileR.Close();
            fileW.Close();



            fileR = (CsvFileR)userRuntime.FileOpen("Big.csv", FILE_CSV | FILE_READ);
            BinaryFileR fileR2 = (BinaryFileR)userRuntime.FileOpen("Big.bin.out", FILE_BIN | FILE_READ);

            do
            {
                string col = fileR.ReadString();
                object col1, col2;
                try
                {
                    col1 = Convert.ToDouble(col);
                    col2 = fileR2.ReadDouble();
                    Assert.IsTrue(UserRuntime.CompareDouble((double)col1, (double)col2), "Content mismatch  " + col1 + " != " + col2 + "   R:" + fileR.Tell() + " R2:" + fileR2.Tell());
                }
                catch (FormatException)
                {
                    col1 = col;
                    col2 = fileR2.ReadString(-1, false);
                    Assert.AreEqual(col1, col2, "Content mismatch  col1 != col2   R:" + fileR.Tell() + " R2:" + fileR2.Tell());
                }

                Assert.IsTrue(fileR.IsEnding() == fileR2.IsEnding(), "EOF mismatch   fileR.IsEnding():" + fileR.IsEnding() + " != fileR2.IsEnding():" + fileR2.IsEnding() + "  col1:" + col1 + "  col2:" + col2 + "   R:" + fileR.Tell() + " R2:" + fileR2.Tell());
            } while (!fileR.IsEnding());

            fileR.Close();
            fileR2.Close();

        }

        [TestMethod()]
        public void BinarySeekAppendTest()
        {
            
            double[] arr =  {21.0112,234.987123,9823.120101,978128.12112,98312.012012,19789.2376085676521576,
                             867362.081278,12893089.12879,12897.01897127,2378.8017781,123.79182,1.0178978912,
                             9.1278987612,112.122112789,1212.0112122389,9.12121212,63.01789762,832.121278912,
                             12.23788923786,1786123.898,231786.0111212,726.1278978,12378.0789862,1278.112234,
                             1212.1212,763.12121212,1.12100001,23786.12786898,0.127812501,12987.111111,9.001,
                             112.001189,9.0176178,0.00000000876,98.0178601,89987.001011221,125.001100101,1.1};
            long[] seekindexes = new long[2 * arr.Length];
            int[] modindexes = { 2, 5, 9, 12, 13, 16, 19, 22, 27, 35 };

            ServerEnvironmentRuntime env = new ServerEnvironmentRuntime("");
            env.SetImportDir("F:\\workspaces\\general_web\\ForexRobots\\windows_dll\\GreenZoneFxEAnalyzer\\UnitTests\\bin\\Debug", this);
            ServerChartGroupRuntime chartGroupRuntime = new ServerChartGroupRuntime(env);
            ServerChartRuntime chartRuntime = new ServerChartRuntime(chartGroupRuntime, true);
            UserRuntime userRuntime = new UserRuntime(chartRuntime, null);

            userRuntime.FileDelete("Append.bin.out");

            BinaryFileW binFileW = (BinaryFileW)userRuntime.FileOpen("Append.bin.out", FILE_BIN | FILE_READ | FILE_WRITE);
            Assert.IsTrue(binFileW.FileMode == FileMode.OpenOrCreate, "fileW.FileMode == FileMode.OpenOrCreate");
            Assert.IsTrue(binFileW.FileAccess == FileAccess.ReadWrite, "fileW.FileAccess == FileAccess.ReadWrite");
            Assert.IsTrue(binFileW.Stream.CanSeek, "fileW.Stream.CanSeek");

            for (int i = 0; i < arr.Length; i++)
            {
                seekindexes[i] = binFileW.Tell();
                binFileW.WriteDouble(arr[i]);
            }
            for (int i = 0; i < modindexes.Length; i++)
            {
                var mind = modindexes[i];
                // bin:
                //binFileW.Seek(mind * sizeof(double) + 2 * mind / 10, SeekOrigin.Begin);
                binFileW.Seek(seekindexes[mind], SeekOrigin.Begin);
                double readNum = binFileW.ReadDouble();
                double originalNum = arr[mind];

                Assert.IsTrue(UserRuntime.CompareDouble(readNum, originalNum), "Content mismatch  readNum != originalNum   readNum:" + readNum + " originalNum:" + originalNum + "  i:" + i + "  mind:" + mind + "  seekindexes[mind]:" + seekindexes[mind]);

                binFileW.Seek(seekindexes[mind], SeekOrigin.Begin);
                if (((int)originalNum) % 10 == 9)
                {
                    binFileW.WriteDouble(originalNum - 1);
                }
                else
                {
                    binFileW.WriteDouble(originalNum + 1);
                }
            }

            for (int i = 0; i < modindexes.Length; i++)
            {
                var mind = modindexes[i];
                binFileW.Seek(seekindexes[mind], SeekOrigin.Begin);
                double readNum = binFileW.ReadDouble();
                double originalNum = arr[mind];

                if (((int)originalNum) % 10 == 9)
                {
                    Assert.IsTrue(UserRuntime.CompareDouble(readNum, originalNum - 1), "Content mismatch  readNum != originalNum - 1   readNum:" + readNum + "  (originalNum - 1):" + (originalNum - 1) + "  i:" + i + "  mind:" + mind + "  seekindexes[mind]:" + seekindexes[mind]);
                }
                else
                {
                    Assert.IsTrue(UserRuntime.CompareDouble(readNum, originalNum + 1), "Content mismatch  readNum != originalNum + 1   readNum:" + readNum + "  (originalNum + 1):" + (originalNum + 1) + "  i:" + i + "  mind:" + mind + "  seekindexes[mind]:" + seekindexes[mind]);
                }
            }

            binFileW.Close();



            binFileW = (BinaryFileW)userRuntime.FileOpen("Append.bin.out", FILE_BIN | FILE_READ | FILE_WRITE);
            Assert.IsTrue(binFileW.FileMode == FileMode.OpenOrCreate, "fileW.FileMode == FileMode.OpenOrCreate");
            Assert.IsTrue(binFileW.FileAccess == FileAccess.ReadWrite, "fileW.FileAccess == FileAccess.ReadWrite");
            Assert.IsTrue(binFileW.Stream.CanSeek, "fileW.Stream.CanSeek");

            binFileW.Seek(seekindexes[36], SeekOrigin.Begin);
            for (int i = 0; i < arr.Length; i++)
            {
                seekindexes[36 + i] = binFileW.Tell();
                binFileW.WriteDouble(arr[i]);
            }

            SortedSet<int> smodindexes = new SortedSet<int>();
            foreach (var mind in modindexes)
            {
                smodindexes.Add(mind);
            }

            binFileW.Seek(0, SeekOrigin.Begin);

            for (int i = 0; i < 36 + arr.Length; i++)
            {
                double readNum = binFileW.ReadDouble();
                double originalNum;
                if (i < 36)
                {
                    originalNum = arr[i];
                }
                else
                {
                    originalNum = arr[i - 36];
                }

                if (smodindexes.Contains(i))
                {
                    if (((int)originalNum) % 10 == 9)
                    {
                        Assert.IsTrue(UserRuntime.CompareDouble(readNum, originalNum - 1), "Content mismatch  readNum != originalNum - 1   readNum:" + readNum + "  (originalNum - 1):" + (originalNum - 1) + "  i:" + i + "  seekindexes[i]:" + seekindexes[i]);
                    }
                    else
                    {
                        Assert.IsTrue(UserRuntime.CompareDouble(readNum, originalNum + 1), "Content mismatch  readNum != originalNum + 1   readNum:" + readNum + "  (originalNum + 1):" + (originalNum + 1) + "  i:" + i + "  seekindexes[i]:" + seekindexes[i]);
                    }
                }
                else
                {
                    Assert.IsTrue(UserRuntime.CompareDouble(readNum, originalNum), "Content mismatch  readNum != originalNum   readNum:" + readNum + "  originalNum:" + originalNum + "  i:" + i + "  seekindexes[i]:" + seekindexes[i]);
                }
            }

            binFileW.Close();


            BinaryFileR binFileR = (BinaryFileR)userRuntime.FileOpen("Append.bin.out", FILE_BIN | FILE_READ);

            for (int i = 0; i < 36 + arr.Length; i++)
            {
                double readNum = binFileR.ReadDouble();
                double originalNum;
                if (i < 36)
                {
                    originalNum = arr[i];
                }
                else
                {
                    originalNum = arr[i - 36];
                }

                if (smodindexes.Contains(i))
                {
                    if (((int)originalNum) % 10 == 9)
                    {
                        Assert.IsTrue(UserRuntime.CompareDouble(readNum, originalNum - 1), "Content mismatch  readNum != originalNum - 1   readNum:" + readNum + "  (originalNum - 1):" + (originalNum - 1) + "  i:" + i + "  seekindexes[i]:" + seekindexes[i]);
                    }
                    else
                    {
                        Assert.IsTrue(UserRuntime.CompareDouble(readNum, originalNum + 1), "Content mismatch  readNum != originalNum + 1   readNum:" + readNum + "  (originalNum + 1):" + (originalNum + 1) + "  i:" + i + "  seekindexes[i]:" + seekindexes[i]);
                    }
                }
                else
                {
                    Assert.IsTrue(UserRuntime.CompareDouble(readNum, originalNum), "Content mismatch  readNum != originalNum   readNum:" + readNum + "  originalNum:" + originalNum + "  i:" + i + "  seekindexes[i]:" + seekindexes[i]);
                }
            }

            binFileR.Close();
        }

    }
}
