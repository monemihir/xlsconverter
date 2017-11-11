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
    /// Application path
    /// </summary>
    private static readonly string ApplicationPath = Path.GetFullPath(new Uri(Assembly.GetExecutingAssembly().CodeBase).LocalPath + @"\..\..\");

    /// <summary>
    ///   Date time ISO format
    /// </summary>
    public static readonly string DateTimeIsoFormat = "yyyy-MM-dd HH:mm:ss";

    /// <summary>
    /// Cache directory
    /// </summary>
    public static readonly string CacheDirectory = Path.Combine(ApplicationPath, "cache");

    /// <summary>
    /// Config key used to store boolean of whether sample data writing should be enabled or not
    /// </summary>
    public static readonly string EnableSampleDataWriterKey = "EnableSampleDataWriter";

    /// <summary>
    /// Constructor
    /// </summary>
    static Constants()
    {
      if (!Directory.Exists(CacheDirectory))
        Directory.CreateDirectory(CacheDirectory);
    }
  }
}