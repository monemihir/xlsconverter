using System;
using System.Windows.Forms;

namespace XLSConverter
{
  public static class ControlExtensions
  {
    public static TResult RunOnUiThread<TControl, TResult>(this TControl control,
                                               Func<TControl, TResult> func)
      where TControl : Control
    {
      return control.InvokeRequired
              ? (TResult)control.Invoke(func, control)
              : func(control);
    }

    public static void RunOnUiThread<TControl>(this TControl control,
                                          Action<TControl> func)
      where TControl : Control
    {
      control.RunOnUiThread(c => { func(c); return c; });
    }

    public static void RunOnUiThread<TControl>(this TControl control, Action action)
      where TControl : Control
    {
      control.RunOnUiThread(c => action());
    }

  }
}
