using opendata.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
//1012
namespace opendata
{
    class Program
    {
        static void Main(string[] args)
        {
            var a = 0;
            var dataset = findopendata();
            showopendata(dataset);
            Console.ReadKey();

        }

        static List<opendata.model.Class1> findopendata()
        {
            List<opendata.model.Class1> result = new List<Class1>();
            var xml = XElement.Load(@"C:\Users\user\Desktop\class1005\opendata\opendata\A17000000J-020028-cAw.xml");
           
            var dataset = xml.Descendants("row").ToList();

            for (var i = 0; i < dataset.Count; i++)
            {
                var row = dataset[i];
                opendata.model.Class1 item = new opendata.model.Class1();
                item.所在縣市 = getvalue(row, "所在縣市");
                item.醫院名稱 = getvalue(row, "醫院名稱");
                item.醫院評鑑結果 = getvalue(row, "醫院評鑑結果");
                result.Add(item);
            }
            return result;
        }
        private static string getvalue(XElement row, string s)
        {
            return row.Element(s)?.Value?.Trim();
        }
        private static void showopendata(List<Class1> dataset)
        {
            Console.WriteLine(string.Format("共收到{0}筆的資料", dataset.Count));
            dataset.GroupBy(row => row.所在縣市).ToList()
                .ForEach(group =>
                {
                    var key = group.Key;
                    var groupDatas = group.ToList();
                    var message = $"所在縣市:{key},共有{groupDatas.Count()}筆資料";
                    Console.WriteLine(message);

                });
        }

    }
}
