using System;
using System.IO;
using System.Linq;
using System.Reflection.Metadata;
using System.Xml;
using System.Xml.Linq;
using S4C.DAL.Models;

namespace S4C.BL
{
    public class XmlLicenseSerializer : IDataDeserialize<License>
    {
        public License Deserialize(string serializedData)
        {
            // XElement doc = XElement.Load(serializedData);
            var xdoc = XDocument.Parse(serializedData);
            var prods = xdoc.Root.Descendants("Products").Elements("Product");
            var lic = new License
            {
                Salt = xdoc.Root.Element("Signature").Elements().First(a=>a.Name == "Salt").Value,
                Hash = xdoc.Root.Element("Signature").Elements().First(a=>a.Name == "Hash").Value,
                Products =prods.Select(a=>new Product()
                {
                    Id =  (int)a.Attribute("id"),
                    Name = a.Value
                }).ToList()
            };
            return lic;
        }
    }
}