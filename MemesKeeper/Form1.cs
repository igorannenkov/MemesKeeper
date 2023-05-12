using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace MemesKeeper
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            this.KeyDown += Form1_KeyDown;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            DirectoryInfo dirInfo = new DirectoryInfo(Environment.CurrentDirectory);
           
            foreach (var item in dirInfo.GetDirectories())
            {
                TabPage tabPage = new TabPage(item.Name);
                ListView listView = new ListView();
                ImageList imageList = new ImageList();
                string[] pics = Directory.GetFiles(item.Name, "*.JPG");
                DirectoryInfo innerDirInfo = new DirectoryInfo(item.FullName);
                FileInfo[] files = innerDirInfo.GetFiles("*.jpg");

                listView.View = View.LargeIcon;                
                imageList.ImageSize = new Size(128, 128);
                imageList.ColorDepth = ColorDepth.Depth32Bit;
                
                for(int i = 0; i<pics.Length; i++)
                {
                    Image image = Image.FromFile(pics[i]);
                    ListViewItem listViewItem = listView.Items.Add(files[i].Name);

                    imageList.Images.Add(image);
                    listViewItem.Tag = pics[i];
                    listViewItem.ImageKey = item.Name;
                    listViewItem.ImageIndex = i;
                }

                listView.LargeImageList = imageList;
                listView.SmallImageList = imageList;
                listView.Dock = DockStyle.Fill;
                listView.MouseDoubleClick += ListView_MouseDoubleClick;
                listView.KeyDown += ListView_KeyDown;
                tabPage.Controls.Add(listView);
                tabControl1.TabPages.Add(tabPage);

                listView.BackColor = Color.Black;
                listView.ForeColor = Color.White;
            }
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F5)
            {
                tabControl1.Controls.Clear();
                Form1_Load(sender, e);
            }
        }

        private void ListView_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.C)
            {
                foreach (ListViewItem itm in (sender as ListView).SelectedItems)
                {
                    int imgIndex = itm.ImageIndex;
                    if (imgIndex >= 0 && imgIndex < itm.ImageList.Images.Count)
                    {
                        Image image = Image.FromFile(itm.Tag.ToString());
                        Clipboard.SetImage(image);
                    }
                }
            }
        }

        private void ListView_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            foreach (ListViewItem itm in (sender as ListView).SelectedItems)
            {
                int imgIndex = itm.ImageIndex;
                if (imgIndex >= 0 && imgIndex < itm.ImageList.Images.Count)
                {
                    Image image = Image.FromFile(itm.Tag.ToString());
                    Clipboard.SetImage(image);
                }
            }
        }
    }
}
