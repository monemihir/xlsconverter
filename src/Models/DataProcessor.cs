// 
// This file is part of - MMVIC Report Generator
// Copyright 2017 Mihir Mone
// 
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
// 
//     http://www.apache.org/licenses/LICENSE-2.0
// 
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Faker;
using HtmlAgilityPack;
using OfficeOpenXml;
using HtmlDocument = HtmlAgilityPack.HtmlDocument;

namespace MMVIC.Models
{
  /// <summary>
  ///   A data processor
  /// </summary>
  public class DataProcessor : IObservable
  {
    private readonly List<IObserver> m_observers = new List<IObserver>();

    /// <summary>
    ///   Makes the orders XLS file from given orders
    /// </summary>
    /// <param name="inputPsvFilePath">PSV file path</param>
    /// <param name="outputFilePath">XLS output file path</param>
    public void ConvertToXls(string inputPsvFilePath, string outputFilePath)
    {
      Random rand = new Random(DateTime.Now.Millisecond);
      try
      {
        int progress = 5;
        NotifyAll(progress);

        string[] lines = File.ReadAllLines(inputPsvFilePath);
        progress = 10;
        NotifyAll(progress);

        const int valA = 'A';
        const int valZ = 'Z';

        ExcelPackage pkg = new ExcelPackage();
        ExcelWorksheet dataSheet = pkg.Workbook.Worksheets.Add("Orders");

        int progressAvailable = 100 - progress;
        int progressStepSize = (int)(1.0 / lines.Length * progressAvailable);
        for (int i = 0; i < lines.Length; i++)
        {
          int rowNum = i + 1;
          string line = lines[i];
          string[] buff = line.Split('|');

          int colNum = valA;
          foreach (string val in buff)
          {
            string address = (char)colNum + rowNum.ToString();

            object finalValue = val;

            // check if double
            double dblValue;
            bool parseOk = double.TryParse(val, out dblValue);

            if (parseOk)
              finalValue = Math.Abs(dblValue % 1) < double.Epsilon ? (int)dblValue : dblValue;

            if (val.StartsWith("+"))
              finalValue = val.Substring(1);

            dataSheet.Cells[address].Value = finalValue;

            colNum++;

            if (colNum > valZ)
              throw new OverflowException("Can not handle more than 26 columns");
          }

          progress += progressStepSize;
          NotifyAll(progress);
        }

        progress = 100;
        NotifyAll(progress);
        pkg.SaveAs(new FileInfo(outputFilePath));

        pkg.Dispose();
        MessageBox.Show("Converting succeeded", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
      }
      catch (Exception e)
      {
        MessageBox.Show(e.Message, "Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
      }
    }

    /// <summary>
    ///   Makes the membership PDF document
    /// </summary>
    /// <param name="members">Members to write to document</param>
    /// <param name="mdConf">Member directory generation config</param>
    public void MakeMembershipDirectory(Membership[] members, MemberDirectoryConfig mdConf)
    {
      try
      {
        // we have max 50% progress available
        const int progressAvailable = 50;
        int progress = 0;

        Action<int> fnNotifyProgress = val =>
        {
          progress += val;
          NotifyAll(progress * progressAvailable / 100);
        };

        string pdfExeFilePath = Path.Combine(Constants.Paths.LibDirectory, "wkhtmltopdf.exe");
        List<string> pdfExeArgs = new List<string>
        {
          "--disable-smart-shrinking",
          "--enable-javascript",
          "--javascript-delay 500"
        };

        if (mdConf.PageOffset > 0)
          pdfExeArgs.Add("--page-offset " + mdConf.PageOffset);

        string baseDir = Constants.Paths.CacheDirectory;
        string htmlFile = Path.Combine(Constants.Paths.CacheDirectory, "member-directory-content.html");
        string headerHtmlFile = Path.Combine(Constants.Paths.CacheDirectory, "member-directory-header.html");
        string footerHtmlFile = Path.Combine(Constants.Paths.CacheDirectory, "member-directory-footer.html");

        // copy all CSS files to cache
        new DirectoryInfo(Constants.Paths.MemberDirectoryTemplatesPath)
          .GetFiles("*.css")
          .ToList()
          .ForEach(f => File.Copy(f.FullName, Path.Combine(baseDir, f.Name), true));
        fnNotifyProgress(20);

        HtmlDocument doc = new HtmlDocument();

        if (!string.IsNullOrWhiteSpace(mdConf.HeaderText))
        {
          // create page header
          doc.Load(Constants.Paths.MemberDirectoryHeaderTemplatePath);
          HtmlNode headerNode = doc.DocumentNode.SelectSingleNode("//header");
          headerNode.InnerHtml = mdConf.HeaderText;
          doc.Save(headerHtmlFile);
          pdfExeArgs.Add("--header-line");
          pdfExeArgs.Add("--header-html \"" + headerHtmlFile + "\"");
          pdfExeArgs.Add("--header-spacing 5");
        }

        if (mdConf.EnableFooter)
        {
          // create page footer
          doc.Load(Constants.Paths.MemberDirectoryFooterTemplatePath);
          doc.Save(footerHtmlFile);
          pdfExeArgs.Add("--footer-line");
          pdfExeArgs.Add("--footer-html \"" + footerHtmlFile + "\"");
          pdfExeArgs.Add("--footer-spacing 5");
        }

        // create page content
        doc = new HtmlDocument();
        doc.Load(Constants.Paths.MemberDirectoryContentTemplatePath);
        HtmlNode contentNode = doc.DocumentNode.SelectSingleNode("//div[@id='content']");
        contentNode.InnerHtml = members.ToHtmlTables();
        doc.Save(htmlFile);

        pdfExeArgs.Add("\"" + htmlFile + "\"");
        pdfExeArgs.Add("\"" + mdConf.OutputFilePath + "\"");
        fnNotifyProgress(40);

        //string pdfExeArgs = string.Format("--disable-smart-shrinking \"{0}\" \"{1}\"", htmlFile, outputFilePath);

        // cleanup
        Action cleanUpFiles = () =>
        {
          File.Delete(htmlFile);

          if (!string.IsNullOrWhiteSpace(footerHtmlFile))
            File.Delete(footerHtmlFile);
        };

        using (Process p = new Process())
        {
          // start pdf process
          p.StartInfo = new ProcessStartInfo
          {
            FileName = pdfExeFilePath,
            WorkingDirectory = baseDir,
            UseShellExecute = false,
            Arguments = string.Join(" ", pdfExeArgs),
            RedirectStandardInput = true,
            RedirectStandardOutput = true,
            RedirectStandardError = true,
            WindowStyle = ProcessWindowStyle.Hidden,
            CreateNoWindow = true
          };

          // set up a listener event to capture stderr data asynchronously as
          // to avoid deadlock conditions when trying to read from stdout and stderr synchronously.
          // http://msdn.microsoft.com/en-us/library/system.diagnostics.process.standarderror(v=VS.90).aspx
          StringBuilder stdErr = new StringBuilder();
          p.ErrorDataReceived += (sendingProcess, data) =>
          {
            if (data.Data != null) stdErr.Append(data.Data);
          };

          // start process
          p.Start();

          // start listening for stderr text asynchronously (through the ErrorDataReceived delegate)
          p.BeginErrorReadLine();

          // wait for process to exit
          p.WaitForExit();

          fnNotifyProgress(90);

          // check for process error
          if (p.ExitCode != 0)
          {
            p.Close();
            cleanUpFiles();
            throw new Exception("Error converting html to pdf: " + stdErr);
          }

          p.Close();
        }

        cleanUpFiles();
        NotifyAll(100);

        MessageBox.Show("Export succeeded!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
      }
      catch (Exception e)
      {
        MessageBox.Show("Export failed with error: " + e.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
      }
    }

    /// <summary>
    ///   Write sample orders
    /// </summary>
    /// <param name="numOrders">[Optional] No. of orders to write. Defaults to 50</param>
    public void WriteSampleOrders(int numOrders = 50)
    {
      Random rand = new Random(DateTime.Now.Millisecond);

      IEnumerable<Order> orders = Enumerable.Range(1, numOrders).Select(i =>
      {
        DateTime dt = DateTime.Now.AddDays(-rand.Next(0, 90));
        return new Order
        {
          OrderId = i,
          OrderDate = dt,
          ProgramName = string.Join(" ", Lorem.Words(2)),
          FirstName = Name.First(),
          LastName = Name.Last(),
          TelNo = Phone.Number(),
          MobileNo = Phone.Number(),
          Email1 = Internet.Email(),
          Email2 = Internet.Email(),
          PaymentDate = dt,
          Quantity = 2,
          TotalAmount = 50,
          TicketType = rand.Next() % 2 == 0 ? "Adult" : "Child",
          PaymentMode = rand.Next() % 3 == 0 ? "Credit card" : "Bank transfer",
          OrderStatus = rand.Next() % 3 == 0 ? "wc-complete" : "wc-processing"
        };
      });

      const string header = "OrderId|OrderDate|ProgramName|FirstName|LastName|TelNo|MobileNo|Email1|Email2|PaymentDate|Quantity|TotalAmount|TicketType|PaymentMode|OrderStatus";

      IEnumerable<string> lines = new[] {header}.Concat(orders.Select(f => f.ToPsvRow()));

      File.WriteAllLines(Path.Combine(Constants.Paths.CacheDirectory, Constants.SampleOrdersDataFileName), lines);
    }

    /// <summary>
    ///   Write sample memberships file
    /// </summary>
    /// <param name="numMembers">[Optional] No. of members to write. Defaults to 50</param>
    public void WriteSampleMemberships(int numMembers = 50)
    {
      Random rand = new Random(DateTime.Now.Millisecond);

      IEnumerable<Membership> orders = Enumerable.Range(1, numMembers).Select(i =>
      {
        DateTime dt = DateTime.Now.AddDays(-rand.Next(0, 90));
        return new Membership
        {
          OrderId = i,
          OrderDate = dt,
          ProgramName = string.Join(" ", Lorem.Words(2)),
          FirstName = Name.First(),
          LastName = Name.Last(),
          SpouseName = Name.First(),
          Children = Enumerable.Range(0, rand.Next(0, 3)).Select(j => Name.First()).ToArray(),
          Address = Address.StreetAddress(),
          Suburb = "Melbourne",
          State = Address.UsState(),
          PostCode = rand.Next(3000, 3800),
          TelNo = Phone.Number(),
          MobileNo = Phone.Number(),
          Email1 = Internet.Email(),
          Email2 = Internet.Email(),
          PaymentDate = dt,
          OrderStatus = rand.Next() % 3 == 0 ? "wc-complete" : "wc-processing"
        };
      });

      const string header = "OrderId|ProgramName|OrderDate|LastName|FirstName|SpouseName|Children|Address|Suburb|State|PostCode|TelNo|MobileNo|Email1|Email2|PaymentMode|OrderStatus";

      IEnumerable<string> lines = new[] {header}.Concat(orders.Select(f => f.ToPsvRow()));

      File.WriteAllLines(Path.Combine(Constants.Paths.CacheDirectory, Constants.SampleMembershipDataFileName), lines);
    }

    #region Implementation of IObservable

    /// <inheritdoc />
    public void RegisterObserver(IObserver observer)
    {
      if (!m_observers.Contains(observer))
        m_observers.Add(observer);
    }

    /// <inheritdoc />
    public void NotifyAll(int progress)
    {
      m_observers.ForEach(f => f.Notify(progress));
    }

    #endregion Implementation of IObservable
  }
}