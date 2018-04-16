using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using DevExpress.Xpf.Grid;
using System.Diagnostics;

namespace Q453145
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            treeListControl1.ItemsSource = CreateData(10);
        }

        List<DataItem> CreateData(int length)
        {
            List<DataItem> list = new List<DataItem>();
            for (int i = 0; i < length; i++)
            {
                list.Add(new DataItem() { ParentId = i % 2, Name = "Item " + i });
            }
            return list;
        }

        private void treeListControl1_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.LeftCtrl && dropManager.DraggingRows != null && dropManager.DraggingRows.Count > 0)
            {
                if (e.IsUp)
                    dropManager.ViewInfo.SetValue(CopyRowsHelper.CopyRowsProperty, false);
                else if (!(bool)dropManager.ViewInfo.GetValue(CopyRowsHelper.CopyRowsProperty))
                    dropManager.ViewInfo.SetValue(CopyRowsHelper.CopyRowsProperty, true);
            }
        }

        private void dropManager_Drop(object sender, DevExpress.Xpf.Grid.DragDrop.TreeListDropEventArgs e)
        {
            if ((bool)e.Manager.ViewInfo.GetValue(CopyRowsHelper.CopyRowsProperty))
                foreach (TreeListNode node in e.DraggedRows)
                {
                    DataItem item = node.Content as DataItem;
                    node.Content = new DataItem() { Name = item.Name };
                }
        }
    }

    public class DataItem
    {
        static int IdSequence = 0;
        public int Id { get; set; }
        public int ParentId { get; set; }
        public string Name { get; set; }

        public DataItem()
        {
            Id = IdSequence++;
        }

        public override string ToString()
        {
            return Name;
        }
    }

    public class CopyRowsHelper : DependencyObject
    {
        public static readonly DependencyProperty CopyRowsProperty = DependencyProperty.RegisterAttached("CopyRows", typeof(bool), typeof(CopyRowsHelper), new UIPropertyMetadata(false));

        public static bool GetCopyRows(DependencyObject target)
        {
            return (bool)target.GetValue(CopyRowsProperty);
        }
        public static void SetCopyRows(DependencyObject target, bool value)
        {
            target.SetValue(CopyRowsProperty, value);
        }
    }
}
