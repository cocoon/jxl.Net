// Copyright (c) 2021 github.com/cocoon
// 
// The copyright notice shall be included in all copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED,
// INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR
// PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE
// FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR
// OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER
// DEALINGS IN THE SOFTWARE.

using jxlNET;
using System;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace WpfExample
{
    public static class ListBoxParameter
    {
        static void AddRow(Grid grid, Control col1, Control col2, Control col3, Control col4)
        {
            // Add row
            grid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
            int row = grid.RowDefinitions.Count - 1;

            // Column 1
            var tb1 = col1;
            Grid.SetRow(tb1, row);
            Grid.SetColumn(tb1, 0);
            grid.Children.Add(tb1);

            // Column 2
            var tb2 = col2;
            Grid.SetRow(tb2, row);
            Grid.SetColumn(tb2, 1);
            grid.Children.Add(tb2);

            // Column 3
            var tb3 = col3;
            Grid.SetRow(tb3, row);
            Grid.SetColumn(tb3, 2);
            grid.Children.Add(tb3);

            // Column 4
            var tb4 = col4;
            Grid.SetRow(tb4, row);
            Grid.SetColumn(tb4, 3);
            grid.Children.Add(tb4);
        }

        public static void FillListBox(ListBox ListBox, jxlNET.Encoder.Encoder Encoder)
        {
            Console.WriteLine("FillListBox started ...");

            Assembly jxlNetAssembly = typeof(jxlNET.Parameter).GetTypeInfo().Assembly;
            string NameSpaceString = "jxlNET.Encoder.Parameters";

            Type[] typelist = GetTypesInNamespace(jxlNetAssembly, NameSpaceString);
            for (int i = 0; i < typelist.Length; i++)
            {
                var t = typelist[i];
                Console.WriteLine(t.Name);

                object o = Activator.CreateInstance(t);
                dynamic instance = Convert.ChangeType(o, t);

                PropertyInfo[] propertyInfos = t.GetProperties();

                // sort properties by name
                Array.Sort(propertyInfos,
                        delegate (PropertyInfo propertyInfo1, PropertyInfo propertyInfo2)
                        { return propertyInfo1.Name.CompareTo(propertyInfo2.Name); });


                //foreach (PropertyInfo propertyInfo in propertyInfos)
                //{
                //Console.WriteLine(propertyInfo.Name);
                //}

                // SKIP this parameter if it is marked as not available
                PropertyInfo propertyInfoAvailable = t.GetProperty("Available");
                if (propertyInfoAvailable != null) {
                    var val = propertyInfoAvailable.GetValue(o, null);
                    if ((bool) val == false)
                        continue;
                }

                PropertyInfo propertyInfoDescription = t.GetProperty("Description");
                string Description = propertyInfoDescription.GetValue(o, null).ToString();

                PropertyInfo propertyInfoName = t.GetProperty("Name");
                string Name = propertyInfoName.GetValue(o, null).ToString();

                PropertyInfo propertyInfoParam = t.GetProperty("Param");
                string Param = propertyInfoParam.GetValue(o, null).ToString();

                PropertyInfo propertyInfoParamVerbose = t.GetProperty("ParamLong");
                string ParamVerbose = propertyInfoParamVerbose.GetValue(o, null).ToString();


                var grid = new Grid();
                grid.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Auto });
                grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
                grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(2, GridUnitType.Star) });
                grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(2, GridUnitType.Star) });

                grid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
                int rowIndex = grid.RowDefinitions.Count - 1;

                ToolTip tTip = new ToolTip();
                tTip.Content = Description;

                EditRow row = new EditRow();
                row.LabelFor = Name;
                row.ToolTip = tTip;

                grid.ToolTip = tTip;

                Label labelname = new Label { Content = Name };

                Button bRemove = new Button();
                bRemove.Height = 20;
                bRemove.Width = 20;
                bRemove.Content = "-";
                bRemove.Click += new RoutedEventHandler((s, e) =>
                {
                    try
                    {
                        Encoder.RemoveParam(instance);
                        Console.WriteLine("Param Remove button clicked: " + Name);
                    }
                    catch (Exception error) { MessageBox.Show(error.ToString()); }
                });


                if (Array.FindIndex(propertyInfos, x => x.Name == "Value") != -1)
                {
                    PropertyInfo propertyInfoValue = t.GetProperty("Value");

                    if (propertyInfoValue.PropertyType == typeof(int))
                    {

                        int Value = (int)propertyInfoValue.GetValue(o, null);

                        TextBox tBox = new TextBox();
                        tBox.Text = Value.ToString();
                        tBox.ToolTip = tTip;

                        Button b = new Button();
                        b.Height = 20;
                        b.Width = 20;
                        b.Content = "+";
                        b.Click += new RoutedEventHandler((s, e) =>
                        {
                            try
                            {
                                int val = Value;
                                int.TryParse(tBox.Text, out val);
                                instance.Value = val;
                                Encoder.AddOrReplaceParam(instance);
                                Console.WriteLine("Param button clicked: " + Name);
                            } catch(Exception error) { MessageBox.Show(error.ToString()); }
                        });
                        
                        Button bAdd = b;
                        AddRow(grid,bRemove, bAdd, labelname, tBox);
                    }
                    else if (propertyInfoValue.PropertyType == typeof(float))
                    {

                        float Value = (float)propertyInfoValue.GetValue(o, null);

                        //encoder expects dot as separator
                        NumberStyles nStyle = NumberStyles.Number;
                        CultureInfo cultureInfo = new CultureInfo("en-US");
                        NumberFormatInfo n = cultureInfo.NumberFormat;
                        n.NumberDecimalSeparator = ".";

                        TextBox tBox = new TextBox();
                        tBox.Text = Value.ToString("N2", n);
                        tBox.ToolTip = tTip;

                        Button b = new Button();
                        b.Height = 20;
                        b.Width = 20;
                        b.Content = "+";
                        b.Click += new RoutedEventHandler((s, e) =>
                        {
                            try
                            {
                                float val = Value;
                                float.TryParse(tBox.Text, nStyle, cultureInfo, out val);
                                instance.Value = val;
                                Encoder.AddOrReplaceParam(instance);
                                Console.WriteLine("Param button clicked: " + Name);
                            }
                            catch (Exception error) { MessageBox.Show(error.ToString()); }
                        });
                        
                        Button bAdd = b;
                        AddRow(grid, bRemove, bAdd, labelname, tBox);

                    }
                    else if (propertyInfoValue.PropertyType == typeof(string))
                    {
                        string Value = propertyInfoValue.GetValue(o, null).ToString();

                        TextBox tBox = new TextBox();
                        tBox.Text = Value.ToString();
                        tBox.ToolTip = tTip;

                        Button b = new Button();
                        b.Height = 20;
                        b.Width = 20;
                        b.Content = "+";
                        b.Click += new RoutedEventHandler((s, e) =>
                        {
                            try
                            {
                                string val = tBox.Text;
                                instance.Value = val;
                                Encoder.AddOrReplaceParam(instance);
                                Console.WriteLine("Param button clicked: " + Name);
                            }
                            catch (Exception error) { MessageBox.Show(error.ToString()); }
                        });

                        Button bAdd = b;
                        AddRow(grid, bRemove, bAdd, labelname, tBox);

                    }
                    //jxlNET.EdgePreservingFilterLevel
                    else if (propertyInfoValue.PropertyType == typeof(EdgePreservingFilterLevelBase))
                    {
                        jxlNET.EdgePreservingFilterLevelBase Level = (jxlNET.EdgePreservingFilterLevelBase)propertyInfoValue.GetValue(o, null);
                        int Value = Level.Value;
                        
                        TextBox tBox = new TextBox();
                        tBox.Text = Value.ToString();
                        tBox.ToolTip = tTip;

                        Button b = new Button();
                        b.Height = 20;
                        b.Width = 20;
                        b.Content = "+";
                        b.Click += new RoutedEventHandler((s, e) =>
                        {
                            try
                            {
                                jxlNET.EdgePreservingFilterLevelBase lvl = Level;

                                int val = Value;
                                int.TryParse(tBox.Text, out val);
                                lvl.Value = val;
                                instance.Value = lvl;
                                Encoder.AddOrReplaceParam(instance);
                                Console.WriteLine("Param button clicked: " + Name);
                            }
                            catch (Exception error) { MessageBox.Show(error.ToString()); }
                        });

                        Button bAdd = b;
                        AddRow(grid, bRemove, bAdd, labelname, tBox);
                    }
                    //jxlNET.Resampling
                    else if (propertyInfoValue.PropertyType == typeof(jxlNET.ResamplingBase))
                    {
                        jxlNET.ResamplingBase Resampling = (jxlNET.ResamplingBase)propertyInfoValue.GetValue(o, null);
                        int Value = Resampling.Value;

                        TextBox tBox = new TextBox();
                        tBox.Text = Value.ToString();
                        tBox.ToolTip = tTip;

                        Button b = new Button();
                        b.Height = 20;
                        b.Width = 20;
                        b.Content = "+";
                        b.Click += new RoutedEventHandler((s, e) =>
                        {
                            try
                            {
                                jxlNET.ResamplingBase res = Resampling;

                                int val = Value;
                                int.TryParse(tBox.Text, out val);
                                res.Value = val;
                                instance.Value = res;
                                Encoder.AddOrReplaceParam(instance);
                                Console.WriteLine("Param button clicked: " + Name);
                            }
                            catch (Exception error) { MessageBox.Show(error.ToString()); }
                        });

                        Button bAdd = b;
                        AddRow(grid, bRemove, bAdd, labelname, tBox);
                    }
                    //ColorTransform
                    else if (propertyInfoValue.PropertyType == typeof(jxlNET.ColorTransformBase))
                    {
                        jxlNET.ColorTransformBase ColTrans = (jxlNET.ColorTransformBase)propertyInfoValue.GetValue(o, null);
                        int Value = ColTrans.Value;

                        TextBox tBox = new TextBox();
                        tBox.Text = Value.ToString();
                        tBox.ToolTip = tTip;

                        Button b = new Button();
                        b.Height = 20;
                        b.Width = 20;
                        b.Content = "+";
                        b.Click += new RoutedEventHandler((s, e) =>
                        {
                            try
                            {
                                jxlNET.ColorTransformBase col = ColTrans;

                                int val = Value;
                                int.TryParse(tBox.Text, out val);
                                col.Value = val;
                                instance.Value = col;
                                Encoder.AddOrReplaceParam(instance);
                                Console.WriteLine("Param button clicked: " + Name);
                            }
                            catch (Exception error) { MessageBox.Show(error.ToString()); }
                        });

                        Button bAdd = b;
                        AddRow(grid, bRemove, bAdd, labelname, tBox);
                    }
                    else if (propertyInfoValue.PropertyType == typeof(jxlNET.ColorSpaceBase))
                    {
                        jxlNET.ColorSpaceBase ColSpace = (jxlNET.ColorSpaceBase)propertyInfoValue.GetValue(o, null);
                        int Value = ColSpace.Value;

                        TextBox tBox = new TextBox();
                        tBox.Text = Value.ToString();
                        tBox.ToolTip = tTip;

                        Button b = new Button();
                        b.Height = 20;
                        b.Width = 20;
                        b.Content = "+";
                        b.Click += new RoutedEventHandler((s, e) =>
                        {
                            try
                            {
                                jxlNET.ColorSpaceBase col = ColSpace;

                                int val = Value;
                                int.TryParse(tBox.Text, out val);
                                col.Value = val;
                                instance.Value = col;
                                Encoder.AddOrReplaceParam(instance);
                                Console.WriteLine("Param button clicked: " + Name);
                            }
                            catch (Exception error) { MessageBox.Show(error.ToString()); }
                        });

                        Button bAdd = b;
                        AddRow(grid, bRemove, bAdd, labelname, tBox);
                    }


                }
                else if (Array.FindIndex(propertyInfos, x => x.Name == "Value") == -1)
                {
                    CheckBox cBox = new CheckBox();
                    cBox.ToolTip = tTip;
                    cBox.VerticalAlignment = VerticalAlignment.Center;
                    //row.Content = cBox;

                    Button b = new Button();
                    //b.HorizontalAlignment = HorizontalAlignment.Left;
                    b.Height = 20;
                    b.Width = 20;
                    b.Content = "+";
                    b.Click += new RoutedEventHandler((s, e) =>
                    {
                        try
                        {
                            if (cBox.IsChecked == true)
                            {
                                Encoder.AddOrReplaceParam(instance);
                            }
                            else
                            {
                                Encoder.RemoveParam(instance);
                            }
                            Console.WriteLine("Param button clicked: " + Name);
                        }
                        catch (Exception error) { MessageBox.Show(error.ToString()); }
                    });


                    Button bAdd = b;
                    AddRow(grid, bRemove, bAdd, labelname, cBox);

                }

                //ListBox.Items.Add(row);
                ListBox.Items.Add(grid);
            }

        }

        private static Type[] GetTypesInNamespace(Assembly assembly, string nameSpace)
        {
            return
              assembly.GetTypes()
                      .Where(t => String.Equals(t.Namespace, nameSpace, StringComparison.Ordinal))
                      .ToArray();
        }


    }
}
