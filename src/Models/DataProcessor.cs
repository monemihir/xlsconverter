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
using System.Threading;
using System.Windows.Forms;
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
  }
}