using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text.Json;
using System.Xml.Linq;
using System.Text;
public partial class index : System.Web.UI.Page
{
    private static int currentProgress = 0; // Shared across requests

    //public void generateqr()
    //{
    //    int totalRecords = 10000000;

    //    Random random = new Random();

    //    currentProgress = 0; // Reset progress

    //    for (int i = 0; i < totalRecords; i++)
    //    {
    //        // Simulate QR data creation
    //        string qrText = "A" + random.Next(1000000000, int.MaxValue).ToString("D10");
    //        string qrValue = random.Next(10000000, 99999999).ToString("D8") + random.Next(10000000, 99999999).ToString("D8");

    //        // Simulate inserting data
    //        Fluree fl = new Fluree();
    //        string result = fl.Insertqrdetails((i + 1).ToString("D7"), qrText, qrValue);

    //        // Update progress
    //        currentProgress = i + 1;

    //        // Simulate delay (remove in production)
    //        System.Threading.Thread.Sleep(10);
    //    }
    //}

    public void GenerateQRAndCSV()
    {
        int totalRecords = 5000;
        Random random = new Random();
        StringBuilder csvContent = new StringBuilder();
        csvContent.AppendLine("SerialNo,QRText,QRValue");

        string serverPath = HttpContext.Current != null
            ? HttpContext.Current.Server.MapPath("~/uploads")
            : Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "uploads");
        Directory.CreateDirectory(serverPath);
        string csvFilePath = Path.Combine(serverPath, "qrcode_data.csv");

        using (StreamWriter writer = new StreamWriter(csvFilePath))
        {
            writer.WriteLine("SerialNo,QRText,QRValue");

            for (int i = 0; i < totalRecords; i++)
            {
                string qrText = "A" + random.Next(1000000000, int.MaxValue).ToString("D10");
                string qrValue = random.Next(10000000, 99999999).ToString("D8") +
                                 random.Next(10000000, 99999999).ToString("D8");
                string serialNo = (i + 1).ToString("D7");

                // Write safe values to CSV (Excel won't convert to scientific)
                writer.WriteLine(serialNo + "," + qrText + "," + qrValue);
            }

            //for (int i = 0; i < totalRecords; i++)
            //{
            //    string qrText = "A" + random.Next(1000000000, int.MaxValue).ToString("D10");
            //    string qrValue = random.Next(10000000, 99999999).ToString("D8") +
            //                     random.Next(10000000, 99999999).ToString("D8");
            //    string serialNo = (i + 1).ToString("D7");

            //    // Insert into Fluree database
            //    Fluree fl = new Fluree();
            //    // string result = fl.Insertqrdetails(serialNo, qrText, qrValue);
            //    // Log result if necessary

            //    // Append data to CSV file
            //    writer.WriteLine(serialNo + "," + qrText + "," + qrValue);

            //    //// Optional: Track progress
            //    //if (i % 100000 == 0)
            //    //{
            //    //    Console.WriteLine($"Progress: {i} records generated.");
            //    //}
            //}
        }

        Console.WriteLine("QR Code data CSV generated successfully at: " + csvFilePath);
    }


    [System.Web.Services.WebMethod]
    public static int GetProgress()
    {
        return currentProgress; // Return the current progress
    }

    protected void btn_submit_Click(object sender, EventArgs e)
    {
        // Start the QR generation in a new thread
        System.Threading.Tasks.Task.Run(() => GenerateQRAndCSV());
    }
}
