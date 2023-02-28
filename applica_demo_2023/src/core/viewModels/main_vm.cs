/*
 * Applica, Inc.
 * By José O. Lara
 * 2023.02.28
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

        #endregion

        #region Constructor
        public MainVm() {
            BtnClose = new RelayCommand<MainWpf>(async param => await ClickBtnClose(param!));
            BtnVdeFile = new RelayCommand(async () => await ClickBtnVdeFile());
            BtnFileOut = new RelayCommand(async () => await ClickBtnFileOut());
            BtnSave = new RelayCommand(async () => await ClickBtnSave());

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
                    _OpenDbfService = new OpenDbfService(new(TxtVdeFile));
                    DataTable T = await _OpenDbfService.GetAllAsDataTableAsync();
                    ListLog.Add($"Vde File: {TxtVdeFile}");
                    ListLog.Add($"Total de Record: {T.Rows.Count:N0}");
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

                throw new NotImplementedException();

            } catch (Exception err) {
                MessageBox.Show(err.ToString(), "ClickBtnSave", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            return Task.CompletedTask;
        }

        #endregion



    }
}
