using APROTestApp.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics;
using System.Drawing;
using System.IO;

namespace APROTestApp.DAL.EF
{
    public class DbInitializer : DropCreateDatabaseAlways<ApplicationDBContext>
    {

        protected override void Seed(ApplicationDBContext context)
        {

            #region producers
            var producers = new Dictionary<String, Producer>();
            producers.Add("LG", new Producer()
            {
                Name = "LG Electronics"
            });
            producers.Add("Samsung", new Producer()
            {
                Name = "Samsung Electronics Co"
            });
            producers.Add("Bosch", new Producer()
            {
                Name = "Robert Bosch GmbH"
            });
            context.Producers.AddRange(producers.Values);
            #endregion

            #region categories
            var categories = new Dictionary<String, Category>();
            categories.Add("Refridgerator", new Category()
            {
                Name = "Refridgerator"
            });
            categories.Add("TV", new Category()
            {
                Name = "TV"
            });
            categories.Add("MicrowaveOven", new Category()
            {
                Name = "MicrowaveOven"
            });
            context.Categories.AddRange(categories.Values);
            #endregion

            #region products
            var products = new Dictionary<String, Product>();
            products.Add("fridge1", new Product()
            {
                Name = "Refridgerator LG GA-B499YMQZ",
                Description = "полный No Frost, электронное управление, класс A++," +
                " полезный объём: 360 л (255 + 105 л), инверторный компрессор," +
                " зона свежести, 59.5x68.8x200 см, серебристый",
                Price = 1286.94,
                OnlinerURL = @"https://catalog.onliner.by/refrigerator/lg/gab499ymqz",
                Picture = ImageConverter.Convert(Resources.Fridge1),
                Category = categories["Refridgerator"],
                Pruducer = producers["LG"]
                
            });
            products.Add("fridge2", new Product()
            {
                Name = "Refridgerator Samsung RB37J5000B1",
                Description = "полный No Frost, электронное управление, класс A+" +
                ", полезный объём: 367 л (269 + 98 л), инверторный компрессор," +
                " зона свежести, 59.5x67.5x201 см, черный/серый",
                Price = 1227.63,
                OnlinerURL = @"https://catalog.onliner.by/refrigerator/samsung/rb37j5000b1",
                Picture = ImageConverter.Convert(Resources.Fridge2),
                Category = categories["Refridgerator"],
                Pruducer = producers["Samsung"]
            });
            products.Add("fridge3", new Product()
            {
                Name = "Refridgerator Samsung RB33J3301SA",
                Description = "полный No Frost, электронное управление, класс A+" +
                ", полезный объём: 304 л (206 + 98 л), инверторный компрессор," +
                " зона свежести, 59.5x69.7x185 см, серебристый",
                Price = 1093.63,
                OnlinerURL = @"https://catalog.onliner.by/refrigerator/samsung/rb33j3301sawt",
                Picture = ImageConverter.Convert(Resources.Fridge3),
                Category = categories["Refridgerator"],
                Pruducer = producers["Samsung"]
            });
            products.Add("fridge4", new Product()
            {
                Name = "Refridgerator Samsung RB33J3420BC",
                Description = "полный No Frost, электронное управление, класс A+," +
                " полезный объём: 328 л (230 + 98 л), инверторный компрессор," +
                " 59.5x66.8x185 см, черный",
                Price = 1099.58,
                OnlinerURL = @"https://catalog.onliner.by/refrigerator/samsung/rb33j3420bc",
                Picture = ImageConverter.Convert(Resources.Fridge4),
                Category = categories["Refridgerator"],
                Pruducer = producers["Samsung"]
            });
            products.Add("fridge5", new Product()
            {
                Name = "Refridgerator Samsung RB33J3420BC",
                Description = "полный No Frost, электронное управление," +
                " полезный объём: 390 л (307 + 83 л), зона свежести," +
                " 70x62x200 см, нержавеющая сталь",
                Price = 1886.00,
                OnlinerURL = @"https://catalog.onliner.by/refrigerator/bosch/kgn49vi20r",
                Picture = ImageConverter.Convert(Resources.Fridge5),
                Category = categories["Refridgerator"],
                Pruducer = producers["Bosch"]
            });
            products.Add("fridge6", new Product()
            {
                Name = "Refridgerator LG GA-B499TGBM",
                Description = "полный No Frost, электронное управление, класс A++," +
                " полезный объём: 360 л (255 + 105 л), инверторный компрессор," +
                " зона свежести, 59.5x66.8x200 см, черный",
                Price = 1844.00,
                OnlinerURL = @"https://catalog.onliner.by/refrigerator/lg/gab499tgbm",
                Picture = ImageConverter.Convert(Resources.Fridge6),
                Category = categories["Refridgerator"],
                Pruducer = producers["LG"]
            });
            products.Add("fridge7", new Product()
            {
                Name = "Refridgerator LG GA-B499TGBM",
                Description = "полный No Frost, электронное управление, класс A++," +
                " полезный объём: 360 л (255 + 105 л), инверторный компрессор," +
                " зона свежести, 59.5x66.8x200 см, черный",
                Price = 1844.00,
                OnlinerURL = @"https://catalog.onliner.by/refrigerator/lg/gab499tgbm",
                Picture = ImageConverter.Convert(Resources.Fridge7),
                Category = categories["Refridgerator"],
                Pruducer = producers["LG"]
            });
            products.Add("oven1", new Product()
            {
                Name = "Microwave Oven Samsung MS23F302TAS",
                Description = "микроволны (соло), объем 23 л," +
                " мощность 800 Вт, управление электронное, автоприготовление, авторазмораживание",
                Price = 256.70,
                OnlinerURL = @"https://catalog.onliner.by/microvawe/samsung/ms23f302tas",
                Picture = ImageConverter.Convert(Resources.MicrowaveOven1),
                Category = categories["MicrowaveOven"],
                Pruducer = producers["Samsung"]
            });
            products.Add("oven2", new Product()
            {
                Name = "Microwave Oven LG MS2042DB",
                Description = "микроволны (соло), объем 23 л," +
                " мощность 800 Вт, управление электронное, автоприготовление, авторазмораживание",
                Price = 190.29,
                OnlinerURL = @"https://catalog.onliner.by/microvawe/lg/ms2042db",
                Picture = ImageConverter.Convert(Resources.MicrowaveOven2),
                Category = categories["MicrowaveOven"],
                Pruducer = producers["LG"]
            });
            products.Add("oven3", new Product()
            {
                Name = "Microwave Oven Bosch HMT75M624 ",
                Description = "микроволны (соло), объем 20 л, мощность 800 Вт," +
                " управление электронное, автоприготовление, авторазмораживание",
                Price = 659.00,
                OnlinerURL = @"https://catalog.onliner.by/microvawe/bosch/hmt75m624",
                Picture = ImageConverter.Convert(Resources.MicrowaveOven3),
                Category = categories["MicrowaveOven"],
                Pruducer = producers["Bosch"]
            });
            products.Add("tv1", new Product()
            {
                Name = "TV LG 43UJ630V",
                Description = "43\" 3840x2160 (4K UHD), матрица IPS," +
                " частота матрицы 50 Гц, индекс динамичных сцен 1600," +
                " Smart TV (LG webOS), HDR, Wi-Fi",
                Price = 977.42,
                OnlinerURL = @"https://catalog.onliner.by/tv/lg/43uj630vza",
                Picture = ImageConverter.Convert(Resources.TV1),
                Category = categories["TV"],
                Pruducer = producers["LG"]
            });
            products.Add("tv2", new Product()
            {
                Name = "TV Samsung UE50KU6000U",
                Description = "50\" 3840x2160 (4K UHD), матрица VA," +
                " частота матрицы 50 Гц, индекс динамичных сцен 1300," +
                " Smart TV (Samsung Tizen), HDR, Wi-Fi",
                Price = 1421.00,
                OnlinerURL = @"https://catalog.onliner.by/tv/samsung/ue50ku6000u",
                Picture = ImageConverter.Convert(Resources.TV2),
                Category = categories["TV"],
                Pruducer = producers["Samsung"]
            });
            products.Add("tv3", new Product()
            {
                Name = "TV Samsung UE43M5500AU",
                Description = "43\" 1920x1080 (Full HD), частота матрицы 50 Гц," +
                " индекс динамичных сцен 800, Smart TV (Samsung Tizen), Wi-Fi",
                Price = 1044.78,
                OnlinerURL = @"https://catalog.onliner.by/tv/samsung/ue50ku6000u",
                Picture = ImageConverter.Convert(Resources.TV3),
                Category = categories["TV"],
                Pruducer = producers["Samsung"]
            });
            context.Products.AddRange(products.Values);
            #endregion

            context.SaveChanges();
        }
    }
}