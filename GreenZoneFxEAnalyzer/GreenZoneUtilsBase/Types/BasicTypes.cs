using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using GreenZoneUtil.Gui;
using System.Drawing.Design;
using System.IO;

namespace GreenZoneUtil.Util
{
    [Serializable]
    [Editor(typeof(WingdingsCharEditor), typeof(UITypeEditor))]
    [TypeConverter(typeof(ExpandableObjectConverter))]
    public struct WingdingsChar : IWingdingsChar
    {
        int charCode;

        public WingdingsChar(int charCode = 0)
        {
            this.charCode = charCode;
        }

        public int CharCode
        {
            get
            {
                return this.charCode;
            }
            set
            {
                this.charCode = value;
            }
        }

        public override bool Equals(object obj)
        {
            return obj != null && CharCode == ((WingdingsChar)obj).CharCode;
        }

        public override int GetHashCode()
        {
            return CharCode;
        }

        public override string ToString()
        {
            return "WingdingsChar " + CharCode;
        }
    }

    [Serializable]
    [Editor(typeof(SelectableFileEditor), typeof(UITypeEditor))]
    [TypeConverter(typeof(ExpandableObjectConverter))]
    public class SelectableFile : ISelectableFile
    {
        FileInfo file;

        public SelectableFile(string file)
        {
            this.file = new FileInfo(file);
        }
        public SelectableFile(FileInfo file)
        {
            this.file = file;
        }

        public FileInfo File
        {
            get
            {
                return file;
            }
        }

        public string Path
        {
            get
            {
                return file.ToString();
            }
        }

        public override string ToString()
        {
            return Path;
        }

        public override bool Equals(object obj)
        {
            return file.Equals(((SelectableFile)obj).file);
        }

        public override int GetHashCode()
        {
            return file.GetHashCode();
        }
    }


    [Serializable]
    [Editor(typeof(SelectableDirEditor), typeof(UITypeEditor))]
    [TypeConverter(typeof(ExpandableObjectConverter))]
    public class SelectableDir : ISelectableDir
    {
        DirectoryInfo dir;

        public SelectableDir(string dir)
        {
            this.dir = new DirectoryInfo(dir);
        }
        public SelectableDir(DirectoryInfo dir)
        {
            this.dir = dir;
        }

        public DirectoryInfo Dir
        {
            get
            {
                return dir;
            }
        }

        public string Path
        {
            get
            {
                return dir.ToString();
            }
        }

        public override string ToString()
        {
            return Path;
        }

        public override bool Equals(object obj)
        {
            return dir.Equals(((SelectableDir)obj).dir);
        }

        public override int GetHashCode()
        {
            return dir.GetHashCode();
        }
    }
}

