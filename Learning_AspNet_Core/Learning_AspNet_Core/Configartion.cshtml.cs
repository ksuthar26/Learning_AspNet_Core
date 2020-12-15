using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace Learning_AspNet_Core
{
    public class ConfigartionModel : PageModel
    {
        private IConfigurationRoot ConfigRoot;

        private IConfiguration _configuration;

        private readonly IConfiguration Config;

        public ArrayExample _array { get; private set; }
        public PositionOptions positionOptions
        {
            get; private set;
        }

        private readonly PositionOptions _options;

        public ConfigartionModel(IConfiguration configRoot, IOptions<PositionOptions> options)
        {
            ConfigRoot = (IConfigurationRoot)configRoot;

            _configuration = configRoot;

            Config = configRoot.GetSection("section2:subsection0");

            _options = options.Value;
        }

        public ContentResult OnGet()
        {
            // Way 1 :

            //var myKeyValue = _configuration["MyKey"];
            //var title = _configuration["Position:Title"];
            //var name = _configuration["Position:Name"];
            //var defaultLogLevel = _configuration["Logging:LogLevel:Default"];


            //return Content($"MyKey value: {myKeyValue} \n" +
            //               $"Title: {title} \n" +
            //               $"Name: {name} \n" +
            //               $"Default Log Level: {defaultLogLevel}");

            // Way 2

            //PositionOptions positionOptions = new PositionOptions();

            //_configuration.GetSection(PositionOptions.Position).Bind(positionOptions);

            //return Content($"Title: {positionOptions.Title} \n" +
            //           $"Name: {positionOptions.Name}");

            //Way 3

            //positionOptions = _configuration.GetSection(PositionOptions.Position).Get<PositionOptions>();

            //return Content($"Title: {positionOptions.Title} \n" +
            //           $"Name: {positionOptions.Name}");

            //Way 4

            //var number = _configuration.GetValue<int>("NumberKey",66);
            //return Content($"{number}");

            //Way 5

            //return Content(
            //    $"section2:subsection0:key0 '{Config["key0"]}'\n" +
            //    $"section2:subsection0:key1:'{Config["key1"]}'");

            //return Content($"Title: {_options.Title} \n" +
            //           $"Name: {_options.Name}");

            //Way 6

            //string s = string.Empty;
            //var selecetion = _configuration.GetSection("section2");
            //if (!selecetion.Exists())
            //{
            //    throw new Exception("secetion2 does not exits");
            //}

            //var children = selecetion.GetChildren();

            //foreach (var subSection in children)
            //{
            //    int i = 0;
            //    var key1 = subSection.Key + ":key" + i++.ToString();
            //    var key2 = subSection.Key + ":key" + i.ToString();

            //    s += key1 + " value: " + selecetion[key1] + "\n";
            //    s += key2 + " value: " + selecetion[key2] + "\n";
            //}
            //return Content(s);

            //way 7

            _array = _configuration.GetSection("array").Get<ArrayExample>();
            string s = null;

            for (int j = 0; j < _array.Entries.Length; j++)
            {
                s += $"Index: {j}  Value:  {_array.Entries[j]} \n";
            }

            return Content(s);

            //string str = "";
            //foreach (var provider in ConfigRoot.Providers.ToList())
            //{
            //    str += provider.ToString() + "\n";
            //}

            //return Content(str);
        }
    }

    public class PositionOptions
    {
        public const string Position = "Position";

        public string Title { get; set; }
        public string Name { get; set; }
    }

    public class ArrayExample
    {
        public string[] Entries { get; set; }
    }

    public class ColourOptions
    {
        public const string Position = "Colour";

        public string Title { get; set; }
        public string Name { get; set; }
    }
}
