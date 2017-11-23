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
using System.Reflection;

namespace MMVIC.Models
{
  /// <summary>
  ///   Utility wide constants
  /// </summary>
  public static class Constants
  {
    /// <summary>
    ///   Date time ISO format
    /// </summary>
    public static readonly string DateTimeIsoFormat = "yyyy-MM-dd HH:mm:ss";

    /// <summary>
    /// Sample memberships data file name
    /// </summary>
    public static readonly string SampleMembershipDataFileName = "sample-members.psv";

    /// <summary>
    /// Sample orders data file name
    /// </summary>
    public static readonly string SampleOrdersDataFileName = "sample-orders.psv";

    /// <summary>
    /// Constant paths
    /// </summary>
    public static class Paths
    {
      private static readonly string ApplicationPath = Path.GetFullPath(new Uri(Assembly.GetExecutingAssembly().CodeBase).LocalPath + @"\..\..\");
      public static readonly string CacheDirectory = Path.Combine(ApplicationPath, "cache");

      public static readonly string CssDirectory = Path.Combine(ApplicationPath, @"content\css");
      public static readonly string FontsDirectory = Path.Combine(ApplicationPath, @"content\fonts");
      public static readonly string MemberDirectoryTemplatesPath = Path.Combine(ApplicationPath, @"content\templates");
      public static readonly string MemberDirectoryContentTemplatePath = Path.Combine(MemberDirectoryTemplatesPath, "content.html");
      public static readonly string MemberDirectoryHeaderTemplatePath = Path.Combine(MemberDirectoryTemplatesPath, "header.html");
      public static readonly string MemberDirectoryFooterTemplatePath = Path.Combine(MemberDirectoryTemplatesPath, "footer.html");

      public static readonly string LibDirectory = Path.Combine(ApplicationPath, "lib");

      /// <summary>
      /// Constructor
      /// </summary>
      static Paths()
      {
        if (!Directory.Exists(CacheDirectory))
          Directory.CreateDirectory(CacheDirectory);
      }
    }
  }
}