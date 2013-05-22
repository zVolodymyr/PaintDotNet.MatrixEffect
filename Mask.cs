#region License
/*******************************************************************************
 * Copyright 2013 Volodymyr Baydalka.
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 * http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 *******************************************************************************/
#endregion
using System;
using System.Collections.Generic;
using System.Text;

namespace MatrixEffect
{
  /// <summary>
  /// 
  /// </summary>
  [Flags]
  public enum ColorComponent
  {
    None = 0,
    A = 0x01,
    R = 0x02,
    G = 0x04,
    B = 0x08,
    RGB = R | G | B,
    ARGB = A | R | G | B
  }
  /// <summary>
  /// 
  /// </summary>
  public class Mask : ICloneable
  {
    #region Members
    private double[,] m_data;
    private int m_colOffset;
    private int m_rowOffset;
    private ColorComponent m_componemts = ColorComponent.RGB;
    #endregion

    #region Properties
    /// <summary>
    /// 
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <returns></returns>
    public double this[int x, int y]
    {
      get
      {
        return m_data[x, y];
      }
      set
      {
        m_data[x, y] = value;
      }
    }
    /// <summary>
    /// 
    /// </summary>
    public int Columns
    {
      get
      {
        return m_data.GetLength(0);
      }
    }
    /// <summary>
    /// 
    /// </summary>
    public int Rows
    {
      get
      {
        return m_data.GetLength(1);
      }
    }
    /// <summary>
    /// 
    /// </summary>
    public int ColumnOffset
    {
      get { return m_colOffset; }
      set { m_colOffset = value; }
    }
    /// <summary>
    /// 
    /// </summary>
    public int RowsOffset
    {
      get { return m_rowOffset; }
      set { m_rowOffset = value; }
    }
    /// <summary>
    /// 
    /// </summary>
    public double Sum
    {
      get
      {
        double s = 0;

        for (int i = 0; i < this.Columns; i++)
        {
          for (int j = 0; j < this.Rows; j++)
          {
            s += m_data[i, j];
          }
        }

        return s;
      }
    }
    /// <summary>
    /// 
    /// </summary>
    public ColorComponent Components
    {
      get { return m_componemts; }
      set { m_componemts = value; }
    }
    #endregion

    #region Constructor
    /// <summary>
    /// 
    /// </summary>
    /// <param name="cols"></param>
    /// <param name="rows"></param>
    public Mask(int cols, int rows)
    {
      m_data = new double[cols, rows];
      m_colOffset = cols / 2;
      m_rowOffset = rows / 2;

      m_data[m_colOffset, m_rowOffset] = 1d;
    }
    #endregion

    #region Implementation
    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    object ICloneable.Clone()
    {
      return this.Clone();
    }
    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public Mask Clone()
    {
      Mask mask = new Mask(this.Columns, this.Rows);

      mask.m_rowOffset = m_rowOffset;
      mask.m_colOffset = m_colOffset;
      mask.m_data = m_data.Clone() as double[,];
      mask.m_componemts = m_componemts;

      return mask;
    }
    #endregion
  }
}
