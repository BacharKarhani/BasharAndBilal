using BasharAndBilal.Interface;
using System.Xml.Linq;

public class CustomerDetailsService : ICustomerDetailsService
{
    public object GetCustomerDetails(string filePath)
    {
        // Check if the file exists
        if (!System.IO.File.Exists(filePath))
        {
            throw new FileNotFoundException("The XML file was not found.");
        }

        // Load the XML document
        XDocument xmlDoc;
        try
        {
            xmlDoc = XDocument.Load(filePath);
        }
        catch
        {
            throw new InvalidOperationException("Failed to load the XML file.");
        }

        // Extract the required data
        var customerDetails = xmlDoc.Descendants("Body").Select(body => new
        {
            Name = body.Element("Full_Name_EN")?.Value,
            Gndr = body.Element("Gndr")?.Value,
            Age = body.Element("Age")?.Value,
            Email = body.Element("Email")?.Value
        }).FirstOrDefault();

        if (customerDetails == null)
        {
            throw new InvalidOperationException("Customer details not found in the XML file.");
        }

        return customerDetails;
    }
}
