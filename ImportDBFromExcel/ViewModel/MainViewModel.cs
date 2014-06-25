using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using HEPDataLayer;
using HEPDataModel;
using ImportDBFromExcel.Import;
using ImportDBFromExcel.Model;
using System.Windows.Forms;


namespace ImportDBFromExcel.ViewModel
{
    /// <summary>
    /// This class contains properties that the main View can data bind to.
    /// <para>
    /// Use the <strong>mvvminpc</strong> snippet to add bindable properties to this ViewModel.
    /// </para>
    /// <para>
    /// You can also use Blend to data bind with the tool's support.
    /// </para>
    /// <para>
    /// See http://www.galasoft.ch/mvvm
    /// </para>
    /// </summary>
    public class MainViewModel : ViewModelBase
    {

        
        /// <summary>
        /// Initializes a new instance of the MainViewModel class.
        /// </summary>
        public MainViewModel()
        {
            UnitOfWork unitOfWork = new UnitOfWork();
            
            ImportedRecordsView = new ObservableCollection<ImportDataRowModel>();


            MachineTypes = new ObservableCollection<MachineType>(unitOfWork.MachineTypeRepository.GetQuery().Where(type => type.LangId == 2).ToList());
            SelectedMachineType = MachineTypes.FirstOrDefault();

            LoadCommand = new RelayCommand(() => Load());
            ImportToDataBaseCommand = new RelayCommand(() => SaveToDataBase(), () => ImportedRecordsView.Any());

            _templateView = new ImportTemplate();
            _templateView.ModelColName = "B";
            _templateView.ArticleColName = "C";
            _templateView.DescriptionColName = "D";
            _templateView.QuantityColName = "E";
            _templateView.CostBaseColName = "F";
            _templateView.CostNativeColName = "G";
            _templateView.OEMColName = "H";
            _templateView.ManufColName = "I";
            _templateView.StartRowNumber = 6;
            _templateView.EndRowNumber = 200;
        }
        
        //Commands
        #region Commands
        public RelayCommand LoadCommand { get; private set; }

        public RelayCommand ImportToDataBaseCommand { get; set; }

        #endregion

        public MachineType SelectedMachineType
        {
            get { return _selectedMachineType; }
            set
            {
                _selectedMachineType = value;
                RaisePropertyChanged(() => SelectedMachineType);
            }
        }

        public ObservableCollection<MachineType> MachineTypes
        {
            get { return _machineTypes; }
            set
            {
                _machineTypes = value;
                RaisePropertyChanged(() => MachineTypes);
            }
        }


        public bool IsBusy
        {
            get { return _isBusy; }
            private set
            {
                _isBusy = value;
                RaisePropertyChanged(() => IsBusy);
            }
        }

        public string BusyContent
        {
            get { return _busyContent; }
            private set
            {
                _busyContent = value;
                RaisePropertyChanged(() => BusyContent);
            }
        }

        private ImportTemplate _templateView;
        public ImportTemplate TemplateView
        {
            get { return _templateView; }
            set
            {
                _templateView = value;
                RaisePropertyChanged(() => TemplateView);
            }
        }


        private ObservableCollection<ImportDataRowModel> _importedRecords;
        private bool _isBusy;
        private string _busyContent;
        private ObservableCollection<MachineType> _machineTypes;
        private MachineType _selectedMachineType;

        public ObservableCollection<ImportDataRowModel> ImportedRecordsView
        {
            get { return _importedRecords; }
            set
            {
                _importedRecords = value;
                RaisePropertyChanged(() => ImportedRecordsView);
            }
        }


        #region Handlers

        private async void Load()
        {

            OpenFileDialog fileDialog = new OpenFileDialog();
            //fileDialog.RestoreDirectory = true;
            fileDialog.Filter = "Excell Files(*.xls;*.xlsx)|*.XLS;*.XLSX" + "|¬се файлы (*.*)|*.* ";
            fileDialog.CheckFileExists = true;
            fileDialog.Multiselect = false;

            

            if (fileDialog.ShowDialog() == DialogResult.OK)
            {
                BusyChange(true, "Loading From Excel ...");

                string fileName = fileDialog.FileName;

                ImportItemsFromExcelModule module = new ImportItemsFromExcelModule();

                try
                {
                    
                    var collection = await
                            Task<IEnumerable<ImportDataRowModel>>.Factory.StartNew(
                                () => module.Import(_templateView, fileName), TaskCreationOptions.LongRunning);

                    //попутно убираем записи которые имеют одинаковый ItemCode (регистр букв не учитываетс€)
                    ImportedRecordsView = new ObservableCollection<ImportDataRowModel>(collection);
                    
                }
                catch (Exception exc)
                {
                    MessageBox.Show(exc.Message);
                    return;
                }
            }

            BusyChange(false, "");

        }

        private void BusyChange(bool isBusy, string content)
        {
            IsBusy = isBusy;
            BusyContent = content;
        }

        private async void SaveToDataBase()
        {
            if (ImportedRecordsView == null || ImportedRecordsView.Any() == false)
            {
                MessageBox.Show("ƒанных дл€ импорта нет");
                return;
            }

            if (SelectedMachineType == null)
            {
                MessageBox.Show("Ќужно выбрать MachineType");
                return;
            }

            BusyChange(true, "Import To DataBase ...");

            try
            {
                bool result = await Task<bool>.Factory.StartNew(() => SaveToDataBaseProcess(ImportedRecordsView)); 
            }
            catch (Exception exc)
            {
                MessageBox.Show("ќшибка импорта: " + exc.Message);
            }
            
            BusyChange(false, "");
        }

        private bool SaveToDataBaseProcess(IEnumerable<ImportDataRowModel> importCollection)
        {
            List<ImportDataRowModel> col = importCollection.ToList();

            UnitOfWork unitOfWork = new UnitOfWork();
            
            //проверим существование всех Brand и добавим если нету
            var brands = col.Select(s => s.Brand).Distinct().ToList();

            var brandsInDb = unitOfWork.BrandRepository.GetAllBrands().ToList();

            bool isNewAdded = false;

            int i = 0;

            foreach (string brandName in brands)
            {
                 if (brandsInDb.FirstOrDefault(s => s.BrandName == brandName) == null)
                {
                    i--;
                    var newBrand = new Brand();
                    newBrand.BrandName = brandName;
                    newBrand.Id = i; 
                    unitOfWork.BrandRepository.Add(newBrand);
                    
                    isNewAdded = true;
                }
            }

            if (isNewAdded)
            {
                try
                {
                    unitOfWork.BeginTransaction();
                    unitOfWork.CommitTransaction();
                    brandsInDb = unitOfWork.BrandRepository.GetAllBrands().ToList();
                }
                catch (Exception exc)
                {
                    MessageBox.Show("Ќе удалось сохранить новые бренды: " + exc.Message);
                    return false;
                }
            }

            //проверим существование всех моделей если нет то добавим
            var models = col.Select(s => new {s.Brand, s.SubModel}).Distinct().ToList();

            var modelInDb = unitOfWork.ModelRepository.GetAllModelsWithInclude().ToList();

            isNewAdded = false;

            i = 0;

            foreach (var model in models)
            {
                if (modelInDb.FirstOrDefault(s => s.ModelName == model.SubModel && s.Brand.BrandName == model.Brand) == null)
                {
                    i--;
                    var newModel = new HEPDataModel.Model();
                    newModel.Id = i; 
                    newModel.ModelName = model.SubModel;
                    newModel.Brand = brandsInDb.FirstOrDefault(q => q.BrandName == model.Brand);
                    newModel.BrandId = newModel.Brand.Id;
                    newModel.MachineTypeId = SelectedMachineType.MachineTypes;
                    unitOfWork.ModelRepository.Add(newModel);

                    isNewAdded = true;
                }
            }

            if (isNewAdded)
            {
                try
                {
                    unitOfWork.BeginTransaction();
                    unitOfWork.CommitTransaction();
                    modelInDb = unitOfWork.ModelRepository.GetAllModelsWithInclude().ToList();
                }
                catch (Exception exc)
                {
                    MessageBox.Show("Ќе удалось сохранить новые модели: " + exc.Message);
                    return false;
                }
            }

            //проверим существование всех производителей если нет то добавим
            var manufs = col.Select(s => s.Manufacturer).Distinct().ToList();

            var manufInDb = unitOfWork.ManufacturerRepository.GetQuery().ToList();

            isNewAdded = false;

            i = 0;

            foreach (var manuf in manufs)
            {
                if (manufInDb.FirstOrDefault(s => s.ManufacturerName == manuf) == null)
                {
                    var newManuf = new HEPDataModel.Manufacturer();
                    newManuf.Id = i;
                    newManuf.ManufacturerName = manuf;
                    unitOfWork.ManufacturerRepository.Add(newManuf);

                    isNewAdded = true;
                }
            }

            if (isNewAdded)
            {
                try
                {
                    unitOfWork.BeginTransaction();
                    unitOfWork.CommitTransaction();
                    manufInDb = unitOfWork.ManufacturerRepository.GetQuery().ToList();
                }
                catch (Exception exc)
                {
                    MessageBox.Show(exc.Message);
                    return false;
                }
            }

            //добавл€ем Item

            var itemsInDb = unitOfWork.ItemRepository.GetQuery().Select(s => s.Article).Distinct().ToList(); 

            unitOfWork = new UnitOfWork(true);

            i = 0;

            int continueCount = 0;

            foreach (ImportDataRowModel row in col)
            {
                i--;

                if (itemsInDb.FirstOrDefault(s => s == row.Article) != null)
                {
                    continueCount++;
                    continue;
                }

                Item newItem = new Item();
                newItem.Id = i;
                newItem.Article = row.Article;
                newItem.CostBase = row.PriceBase;
                newItem.CostNative = row.PriceNative;
                
                // + 22/06/2014
                newItem.CategoryId = 1;
                newItem.CountryId = 1;
                // - 22/06/2014
                
                DescriptionText descriptionText = new DescriptionText() {ItemId = i,Item = newItem, LangId = 1, Text = row.Description};
                unitOfWork.DescriptionRepository.Add(descriptionText);
                newItem.DescriptionTextList.Add(descriptionText);

                newItem.IsAvailable = row.Quantity;
                
                Manufacturer manufacturer = manufInDb.FirstOrDefault(s => s.ManufacturerName == row.Manufacturer);
                if (manufacturer != null)
                {
                    newItem.ManufacturerId = manufacturer.Id;
                    //newItem.Manufacturer = manufacturer;
                }

                HEPDataModel.Model model = modelInDb.FirstOrDefault(s => s.ModelName == row.SubModel && s.Brand.BrandName == row.Brand);
                if (model != null)
                {
                    //newItem.Model = model;
                    newItem.ModelId = model.Id;
                }

                foreach (string oem in row.OEMList)
                {
                    OEM newOEM = new OEM();
                    newOEM.Item = newItem;
                    newOEM.ItemId = newItem.Id;
                    newOEM.OEMCode = oem;
                    unitOfWork.OemRepository.Add(newOEM);
                    newItem.OEMList.Add(newOEM);
                }
                
                unitOfWork.ItemRepository.Add(newItem);
                
            }


            try
            {
                unitOfWork.BeginTransaction();
                unitOfWork.CommitTransaction();
                MessageBox.Show("Items импортированы: " + (-i - continueCount).ToString() + " пропущено: " + continueCount.ToString());
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
                return false;
            }
            
            return true;
        }

        #endregion

    }
}