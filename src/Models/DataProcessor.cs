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
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using Faker;
using OfficeOpenXml;

namespace MMVIC.Models
{
  /// <summary>
  ///   A data processor
  /// </summary>
  public class DataProcessor : IObservable
  {
    private readonly List<IObserver> m_observers = new List<IObserver>();

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

    /// <summary>
    ///   Makes the orders XLS file from given orders
    /// </summary>
    /// <param name="ordersPsvFilePath">Orders PSV file</param>
    /// <param name="outputFilePath">XLS output file path</param>
    public void MakeOrdersXls(string ordersPsvFilePath, string outputFilePath)
    {
      Random rand = new Random(DateTime.Now.Millisecond);
      try
      {
        int progress = 5;
        NotifyAll(progress);

        string[] lines = File.ReadAllLines(ordersPsvFilePath);
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
            bool parseOk = Double.TryParse(val, out dblValue);

            if (parseOk)
              finalValue = Math.Abs(dblValue % 1) < Double.Epsilon ? (int)dblValue : dblValue;

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
    /// Write sample orders
    /// </summary>
    /// <param name="numOrders">[Optional] No. of orders to write. Defaults to 50</param>
    public void WriteSampleOrders(int numOrders = 50)
    {
      Random rand = new Random(DateTime.Now.Millisecond);

      var orders = Enumerable.Range(1, numOrders).Select(i =>
      {
        DateTime dt = DateTime.Now.AddDays(-rand.Next(0, 90));
        return new Order
        {
          OrderId = i,
          OrderDate = dt,
          ProgramName = String.Join(" ", Lorem.Words(2)),
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

      var lines = new[] { header }.Concat(orders.Select(f => f.ToPsvRow()));

      File.WriteAllLines(Path.Combine(Constants.CacheDirectory, "sample-orders.psv"), lines);
    }

    /// <summary>
    /// Write sample memberships file
    /// </summary>
    /// <param name="numMembers">[Optional] No. of members to write. Defaults to 50</param>
    public void WriteSampleMemberships(int numMembers = 50)
    {
      Random rand = new Random(DateTime.Now.Millisecond);

      var orders = Enumerable.Range(1, numMembers).Select(i =>
      {
        DateTime dt = DateTime.Now.AddDays(-rand.Next(0, 90));
        return new Membership
        {
          OrderId = i,
          OrderDate = dt,
          ProgramName = String.Join(" ", Lorem.Words(2)),
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

      var lines = new[] { header }.Concat(orders.Select(f => f.ToPsvRow()));

      File.WriteAllLines(Path.Combine(Constants.CacheDirectory, "sample-members.psv"), lines);
    }
  }
}