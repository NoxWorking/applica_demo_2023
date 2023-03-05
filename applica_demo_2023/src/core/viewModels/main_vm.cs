/*
 * Applica, Inc.
 * By José O. Lara
 * 2023.02.28
 * 
 * MVVM
 * MV
 */

namespace applica_demo_2023.src.core.viewModels {
    using System;
    using System.Threading.Tasks;

    using CommunityToolkit.Mvvm.ComponentModel;
    using CommunityToolkit.Mvvm.Input;
    using CommunityToolkit.Mvvm.Messaging;
    using CommunityToolkit.Mvvm.Messaging.Messages;

    using applica_demo_2023.src.ui.views;
    using System.Windows;
    using System.Configuration;
    using System.Collections.ObjectModel;
    using System.ComponentModel;
    using System.Windows.Controls;
    using System.Windows.Threading;
    using Microsoft.Win32;
    using applica_demo_2023.src.core.interfaces.services;
    using applica_demo_2023.src.core.services;
    using applica_demo_2023.src.core.data.configuration;
    using System.Data;
    using System.Reflection;
    using System.IO;
    using System.Text;
    using Microsoft.Office.Interop.Excel;


    public partial class MainVm : ObservableObject {

        #region Property
        private IOpenDbfService? _OpenDbfService;

        public RelayCommand<MainWpf> BtnClose { get; }
        public RelayCommand BtnVdeFile { get; }
        public RelayCommand BtnFileOut { get; }
        public RelayCommand BtnSave { get; }

        [ObservableProperty]
        private string? _TxtVdeFile;

        [ObservableProperty]
        private string _TxtFileOut = "";

        [ObservableProperty]
        ObservableCollection<string> _ListLog = new();

        [ObservableProperty]
        public ObservableCollection<int> _LibLogSelectedIndex = new() { 0 };

        [ObservableProperty]
        public string _TxtTitel = "";

        [ObservableProperty]
        private bool _isMyCheckBox;

        private int _Totales;

        private int _TotalesDeReclamaciones;



        /*
        public bool IsMyCheckBox
        {
            get => _isMyCheckBox;
            set
            {
                _isMyCheckBox = value;
                OnPropertyChanged("IsMyCheckBox");
            }
        }
        */

        #endregion

        #region Constructor
        public MainVm() {
            BtnClose = new RelayCommand<MainWpf>(async param => await ClickBtnClose(param!));
            BtnVdeFile = new RelayCommand(async () => await ClickBtnVdeFile());
            BtnFileOut = new RelayCommand(async () => await ClickBtnFileOut());
            BtnSave = new RelayCommand(async () => await ClickBtnSave());

            IsMyCheckBox = true;

            TxtTitel =
            $@"Applica Demo 2023  
Version: {GetType().Assembly.GetName().Version}";


        }

        #endregion

        #region Methods or functions

        private static Task ClickBtnClose(MainWpf mainWpf) {

            try {
                mainWpf?.Close();
            } catch (Exception err) {
                MessageBox.Show(err.ToString(), "ClickBtnClose", MessageBoxButton.OK, MessageBoxImage.Error);
                System.Windows.Application.Current.Shutdown();
            }

            return Task.CompletedTask;
        }

        private async Task ClickBtnVdeFile() {
            try {
                TimeSpan timeSpan = new(DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);
                ListLog.Add($"Start GetData {timeSpan.ToString()}");

                OpenFileDialog op = new() {
                    DefaultExt = ".dbf",
                    Filter = "Vde Files|*.dbf|All Files|*.*"
                };

                bool? result = op.ShowDialog();
                if (result != null && result.Value) {
                    TxtVdeFile = op.FileName;

                    //DbfConfiguration dbfConfiguration = new DbfConfiguration(TxtVdeFile);
                    //_OpenDbfService = new OpenDbfService(dbfConfiguration);

                    //DbfConfiguration dbfConfiguration = new(TxtVdeFile);
                    //_OpenDbfService = new OpenDbfService(dbfConfiguration);

                    //_OpenDbfService = new OpenDbfService(new DbfConfiguration(TxtVdeFile));
                    _OpenDbfService = new OpenDbfService(new(TxtVdeFile));

                    System.Data.DataTable T = await _OpenDbfService.GetAllAsDataTableAsync();
                    ListLog.Add($"Vde File: {TxtVdeFile}");
                    ListLog.Add($"Total de Record: {T.Rows.Count:N0}");
                    _Totales = T.Rows.Count;

                    int count = 0;
                    /*
                    
                    for (int i=0;i<T.Rows.Count;i++) {

                        string v1Page = T.Rows[i]["V1PAGE"].ToString() ?? "" ;

                        if (v1Page != "99")
                        {
                            count++;
                            //ListLog.Add(count.ToString() + "-" +   T.Rows[i]["V1PAGE"].ToString() ?? "Error");
                            string msg = $"{count:N0} - {T.Rows[i]["v1page"]}";
                            ListLog.Add (msg);
                        }
                    }

                    foreach (DataRow row in T.Rows)
                    {
                        count++;
                        string msg = $"{count:N0} -  {row["v1page"]}";
                        ListLog.Add(msg);
                    }

                    */

                    //int totalRec = T.Select("v1page <> '99'").Length;
                    //ListLog.Add($"Total de Rec: {totalRec:N0}");

                    ListLog.Add($"Total de Reclamaciones: {T.Select("v1page <> '99'").Length:N0}");
                    _TotalesDeReclamaciones = T.Select("v1page <> '99'").Length;

                }

            } catch (Exception err) {
                MessageBox.Show(err.ToString(), "ClickBtnVdeFile", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            //return Task.CompletedTask;
        }

        public Task ClickBtnFileOut() {
            try {

                SaveFileDialog fb = new SaveFileDialog();
                fb.Filter = "Text Files|*.txt|Excel Files|*.xlsx|Pdf Files|*.ddf";
                fb.FileName = "Totales.txt";
                bool? result = fb.ShowDialog();

                if (result != null && result.Value) {
                    TxtFileOut = fb.FileName;
                    ListLog.Add(TxtFileOut);
                }

            } catch (Exception err) {
                MessageBox.Show(err.ToString(), "ClickBtnFileOut", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            return Task.CompletedTask;
        }

        private Task ClickBtnSave() {
            try {
             
                Microsoft.Office.Interop.Excel.Application excel = new Microsoft.Office.Interop.Excel.Application();
                Workbook wb;
                Worksheet ws;

                wb = excel.Workbooks.Add(XlWBATemplate.xlWBATWorksheet);
                ws = wb.Worksheets[1];

                Microsoft.Office.Interop.Excel.Range cellRange = ws.Range["C3:G3"];


               


                string[] info = new[] { "Numero totales", Convert.ToString(_Totales), " ", "Reclamaciones",  Convert.ToString(_TotalesDeReclamaciones) };

                cellRange.set_Value(XlRangeValueDataType.xlRangeValueDefault, info);

                wb.SaveAs(TxtFileOut);
                wb.Close();






                IsMyCheckBox = IsMyCheckBox ? false: true;
              

                //TODO: ADD !!!!!
                MessageBox.Show("Saved","saved",MessageBoxButton.OK,MessageBoxImage.Exclamation);


            } catch (Exception err) {
                MessageBox.Show(err.ToString(), "ClickBtnSave", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            return Task.CompletedTask;
        }

        #endregion



    }
}
