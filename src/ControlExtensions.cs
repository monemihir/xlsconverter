using System;
using System.Windows.Forms;

namespace XLSConverter
{
  /// <summary>
  ///   Extensions for <see cref="Control" />
  /// </summary>
  public static class ControlExtensions
  {
    /// <summary>
    ///   Run given function on UI thread
    /// </summary>
    /// <typeparam name="TControl">Type of control</typeparam>
    /// <typeparam name="TResult">Type of result expected</typeparam>
    /// <param name="control">Control to execute method on</param>
    /// <param name="func">Function to execute</param>
    /// <returns>Result of operation</returns>
    public static TResult RunOnUiThread<TControl, TResult>(this TControl control,
      Func<TControl, TResult> func)
      where TControl : Control
    {
      return control.InvokeRequired
        ? (TResult)control.Invoke(func, control)
        : func(control);
    }

    /// <summary>
    ///   Run given action on UI thread
    /// </summary>
    /// <typeparam name="TControl">Type of control</typeparam>
    /// <param name="control">Control to execute action on</param>
    /// <param name="action">Action to execute</param>
    public static void RunOnUiThread<TControl>(this TControl control,
      Action<TControl> action)
      where TControl : Control
    {
      control.RunOnUiThread(c =>
      {
        action(c);
        return c;
      });
    }

    /// <summary>
    ///   Run given action on UI thread
    /// </summary>
    /// <typeparam name="TControl">Type of control</typeparam>
    /// <param name="control">Control to execute action on</param>
    /// <param name="action">Action to execute</param>
    public static void RunOnUiThread<TControl>(this TControl control, Action action)
      where TControl : Control
    {
      control.RunOnUiThread(c => action());
    }
  }
}