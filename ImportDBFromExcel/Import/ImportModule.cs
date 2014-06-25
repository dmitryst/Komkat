using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using HEPDataModel;
using ImportDBFromExcel.Model;
using NetOffice;
using NetOffice.ExcelApi;


namespace ImportDBFromExcel.Import
{
    interface IImportItemsModule
    {
        IEnumerable<ImportDataRowModel> Import(ImportTemplate template, string fileName);
    }


    public class ImportItemsFromExcelModule : IImportItemsModule
    {


        private decimal ParsingPrice(string value)
        {
            if (value == null)
                return 0;

            if (string.IsNullOrWhiteSpace(value) || value == "-2146826246")
                return 0;

            decimal price = 0;
            decimal.TryParse(value, out price);
            return price;
        }

        private bool ParsingQty(string value)
        {
            if (value == null)
                return false;

            if (string.IsNullOrWhiteSpace(value) || value == "-2146826246")
                return false;

            string temp = value.TrimEnd(' ').TrimStart(' ');
            if (temp == "+")
            {
                return true;
            }
            else
            {
                return false;
            }
           
        }

        private List<string> oemParsingToList(string value, out string outValue)
        {
            outValue = "";
            if (value == null)
            {
                return new List<string>();
            }

            if (string.IsNullOrWhiteSpace(value) || value == "-2146826246")
                return new List<string>();

            string temp = value.TrimStart(' ').TrimEnd(' ');

            if (temp == "#Н/Д" || temp == "0")
            {
                return new List<string>();
            }

            if (temp.Count() > 2)
            {
                outValue = temp;
                return temp.Split('/').ToList();
            }
            else
            {
                outValue = temp;
                return new List<string>() {temp};
            }
        }

     
        

        private int StringToUIntConvert(string str)
        {
            int value = 1;
            foreach (char c in str.ToUpper())
            {
                value = value * (Convert.ToUInt16(c) - 64);
            }

            return value;
        }

        private string GetMaxColumn(ImportTemplate template)
        {
            string value = template.ArticleColName;
            if (StringToUIntConvert(value) < StringToUIntConvert(template.CostBaseColName))
                value = template.CostBaseColName;
            if (StringToUIntConvert(value) < StringToUIntConvert(template.CostNativeColName))
                value = template.CostNativeColName;
            if (StringToUIntConvert(value) < StringToUIntConvert(template.DescriptionColName))
                value = template.DescriptionColName;
            if (StringToUIntConvert(value) < StringToUIntConvert(template.ManufColName))
                value = template.ManufColName;
            if (StringToUIntConvert(value) < StringToUIntConvert(template.ModelColName))
                value = template.ModelColName;
            if (StringToUIntConvert(value) < StringToUIntConvert(template.OEMColName))
                value = template.OEMColName;
            if (StringToUIntConvert(value) < StringToUIntConvert(template.QuantityColName))
                value = template.QuantityColName;

            return value;

        }

        private string GetMinColumn(ImportTemplate template)
        {
            string value = template.ArticleColName;
            if (StringToUIntConvert(value) > StringToUIntConvert(template.CostBaseColName))
                value = template.CostBaseColName;
            if (StringToUIntConvert(value) > StringToUIntConvert(template.CostNativeColName))
                value = template.CostNativeColName;
            if (StringToUIntConvert(value) > StringToUIntConvert(template.DescriptionColName))
                value = template.DescriptionColName;
            if (StringToUIntConvert(value) > StringToUIntConvert(template.ManufColName))
                value = template.ManufColName;
            if (StringToUIntConvert(value) > StringToUIntConvert(template.ModelColName))
                value = template.ModelColName;
            if (StringToUIntConvert(value) > StringToUIntConvert(template.OEMColName))
                value = template.OEMColName;
            if (StringToUIntConvert(value) > StringToUIntConvert(template.QuantityColName))
                value = template.QuantityColName;
            return value;

        }

        public IEnumerable<ImportDataRowModel> Import(ImportTemplate template, string fileName)
        {

            NetOffice.ExcelApi.Application _app;
            NetOffice.ExcelApi.Workbook _workbook;
            NetOffice.ExcelApi.Worksheet _worksheet;
            //Excel.Range _range;

            // start excel and turn off msg boxes
            try
            {
                _app = new NetOffice.ExcelApi.Application();
                _app.DisplayAlerts = false;
                _app.Visible = false;
            }
            catch (Exception exc)
            {
                throw new Exception("Не удалось подключиться к Excell");
            }

            // OpenFile
            try
            {
                _workbook = _app.Workbooks.Open(fileName);
            }
            catch (Exception exc)
            {
                _app.Quit();
                _app.Dispose();
                throw new FileNotFoundException(fileName);
            }

            _worksheet = (NetOffice.ExcelApi.Worksheet)_workbook.Worksheets.FirstOrDefault();
            if (_worksheet == null)
            {
                _app.Quit();
                _app.Dispose();
                throw new Exception("Книга не найдена");
            }


            //количество ошибок чтения
            int errorCount = 1;
            //текущее количество ошибок
            int error = 0;
            //количество строк для обработки
            int countRow = 500;
            int endRow;
            
            countRow = template.EndRowNumber - template.StartRowNumber;
            endRow = template.EndRowNumber;
            
           

            string startColumn = GetMinColumn(template);
            string endColumn = GetMaxColumn(template);

            object[,] values = (object[,])_worksheet.get_Range(startColumn + template.StartRowNumber, endColumn + endRow).Value2;

            int shiftColumn = 0;
            if (StringToUIntConvert(startColumn) != 1)
            {
                //расчитываем сдвиг по горизонтали
                shiftColumn = StringToUIntConvert(startColumn) - 1;
            }

            int articleColNum = StringToUIntConvert(template.ArticleColName) - shiftColumn;
            int costBaseColNum = StringToUIntConvert(template.CostBaseColName) - shiftColumn;
            int costNativeColNum = StringToUIntConvert(template.CostNativeColName) - shiftColumn;
            int descriptionColNum = StringToUIntConvert(template.DescriptionColName) - shiftColumn;
            int manufColNum = StringToUIntConvert(template.ManufColName) - shiftColumn;
            int modelColNum = StringToUIntConvert(template.ModelColName) - shiftColumn;
            int oemColNum = StringToUIntConvert(template.OEMColName) - shiftColumn;
            int quantityColNum = StringToUIntConvert(template.QuantityColName) - shiftColumn;

            string brandStr = "";
            string modelStr = "";

            // считываем 
            List<ImportDataRowModel> rowsFromExcel = new List<ImportDataRowModel>();
            for (int i = 1; i <= countRow + 1; i++)
            {
                if (error > errorCount)
                    break;

                try
                {

                    int outLevel = Convert.ToInt32(_worksheet.Rows[i + template.StartRowNumber - 1].OutlineLevel);
                    if (outLevel == 1)
                    {
                        brandStr = values[i, modelColNum] != null ? Convert.ToString(values[i, modelColNum]) : "";
                        brandStr = brandStr.TrimEnd(' ').TrimStart(' ');
                    }
                    else if (outLevel == 2)
                    {
                        modelStr = values[i, modelColNum] != null ? Convert.ToString(values[i, modelColNum]) : "";
                        modelStr = modelStr.TrimEnd(' ').TrimStart(' ');
                    }
                    else if (outLevel > 2)
                    {
                        string subModelStr = values[i, modelColNum] != null
                            ? Convert.ToString(values[i, modelColNum])
                            : "";
                        subModelStr = subModelStr.TrimEnd(' ').TrimStart(' ');

                        string articleStr = values[i, articleColNum] != null
                            ? Convert.ToString(values[i, articleColNum])
                            : "";
                        articleStr = articleStr.TrimEnd(' ').TrimStart(' ');

                        //если Article не найден то пропускаем строку
                        if (string.IsNullOrWhiteSpace(articleStr))
                        {
                            continue;
                        }

                        string priceBaseStr = values[i, costBaseColNum] != null
                            ? Convert.ToString(values[i, costBaseColNum])
                            : "";
                        decimal priceBase = ParsingPrice(priceBaseStr);

                        string priceNativeStr = values[i, costNativeColNum] != null
                            ? Convert.ToString(values[i, costNativeColNum])
                            : "";
                        decimal priceNative = ParsingPrice(priceNativeStr);

                        string descriptionStr = values[i, descriptionColNum] != null
                            ? Convert.ToString(values[i, descriptionColNum])
                            : "";
                        descriptionStr = descriptionStr.TrimEnd(' ').TrimStart(' ');
                        if (descriptionStr == "-2146826246")
                        {
                            descriptionStr = "";
                        }

                        string manufStr = values[i, manufColNum] != null ? Convert.ToString(values[i, manufColNum]) : "";
                        manufStr = manufStr.TrimEnd(' ').TrimStart(' ');
                        if (manufStr == "-2146826246")
                        {
                            manufStr = "";
                        }


                        string oemStr = values[i, oemColNum] != null ? Convert.ToString(values[i, oemColNum]) : "";
                        List<string> oemList = oemParsingToList(oemStr, out oemStr);

                        string qtyStr = values[i, oemColNum] != null ? Convert.ToString(values[i, quantityColNum]) : "";
                        bool qty = ParsingQty(qtyStr);


                        rowsFromExcel.Add(new ImportDataRowModel()
                        {
                            Brand = brandStr,
                            Article = articleStr,
                            PriceBase = priceBase,
                            PriceNative = priceNative,
                            Description = descriptionStr,
                            Manufacturer = manufStr,
                            Quantity = qty,
                            Model = modelStr,
                            SubModel = subModelStr,
                            OEMs = oemStr,
                            OEMList = oemList
                        });
                    }

                }
                catch (Exception)
                {
                    error += 1; //увеличиваем счетчик ошибок
                    continue;
                }

                error = 0; //если чтение прошло удачно обнуляем

               

            }

            //if (error > errorCount)
            //{
            //    throw  new Exception("Не верный формат данных");
            //}

            _workbook.Close();
            _app.Quit();
            _app.Dispose();
            GC.Collect();

            return rowsFromExcel;
        }


    }

    
}
