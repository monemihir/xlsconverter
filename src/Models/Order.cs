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
using System.Reflection;

namespace MMVIC.Models
{
  /// <summary>
  ///   Denotes an order
  /// </summary>
  public class Order
  {
    public int OrderId { get; set; }
    public string ProgramName { get; set; }
    public DateTime OrderDate { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string TelNo { get; set; }
    public string MobileNo { get; set; }
    public string Email1 { get; set; }
    public string Email2 { get; set; }
    public DateTime PaymentDate { get; set; }
    public int Quantity { get; set; }
    public double TotalAmount { get; set; }
    public string TicketType { get; set; }
    public string PaymentMode { get; set; }
    public string OrderStatus { get; set; }

    /// <summary>
    /// Process an orders PSV file
    /// </summary>
    /// <param name="ordersPsvFilePath">Full absolute path to orders PSV file</param>
    /// <returns>Orders</returns>
    public static Order[] ProcessFile(string ordersPsvFilePath)
    {
      string[] lines = File.ReadAllLines(ordersPsvFilePath);

      PropertyInfo[] properties = typeof(Order).GetProperties();
      string[] firstLineBuff = lines[0].Split('|');

      List<Order> orders = new List<Order>();

      lines.Skip(1).ToList().ForEach(f =>
      {
        Order order = new Order();
        string[] buff = f.Split('|');
        for (int i = 0; i < buff.Length; i++)
        {
          PropertyInfo prop = properties.First(p => p.Name == firstLineBuff[i]);
          prop.SetValue(order, Convert.ChangeType(buff[i], prop.PropertyType));
        }
        orders.Add(order);
      });

      return orders.ToArray();
    }

    /// <summary>
    ///  Convert current row to PSV row
    /// </summary>
    /// <returns>PSV row</returns>
    public string ToPsvRow()
    {
      string[] data = {
        OrderId.ToString(),
        ProgramName,
        OrderDate.ToString(Constants.DateTimeIsoFormat),
        FirstName,
        LastName,
        TelNo,
        MobileNo,
        Email1,
        Email2,
        PaymentDate.ToString(Constants.DateTimeIsoFormat),
        Quantity.ToString(),
        TotalAmount.ToString("f02"),
        TicketType,
        PaymentMode,
        OrderStatus
      };

      return string.Join("|", data);
    }
  }
}