using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebExtras.Html;

namespace MMVIC.Models
{
  public static class MembershipExtensions
  {
    public static IHtmlComponent ToHtmlPages(this IEnumerable<Membership> members)
    {
      NullWrapperComponent component = new NullWrapperComponent();

      return component;
    }
  }
}
