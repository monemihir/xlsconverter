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
using System.Windows.Forms;

namespace MMVIC.Models
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