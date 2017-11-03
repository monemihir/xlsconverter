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

namespace MMVIC.Models
{
  /// <summary>
  ///   An observer
  /// </summary>
  public interface IObserver
  {
    /// <summary>
    ///   Handles the progress update
    /// </summary>
    /// <param name="progress">Progress percentage</param>
    void Notify(int progress);
  }

  /// <summary>
  ///   An observable
  /// </summary>
  public interface IObservable
  {
    /// <summary>
    ///   Register an observer
    /// </summary>
    /// <param name="observer">Observer to register</param>
    void RegisterObserver(IObserver observer);

    /// <summary>
    ///   Notify all observers
    /// </summary>
    /// <param name="progress">Progress percentage</param>
    void NotifyAll(int progress);
  }
}