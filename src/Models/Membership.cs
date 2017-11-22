﻿// 
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
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Reflection;
using WebExtras.Core;
using WebExtras.Html;

namespace MMVIC.Models
{
  /// <summary>
  ///   Denotes a membership
  /// </summary>
  public class Membership
  {
    public int OrderId { get; set; }
    public string ProgramName { get; set; }
    public DateTime OrderDate { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string SpouseName { get; set; }
    public string[] Children { get; set; }
    public string Address { get; set; }
    public string Suburb { get; set; }
    public string State { get; set; }
    public int PostCode { get; set; }
    public string TelNo { get; set; }
    public string MobileNo { get; set; }
    public string Email1 { get; set; }
    public string Email2 { get; set; }
    public DateTime PaymentDate { get; set; }
    public string OrderStatus { get; set; }

    /// <summary>
    ///   Constructor
    /// </summary>
    public Membership()
    {
      Children = new string[0];
    }

    /// <summary>
    ///   Process a memberships PSV file
    /// </summary>
    /// <param name="membershipPsvFilePath">Full absolute path to membership PSV file</param>
    /// <returns>Memberships</returns>
    public static Membership[] ProcessFile(string membershipPsvFilePath)
    {
      string[] lines = File.ReadAllLines(membershipPsvFilePath);

      PropertyInfo[] properties = typeof(Membership).GetProperties();
      string[] firstLineBuff = lines[0].Split('|');

      List<Membership> orders = new List<Membership>();

      Dictionary<Type, TypeConverter> converterLookup = new Dictionary<Type, TypeConverter>();

      lines.Skip(1).ToList().ForEach(f =>
      {
        Membership order = new Membership();
        string[] buff = f.Split('|');
        for (int i = 0; i < buff.Length; i++)
        {
          PropertyInfo prop = properties.FirstOrDefault(p => p.Name == firstLineBuff[i]);
          if (prop == null)
            continue;

          if (!converterLookup.ContainsKey(prop.PropertyType))
            converterLookup[prop.PropertyType] = TypeDescriptor.GetConverter(prop.PropertyType);

          TypeConverter converter = converterLookup[prop.PropertyType];

          if (prop.PropertyType.IsArray)
          {
            string[] data = buff[i].Split(new[] {','}, StringSplitOptions.RemoveEmptyEntries);

            prop.SetValue(order, data);
          }
          else
          {
            prop.SetValue(order, converter.ConvertFrom(buff[i]));
          }
        }
        orders.Add(order);
      });

      return orders.ToArray();
    }

    /// <summary>
    ///   Converts current object to a PSV row
    /// </summary>
    public string ToPsvRow()
    {
      string[] data =
      {
        OrderId.ToString(),
        ProgramName,
        OrderDate.ToString(Constants.DateTimeIsoFormat),
        LastName,
        FirstName,
        SpouseName,
        string.Join(",", Children),
        Address,
        Suburb,
        State,
        PostCode.ToString(),
        TelNo,
        MobileNo,
        Email1,
        Email2,
        PaymentDate.ToString(Constants.DateTimeIsoFormat),
        OrderStatus
      };

      return string.Join("|", data);
    }

    /// <summary>
    ///   Sanitize name - removes 'lastname', 'firstname', 'and', '&' from given name
    /// </summary>
    /// <param name="name">Name to sanitize</param>
    /// <returns>Sanitized name</returns>
    private string SanitizeName(string name)
    {
      string[] lookup = {LastName, FirstName, "and", "&"};

      Array.ForEach(lookup, str => { name = name.Replace(str, string.Empty).Trim(); });

      return name.ToTitleCase();
    }

    /// <summary>
    ///   Convert current membership to it's HTML equivalent with appropriate layout
    /// </summary>
    public HtmlDiv ToHtmlComponent()
    {
      HtmlDiv br = new HtmlDiv("<br/>");

      HtmlDiv div = new HtmlDiv();

      HtmlDiv name = new HtmlDiv(LastName.ToUpperInvariant() + " " + FirstName.ToTitleCase());
      if (!string.IsNullOrWhiteSpace(SpouseName))
        name.InnerHtml += " & " + SanitizeName(SpouseName);
      name.CssClasses.Add("strong");
      div.AppendTags.Add(name);

      if (Children.Any())
      {
        HtmlDiv children = new HtmlDiv(string.Join(", ", Children.Select(c => string.Join(", ", SanitizeName(c).Split(new[] {' '}, StringSplitOptions.RemoveEmptyEntries)))));
        children.CssClasses.Add("strong");
        div.AppendTags.Add(children);
      }
      else
        div.AppendTags.Add(br);

      div.AppendTags.Add(br);

      HtmlDiv address = new HtmlDiv(Address + "<br>" + Suburb + ". <br>" + State.ToUpperInvariant() + " - " + PostCode);
      div.AppendTags.Add(address);

      div.AppendTags.Add(br);

      HtmlDiv phone = new HtmlDiv("Tel: " + TelNo + "<br>" + "Mobile: " + MobileNo);
      div.AppendTags.Add(phone);

      return div;
    }
  }
}