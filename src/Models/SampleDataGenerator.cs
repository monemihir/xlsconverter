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
using System.IO;
using System.Linq;

namespace MMVIC.Models
{
  /// <summary>
  ///   Generates sample data
  /// </summary>
  public static class SampleDataGenerator
  {
    /// <summary>
    /// Write sample orders
    /// </summary>
    /// <param name="numOrders">No. of orders to write</param>
    public static void WriteSampleOrders(int numOrders = 50)
    {
      Random rand = new Random(DateTime.Now.Millisecond);

      var orders = Enumerable.Range(1, numOrders).Select(i =>
      {
        DateTime dt = DateTime.Now.AddDays(-rand.Next(0, 90));
        return new Order
        {
          OrderId = i,
          OrderDate = dt,
          ProgramName = "Program 1",
          FirstName = "Mihir",
          LastName = "Mone",
          TelNo = "0300000000",
          MobileNo = "0400000000",
          Email1 = "email1@example.com",
          Email2 = "email2@example.com",
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
  }
}