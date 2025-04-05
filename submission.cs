using Newtonsoft.Json;
using System;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Schema;

/**
Assignment 4 template file:
 * Please do not modify or delete any existing class/variable/method names. However, you can add more variables and functions.
 * Uploading this file directly will not pass the autograder's compilation check, resulting in a grade of 0.
 * **/

namespace ConsoleApp1
{
    public class Program
    {

        public static string xmlURL = "https://affanshaikh96.github.io/Hotels.xml";
        public static string xmlErrorURL = "https://affanshaikh96.github.io/HotelsErrors.xml";
        public static string xsdURL = "https://affanshaikh96.github.io/Hotels.xsd";

        public static void Main(string[] args)
        {
            // Validate the XML file at xmlURL against the XSD schema at xsdURL
            string result = Verification(xmlURL, xsdURL);
            // Print the result of the validation
            Console.WriteLine(result);

            // Validate the XML file at xmlErrorURL against the XSD schema at xsdURL
            result = Verification(xmlErrorURL, xsdURL);
            // Print the result of the validation
            Console.WriteLine(result);

            // Convert the XML file at xmlURL to a JSON string
            result = Xml2Json(xmlURL);
            // Print the JSON string
            Console.WriteLine(result);
        }

        /// <summary>
        /// Verifies the XML file against the provided XSD schema.
        /// </summary>
        /// <param name="xmlUrl">The URL of the XML file to be validated.</param>
        /// <param name="xsdUrl">The URL of the XSD schema file.</param>
        /// <returns>A string indicating the result of the validation.</returns>
        public static string Verification(string xmlUrl, string xsdUrl)
        {
            try
            {
                // Create a new XmlSchemaSet to hold the XSD schema
                XmlSchemaSet schema = new XmlSchemaSet();

                // Add the XSD schema to the XmlSchemaSet
                schema.Add(null, xsdUrl);

                // Load the XML document from the specified URL
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(xmlUrl);

                // Add the schema to the XML document for validation
                xmlDoc.Schemas.Add(schema);

                // Validate the XML document against the schema
                xmlDoc.Validate((sender, e) =>
                {
                    // Throw an exception if a validation error occurs
                    throw new Exception($"Validation Error: {e.Message}");
                });

                // Return a message indicating no errors were found
                return "No Error";
            }
            catch (Exception ex)
            {
                // Return the exception message if an error occurs
                return ex.Message;
            }
        }

        /// <summary>
        /// Converts the XML file to a JSON string.
        /// </summary>
        /// <param name="xmlUrl">The URL of the XML file to be converted.</param>
        /// <returns>A JSON string representation of the XML file.</returns>
        public static string Xml2Json(string xmlUrl)
        {
            try
            {
                // Load the XML document from the specified URL
                XDocument xmlDoc = XDocument.Load(xmlUrl);

                //Set the declaration to null to avoid including it in the JSON output
                xmlDoc.Declaration = null;

                // Convert the XML document to a JSON string with indented formatting
                string jsonText = JsonConvert.SerializeXNode(xmlDoc, Newtonsoft.Json.Formatting.Indented);

                // Return the JSON string
                return jsonText;
            }
            catch (Exception ex)
            {
                return $"Error converting XML to JSON: {ex.Message}";
            }
        }
    }
}
