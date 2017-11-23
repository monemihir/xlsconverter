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

using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using WebExtras.Html;

namespace MMVIC.Models
{
  /// <summary>
  ///   Extension methods for <see cref="Membership" />
  /// </summary>
  public static class MembershipExtensions
  {
    /// <summary>
    ///   Convert given membership to an HTML tables
    /// </summary>
    /// <param name="members">Members to convert</param>
    /// <param name="entriesPerPage">Entries per page</param>
    /// <returns>HTML table with page break after every <see cref="M:entriesPerPage" /> records</returns>
    public static string ToHtmlTables(this IEnumerable<Membership> members, int entriesPerPage = 8)
    {
      if (members == null)
        return string.Empty;

      string html = string.Empty;

      HtmlDiv pageBreak = new HtmlDiv();
      pageBreak.CssClasses.Add("new-page");

      TagBuilder builder = new TagBuilder("table") {Attributes = {["class"] = "table table-bordered"}};

      HtmlDiv[] dataArr = members.OrderBy(m => m.LastName).Select(m => m.ToHtmlComponent()).ToArray();

      for (int i = 0; i < dataArr.Length; i += 2)
      {
        if (i > 0 && i % entriesPerPage == 0)
        {
          html += builder.ToString(TagRenderMode.Normal);
          html += pageBreak.ToHtml();

          builder = new TagBuilder("table") {Attributes = {["class"] = "table table-bordered"}};
        }

        TagBuilder row = new TagBuilder("tr");

        TagBuilder tdLeft = new TagBuilder("td")
        {
          InnerHtml = dataArr[i].ToHtml(),
          Attributes = {["width"] = "50%"}
        };

        TagBuilder tdRight = new TagBuilder("td")
        {
          Attributes = {["width"] = "50%"}
        };
        if (i + 1 < dataArr.Length)
          tdRight.InnerHtml = dataArr[i + 1].ToHtml();

        row.InnerHtml = tdLeft.ToString(TagRenderMode.Normal) + tdRight.ToString(TagRenderMode.Normal);

        builder.InnerHtml += row.ToString(TagRenderMode.Normal);
      }

      html += builder.ToString(TagRenderMode.Normal);

      return html;
    }
  }
}