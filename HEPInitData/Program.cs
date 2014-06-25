﻿using HEPDataLayer;
using HEPDataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HEPInitData
{
    class Program
    {
        static void Main(string[] args)
        {
            UnitOfWork unitOfWork = new UnitOfWork();

            Console.WriteLine("Creating Initial Data in the Database...");

            CreateData(unitOfWork);

            Console.WriteLine("Done");
            Console.ReadKey();
        }

        private static void CreateData(UnitOfWork unitOfWork)
        {
            unitOfWork.BeginTransaction();

            // languages
            unitOfWork.LanguageRepository.Add(new Language() { Id = 1, Title = "English" });
            unitOfWork.LanguageRepository.Add(new Language() { Id = 2, Title = "Russian" });

            // countries
            unitOfWork.CountryRepository.Add(new Country() { Id = 1, LangId = 1, CountryName = "Italy" });
            unitOfWork.CountryRepository.Add(new Country() { Id = 2, LangId = 2, CountryName = "Италия" });
            unitOfWork.CountryRepository.Add(new Country() { Id = 3, LangId = 1, CountryName = "USA" });
            unitOfWork.CountryRepository.Add(new Country() { Id = 4, LangId = 2, CountryName = "США" });

            // machine types
            unitOfWork.MachineTypeRepository.Add(new MachineType() { Id = 1, MachineTypes = MachineTypeEnum.Excavator, LangId = 1, Text = "Crawler Excavator" });
            unitOfWork.MachineTypeRepository.Add(new MachineType() { Id = 2, MachineTypes = MachineTypeEnum.Excavator, LangId = 2, Text = "Гусеничный экскаватор" });
            unitOfWork.MachineTypeRepository.Add(new MachineType() { Id = 3, MachineTypes = MachineTypeEnum.Bulldozer, LangId = 1, Text = "Bulldozer" });
            unitOfWork.MachineTypeRepository.Add(new MachineType() { Id = 4, MachineTypes = MachineTypeEnum.Bulldozer, LangId = 2, Text = "Бульдозер" });
            unitOfWork.MachineTypeRepository.Add(new MachineType() { Id = 5, MachineTypes = MachineTypeEnum.PassengerVehicle, LangId = 1, Text = "Passenger Vehicle" });
            unitOfWork.MachineTypeRepository.Add(new MachineType() { Id = 6, MachineTypes = MachineTypeEnum.PassengerVehicle, LangId = 2, Text = "Легковой Автомобиль" });


            // brands
            unitOfWork.BrandRepository.Add(new Brand() { Id = 1, BrandName = "KOMATSU" });
            unitOfWork.BrandRepository.Add(new Brand() { Id = 2, BrandName = "CATERPILLAR" });

            // models
            unitOfWork.ModelRepository.Add(new Model() { Id = 1, BrandId = 1, MachineTypeId = MachineTypeEnum.Excavator, ModelName = "PC400LC-7" });
            unitOfWork.ModelRepository.Add(new Model() { Id = 2, BrandId = 1, MachineTypeId = MachineTypeEnum.Excavator, ModelName = "PC300LC-3" });
            unitOfWork.ModelRepository.Add(new Model() { Id = 3, BrandId = 1, MachineTypeId = MachineTypeEnum.Bulldozer, ModelName = "D355A-3" });
            unitOfWork.ModelRepository.Add(new Model() { Id = 4, BrandId = 2, MachineTypeId = MachineTypeEnum.Bulldozer, ModelName = "CAT320" });

            // category
            unitOfWork.CategoryRepository.Add(new Category() { Id = 1, LangId = 1, CategoryName = "Undercarriage" });
            unitOfWork.CategoryRepository.Add(new Category() { Id = 2, LangId = 2, CategoryName = "Ходовая часть" });
            unitOfWork.CategoryRepository.Add(new Category() { Id = 3, LangId = 1, CategoryName = "Crawler belt" });
            unitOfWork.CategoryRepository.Add(new Category() { Id = 4, LangId = 2, CategoryName = "Гусеницы" });

           
            // item1
            Item item = unitOfWork.ItemRepository.Create();

            item.Id = 1;
            item.ModelId = 1;
            item.CategoryId = 1;
            item.Article = "208-30-00431";
            item.CountryId = 1;
            item.IsAvailable = true;
            
            unitOfWork.ItemRepository.Add(item);

            OEM oem = new OEM() { OEMCode = "208-30-00201", ItemId = 1};
            
            item.OEMList.Add(oem); 
            
            unitOfWork.OemRepository.Add(oem);

            // description
            unitOfWork.DescriptionRepository.Add(new DescriptionText() { Id = 1, ItemId = 1, LangId = 1, Text = "Carrier Roller" });
            unitOfWork.DescriptionRepository.Add(new DescriptionText() { Id = 2, ItemId = 1, LangId = 2, Text = "Поддерживающий каток" });
          

            // item2
            Item item2 = unitOfWork.ItemRepository.Create();

            item2.Id = 2;
            item2.ModelId = 1;
            item2.CategoryId = 1;
            item2.Article = "AD441-1108R-LD-E";
            item2.CountryId = 1;
            item2.IsAvailable = true;

            oem = new OEM() { OEMCode = "8A0505465", ItemId = 2 };

            item2.OEMList.Add(oem);

            unitOfWork.OemRepository.Add(oem);

            unitOfWork.ItemRepository.Add(item2);

            unitOfWork.DescriptionRepository.Add(new DescriptionText() { Id = 3, ItemId = 2, LangId = 1, Text = "Front Idler" });
            unitOfWork.DescriptionRepository.Add(new DescriptionText() { Id = 4, ItemId = 2, LangId = 2, Text = "Передний ленивец" });

            // item3
            Item item3 = unitOfWork.ItemRepository.Create();

            item3.Id = 3;
            item3.ModelId = 1;
            item3.CategoryId = 1;
            item3.Article = "AU230009S-0L00";
            item3.CountryId = 1;
            item3.IsAvailable = true;

            oem = new OEM() { OEMCode = "8A0505548", ItemId = 3 };

            item3.OEMList.Add(oem);

            unitOfWork.OemRepository.Add(oem);

            unitOfWork.ItemRepository.Add(item3);

            unitOfWork.ItemRepository.Add(item3);
            unitOfWork.DescriptionRepository.Add(new DescriptionText() { Id = 5, ItemId = 3, LangId = 1, Text = "Crawler belt(type of ckvk)(2 fjjgks)" });
            unitOfWork.DescriptionRepository.Add(new DescriptionText() { Id = 6, ItemId = 3, LangId = 2, Text = "Гусеницы (смазыв.типа)(2 сцепных звена)" });

            // item4
            Item item4 = unitOfWork.ItemRepository.Create();

            item4.Id = 4;
            item4.ModelId = 1;
            item4.CategoryId = 1;
            item4.Article = "AU230009S-0L00";
            item4.CountryId = 1;
            item4.IsAvailable = true;

            oem = new OEM() { OEMCode = "8A0505548", ItemId = 4 };

            item4.OEMList.Add(oem);

            unitOfWork.OemRepository.Add(oem);

            unitOfWork.ItemRepository.Add(item4);

            unitOfWork.DescriptionRepository.Add(new DescriptionText() { Id = 7, ItemId = 4, LangId = 1, Text = "Crawler belt 45L 3G 800 мм" });
            unitOfWork.DescriptionRepository.Add(new DescriptionText() { Id = 8, ItemId = 4, LangId = 2, Text = "Гусеница в сборе 45L 3G 800 мм" });

            // item5
            Item item5 = unitOfWork.ItemRepository.Create();

            item5.Id = 5;
            item5.ModelId = 1;
            item5.CategoryId = 1;
            item5.Article = "AD441-1955R-LD-UE";
            item5.CountryId = 1;
            item5.IsAvailable = true;
            oem = new OEM() { OEMCode = "8A0807424A01C", ItemId = 5 };

            item5.OEMList.Add(oem);

            unitOfWork.OemRepository.Add(oem);

            unitOfWork.ItemRepository.Add(item5);

        

            // currencies
            unitOfWork.CurrencyRepository.Add(new Currency() { Id = 1, CurrencyDesignation = "USD" });
            unitOfWork.CurrencyRepository.Add(new Currency() { Id = 2, CurrencyDesignation = "RUB" });

            // prices
            unitOfWork.ItemPriceRepository.Add(new ItemPriceReg() { Period = DateTime.Now, CurrencyId = 2, ItemId = 1, Value = (decimal)5950.00});
            unitOfWork.ItemPriceRepository.Add(new ItemPriceReg() { Period = DateTime.Now, CurrencyId = 2, ItemId = 2, Value = (decimal)53950.00 });
            unitOfWork.ItemPriceRepository.Add(new ItemPriceReg() { Period = DateTime.Now, CurrencyId = 2, ItemId = 3, Value = (decimal)5420.00 });

            unitOfWork.CommitTransaction();
        }
    }
}
